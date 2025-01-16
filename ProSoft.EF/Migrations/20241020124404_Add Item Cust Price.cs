using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddItemCustPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ITEMS_CUST_PRICE",
                schema: "dbo",
                columns: table => new
                {
                    ITEM_CODE = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CUST_NO = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BAR_CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NAME1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UNIT_CODE = table.Column<int>(type: "int", nullable: true),
                    ITEM_PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ITEM_TAX1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ITEM_TAX2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BRANCH_ID = table.Column<float>(type: "real", nullable: true),
                    TAX_PRC = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MAIN_CODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FLAG1 = table.Column<float>(type: "real", nullable: true),
                    CATEGORY_PRICE = table.Column<float>(type: "real", nullable: true),
                    DISCOUNT_RATE = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEMS_CUST_PRICE", x => new { x.ITEM_CODE, x.CUST_NO });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITEMS_CUST_PRICE",
                schema: "dbo");
        }
    }
}
