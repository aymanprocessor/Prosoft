using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class addFKSubItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ITEM_CODE",
                table: "SUB_ITEM",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SUB_ITEM_ITEM_CODE",
                table: "SUB_ITEM",
                column: "ITEM_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_SUB_ITEM_ITEM_CODE",
                table: "SUB_ITEM",
                column: "ITEM_CODE",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_STKBALANCE_ITEM_CODE",
                table: "STKBALANCE",
                column: "ITEM_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_STKBALANCE_SUB_ITEM_ITEM_CODE",
                table: "STKBALANCE",
                column: "ITEM_CODE",
                principalTable: "SUB_ITEM",
                principalColumn: "ITEM_CODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_STKBALANCE_SUB_ITEM_ITEM_CODE",
                table: "STKBALANCE");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SUB_ITEM_ITEM_CODE",
                table: "SUB_ITEM");

            migrationBuilder.DropIndex(
                name: "IX_SUB_ITEM_ITEM_CODE",
                table: "SUB_ITEM");

            migrationBuilder.DropIndex(
                name: "IX_STKBALANCE_ITEM_CODE",
                table: "STKBALANCE");

            migrationBuilder.AlterColumn<string>(
                name: "ITEM_CODE",
                table: "SUB_ITEM",
                type: "varchar(30)",
                unicode: false,
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldUnicode: false,
                oldMaxLength: 30);
        }
    }
}
