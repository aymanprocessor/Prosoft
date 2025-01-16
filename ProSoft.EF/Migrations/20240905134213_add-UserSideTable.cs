using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class addUserSideTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER_SIDE",
                columns: table => new
                {
                    USER_CODE = table.Column<int>(type: "int", nullable: false),
                    SIDE_ID = table.Column<int>(type: "int", nullable: false),
                    DEPT_ID = table.Column<int>(type: "int", nullable: false),
                    BRANCH_ID = table.Column<int>(type: "int", nullable: false),
                    USER_G_ID = table.Column<int>(type: "int", nullable: false),
                    FLAG1 = table.Column<int>(type: "int", nullable: false),
                    EIS_SYS_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_SIDE", x => new { x.USER_CODE, x.SIDE_ID, x.DEPT_ID, x.BRANCH_ID });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER_SIDE");
        }
    }
}
