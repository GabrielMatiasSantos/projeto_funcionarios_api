using System.ComponentModel.DataAnnotations;

namespace funcionarios_api.Dtos
{
    public class FuncionarioLoginRegistrar
    {
        [Required(ErrorMessage = "Informe o e-mail do funcionário")]
        [EmailAddress]
        public string Email { get; set;}

        [Required(ErrorMessage = "Informe uma senha")]
        [MinLength(8, ErrorMessage = "A senha deve conter pelo menos 8 caracteres")]
        public string Senha { get; set;}

        [Compare("Senha", ErrorMessage = "Falha na confirmação de senha")]
        public string ConfirmarSenha { get; set; }
    }
}
