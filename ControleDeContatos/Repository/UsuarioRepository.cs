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
            usuario.DataCadastro = DateTime.Now;
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
            var usuarioDB = Buscar(usuario.Id);
            if (usuarioDB == null)
                throw new Exception($"Ocorreu um erro na atualização dos dados do usuário");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Login =
            usuarioDB.Email = usuario.Email;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDb = Buscar(id);
            if (usuarioDb == null)
                throw new Exception($"Nenhum usuario encontrado");

            _bancoContext.Usuarios.Remove(usuarioDb);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
