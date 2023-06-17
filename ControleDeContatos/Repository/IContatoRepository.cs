using ControleDeContatos.Models;

namespace ControleDeContatos.Repository
{
    public interface IContatoRepository
    {
        List<ContatoModel> Buscartodos();
        ContatoModel Adicionar(ContatoModel contato);
    }
}
