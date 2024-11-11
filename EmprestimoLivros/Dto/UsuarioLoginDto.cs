using System.ComponentModel.DataAnnotations;

namespace EmprestimoLivros.Dto
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "Digite seu Email!")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Digite sua Senha!")]
        public string Senha { get; set; }
    }
}
