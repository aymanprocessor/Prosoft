using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class trans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACC_TRANS_DETAIL",
                columns: table => new
                {
                    TRANS_DTL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CO_CODE = table.Column<int>(type: "int", nullable: true),
                    F_YEAR = table.Column<int>(type: "int", nullable: true),
                    TRANS_TYPE = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    TRANS_NO = table.Column<int>(type: "int", nullable: true),
                    TRANS_DATE = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    TRANS_SERIAL = table.Column<long>(type: "bigint", nullable: true),
                    MAIN_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    SUB_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    VAL_DEP = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    VAL_CREDIT = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    VAL_DEP_CUR = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    VAL_CREDIT_CUR = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    DOC_NO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    DOC_STATUS = table.Column<int>(type: "int", nullable: true),
                    DOC_DATE = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    COST_CENTER_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    ACC_NAME = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LINE_DESC = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    OK_POST = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    CUR_CODE = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    DOC_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    YEAR_TRANS_NO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    F_MONTH = table.Column<int>(type: "int", nullable: true),
                    USER_CODE = table.Column<int>(type: "int", nullable: true),
                    ENTRY_TYPE = table.Column<int>(type: "int", nullable: true),
                    BR_REPLC = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ENTRY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    M_CODE_DTL = table.Column<int>(type: "int", nullable: true),
                    POST_RECIPT = table.Column<int>(type: "int", nullable: true),
                    USER_CODE_MODIFY = table.Column<int>(type: "int", nullable: true),
                    USER_DATE_MODIFY = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACC_TRANS_DETAIL", x => x.TRANS_DTL_ID);
                });

            migrationBuilder.CreateTable(
                name: "ACC_TRANS_MASTER",
                columns: table => new
                {
                    TRANS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CO_CODE = table.Column<int>(type: "int", nullable: true),
                    F_YEAR = table.Column<int>(type: "int", nullable: true),
                    TRANS_TYPE = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    TRANS_NO = table.Column<int>(type: "int", nullable: true),
                    TRANS_DATE = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    TRANS_DESC = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: true),
                    TOTAL_TRANS = table.Column<decimal>(type: "decimal(12,2)", nullable: true),
                    OK_POST = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    CUR_CODE = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    APR_DATE = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    CUR_RATE = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    YEAR_TRANS_NO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    F_MONTH = table.Column<int>(type: "int", nullable: true),
                    MASTER_ID = table.Column<int>(type: "int", nullable: true),
                    M_CODE = table.Column<int>(type: "int", nullable: true),
                    DOC_STATUS = table.Column<int>(type: "int", nullable: true),
                    BR_REPLC = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ENTRY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    M_CODE_DTL = table.Column<int>(type: "int", nullable: true),
                    DOC_NO = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    POST_RECIPT = table.Column<int>(type: "int", nullable: true),
                    USER_CODE_MODIFY = table.Column<int>(type: "int", nullable: true),
                    USER_DATE_MODIFY = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACC_TRANS_MASTER", x => x.TRANS_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACC_TRANS_DETAIL");

            migrationBuilder.DropTable(
                name: "ACC_TRANS_MASTER");
        }
    }
}
