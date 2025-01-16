using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class SystemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SYSTEM_TABLE",
                columns: table => new
                {
                    SYS_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SYS_VALUE = table.Column<int>(type: "int", nullable: true),
                    SYS_DEC = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    SYS_TYPE = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSTEM_TABLE", x => x.SYS_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYSTEM_TABLE");
        }
    }
}
