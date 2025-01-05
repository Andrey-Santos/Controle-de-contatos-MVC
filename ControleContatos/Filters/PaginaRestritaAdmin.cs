using ControleContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ControleContatos.Filters
{
    public class PaginaRestritaAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToActionResult("Index", "Login", null);
            }
            else
            {
                UsuarioModel? usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

                if (usuario == null)
                {
                    context.Result = new RedirectToActionResult("Index", "Login", null);

                } else if (usuario.Perfil != Enums.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToActionResult("Index", "Restrito", null);
                } 
            }

            base.OnActionExecuting(context);
        }
    }
}
