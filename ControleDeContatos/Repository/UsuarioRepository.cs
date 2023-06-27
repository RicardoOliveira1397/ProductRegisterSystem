using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System.Linq;

namespace ControleDeContatos.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BancoContext _bancoContext; //variavel privada para passar o contexto no construtor

        public UsuarioRepository(BancoContext bancoContext) 
        {
            _bancoContext = bancoContext;
        }
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public List<UsuarioModel> Buscartodos()
        {
            return _bancoContext.Usuarios.OrderBy(x => x.Id).ToList();
        }

        public UsuarioModel Buscar(int id)
        {
            UsuarioModel? usuarioModel = _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
            if (usuarioModel == null)
                throw new Exception($"Ocorreu um erro ao buscar contato {usuarioModel?.Id}");

            return usuarioModel;
        }

        public UsuarioModel Alterar(UsuarioModel usuario)
        {
            var contatoDB = Buscar(usuario.Id);
            if (contatoDB == null)
                throw new Exception($"Ocorreu um erro na atualização dos dados do contato {contatoDB?.Nome}");

            contatoDB.Nome = usuario.Nome;
            contatoDB.Login = 
            contatoDB.Email = usuario.Email;

            _bancoContext.Usuarios.Update(contatoDB);
            _bancoContext.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            var existente = Buscar(id);
            if (existente == null)
                throw new Exception($"Nenhum contato encontrado, exclusão não permitida");

            _bancoContext.Usuarios.Remove(existente);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
