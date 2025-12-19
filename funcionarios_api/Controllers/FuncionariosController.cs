using funcionarios_api.Dtos;
using funcionarios_api.Repositótios.Interfaces;
using funcionarios_api.Retornos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace funcionarios_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarios _funcionarios;

        public FuncionariosController(IFuncionarios funcionarios)
        {
            _funcionarios = funcionarios;
        }


        [Authorize]
        [HttpGet("buscarFuncionarios")]

        public async Task<RetornoFuncionarios> BuscarFuncionarios()
        {
            RetornoFuncionarios retorno = await _funcionarios.BuscarFuncionarios();

            return retorno;
        }


        [Authorize]
        [HttpGet("buscarFuncionario/{id}")]

        public async Task<RetornoFuncionarios> BuscarFuncionario(int id)
        {
            RetornoFuncionarios retorno = await _funcionarios.BuscarFuncionario(id);

            return retorno;
        }


        [Authorize]
        [HttpPost("registrarFuncionario")]

        public async Task<RetornoFuncionarios> RegistrarFuncionario(FuncionarioRegistrar funcionario)
        {
            if (!ModelState.IsValid)
            {
                RetornoFuncionarios retorno = new RetornoFuncionarios();

                retorno.Dados = null;
                retorno.Mensagem = "Informe os dados do funcionário corretamente";

                return retorno;
            }
            else
            {
                RetornoFuncionarios retorno = await _funcionarios.RegistrarFuncionario(funcionario);

                return retorno;
            }
        }


        [Authorize]
        [HttpPut("alterarFuncionario")]

        public async Task<RetornoFuncionarios> AlterarFuncionario(FuncionarioAlterar funcionario)
        {
            if (!ModelState.IsValid)
            {
                RetornoFuncionarios retorno = new RetornoFuncionarios();

                retorno.Dados = null;
                retorno.Mensagem = "Informe os dados do funcionário corretamente";

                return retorno;
            }
            else
            {
                RetornoFuncionarios retorno = await _funcionarios.AlterarFuncionario(funcionario);

                return retorno;
            }
        }


        [Authorize]
        [HttpDelete("excluirFuncionario/{id}")]

        public async Task<RetornoFuncionarios> ExcluirFuncionario(int id)
        {
            RetornoFuncionarios retorno = await _funcionarios.ExcluirFuncionario(id);

            return retorno;
        }
    }
}
