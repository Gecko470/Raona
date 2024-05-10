using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JonesBank.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumeroCuenta = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Cliente = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Saldo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pass = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cuentas",
                columns: new[] { "Id", "Cliente", "NumeroCuenta", "Saldo" },
                values: new object[,]
                {
                    { 1, "Pedro García", "ES012345678901234567", 2500.50m },
                    { 2, "Francisco López", "FR012345678901234567", 3740.20m },
                    { 3, "María Pérez", "BE012345678901234567", 1765m },
                    { 4, "Susana Sanz", "IT012345678901234567", 5428.75m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Nombre", "Pass" },
                values: new object[,]
                {
                    { 1, "mariaGr@gmail.com", "María García", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" },
                    { 2, "susAz@gmail.com", "Susana Sanz", "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cuentas");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
