using ControleContatos.Helper;
using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()
        {
            //SE O USUÁRIO JÁ ESTIVER LOGADO, REDIRECIONA PARA A PÁGINA HOME

            if (_sessao.BuscarSessaoUsuario() != null)
                return RedirectToAction("Index", "Contato");

            return View();
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }

        public IActionResult Sair()
        {
            _sessao.DestruirSessaoUsuario();

            return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null && usuario.SenhaValida(loginModel.Senha))
                    {
                        _sessao.CriarSessaoUsuario(usuario);
                        return RedirectToAction("Index", "Contato");
                    }

                    TempData["MensagemErro"] = "Login ou senha inválidos. Por favor, tente novamente.";

                }

                return View("Index", loginModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao realizar o login, tente novamente, detalhe: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkParaRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Olá {usuario.Nome}, sua nova senha é: {novaSenha}";
                        bool emailEnviado = _email.Enviar(usuario.Email, "Sitema de contatos - nova senha", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = "Enviamos para seu e-mail cadastrado uma nova senha";
                        }
                        else
                        {
                            TempData["MensagemErro"] = "Não conseguimos enviar o e-mail. Por favor, tente novamente.";
                        }

                        return RedirectToAction("Index");
                    }

                    TempData["MensagemErro"] = "Login ou email inválidos. Por favor, tente novamente.";
                }

                return View("Index", redefinirSenhaModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Erro ao redefinir sua senha, tente novamente, detalhe: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

    }

}
