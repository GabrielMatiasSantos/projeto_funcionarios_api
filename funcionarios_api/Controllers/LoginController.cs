using funcionarios_api.Banco_de_dados;
using funcionarios_api.Dtos;
using funcionarios_api.Repositótios;
using funcionarios_api.Repositótios.Interfaces;
using funcionarios_api.Retornos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace funcionarios_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _login;

        public LoginController(ILogin login)
        {
            _login = login;
        }


        
        [HttpPost("FuncionárioLogin")]

        public async Task<RetornoLogin> Login([FromBody]FuncionarioLogin login)
        {
            RetornoLogin retorno = await _login.Login(login);

            return retorno;
        }
    }
}
