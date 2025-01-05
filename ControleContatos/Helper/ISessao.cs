using ControleContatos.Models;

namespace ControleContatos.Helper
{
    public interface ISessao
    {
        void CriarSessaoUsuario(UsuarioModel usuario);
        void DestruirSessaoUsuario();
        UsuarioModel BuscarSessaoUsuario();
    }
}
