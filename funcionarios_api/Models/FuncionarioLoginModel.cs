using System.ComponentModel.DataAnnotations.Schema;

namespace funcionarios_api.Models
{
    public class FuncionarioLoginModel
    {
        public int Id { get; set; }

        public int Nome { get; set; }

        public int Email { get; set; }

        public FuncionarioModel Funcionario { get; set; }

        public byte[] SenhaSalt { get; set; }

        public string SenhaHash { get; set; }


        public FuncionarioLoginModel(int nome, int email, byte[] senhaSalt, string senhaHash)
        {
            Nome = nome;
            Email = email;
            SenhaSalt = senhaSalt;
            SenhaHash = senhaHash;
        }
    }
}
