using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class addstock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_STOCK_EMP_STKCOD",
                table: "STOCK_EMP",
                column: "STKCOD");

            migrationBuilder.AddForeignKey(
                name: "FK_STOCK_EMP_STOCK_STKCOD",
                table: "STOCK_EMP",
                column: "STKCOD",
                principalTable: "STOCK",
                principalColumn: "STKCOD");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STOCK_EMP_STOCK_STKCOD",
                table: "STOCK_EMP");

            migrationBuilder.DropIndex(
                name: "IX_STOCK_EMP_STKCOD",
                table: "STOCK_EMP");
        }
    }
}
