using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class accsafeCheckEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccSafeChecks",
                table: "AccSafeChecks");

            migrationBuilder.RenameTable(
                name: "AccSafeChecks",
                newName: "ACC_SAFE_CHECK");

            migrationBuilder.RenameIndex(
                name: "IX_AccSafeChecks_SAFE_CODE",
                table: "ACC_SAFE_CHECK",
                newName: "IX_ACC_SAFE_CHECK_SAFE_CODE");

            migrationBuilder.RenameIndex(
                name: "IX_AccSafeChecks_CURRENCY_CODE",
                table: "ACC_SAFE_CHECK",
                newName: "IX_ACC_SAFE_CHECK_CURRENCY_CODE");

            migrationBuilder.RenameIndex(
                name: "IX_AccSafeChecks_COST_CENTER_CODE",
                table: "ACC_SAFE_CHECK",
                newName: "IX_ACC_SAFE_CHECK_COST_CENTER_CODE");

            migrationBuilder.RenameIndex(
                name: "IX_AccSafeChecks_ACC_TRANS_TYPE",
                table: "ACC_SAFE_CHECK",
                newName: "IX_ACC_SAFE_CHECK_ACC_TRANS_TYPE");

            migrationBuilder.AddColumn<int>(
                name: "USER_CODE",
                table: "ACC_SAFE_CHECK",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ACC_SAFE_CHECK",
                table: "ACC_SAFE_CHECK",
                column: "SAFE_CECK_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ACC_SAFE_CHECK_ACC_GLOBAL_DEF_CURRENCY_CODE",
                table: "ACC_SAFE_CHECK",
                column: "CURRENCY_CODE",
                principalTable: "ACC_GLOBAL_DEF",
                principalColumn: "CODE_NO");

            migrationBuilder.AddForeignKey(
                name: "FK_ACC_SAFE_CHECK_COST_CENTERS_COST_CENTER_CODE",
                table: "ACC_SAFE_CHECK",
                column: "COST_CENTER_CODE",
                principalTable: "COST_CENTERS",
                principalColumn: "COST_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_ACC_SAFE_CHECK_JOURNAL_TYPE_ACC_TRANS_TYPE",
                table: "ACC_SAFE_CHECK",
                column: "ACC_TRANS_TYPE",
                principalTable: "JOURNAL_TYPE",
                principalColumn: "JOURNAL_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_ACC_SAFE_CHECK_SAFE_NAME_SAFE_CODE",
                table: "ACC_SAFE_CHECK",
                column: "SAFE_CODE",
                principalTable: "SAFE_NAME",
                principalColumn: "SAFE_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_CUST_COLLECTIONS_DISCOUNT_ACC_SAFE_CHECK_AccSafeCheckSafeCeckId",
                table: "CUST_COLLECTIONS_DISCOUNT",
                column: "AccSafeCheckSafeCeckId",
                principalTable: "ACC_SAFE_CHECK",
                principalColumn: "SAFE_CECK_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACC_SAFE_CHECK_ACC_GLOBAL_DEF_CURRENCY_CODE",
                table: "ACC_SAFE_CHECK");

            migrationBuilder.DropForeignKey(
                name: "FK_ACC_SAFE_CHECK_COST_CENTERS_COST_CENTER_CODE",
                table: "ACC_SAFE_CHECK");

            migrationBuilder.DropForeignKey(
                name: "FK_ACC_SAFE_CHECK_JOURNAL_TYPE_ACC_TRANS_TYPE",
                table: "ACC_SAFE_CHECK");

            migrationBuilder.DropForeignKey(
                name: "FK_ACC_SAFE_CHECK_SAFE_NAME_SAFE_CODE",
                table: "ACC_SAFE_CHECK");

            migrationBuilder.DropForeignKey(
                name: "FK_CUST_COLLECTIONS_DISCOUNT_ACC_SAFE_CHECK_AccSafeCheckSafeCeckId",
                table: "CUST_COLLECTIONS_DISCOUNT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ACC_SAFE_CHECK",
                table: "ACC_SAFE_CHECK");

            migrationBuilder.DropColumn(
                name: "USER_CODE",
                table: "ACC_SAFE_CHECK");

            migrationBuilder.RenameTable(
                name: "ACC_SAFE_CHECK",
                newName: "AccSafeChecks");

            migrationBuilder.RenameIndex(
                name: "IX_ACC_SAFE_CHECK_SAFE_CODE",
                table: "AccSafeChecks",
                newName: "IX_AccSafeChecks_SAFE_CODE");

            migrationBuilder.RenameIndex(
                name: "IX_ACC_SAFE_CHECK_CURRENCY_CODE",
                table: "AccSafeChecks",
                newName: "IX_AccSafeChecks_CURRENCY_CODE");

            migrationBuilder.RenameIndex(
                name: "IX_ACC_SAFE_CHECK_COST_CENTER_CODE",
                table: "AccSafeChecks",
                newName: "IX_AccSafeChecks_COST_CENTER_CODE");

            migrationBuilder.RenameIndex(
                name: "IX_ACC_SAFE_CHECK_ACC_TRANS_TYPE",
                table: "AccSafeChecks",
                newName: "IX_AccSafeChecks_ACC_TRANS_TYPE");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccSafeChecks",
                table: "AccSafeChecks",
                column: "SAFE_CECK_ID");

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
    }
}
