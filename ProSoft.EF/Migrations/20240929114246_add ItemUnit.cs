using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class addItemUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ITEM_UNITS",
                columns: table => new
                {
                    ITEM_CODE = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    UNIT_CODE = table.Column<int>(type: "int", nullable: false),
                    FLAG1 = table.Column<int>(type: "int", nullable: false),
                    BRANCH_ID = table.Column<int>(type: "int", nullable: false),
                    ITEM_QTY = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    DEFULT_UNIT = table.Column<int>(type: "int", nullable: false),
                    BR_REPLC = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ITEM_UNITS", x => new { x.ITEM_CODE, x.UNIT_CODE, x.FLAG1, x.BRANCH_ID });
                    table.ForeignKey(
                        name: "FK_ITEM_UNITS_SUB_ITEM_ITEM_CODE",
                        column: x => x.ITEM_CODE,
                        principalTable: "SUB_ITEM",
                        principalColumn: "ITEM_CODE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ITEM_UNITS_UNIT_CODE_UNIT_CODE",
                        column: x => x.UNIT_CODE,
                        principalTable: "UNIT_CODE",
                        principalColumn: "CODE",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ITEM_UNITS_UNIT_CODE",
                table: "ITEM_UNITS",
                column: "UNIT_CODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ITEM_UNITS");
        }
    }
}
