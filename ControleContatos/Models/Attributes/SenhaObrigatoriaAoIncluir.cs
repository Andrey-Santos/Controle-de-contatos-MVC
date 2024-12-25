using ControleContatos.Models;
using System.ComponentModel.DataAnnotations;

namespace ControleContatos.Attributes
{
    public class SenhaObrigatoriaAoIncluir : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var usuario = (UsuarioModel)validationContext.ObjectInstance;

            // Valida se o campo de senha é nulo ou vazio apenas ao incluir
            if (usuario.Id == 0 && string.IsNullOrWhiteSpace((string?)value))
            {
                return new ValidationResult("Digite a senha do usuário");
            }

            return ValidationResult.Success;
        }
    }
}
