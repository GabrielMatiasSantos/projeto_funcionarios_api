using funcionarios_api.Enumeradores;
using System.ComponentModel.DataAnnotations;

namespace funcionarios_api.Models
{
    public class FuncionarioModel
    {
        [Key]
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public DateOnly DataNascimento { get; set; }

        public DateOnly DataAdmissao { get; set; }

        public DepartamentosEnum Departamento { get; set; }

        public FuncionarioLoginModel Login { get; set; }

        
        public FuncionarioModel(string nome, string cpf, string telefone, string email, DateOnly dataNascimento, DateOnly dataAdmissao, DepartamentosEnum departamento)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Email = email;
            this.DataNascimento = dataNascimento;
            this.DataAdmissao = dataAdmissao;
            this.Departamento = departamento;
        }

        public FuncionarioModel(int id, string nome, string cpf, string telefone, string email, DateOnly dataNascimento, DateOnly dataAdmissao, DepartamentosEnum departamento)
        {
            this.Id = id;
            this.Nome = nome;
            this.Cpf = cpf;
            this.Telefone = telefone;
            this.Email = email;
            this.DataNascimento = dataNascimento;
            this.DataAdmissao = dataAdmissao;
            this.Departamento = departamento;
        }
    }    
}
