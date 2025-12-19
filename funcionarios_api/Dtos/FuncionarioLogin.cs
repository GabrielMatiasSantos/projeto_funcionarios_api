using System.ComponentModel.DataAnnotations;

namespace funcionarios_api.Dtos
{
    public class FuncionarioLogin
    {
        [Required(ErrorMessage = "Informe o e-mail do funcionário")]
        public string Email { get; set; }

        
        [Required(ErrorMessage = "Informe a senha do funcionário")]
        
        public string Senha { get; set; }
    }
}
