using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace funcionarios_api.Migrations
{
    /// <inheritdoc />
    public partial class criarDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Cpf = table.Column<string>(type: "CHAR(14)", maxLength: 14, nullable: false),
                    Telefone = table.Column<string>(type: "CHAR(14)", maxLength: 14, nullable: false),
                    Email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    DataNascimento = table.Column<DateOnly>(type: "DATE", nullable: false),
                    DataAdmissao = table.Column<DateOnly>(type: "DATE", nullable: false),
                    Departamento = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FuncionariosLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<int>(type: "int", nullable: false),
                    SenhaSalt = table.Column<byte[]>(type: "BINARY(20)", nullable: false),
                    SenhaHash = table.Column<string>(type: "CHAR(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncionariosLogin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncionariosLogin_Funcionarios_Email",
                        column: x => x.Email,
                        principalTable: "Funcionarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FuncionariosLogin_Email",
                table: "FuncionariosLogin",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncionariosLogin");

            migrationBuilder.DropTable(
                name: "Funcionarios");
        }
    }
}
