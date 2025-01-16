using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class RelationStockEmpFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_STOCK_EMP_FLAG_BRANCH_ID",
                table: "STOCK_EMP_FLAG",
                column: "BRANCH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STOCK_EMP_FLAG_K_ID",
                table: "STOCK_EMP_FLAG",
                column: "K_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STOCK_EMP_FLAG_STKCOD",
                table: "STOCK_EMP_FLAG",
                column: "STKCOD");

            migrationBuilder.AddForeignKey(
                name: "FK_STOCK_EMP_FLAG_BRANCHS_BRANCH_ID",
                table: "STOCK_EMP_FLAG",
                column: "BRANCH_ID",
                principalTable: "BRANCHS",
                principalColumn: "BRANCH_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STOCK_EMP_FLAG_KIND_STORE_K_ID",
                table: "STOCK_EMP_FLAG",
                column: "K_ID",
                principalTable: "KIND_STORE",
                principalColumn: "K_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_STOCK_EMP_FLAG_STOCK_STKCOD",
                table: "STOCK_EMP_FLAG",
                column: "STKCOD",
                principalTable: "STOCK",
                principalColumn: "STKCOD",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STOCK_EMP_FLAG_BRANCHS_BRANCH_ID",
                table: "STOCK_EMP_FLAG");

            migrationBuilder.DropForeignKey(
                name: "FK_STOCK_EMP_FLAG_KIND_STORE_K_ID",
                table: "STOCK_EMP_FLAG");

            migrationBuilder.DropForeignKey(
                name: "FK_STOCK_EMP_FLAG_STOCK_STKCOD",
                table: "STOCK_EMP_FLAG");

            migrationBuilder.DropIndex(
                name: "IX_STOCK_EMP_FLAG_BRANCH_ID",
                table: "STOCK_EMP_FLAG");

            migrationBuilder.DropIndex(
                name: "IX_STOCK_EMP_FLAG_K_ID",
                table: "STOCK_EMP_FLAG");

            migrationBuilder.DropIndex(
                name: "IX_STOCK_EMP_FLAG_STKCOD",
                table: "STOCK_EMP_FLAG");
        }
    }
}
