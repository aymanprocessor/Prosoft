using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class master : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DOC_DATE",
                table: "ACC_TRANS_MASTER",
                type: "datetime2(6)",
                precision: 6,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LINE_DESC",
                table: "ACC_TRANS_MASTER",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DOC_DATE",
                table: "ACC_TRANS_MASTER");

            migrationBuilder.DropColumn(
                name: "LINE_DESC",
                table: "ACC_TRANS_MASTER");
        }
    }
}
