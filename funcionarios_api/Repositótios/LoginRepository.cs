using funcionarios_api.Banco_de_dados;
using funcionarios_api.Dtos;
using funcionarios_api.Models;
using funcionarios_api.Repositótios.Interfaces;
using funcionarios_api.Retornos;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace funcionarios_api.Repositótios
{
    public class LoginRepository : ILogin
    {
        private readonly FuncionariosContext _funcionarios;

        private readonly IConfiguration _config;

        public LoginRepository(FuncionariosContext funcionarios, IConfiguration config)
        {
            _funcionarios = funcionarios;
            _config = config;
        }


        public async Task<RetornoLogin> Login(FuncionarioLogin login)
        {
            RetornoLogin retorno = new RetornoLogin();

            try
            {
                FuncionarioModel? funcionarioEmail = await _funcionarios.Funcionarios.FirstOrDefaultAsync(func => func.Email == login.Email);

                if (funcionarioEmail == null)
                {
                    retorno.Mensagem = "Não há funcionário com este e-mail";
                    retorno.Token = null;
                }
                else
                {
                    FuncionarioLoginModel? funcionarioLogin = await _funcionarios.Funcionarios_login.FirstOrDefaultAsync(func => func.Nome == funcionarioEmail.Id);

                    if (funcionarioLogin == null)
                    {
                        retorno.Mensagem = "Este funcionário não possui registro de login";
                        retorno.Token = null;
                    }
                    else
                    {
                        byte[] senha = Encoding.ASCII.GetBytes(login.Senha);

                        byte[] senhaComSalt = new byte[20 + senha.Length];

                        int contador = 0;

                        foreach (byte b in senha)
                        {
                            senhaComSalt[contador] = b;

                            contador++;
                        }


                        foreach (byte b in funcionarioLogin.SenhaSalt)
                        {
                            senhaComSalt[contador] = b;


                            if (contador < 19 + senha.Length)
                            {
                                contador++;
                            }
                        }

                        SHA256 sha256 = SHA256.Create();

                        byte[] senhaHash = sha256.ComputeHash(senhaComSalt);

                        string senhaHashHexadecimal = Convert.ToHexString(senhaHash);


                        if (funcionarioLogin.SenhaHash != senhaHashHexadecimal)
                        {
                            retorno.Mensagem = "Falha ao fazer login";
                            retorno.Token = null;
                        }
                        else
                        {
                            List<Claim> dadosFuncionario = new List<Claim>()
                            {
                                new Claim("Nome", funcionarioEmail.Nome),
                                new Claim("CPF", funcionarioEmail.Cpf),
                                new Claim("E-mail", funcionarioEmail.Email)
                            };


                            SymmetricSecurityKey chaveDeSegurança = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value));

                            SigningCredentials credenciais = new SigningCredentials(chaveDeSegurança, SecurityAlgorithms.HmacSha256Signature);

                            JwtSecurityToken chaveJwt = new JwtSecurityToken
                            (
                                claims: dadosFuncionario,
                                expires: DateTime.Now.AddHours(1),
                                signingCredentials: credenciais
                            );

                            string token = new JwtSecurityTokenHandler().WriteToken(chaveJwt);

                            retorno.Token = token;
                            retorno.Mensagem = "Login feito com sucesso";
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                retorno.Mensagem = erro.Message;
                retorno.Token = null;
            }

            return retorno;
        }
    }
}
