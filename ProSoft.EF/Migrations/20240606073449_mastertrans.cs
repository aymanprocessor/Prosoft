using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class mastertrans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LINE_DESC",
                table: "ACC_TRANS_MASTER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LINE_DESC",
                table: "ACC_TRANS_MASTER",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: true);
        }
    }
}
