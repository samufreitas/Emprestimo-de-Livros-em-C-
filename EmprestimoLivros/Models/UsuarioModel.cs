using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestimoLivros.Models
{
    public class UsuarioModel
    {
        public  long Id { get; set; }

        [Column(TypeName = "VARCHAR(255)")]

        public string Nome { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Sobrenome { get; set; }

        [Column(TypeName = "VARCHAR(255)")]
        public string Email { get; set; }

        public byte[] SenhaHash { get; set; }

        public byte[] SenhaSalt { get; set; }
    }
}
