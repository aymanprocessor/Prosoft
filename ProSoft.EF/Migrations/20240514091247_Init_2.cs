using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class Init_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CORONARY_ANGIOGRAPHY_REPORT",
                columns: table => new
                {
                    SERIAL_HISTORY = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PAT_SSN = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    ENTRY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    FIRST_OPERATOR = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    SECOND_OPERATOR = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CATHETER_FOR_RCA = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CATHETER_FOR_LEFT_CORONARY = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ACCESS_ = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    AMOUNT_OF_CONTRAST_ML = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PREOPRATIVE_ASSESSMENT = table.Column<double>(type: "float", nullable: true),
                    LEFT_MAIN = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    LAD = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    LCX = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    RCA = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    HEMATOMA_D = table.Column<double>(type: "float", nullable: true),
                    CIN_DIAGNOSTIC = table.Column<double>(type: "float", nullable: true),
                    PREOPRATIVE_ASSESSMENT_ = table.Column<double>(type: "float", nullable: true),
                    LEFT_MAIN_CORONARY = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LEFT_ANTERIOR_DESCENDING = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LEFT_CIRCUMFLEX = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RIGHT_CORONARY_ARTERY = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    OTHER_VESSELS = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    DIAGNOSIS = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RECOMENDATION = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PAT_ID = table.Column<int>(type: "int", nullable: true),
                    SERIAL = table.Column<decimal>(type: "numeric(8,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CORONARY_ANGIOGRAPHY_REPORT", x => x.SERIAL_HISTORY);
                    table.ForeignKey(
                        name: "FK_CORONARY_ANGIOGRAPHY_REPORT_PAT_PAT_ID",
                        column: x => x.PAT_ID,
                        principalTable: "PAT",
                        principalColumn: "PAT_ID");
                });

            migrationBuilder.CreateTable(
                name: "DAILY_FOLLOW_UP_CCU_CHANT",
                columns: table => new
                {
                    SERIAL_HISTORY = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PAT_SSN = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    ENTRY_DATE = table.Column<DateTime>(type: "datetime", nullable: true),
                    CURRENT_C_O = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BLPR = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CONSCIOUS_LEVEL = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    LOWER_LIMB_EDEMA = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    UNINE_OUT_PUT = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CHEST_CONDITION = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    ECG = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    O2_SAT = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    TEMP = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    RBS = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    INSULIN_GIVEN = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CNP = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    INFUSIONS = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    UPDATES_IN_MEDICATION = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    INVESTIGASIONS_ORDERD = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    CONSULTANT_VISIT_RECOMMENDATI = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PAT_ID = table.Column<int>(type: "int", nullable: true),
                    SERIAL = table.Column<decimal>(type: "numeric(8,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DAILY_FOLLOW_UP_CCU_CHANT", x => x.SERIAL_HISTORY);
                    table.ForeignKey(
                        name: "FK_DAILY_FOLLOW_UP_CCU_CHANT_PAT_PAT_ID",
                        column: x => x.PAT_ID,
                        principalTable: "PAT",
                        principalColumn: "PAT_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CORONARY_ANGIOGRAPHY_REPORT_PAT_ID",
                table: "CORONARY_ANGIOGRAPHY_REPORT",
                column: "PAT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_DAILY_FOLLOW_UP_CCU_CHANT_PAT_ID",
                table: "DAILY_FOLLOW_UP_CCU_CHANT",
                column: "PAT_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CORONARY_ANGIOGRAPHY_REPORT");

            migrationBuilder.DropTable(
                name: "DAILY_FOLLOW_UP_CCU_CHANT");
        }
    }
}
