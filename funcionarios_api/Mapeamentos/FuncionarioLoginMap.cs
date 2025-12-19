using funcionarios_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography.Xml;

namespace funcionarios_api.Mapeamentos
{
    public class FuncionarioLoginMap : IEntityTypeConfiguration<FuncionarioLoginModel>
    {
        public void Configure(EntityTypeBuilder<FuncionarioLoginModel> builder)
        {
            builder.ToTable("FuncionariosLogin");

            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Funcionario).WithOne(z  => z.Login).HasForeignKey<FuncionarioLoginModel>(x => x.Nome);
            builder.HasOne(x => x.Funcionario).WithOne(z => z.Login).HasForeignKey<FuncionarioLoginModel>(x => x.Email);
            builder.Property(x => x.SenhaSalt).IsRequired().HasColumnType("BINARY(20)");
            builder.Property(x => x.SenhaHash).IsRequired().HasColumnType("CHAR(64)");
        }
    }
}
