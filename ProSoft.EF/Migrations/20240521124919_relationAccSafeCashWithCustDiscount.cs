using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class relationAccSafeCashWithCustDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SAFE_CASH_ID",
                table: "CUST_COLLECTIONS_DISCOUNT",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CUST_COLLECTIONS_DISCOUNT_SAFE_CASH_ID",
                table: "CUST_COLLECTIONS_DISCOUNT",
                column: "SAFE_CASH_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CUST_COLLECTIONS_DISCOUNT_ACC_SAFE_CASH_SAFE_CASH_ID",
                table: "CUST_COLLECTIONS_DISCOUNT",
                column: "SAFE_CASH_ID",
                principalTable: "ACC_SAFE_CASH",
                principalColumn: "SAFE_CASH_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CUST_COLLECTIONS_DISCOUNT_ACC_SAFE_CASH_SAFE_CASH_ID",
                table: "CUST_COLLECTIONS_DISCOUNT");

            migrationBuilder.DropIndex(
                name: "IX_CUST_COLLECTIONS_DISCOUNT_SAFE_CASH_ID",
                table: "CUST_COLLECTIONS_DISCOUNT");

            migrationBuilder.DropColumn(
                name: "SAFE_CASH_ID",
                table: "CUST_COLLECTIONS_DISCOUNT");
        }
    }
}
