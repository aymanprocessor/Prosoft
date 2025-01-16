using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class removestock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STOCK_EMP_STOCK_STKCOD",
                table: "STOCK_EMP");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_STOCK_EMP_STOCK_STKCOD",
                table: "STOCK_EMP",
                column: "STKCOD",
                principalTable: "STOCK",
                principalColumn: "STKCOD");
        }
    }
}
