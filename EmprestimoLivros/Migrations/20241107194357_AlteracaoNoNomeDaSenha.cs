using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmprestimoLivros.Migrations
{
    /// <inheritdoc />
    public partial class AlteracaoNoNomeDaSenha : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "senhaSalt",
                table: "Usuarios",
                newName: "SenhaSalt");

            migrationBuilder.RenameColumn(
                name: "senhaHash",
                table: "Usuarios",
                newName: "SenhaHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SenhaSalt",
                table: "Usuarios",
                newName: "senhaSalt");

            migrationBuilder.RenameColumn(
                name: "SenhaHash",
                table: "Usuarios",
                newName: "senhaHash");
        }
    }
}
