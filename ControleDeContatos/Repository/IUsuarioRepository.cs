using ControleDeContatos.Models;

namespace ControleDeContatos.Repository
{
    public interface IUsuarioRepository
    {
        List<UsuarioModel> Buscartodos();
        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Buscar(int  usuarioId);

        UsuarioModel Alterar(UsuarioModel usuario);

        bool Apagar(int usuarioId);
    }
}
