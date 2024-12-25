using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Digite o login")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Digite a senha")]
        public required string Senha { get; set; }
    }
}
