using funcionarios_api.Models;

namespace funcionarios_api.Dtos
{
    public class FuncionarioLoginExibir
    {
        public int Id { get; set; }

        public int Nome { get; set; }

        public int Email { get; set; }

        public byte[] SenhaSalt { get; set; }

        public string SenhaHash { get; set; }


        public FuncionarioLoginExibir(int id, int nome, int email, byte[] senhaSalt, string senhaHash) 
        {
            Id = id;
            Nome = nome;
            Email = email;
            SenhaSalt = senhaSalt;
            SenhaHash = senhaHash;        
        }
    }
}
