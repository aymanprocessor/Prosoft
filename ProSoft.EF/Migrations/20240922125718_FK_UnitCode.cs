using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class FK_UnitCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_STKBALANCE_UNIT_CODE",
                table: "STKBALANCE",
                column: "UNIT_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_STKBALANCE_UNIT_CODE_UNIT_CODE",
                table: "STKBALANCE",
                column: "UNIT_CODE",
                principalTable: "UNIT_CODE",
                principalColumn: "CODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STKBALANCE_UNIT_CODE_UNIT_CODE",
                table: "STKBALANCE");

            migrationBuilder.DropIndex(
                name: "IX_STKBALANCE_UNIT_CODE",
                table: "STKBALANCE");
        }
    }
}
