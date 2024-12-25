using ControleContatos.Attributes;
using ControleContatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário")]
        public required string Login { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O E-mail informado não é válido")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Informe o pefil do usuário")]
        public required PerfilEnum? Perfil { get; set; }

        [SenhaObrigatoriaAoIncluir]
        public string? Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }
    }
}
