using funcionarios_api.Mapeamentos;
using funcionarios_api.Models;
using Microsoft.EntityFrameworkCore;

namespace funcionarios_api.Banco_de_dados
{
    public class FuncionariosContext : DbContext
    {
      public FuncionariosContext(DbContextOptions<FuncionariosContext> options) : base(options)
      { 
      }

        public DbSet<FuncionarioModel> Funcionarios {  get; set; }
        public DbSet<FuncionarioLoginModel> Funcionarios_login { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FuncionarioMap());
            modelBuilder.ApplyConfiguration(new FuncionarioLoginMap());
        }
    }
}
