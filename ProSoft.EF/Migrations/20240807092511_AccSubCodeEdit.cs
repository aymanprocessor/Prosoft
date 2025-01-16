using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class AccSubCodeEdit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACC_SUB_CODE_EDIT",
                columns: table => new
                {
                    MAIN_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    SUB_CODE_FR = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false),
                    SUB_CODE_TO = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACC_SUB_CODE_EDIT", x => new { x.MAIN_CODE, x.SUB_CODE_FR, x.SUB_CODE_TO });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACC_SUB_CODE_EDIT");
        }
    }
}
