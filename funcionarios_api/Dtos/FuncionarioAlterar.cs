using funcionarios_api.Enumeradores;
using System.ComponentModel.DataAnnotations;

namespace funcionarios_api.Dtos
{
    public class FuncionarioAlterar
    {
        [Required(ErrorMessage = "Informe o id do funcionário")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Informe o nome do funcionário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o CPF do funcionário")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "CPF informado incorretamente")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o telefone do funcionário")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "Telefone informado incorretamente")]
        [Phone]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "Informe o e-mail do funcionário")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a data de nascimento do funcionário")]
        public DateOnly DataNascimento { get; set; }

        [Required(ErrorMessage = "Informe a data de admissão do funcionário")]
        public DateOnly DataAdmissao { get; set; }

        [Required(ErrorMessage = "Informe o departamento do funcionário")]
        public DepartamentosEnum Departamento { get; set; }
    }
}
