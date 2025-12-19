using funcionarios_api.Dtos;
using funcionarios_api.Models;

namespace funcionarios_api.Retornos
{
    public class RetornoFuncionariosLogin
    {
        public List<FuncionarioLoginExibir>? Dados { get; set; }

        public string? Mensagem { get; set; }
    }
}
