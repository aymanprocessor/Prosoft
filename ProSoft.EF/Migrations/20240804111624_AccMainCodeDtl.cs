using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class AccMainCodeDtl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACC_MAIN_CODE_DTL",
                columns: table => new
                {
                    MAIN_CODE = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false),
                    SEC_CODE = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false),
                    CO_CODE = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACC_MAIN_CODE_DTL", x => new { x.CO_CODE, x.SEC_CODE, x.MAIN_CODE });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACC_MAIN_CODE_DTL");
        }
    }
}
