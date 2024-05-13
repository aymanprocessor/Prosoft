using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
      name: "ACC_SAFE_CASH",
      columns: table => new
      {
          SAFE_CASH_ID = table.Column<int>(type: "int", nullable: false)
              .Annotation("SqlServer:Identity", "1, 1"),
          CO_CODE = table.Column<int>(type: "int", nullable: true),
          F_YEAR = table.Column<int>(type: "int", nullable: true),
          DOC_TYPE = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
          DOC_NO = table.Column<int>(type: "int", nullable: true),
          DOC_DATE = table.Column<DateTime>(type: "datetime2(6)", precision: 6, nullable: true),
          CUR_CODE = table.Column<int>(type: "int", nullable: true),
          PERSON_NAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
          VALUE_PAY = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
          COMMENTT = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
          DISCOUNT_VAL = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
          MAIN_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
          SUB_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
          DELETE_FLAG = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
          APROVED_FLAG = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
          FLAG_PAY = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
          PROFIT_TAX = table.Column<decimal>(type: "decimal(9,3)", nullable: true),
          VAL_PAY_AFTER = table.Column<decimal>(type: "decimal(9,3)", nullable: true),
          ACC_NAME = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
          CUR_SER = table.Column<int>(type: "int", nullable: true),
          FLAG = table.Column<int>(type: "int", nullable: true),
          RATE1 = table.Column<decimal>(type: "decimal(7,4)", nullable: true),
          F_MONTH = table.Column<double>(type: "float", nullable: true),
          ACC_TRANS_NO = table.Column<long>(type: "bigint", nullable: true),
          ACC_TRANS_TYPE = table.Column<int>(type: "int", nullable: true),
          SAFE_CODE = table.Column<int>(type: "int", nullable: true),
          BRANCH_ID = table.Column<int>(type: "int", nullable: true),
          SM_NO = table.Column<int>(type: "int", nullable: true),
          MASTER_ID = table.Column<int>(type: "int", nullable: true),
          REPLCATE = table.Column<int>(type: "int", nullable: true),
          USER_CODE = table.Column<int>(type: "int", nullable: true),
          ENTRY_TYPE = table.Column<int>(type: "int", nullable: true),
          PAT_RET_FLAG = table.Column<int>(type: "int", nullable: true),
          ENTRY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
          BR_REPLC = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
          M_CODE_DTL = table.Column<int>(type: "int", nullable: true),
          POST_RECIPT = table.Column<int>(type: "int", nullable: true),
          COST_CENTER_CODE = table.Column<int>(type: "int", nullable: true),
          G_VALUE_PAY = table.Column<decimal>(type: "decimal(12,3)", nullable: true),
          SAFE_CODE2 = table.Column<int>(type: "int", nullable: true),
          DOC_NO_FR = table.Column<int>(type: "int", nullable: true),
          CSH_ORD_NUM = table.Column<int>(type: "int", nullable: true),
          SER_ID = table.Column<int>(type: "int", nullable: true)
      },
      constraints: table =>
      {
          table.PrimaryKey("PK_ACC_SAFE_CASH", x => x.SAFE_CASH_ID);
      });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
               name: "ACC_SAFE_CASH");
        }
    }
}
