using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class accStartBallll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACC_START_BAL",
                columns: table => new
                {
                    START_BAL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CO_CODE = table.Column<int>(type: "int", nullable: true),
                    F_YEAR = table.Column<int>(type: "int", nullable: true),
                    MAIN_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    SUB_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    F_DEP_OR = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    F_CREDIT_OR = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    F_DEP_CUR = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    F_CREDIT_CUR = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    CUR_CODE = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    CUR_RATE = table.Column<decimal>(type: "decimal(6,5)", nullable: true),
                    ACC_NAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    COMMENTT = table.Column<string>(type: "varchar(60)", unicode: false, maxLength: 60, nullable: true),
                    BR_REPLC = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    TRANS_TYPE = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    COST_CENTER_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACC_START_BAL", x => x.START_BAL_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACC_START_BAL");
        }
    }
}
