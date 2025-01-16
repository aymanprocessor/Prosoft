using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class DropkeytostentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_STENT_DES",
            //    table: "STENT_DES");

            //migrationBuilder.DropColumn(
            //    name: "STENT_ID",
            //    table: "STENT_DES");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "STENT_ID",
            //    table: "STENT_DES",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0)
            //    .Annotation("SqlServer:Identity", "1, 1");

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_STENT_DES",
            //    table: "STENT_DES",
            //    column: "STENT_ID");
        }
    }
}
