using funcionarios_api.Dtos;
using funcionarios_api.Models;

namespace funcionarios_api.Retornos
{
    public class RetornoFuncionarios
    {
        public List<FuncionarioExibir>? Dados { get; set; }

        public string? Mensagem {  get; set; }
    }
}
