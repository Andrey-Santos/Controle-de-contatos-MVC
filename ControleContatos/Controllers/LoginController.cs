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

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            //SE O USUÁRIO JÁ ESTIVER LOGADO, REDIRECIONA PARA A PÁGINA HOME

            if (_sessao.BuscarSessaoUsuario() != null)
                return RedirectToAction("Index", "Contato");

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
    }

}
