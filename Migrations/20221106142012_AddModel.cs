using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace practicaparcial1.Migrations
{
    public partial class AddModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "calc",
                columns: table => new
                {
                    CalculationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Operation = table.Column<int>(type: "int", nullable: false),
                    NumberA = table.Column<int>(type: "int", nullable: false),
                    NumberB = table.Column<int>(type: "int", nullable: false),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_calc", x => x.CalculationID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calc");
        }
    }
}
