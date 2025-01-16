using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class accSafeChecks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccSafeChecks",
                columns: table => new
                {
                    SAFE_CECK_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TRAN_TYPE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    CO_CODE = table.Column<int>(type: "int", nullable: true),
                    F_YEAR = table.Column<int>(type: "int", nullable: true),
                    DOC_NO = table.Column<int>(type: "int", nullable: true),
                    DOC_DATE = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    PERSON_NAME = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CHEK_NO = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CHEK_DATE = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    SATTL_DATE = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    VALUE_PAY = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
                    COMMENTT = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    MAIN_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    SUB_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    FLAG_APR = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    FLAG_PAY = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    BANK_MAIN_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    BANK_SUB_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    FLAG_PAY_STATUS = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    CURRENCY_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    CHECK_STATUS = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    ACC_NAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    FLAG_S = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    CUR_SER = table.Column<int>(type: "int", nullable: true),
                    FLAG = table.Column<int>(type: "int", nullable: true),
                    RATE1 = table.Column<decimal>(type: "decimal(7,4)", nullable: true),
                    F_MONTH = table.Column<int>(type: "int", nullable: true),
                    CHEK_DATE2 = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
                    DOC_NO1 = table.Column<int>(type: "int", nullable: true),
                    DOC_NO2 = table.Column<int>(type: "int", nullable: true),
                    PROFIT_TAX = table.Column<decimal>(type: "decimal(9,3)", nullable: true),
                    DISCOUNT_VAL = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
                    MAIN_CODE2 = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    SUB_CODE2 = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    ACC_NAME2 = table.Column<string>(type: "varchar(70)", unicode: false, maxLength: 70, nullable: true),
                    ACC_TRANS_NO = table.Column<int>(type: "int", nullable: true),
                    ACC_TRANS_TYPE = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    SAFE_CODE = table.Column<int>(type: "int", nullable: true),
                    BRANCH_ID = table.Column<int>(type: "int", nullable: true),
                    SM_NO = table.Column<int>(type: "int", nullable: true),
                    ACC_TRANS_NO2 = table.Column<int>(type: "int", nullable: true),
                    ACC_TRANS_TYPE2 = table.Column<int>(type: "int", nullable: true),
                    ACC_TRANS_NO3 = table.Column<int>(type: "int", nullable: true),
                    ACC_TRANS_TYPE3 = table.Column<int>(type: "int", nullable: true),
                    CHECK_TYPE = table.Column<int>(type: "int", nullable: true),
                    ENTRY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    BR_REPLC = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    M_CODE_DTL = table.Column<int>(type: "int", nullable: true),
                    SUB_CODE_BANK = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    COST_CENTER_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    G_VALUE_PAY = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
                    CSH_ORD_NUM = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccSafeChecks", x => x.SAFE_CECK_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccSafeChecks");
        }
    }
}
