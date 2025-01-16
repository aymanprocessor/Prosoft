using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class FK_MainItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MAIN_ID",
                table: "STKBALANCE",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_STKBALANCE_MAIN_ID",
                table: "STKBALANCE",
                column: "MAIN_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_STKBALANCE_MAIN_ITEM_MAIN_ID",
                table: "STKBALANCE",
                column: "MAIN_ID",
                principalTable: "MAIN_ITEM",
                principalColumn: "MAIN_ID",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STKBALANCE_MAIN_ITEM_MAIN_ID",
                table: "STKBALANCE");

            migrationBuilder.DropIndex(
                name: "IX_STKBALANCE_MAIN_ID",
                table: "STKBALANCE");

            migrationBuilder.DropColumn(
                name: "MAIN_ID",
                table: "STKBALANCE");
        }
    }
}
