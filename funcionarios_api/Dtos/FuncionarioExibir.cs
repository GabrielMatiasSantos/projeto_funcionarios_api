using funcionarios_api.Enumeradores;

namespace funcionarios_api.Dtos
{
    public class FuncionarioExibir
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public DateOnly DataNascimento { get; set; }

        public DateOnly DataAdmissao { get; set; }

        public DepartamentosEnum Departamento { get; set; }


        public FuncionarioExibir(int id, string nome, string cpf, string telefone, string email, DateOnly dataNascimento, DateOnly dataAdmissao, DepartamentosEnum departamento) 
        {
            Id = id;
            Nome = nome;
            Cpf = cpf;
            Telefone = telefone;
            Email = email;
            DataNascimento = dataNascimento;
            DataAdmissao = dataAdmissao;
            Departamento = departamento;
        }
    }
}
