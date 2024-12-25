using ControleContatos.Models;
using ControleContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleContatos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                        if (usuario.SenhaValida(loginModel.Senha))
                            return RedirectToAction("Index", "Contato");

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
