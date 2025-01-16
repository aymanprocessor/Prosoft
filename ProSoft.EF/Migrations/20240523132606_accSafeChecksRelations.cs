using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class accSafeChecksRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccSafeCheckSafeCeckId",
                table: "CUST_COLLECTIONS_DISCOUNT",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CURRENCY_CODE",
                table: "AccSafeChecks",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldUnicode: false,
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "COST_CENTER_CODE",
                table: "AccSafeChecks",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(6)",
                oldUnicode: false,
                oldMaxLength: 6,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ACC_TRANS_TYPE",
                table: "AccSafeChecks",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUST_COLLECTIONS_DISCOUNT_AccSafeCheckSafeCeckId",
                table: "CUST_COLLECTIONS_DISCOUNT",
                column: "AccSafeCheckSafeCeckId");

            migrationBuilder.CreateIndex(
                name: "IX_AccSafeChecks_ACC_TRANS_TYPE",
                table: "AccSafeChecks",
                column: "ACC_TRANS_TYPE");

            migrationBuilder.CreateIndex(
                name: "IX_AccSafeChecks_COST_CENTER_CODE",
                table: "AccSafeChecks",
                column: "COST_CENTER_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_AccSafeChecks_CURRENCY_CODE",
                table: "AccSafeChecks",
                column: "CURRENCY_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_AccSafeChecks_SAFE_CODE",
                table: "AccSafeChecks",
                column: "SAFE_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_AccSafeChecks_ACC_GLOBAL_DEF_CURRENCY_CODE",
                table: "AccSafeChecks",
                column: "CURRENCY_CODE",
                principalTable: "ACC_GLOBAL_DEF",
                principalColumn: "CODE_NO");

            migrationBuilder.AddForeignKey(
                name: "FK_AccSafeChecks_COST_CENTERS_COST_CENTER_CODE",
                table: "AccSafeChecks",
                column: "COST_CENTER_CODE",
                principalTable: "COST_CENTERS",
                principalColumn: "COST_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_AccSafeChecks_JOURNAL_TYPE_ACC_TRANS_TYPE",
                table: "AccSafeChecks",
                column: "ACC_TRANS_TYPE",
                principalTable: "JOURNAL_TYPE",
                principalColumn: "JOURNAL_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_AccSafeChecks_SAFE_NAME_SAFE_CODE",
                table: "AccSafeChecks",
                column: "SAFE_CODE",
                principalTable: "SAFE_NAME",
                principalColumn: "SAFE_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_CUST_COLLECTIONS_DISCOUNT_AccSafeChecks_AccSafeCheckSafeCeckId",
                table: "CUST_COLLECTIONS_DISCOUNT",
                column: "AccSafeCheckSafeCeckId",
                principalTable: "AccSafeChecks",
                principalColumn: "SAFE_CECK_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccSafeChecks_ACC_GLOBAL_DEF_CURRENCY_CODE",
                table: "AccSafeChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_AccSafeChecks_COST_CENTERS_COST_CENTER_CODE",
                table: "AccSafeChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_AccSafeChecks_JOURNAL_TYPE_ACC_TRANS_TYPE",
                table: "AccSafeChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_AccSafeChecks_SAFE_NAME_SAFE_CODE",
                table: "AccSafeChecks");

            migrationBuilder.DropForeignKey(
                name: "FK_CUST_COLLECTIONS_DISCOUNT_AccSafeChecks_AccSafeCheckSafeCeckId",
                table: "CUST_COLLECTIONS_DISCOUNT");

            migrationBuilder.DropIndex(
                name: "IX_CUST_COLLECTIONS_DISCOUNT_AccSafeCheckSafeCeckId",
                table: "CUST_COLLECTIONS_DISCOUNT");

            migrationBuilder.DropIndex(
                name: "IX_AccSafeChecks_ACC_TRANS_TYPE",
                table: "AccSafeChecks");

            migrationBuilder.DropIndex(
                name: "IX_AccSafeChecks_COST_CENTER_CODE",
                table: "AccSafeChecks");

            migrationBuilder.DropIndex(
                name: "IX_AccSafeChecks_CURRENCY_CODE",
                table: "AccSafeChecks");

            migrationBuilder.DropIndex(
                name: "IX_AccSafeChecks_SAFE_CODE",
                table: "AccSafeChecks");

            migrationBuilder.DropColumn(
                name: "AccSafeCheckSafeCeckId",
                table: "CUST_COLLECTIONS_DISCOUNT");

            migrationBuilder.AlterColumn<string>(
                name: "CURRENCY_CODE",
                table: "AccSafeChecks",
                type: "varchar(6)",
                unicode: false,
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "COST_CENTER_CODE",
                table: "AccSafeChecks",
                type: "varchar(6)",
                unicode: false,
                maxLength: 6,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ACC_TRANS_TYPE",
                table: "AccSafeChecks",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
