using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddStockEmpFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "STOCK_EMP_FLAG",
                columns: table => new
                {
                    USER_ID = table.Column<float>(type: "real", nullable: false),
                    STKCOD = table.Column<float>(type: "real", nullable: false),
                    BRANCH_ID = table.Column<float>(type: "real", nullable: false),
                    K_ID = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOCK_EMP_FLAG", x => new { x.USER_ID, x.STKCOD, x.BRANCH_ID, x.K_ID });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STOCK_EMP_FLAG");
        }
    }
}
