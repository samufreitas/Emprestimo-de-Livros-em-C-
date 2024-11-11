using EmprestimoLivros.Dto;
using EmprestimoLivros.Models;

namespace EmprestimoLivros.Services.LoginService
{
    public interface ILoginInterface
    {
        Task<ResponseModel<UsuarioModel>> RegistrarUsuario(UsuarioRegisterDto registerDto);

        Task<ResponseModel<UsuarioModel>> Login(UsuarioLoginDto usuarioLoginDto);

    }
}
