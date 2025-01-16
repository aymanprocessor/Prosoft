using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class Dropstent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropTable(
            //    name: "STENT_DES");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "STENT_DES",
            //    columns: table => new
            //    {
            //        STENT_DESC = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
            //        STENT_TYPE = table.Column<int>(type: "int", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //    });
        }
    }
}
