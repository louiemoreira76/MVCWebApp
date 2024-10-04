using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCWebApp.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                 name: "Cep",
                 table: "Fornecedores",
                 type: "char(8)",
                 nullable: false,
                 oldClrType: typeof(int),
                 oldType: "int")
                 .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                 name: "Cep",
                 table: "Fornecedores",
                 type: "int",
                 nullable: false,
                 oldClrType: typeof(string),
                 oldType: "char(8)")
                 .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
