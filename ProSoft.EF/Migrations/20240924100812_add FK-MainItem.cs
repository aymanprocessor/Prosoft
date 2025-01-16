using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class addFKMainItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MAIN_CODE",
                table: "MAIN_ITEM",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_MAIN_ITEM_MAIN_CODE",
                table: "MAIN_ITEM",
                column: "MAIN_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_STKBALANCE_MAIN_CODE",
                table: "STKBALANCE",
                column: "MAIN_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_MAIN_ITEM_MAIN_CODE",
                table: "MAIN_ITEM",
                column: "MAIN_CODE",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_STKBALANCE_MAIN_ITEM_MAIN_CODE",
                table: "STKBALANCE",
                column: "MAIN_CODE",
                principalTable: "MAIN_ITEM",
                principalColumn: "MAIN_CODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STKBALANCE_MAIN_ITEM_MAIN_CODE",
                table: "STKBALANCE");

            migrationBuilder.DropIndex(
                name: "IX_STKBALANCE_MAIN_CODE",
                table: "STKBALANCE");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_MAIN_ITEM_MAIN_CODE",
                table: "MAIN_ITEM");

            migrationBuilder.DropIndex(
                name: "IX_MAIN_ITEM_MAIN_CODE",
                table: "MAIN_ITEM");

            migrationBuilder.AlterColumn<string>(
                name: "MAIN_CODE",
                table: "MAIN_ITEM",
                type: "varchar(10)",
                unicode: false,
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldUnicode: false,
                oldMaxLength: 10);
        }
    }
}
