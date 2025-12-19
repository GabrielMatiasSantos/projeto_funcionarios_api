using funcionarios_api.Banco_de_dados;
using funcionarios_api.Dtos;
using funcionarios_api.Enumeradores;
using funcionarios_api.Models;
using funcionarios_api.Repositótios.Interfaces;
using funcionarios_api.Retornos;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace funcionarios_api.Repositótios
{
    public class FuncionariosLoginRepository : IFuncionariosLogin
    {
        private readonly FuncionariosContext _funcionarios;

        public FuncionariosLoginRepository(FuncionariosContext funcionarios)
        {
            _funcionarios = funcionarios;
        }

        public async Task<RetornoFuncionariosLogin> BuscarFuncionariosLogin()
        {
            RetornoFuncionariosLogin retorno = new RetornoFuncionariosLogin();

            try
            {
                List<FuncionarioLoginModel> funcionarios = await _funcionarios.Funcionarios_login.ToListAsync();

                List <FuncionarioLoginExibir> funcionarios2 = new List<FuncionarioLoginExibir>();

                foreach (FuncionarioLoginModel funcionario in funcionarios)
                {
                    funcionarios2.Add(new FuncionarioLoginExibir(funcionario.Id, funcionario.Nome, funcionario.Email, funcionario.SenhaSalt, funcionario.SenhaHash));
                }

                retorno.Dados = funcionarios2;
                retorno.Mensagem = "Busca de registros de login de funcionário bem sucedida";
            }
            catch (Exception erro)
            {
                retorno.Dados = null;
                retorno.Mensagem = erro.Message;
            }

            return retorno;
        }

        public async Task<RetornoFuncionariosLogin> BuscarFuncionarioLogin(int id)
        {
            RetornoFuncionariosLogin retorno = new RetornoFuncionariosLogin();

            try
            {
                List<FuncionarioLoginModel> funcionarios = await _funcionarios.Funcionarios_login.ToListAsync();

                List<FuncionarioLoginModel> funcionario = funcionarios.Where(func => func.Id == id).ToList();

                if (funcionario.Count() == 0)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Este id não correspende a nenhum registro de login de funcionário";
                }
                else
                {
                    List<FuncionarioLoginExibir> funcionarios2 = new List<FuncionarioLoginExibir>();

                    foreach (FuncionarioLoginModel funcionario2 in funcionario)
                    {
                        funcionarios2.Add(new FuncionarioLoginExibir(funcionario2.Id, funcionario2.Nome, funcionario2.Email, funcionario2.SenhaSalt, funcionario2.SenhaHash));
                    }


                    retorno.Dados = funcionarios2;
                    retorno.Mensagem = "Busca por registro de login de funcionário feita com sucesso";
                }
            }
            catch (Exception erro)
            {
                retorno.Dados = null;
                retorno.Mensagem = erro.Message;
            }

            return retorno;
        }

        public async Task<RetornoFuncionariosLogin> RegistrarFuncionarioLogin(FuncionarioLoginRegistrar login)
        {
            RetornoFuncionariosLogin retorno = new RetornoFuncionariosLogin();

            try
            {
                List<FuncionarioModel> funcionarios = await _funcionarios.Funcionarios.ToListAsync();

                List<FuncionarioModel> funcionarioLogin = funcionarios.Where(func => func.Email == login.Email).ToList();

                if (funcionarioLogin.Count() == 0) 
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Este e-mail não corresponde a de nenhum funcionário";
                }
                else 
                {
                    int id = default;
                    int departamento = default;

                    foreach (FuncionarioModel func in funcionarioLogin)
                    {
                        id = func.Id;
                        departamento = (int)func.Departamento;
                    }

                    if (departamento != 1)
                    {
                        retorno.Dados = null;
                        retorno.Mensagem = "O funcionário precisa ser da administração para ter um registro de login";
                    }
                    else
                    {
                        List<FuncionarioLoginModel> funcionarioLogin2 = await _funcionarios.Funcionarios_login.ToListAsync();

                        List<FuncionarioLoginModel> funcionarioLogin3 = funcionarioLogin2.Where(func => func.Email == id).ToList();

                        if (funcionarioLogin3.Count() > 0)
                        {
                            retorno.Dados = null;
                            retorno.Mensagem = "Este funcionário já possui um registro de login";
                        }
                        else
                        {
                            RandomNumberGenerator numerosAleatorios = RandomNumberGenerator.Create();

                            byte[] senhaSalt = new byte[20];

                            numerosAleatorios.GetBytes(senhaSalt);

                            byte[] senha2 = Encoding.ASCII.GetBytes(login.Senha);

                            byte[] senhaComSalt = new byte[senha2.Length + 20];

                            int contador = 0;

                            foreach (byte b in senha2)
                            {
                                senhaComSalt[contador] = b;

                                contador++;
                            }

                            foreach (byte b in senhaSalt)
                            {
                                senhaComSalt[contador] = b;

                                if (contador < senha2.Length + 19)
                                {
                                    contador++;
                                }
                            }

                            SHA256 sha256 = SHA256.Create();

                            byte[] senhaHash = sha256.ComputeHash(senhaComSalt);

                            string senhaHashHexadecimal = Convert.ToHexString(senhaHash);

                            FuncionarioLoginModel funcionarioLoginRegistrar = new FuncionarioLoginModel(id, id, senhaSalt, senhaHashHexadecimal);

                            _funcionarios.Funcionarios_login.Add(funcionarioLoginRegistrar);

                            await _funcionarios.SaveChangesAsync();

                            List<FuncionarioLoginModel> funcionarios2 = await _funcionarios.Funcionarios_login.ToListAsync();

                            List<FuncionarioLoginExibir> funcionarios3 = new List<FuncionarioLoginExibir>();

                            foreach (FuncionarioLoginModel funcionario in funcionarios2)
                            {
                                funcionarios3.Add(new FuncionarioLoginExibir(funcionario.Id, funcionario.Nome, funcionario.Email, funcionario.SenhaSalt, funcionario.SenhaHash));
                            }

                            retorno.Dados = funcionarios3;
                            retorno.Mensagem = "Novo login registrado com sucesso";
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                retorno.Dados = null;
                retorno.Mensagem = erro.Message;
            }

            return retorno;
        }

        public async Task<RetornoFuncionariosLogin> AlterarFuncionarioLogin(FuncionarioLoginAlterar login)
        {
            RetornoFuncionariosLogin retorno = new RetornoFuncionariosLogin();

            try
            {
                FuncionarioLoginModel? funcionarioAlterar = await _funcionarios.Funcionarios_login.FirstOrDefaultAsync(func => func.Id == login.Id);

                List<FuncionarioModel> funcionarios = await _funcionarios.Funcionarios.ToListAsync();

                List<FuncionarioModel> funcionarioLogin = funcionarios.Where(func => func.Email == login.Email).ToList();

                if (funcionarioAlterar == null)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Este id não corresponde a de nenhum registro de login";
                }
                else if (funcionarioLogin.Count == 0)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Este e-mail não corresponde a de nenhum funcionário";
                }
                else
                {
                    int id = default;
                    int departamento = default;

                    foreach (FuncionarioModel func in funcionarioLogin)
                    {
                        id = func.Id;
                        departamento = (int)func.Departamento;
                    }

                    if (departamento != 1)
                    {
                        retorno.Dados = null;
                        retorno.Mensagem = "Este e-mail pertence a um funcionário que não faz parte da administração";
                    }
                    else
                    {
                        List<FuncionarioLoginModel> funcionarioLogin2 = await _funcionarios.Funcionarios_login.ToListAsync();

                        List<FuncionarioLoginModel> funcionarioLogin3 = funcionarioLogin2.Where(func => func.Email == id && func.Id != login.Id).ToList();

                        if (funcionarioLogin3.Count > 0)
                        {
                            retorno.Dados = null;
                            retorno.Mensagem = "Este e-mail já está registrado em outro registro de login";
                        }
                        else
                        {                           
                            byte[] senhaSalt = new byte[20];

                            RandomNumberGenerator numerosAleatorios = RandomNumberGenerator.Create();

                            numerosAleatorios.GetBytes(senhaSalt);

                            byte[] senha = Encoding.ASCII.GetBytes(login.Senha);

                            byte[] senhaComSalt = new byte[senha.Length + 20];

                            int contador = 0;

                            foreach (byte b in senha)
                            {
                                senhaComSalt[contador] = b;

                                contador++;
                            }

                            foreach (byte b in senhaSalt)
                            {
                                senhaComSalt[contador] = b;

                                if (contador < senha.Length + 19)
                                {
                                    contador++;
                                }
                            }

                            SHA256 sha256 = SHA256.Create();

                            byte[] senhaHash = sha256.ComputeHash(senhaComSalt);

                            string senhaHashHexadecimal = Convert.ToHexString(senhaHash);

                            funcionarioAlterar.Nome = id;
                            funcionarioAlterar.Email = id;
                            funcionarioAlterar.SenhaSalt = senhaSalt;
                            funcionarioAlterar.SenhaHash = senhaHashHexadecimal;

                            await _funcionarios.SaveChangesAsync();

                            List<FuncionarioLoginModel> funcionarios2 = await _funcionarios.Funcionarios_login.ToListAsync();

                            List<FuncionarioLoginExibir> funcionarios3 = new List<FuncionarioLoginExibir>();

                            foreach (FuncionarioLoginModel funcionario in funcionarios2)
                            {
                                funcionarios3.Add(new FuncionarioLoginExibir(funcionario.Id, funcionario.Nome, funcionario.Email, funcionario.SenhaSalt, funcionario.SenhaHash));
                            }

                            retorno.Dados = funcionarios3;
                            retorno.Mensagem = "Registro de login atualizado com sucesso";
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                retorno.Dados = null;
                retorno.Mensagem = erro.Message;
            }

            return retorno;
        }      

        public async Task<RetornoFuncionariosLogin> ExcluirFuncionarioLogin(int id)
        {
            RetornoFuncionariosLogin retorno = new RetornoFuncionariosLogin();

            try
            {
                FuncionarioLoginModel? loginRemover = await _funcionarios.Funcionarios_login.FirstOrDefaultAsync(func => func.Id == id);

                if (loginRemover == null)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Este id não corresponde a de nenhum registro de login";
                }
                else
                {
                    _funcionarios.Funcionarios_login.Remove(loginRemover);

                    await _funcionarios.SaveChangesAsync();

                    List<FuncionarioLoginModel> funcionarios = await _funcionarios.Funcionarios_login.ToListAsync();

                    List<FuncionarioLoginExibir> funcionarios2 = new List<FuncionarioLoginExibir>();

                    foreach (FuncionarioLoginModel funcionario in funcionarios)
                    {
                        funcionarios2.Add(new FuncionarioLoginExibir(funcionario.Id, funcionario.Nome, funcionario.Email, funcionario.SenhaSalt, funcionario.SenhaHash));
                    }

                    retorno.Dados = funcionarios2;
                    retorno.Mensagem = "Registro de login removido com sucesso";
                }
            }
            catch (Exception erro)
            {
                retorno.Dados = null;
                retorno.Mensagem = erro.Message;
            }

            return retorno;
        }       
    }
}
