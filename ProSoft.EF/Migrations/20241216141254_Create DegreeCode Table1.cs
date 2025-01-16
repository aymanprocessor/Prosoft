using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class CreateDegreeCodeTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DEGREE_CODE",
                columns: table => new
                {
                    D_CODE = table.Column<int>(type: "int", nullable: false),
                    D_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D_NAME2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    D_TYPE = table.Column<int>(type: "int", nullable: false),
                    BRANCH = table.Column<int>(type: "int", nullable: false),
                    D_ON_OFF = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEGREE_CODE", x => x.D_CODE);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DEGREE_CODE");
        }
    }
}
