using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System.Linq;

namespace ControleDeContatos.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly BancoContext _bancoContext; //variavel privada para passar o contexto no construtor

        public ContatoRepository(BancoContext bancoContext) 
        {
            _bancoContext = bancoContext;
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public List<ContatoModel> Buscartodos()
        {
            return _bancoContext.Contatos.OrderBy(x => x.Id).ToList();
        }

        public ContatoModel BuscarContato(int id)
        {
            ContatoModel? contatoModel = _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
            if (contatoModel == null)
                throw new Exception($"Ocorreu um erro ao buscar contato {contatoModel?.Id}");

            return contatoModel;
        }

        public ContatoModel AlterarContato(ContatoModel contato)
        {
            var contatoDB = BuscarContato(contato.Id);
            if (contatoDB == null)
                throw new Exception($"Ocorreu um erro na atualização dos dados do contato {contatoDB?.Nome}");

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _bancoContext.Contatos.Update(contatoDB);
            _bancoContext.SaveChanges();

            return contatoDB;
        }

        public bool ApagarContato(int id)
        {
            var existente = BuscarContato(id);
            if (existente == null)
                throw new Exception($"Nenhum contato encontrado, exclusão não permitida");

            _bancoContext.Contatos.Remove(existente);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
