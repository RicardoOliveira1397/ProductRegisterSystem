using ControleDeContatos.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Insira o nome do usuário")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Insira o login do usuário")]
        public string? Login { get; set;}
        [Required(ErrorMessage = "Insira o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O email informado não é valido")]
        public string? Email { get; set;}
        public PerfilEnum Perfil { get; set;}
        [Required(ErrorMessage = "Insira a senha do usuário")]
        public string? Senha { get; set;}
        public DateTime DataCadastro { get; set;}
        public DateTime? DataAtualizacao { get; set;}
    }
}
