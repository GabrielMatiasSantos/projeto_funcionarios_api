using funcionarios_api.Dtos;
using funcionarios_api.Models;
using funcionarios_api.Retornos;

namespace funcionarios_api.Repositótios.Interfaces
{
    public interface IFuncionarios
    {
        Task<RetornoFuncionarios> BuscarFuncionarios();

        Task<RetornoFuncionarios> BuscarFuncionario(int id);

        Task<RetornoFuncionarios> RegistrarFuncionario(FuncionarioRegistrar funcionario);

        Task<RetornoFuncionarios> AlterarFuncionario(FuncionarioAlterar funcionario);

        Task<RetornoFuncionarios> ExcluirFuncionario(int id);
    }
}
