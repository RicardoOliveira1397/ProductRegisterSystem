using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models
{
    public class ContatoModel
    {
        /// <summary>
        /// Atributos do Data Anotation, recurso do .NET
        /// </summary>
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o Nome do contato")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "Digite o E-mail do contato")]
        [EmailAddress(ErrorMessage = "E-mail informado inválido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Digite o Celular do contato")]
        [Phone(ErrorMessage = "Celular informado inválido")]
        public string? Celular { get; set; }
    }
}
