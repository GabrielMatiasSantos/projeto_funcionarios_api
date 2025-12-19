using funcionarios_api.Banco_de_dados;
using funcionarios_api.Dtos;
using funcionarios_api.Retornos;

namespace funcionarios_api.Repositótios.Interfaces
{
    public interface IFuncionariosLogin
    {
        Task<RetornoFuncionariosLogin> BuscarFuncionariosLogin();

        Task<RetornoFuncionariosLogin> BuscarFuncionarioLogin(int id);

        Task<RetornoFuncionariosLogin> RegistrarFuncionarioLogin(FuncionarioLoginRegistrar login);

        Task<RetornoFuncionariosLogin> AlterarFuncionarioLogin(FuncionarioLoginAlterar login);

        Task<RetornoFuncionariosLogin> ExcluirFuncionarioLogin(int id);
    }
}
