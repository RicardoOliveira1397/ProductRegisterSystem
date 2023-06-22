using ControleDeContatos.Models;

namespace ControleDeContatos.Repository
{
    public interface IContatoRepository
    {
        List<ContatoModel> Buscartodos();
        ContatoModel Adicionar(ContatoModel contato);

        ContatoModel BuscarContato(int  contatoId);

        ContatoModel AlterarContato(ContatoModel contato);

        bool ApagarContato(int contatoId);
    }
}
