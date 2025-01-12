using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a senha atual")]
        public required string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Digite a nova senha")]
        public required string NovaSenha { get; set; }

        [Required(ErrorMessage = "Confirme a nova senha")]
        [Compare("NovaSenha", ErrorMessage = "As senhas não conferem")]
        public required string ConfirmarNovaSenha { get; set; }
    }
}
