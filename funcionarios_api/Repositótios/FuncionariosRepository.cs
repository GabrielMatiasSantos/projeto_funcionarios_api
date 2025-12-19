using funcionarios_api.Banco_de_dados;
using funcionarios_api.Dtos;
using funcionarios_api.Models;
using funcionarios_api.Repositótios.Interfaces;
using funcionarios_api.Retornos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace funcionarios_api.Repositótios
{
    public class FuncionariosRepository : IFuncionarios
    {
        private readonly FuncionariosContext _funcionarios;

        public FuncionariosRepository(FuncionariosContext funcionarios)
        {
            _funcionarios = funcionarios;
        }       
       

        public async Task<RetornoFuncionarios> BuscarFuncionarios()
        {
            RetornoFuncionarios retorno = new RetornoFuncionarios();

            try
            {
                List<FuncionarioModel> funcionarios = await _funcionarios.Funcionarios.ToListAsync();

                List<FuncionarioExibir> funcionarios2 = new List<FuncionarioExibir>();

                foreach (FuncionarioModel funcionario in funcionarios)
                {
                    funcionarios2.Add(new FuncionarioExibir(funcionario.Id, funcionario.Nome, funcionario.Cpf, funcionario.Telefone, funcionario.Email, funcionario.DataNascimento, funcionario.DataAdmissao, funcionario.Departamento));
                }

                retorno.Dados = funcionarios2;
                retorno.Mensagem = "Busca de funcionários feita com sucesso";
            }
            catch (Exception erro)
            {
                retorno.Dados = null;
                retorno.Mensagem = erro.Message;
            }

            return retorno;
        }

        public async Task<RetornoFuncionarios> BuscarFuncionario(int id)
        {
            RetornoFuncionarios retorno = new RetornoFuncionarios();

            try
            {
                List<FuncionarioModel> funcionarios = await _funcionarios.Funcionarios.ToListAsync();

                List<FuncionarioModel> funcionario = funcionarios.Where(func => func.Id == id).ToList();

                if (funcionario.Count() == 0)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Nenhum funcionário encontrado";
                }
                else
                {
                    List<FuncionarioExibir> funcionario2 = new List<FuncionarioExibir>();

                    foreach (FuncionarioModel funcionario3 in funcionario)
                    {
                        funcionario2.Add(new FuncionarioExibir(funcionario3.Id, funcionario3.Nome, funcionario3.Cpf, funcionario3.Telefone, funcionario3.Email, funcionario3.DataNascimento, funcionario3.DataAdmissao, funcionario3.Departamento));
                    }

                    retorno.Dados = funcionario2;
                    retorno.Mensagem = "funcionário encontrado";
                }
            }
            catch (Exception erro)
            {
                retorno.Dados = null;
                retorno.Mensagem = erro.Message;
            }

            return retorno;
        }

        public async Task<RetornoFuncionarios> RegistrarFuncionario(FuncionarioRegistrar funcionario)
        {
            RetornoFuncionarios retorno = new RetornoFuncionarios();

            try
            {
                List<FuncionarioModel> funcionarios = await _funcionarios.Funcionarios.ToListAsync();

                List<FuncionarioModel> funcionarioCpf = funcionarios.Where(func => func.Cpf == funcionario.Cpf).ToList();
                List<FuncionarioModel> funcionarioTelefone = funcionarios.Where(func => func.Telefone == funcionario.Telefone).ToList();
                List<FuncionarioModel> funcionarioEmail = funcionarios.Where(func => func.Email == funcionario.Email).ToList();

                if (funcionarioCpf.Count() > 0)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Já existe um funcionário com este CPF";
                }
                else if (funcionarioTelefone.Count() > 0)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Já existe um funcionário com este telefone";
                }
                else if (funcionarioEmail.Count() > 0)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Já existe um funcionário com este e-mail";
                }
                else
                {
                    FuncionarioModel funcionarioRegistrar = new FuncionarioModel(funcionario.Nome, funcionario.Cpf, funcionario.Telefone, funcionario.Email, funcionario.DataNascimento, funcionario.DataAdmissao, funcionario.Departamento);

                    _funcionarios.Funcionarios.Add(funcionarioRegistrar);

                    await _funcionarios.SaveChangesAsync();

                    List<FuncionarioModel> funcionarios2 = await _funcionarios.Funcionarios.ToListAsync();

                    List<FuncionarioExibir> funcionarios3 = new List<FuncionarioExibir>();

                    foreach (FuncionarioModel funcionario2 in funcionarios2)
                    {
                        funcionarios3.Add(new FuncionarioExibir(funcionario2.Id, funcionario2.Nome, funcionario2.Cpf, funcionario2.Telefone, funcionario2.Email, funcionario2.DataNascimento, funcionario2.DataAdmissao, funcionario2.Departamento));
                    }

                    retorno.Dados = funcionarios3;
                    retorno.Mensagem = "Novo funcionário registrado com sucesso";
                }
            }
            catch (Exception erro)
            {
                retorno.Dados = null;
                retorno.Mensagem = erro.Message;
            }

            return retorno;
        }

        public async Task<RetornoFuncionarios> AlterarFuncionario(FuncionarioAlterar funcionario)
        {
            RetornoFuncionarios retorno = new RetornoFuncionarios();

            try
            {
                FuncionarioModel? funcionarioAlterar = await _funcionarios.Funcionarios.FirstOrDefaultAsync(func => func.Id == funcionario.Id);

                List<FuncionarioModel> funcionarios = await _funcionarios.Funcionarios.ToListAsync();

                List<FuncionarioModel> funcionarioCpf = funcionarios.Where(func => func.Cpf == funcionario.Cpf && func.Id != funcionario.Id).ToList();
                List<FuncionarioModel> funcionarioTelefone = funcionarios.Where(func => func.Telefone == funcionario.Telefone && func.Id != funcionario.Id).ToList();
                List<FuncionarioModel> funcionarioEmail = funcionarios.Where(func => func.Email == funcionario.Email && func.Id != funcionario.Id).ToList();

                if (funcionarioAlterar == null)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Este id não corresponde a de nenhum funcionário";
                }
                else if (funcionarioCpf.Count() > 0)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Já há um funcionário com este CPF registrado";
                }
                else if (funcionarioTelefone.Count() > 0)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Já há um funcionário com este telefone registrado";
                }
                else if (funcionarioEmail.Count() > 0)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Já há um funcionário com este e-mail registrado";
                }
                else
                {                   
                    funcionarioAlterar.Nome = funcionario.Nome;
                    funcionarioAlterar.Cpf = funcionario.Cpf;
                    funcionarioAlterar.Telefone = funcionario.Telefone;
                    funcionarioAlterar.Email = funcionario.Email;
                    funcionarioAlterar.DataNascimento = funcionario.DataNascimento;
                    funcionarioAlterar.DataAdmissao = funcionario.DataAdmissao;
                    funcionarioAlterar.Departamento = funcionario.Departamento;

                    await _funcionarios.SaveChangesAsync();

                    List<FuncionarioModel> funcionarios2 = await _funcionarios.Funcionarios.ToListAsync();

                    List<FuncionarioExibir> funcionarios3 = new List<FuncionarioExibir>();

                    foreach (FuncionarioModel funcionario2 in funcionarios2)
                    {
                        funcionarios3.Add(new FuncionarioExibir(funcionario2.Id, funcionario2.Nome, funcionario2.Cpf, funcionario2.Telefone, funcionario2.Email, funcionario.DataNascimento, funcionario.DataAdmissao, funcionario.Departamento));
                    }

                    retorno.Dados = funcionarios3;
                    retorno.Mensagem = "Dados de funcionário atualizados com sucesso";
                }
            }
            catch (Exception erro)
            {
                retorno.Dados = null;
                retorno.Mensagem = erro.Message;
            }

            return retorno;
        }

        public async Task<RetornoFuncionarios> ExcluirFuncionario(int id)
        {
            RetornoFuncionarios retorno = new RetornoFuncionarios();

            try
            {
                FuncionarioModel? funcionarioRemover = await _funcionarios.Funcionarios.FirstOrDefaultAsync(func => func.Id == id);

                if (funcionarioRemover == null)
                {
                    retorno.Dados = null;
                    retorno.Mensagem = "Este id não corresponde a de nenhum funcionário";
                }
                else
                {
                    if (Convert.ToInt32(funcionarioRemover.Departamento) == 1)
                    {
                        FuncionarioLoginModel? funcionarioLoginRemover = await _funcionarios.Funcionarios_login.FirstOrDefaultAsync(func => func.Nome == funcionarioRemover.Id);

                        if (funcionarioLoginRemover != null)
                        {
                            _funcionarios.Funcionarios_login.Remove(funcionarioLoginRemover);
                        }
                    }

                    _funcionarios.Funcionarios.Remove(funcionarioRemover);

                    await _funcionarios.SaveChangesAsync();

                    List<FuncionarioModel> funcionarios = await _funcionarios.Funcionarios.ToListAsync();

                    List<FuncionarioExibir> funcionarios2 = new List<FuncionarioExibir>();

                    foreach (FuncionarioModel funcionario in funcionarios)
                    {
                        funcionarios2.Add(new FuncionarioExibir(funcionario.Id, funcionario.Nome, funcionario.Cpf, funcionario.Telefone, funcionario.Email, funcionario.DataNascimento, funcionario.DataAdmissao, funcionario.Departamento));
                    }

                    retorno.Dados = funcionarios2;
                    retorno.Mensagem = "Dados de funcionário removidos com sucesso";
                }
            }
            catch(Exception erro)
            {
                retorno.Dados = null;
                retorno.Mensagem = erro.Message;
            }

            return retorno;
        }       
    }
}
