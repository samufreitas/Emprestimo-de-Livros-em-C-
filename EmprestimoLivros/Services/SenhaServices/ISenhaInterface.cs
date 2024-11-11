﻿namespace EmprestimoLivros.Services.SenhaServices
{
    public interface ISenhaInterface
    {
         void CriarSenhaHash(string senha, out byte[] senhaHash, out byte[] senhaSalt);

         bool VerificaSenha(string senha, byte[] senhaHash, byte[] senhaSalt); 
    }
}