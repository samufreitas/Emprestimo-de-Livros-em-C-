﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmprestimoLivros.Dto
{
    public class UsuarioRegisterDto
    {
        [Required(ErrorMessage ="Digite o nome!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o sobrenome!")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "Digite o email!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a senha!")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Digite a confirmação de senha!"),
            Compare("Senha", ErrorMessage = "As senhas não estão iguais!")]
        public string ConfirmaSenha { get; set; }
    }
}
