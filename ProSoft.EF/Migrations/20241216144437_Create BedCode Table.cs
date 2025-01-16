using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class CreateBedCodeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BED_CODE",
                columns: table => new
                {
                    B_CODE = table.Column<int>(type: "int", nullable: false),
                    B_CODE_SYS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    R_CODE = table.Column<int>(type: "int", nullable: true),
                    D_CODE = table.Column<int>(type: "int", nullable: true),
                    PRICE_PAT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PRICE_RELATIVET = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BRANCH = table.Column<int>(type: "int", nullable: true),
                    B_ON_OFF = table.Column<int>(type: "int", nullable: true),
                    BOOK_ID = table.Column<int>(type: "int", nullable: true),
                    PAT_ID = table.Column<int>(type: "int", nullable: true),
                    PAT_AD_DATE = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BED_CODE", x => x.B_CODE);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BED_CODE");
        }
    }
}
