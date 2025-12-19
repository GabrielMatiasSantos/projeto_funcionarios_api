using funcionarios_api.Dtos;
using funcionarios_api.Retornos;

namespace funcionarios_api.Repositótios.Interfaces
{
    public interface ILogin
    {
        Task<RetornoLogin> Login(FuncionarioLogin login);
    }
}
