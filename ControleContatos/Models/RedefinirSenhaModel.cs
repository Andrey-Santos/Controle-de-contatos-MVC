using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class RedefinirSenhaModel
    {

        [Required(ErrorMessage = "Digite o login")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail")]
        [EmailAddress(ErrorMessage = "O E-mail informado não é válido")]
        public required string Email { get; set; }
    }
}
