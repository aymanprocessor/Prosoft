using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACC_GLOBAL_DEF",
                columns: table => new
                {
                    CODE_NO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CO_CODE = table.Column<int>(type: "int", nullable: true),
                    TABLE_CODE = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    CODE_DESC = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    CUR_RATE = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    CODE_KEY = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    MAIN_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    SUB_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    JOURNAL_CODE = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    JOURNAL_CODE2 = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    SYMBOL = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACC_GLOBAL_DEF", x => x.CODE_NO);
                });

            migrationBuilder.CreateTable(
                name: "G_TABLE",
                columns: table => new
                {
                    G_CODE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    G_DESC = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FLAG = table.Column<int>(type: "int", nullable: true),
                    G_TYPE = table.Column<int>(type: "int", nullable: true),
                    G_VALUE = table.Column<decimal>(type: "decimal(11,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_G_TABLE", x => x.G_CODE);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ACC_SAFE_CASH_ACC_TRANS_TYPE",
                table: "ACC_SAFE_CASH",
                column: "ACC_TRANS_TYPE");

            migrationBuilder.CreateIndex(
                name: "IX_ACC_SAFE_CASH_COST_CENTER_CODE",
                table: "ACC_SAFE_CASH",
                column: "COST_CENTER_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_ACC_SAFE_CASH_CUR_CODE",
                table: "ACC_SAFE_CASH",
                column: "CUR_CODE");

            migrationBuilder.CreateIndex(
                name: "IX_ACC_SAFE_CASH_ENTRY_TYPE",
                table: "ACC_SAFE_CASH",
                column: "ENTRY_TYPE");

            migrationBuilder.CreateIndex(
                name: "IX_ACC_SAFE_CASH_SAFE_CODE",
                table: "ACC_SAFE_CASH",
                column: "SAFE_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_ACC_SAFE_CASH_ACC_GLOBAL_DEF_CUR_CODE",
                table: "ACC_SAFE_CASH",
                column: "CUR_CODE",
                principalTable: "ACC_GLOBAL_DEF",
                principalColumn: "CODE_NO");

            migrationBuilder.AddForeignKey(
                name: "FK_ACC_SAFE_CASH_COST_CENTERS_COST_CENTER_CODE",
                table: "ACC_SAFE_CASH",
                column: "COST_CENTER_CODE",
                principalTable: "COST_CENTERS",
                principalColumn: "COST_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_ACC_SAFE_CASH_G_TABLE_ENTRY_TYPE",
                table: "ACC_SAFE_CASH",
                column: "ENTRY_TYPE",
                principalTable: "G_TABLE",
                principalColumn: "G_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_ACC_SAFE_CASH_JOURNAL_TYPE_ACC_TRANS_TYPE",
                table: "ACC_SAFE_CASH",
                column: "ACC_TRANS_TYPE",
                principalTable: "JOURNAL_TYPE",
                principalColumn: "JOURNAL_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_ACC_SAFE_CASH_SAFE_NAME_SAFE_CODE",
                table: "ACC_SAFE_CASH",
                column: "SAFE_CODE",
                principalTable: "SAFE_NAME",
                principalColumn: "SAFE_CODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACC_SAFE_CASH_ACC_GLOBAL_DEF_CUR_CODE",
                table: "ACC_SAFE_CASH");

            migrationBuilder.DropForeignKey(
                name: "FK_ACC_SAFE_CASH_COST_CENTERS_COST_CENTER_CODE",
                table: "ACC_SAFE_CASH");

            migrationBuilder.DropForeignKey(
                name: "FK_ACC_SAFE_CASH_G_TABLE_ENTRY_TYPE",
                table: "ACC_SAFE_CASH");

            migrationBuilder.DropForeignKey(
                name: "FK_ACC_SAFE_CASH_JOURNAL_TYPE_ACC_TRANS_TYPE",
                table: "ACC_SAFE_CASH");

            migrationBuilder.DropForeignKey(
                name: "FK_ACC_SAFE_CASH_SAFE_NAME_SAFE_CODE",
                table: "ACC_SAFE_CASH");

            migrationBuilder.DropTable(
                name: "ACC_GLOBAL_DEF");

            migrationBuilder.DropTable(
                name: "G_TABLE");

            migrationBuilder.DropIndex(
                name: "IX_ACC_SAFE_CASH_ACC_TRANS_TYPE",
                table: "ACC_SAFE_CASH");

            migrationBuilder.DropIndex(
                name: "IX_ACC_SAFE_CASH_COST_CENTER_CODE",
                table: "ACC_SAFE_CASH");

            migrationBuilder.DropIndex(
                name: "IX_ACC_SAFE_CASH_CUR_CODE",
                table: "ACC_SAFE_CASH");

            migrationBuilder.DropIndex(
                name: "IX_ACC_SAFE_CASH_ENTRY_TYPE",
                table: "ACC_SAFE_CASH");

            migrationBuilder.DropIndex(
                name: "IX_ACC_SAFE_CASH_SAFE_CODE",
                table: "ACC_SAFE_CASH");
        }
    }
}
