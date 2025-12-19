using funcionarios_api.Dtos;
using funcionarios_api.Repositótios.Interfaces;
using funcionarios_api.Retornos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace funcionarios_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosLoginController : ControllerBase
    {
        private readonly IFuncionariosLogin _funcionarios;

        public FuncionariosLoginController(IFuncionariosLogin funcionarios)
        {
            _funcionarios = funcionarios;
        }


        [Authorize]
        [HttpGet("buscarLoginFuncionarios")]
        public async Task<RetornoFuncionariosLogin> BuscarFuncionariosLogin()
        {
            RetornoFuncionariosLogin retorno = await _funcionarios.BuscarFuncionariosLogin();

            return retorno;
        }


        [Authorize]
        [HttpGet("buscarLoginFuncionario/{id}")]
        public async Task<RetornoFuncionariosLogin> BuscarFuncionarioLogin(int id)
        {
            RetornoFuncionariosLogin retorno = await _funcionarios.BuscarFuncionarioLogin(id);

            return retorno;
        }


        [Authorize]
        [HttpPost("registrarLoginFuncionario")]

        public async Task<RetornoFuncionariosLogin> RegistrarFuncionarioLogin(FuncionarioLoginRegistrar funcionario)
        {
            if (!ModelState.IsValid)
            {
                RetornoFuncionariosLogin retorno = new RetornoFuncionariosLogin();

                retorno.Dados = null;
                retorno.Mensagem = "Informe os dados do funcionário corretamente";

                return retorno;
            }
            else
            {
                RetornoFuncionariosLogin retorno = await _funcionarios.RegistrarFuncionarioLogin(funcionario);

                return retorno;
            }
        }


        [Authorize]
        [HttpPut("alterarLoginFuncionario")]

        public async Task<RetornoFuncionariosLogin> AlterarFuncionarioLogin(FuncionarioLoginAlterar funcionario)
        {
            if (!ModelState.IsValid)
            {
                RetornoFuncionariosLogin retorno = new RetornoFuncionariosLogin();

                retorno.Dados = null;
                retorno.Mensagem = "Informe os dados do funcionário corretamente";

                return retorno;
            }
            else
            {
                RetornoFuncionariosLogin retorno = await _funcionarios.AlterarFuncionarioLogin(funcionario);

                return retorno;
            }
        }


        [Authorize]
        [HttpPut("excluirLoginFuncionario/{id}")]

        public async Task<RetornoFuncionariosLogin> ExcluirFuncionarioLogin(int id)
        {
            RetornoFuncionariosLogin retorno = await _funcionarios.ExcluirFuncionarioLogin(id);

            return retorno;
        }
    }
}
