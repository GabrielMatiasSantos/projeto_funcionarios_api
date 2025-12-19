using Microsoft.EntityFrameworkCore;
using funcionarios_api.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace funcionarios_api.Mapeamentos
{
    public class FuncionarioMap : IEntityTypeConfiguration<FuncionarioModel>
    {
        public void Configure(EntityTypeBuilder<FuncionarioModel> builder)
        {
            builder.ToTable("Funcionarios");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasColumnType("VARCHAR(50)");
            builder.Property(x => x.Cpf).IsRequired().HasColumnType("CHAR(14)");
            builder.Property(x => x.Telefone).IsRequired().HasColumnType("CHAR(14)");
            builder.Property(x => x.Email).IsRequired().HasColumnType("VARCHAR(100)");
            builder.Property(x => x.DataNascimento).IsRequired().HasColumnType("DATE");
            builder.Property(x => x.DataAdmissao).IsRequired().HasColumnType("DATE");
            builder.Property(x => x.Departamento).IsRequired().HasColumnType("INT");
        }
    }
}
