using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class accbalall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACC_BAL_ALL",
                columns: table => new
                {
                    BAL_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CO_CODE = table.Column<int>(type: "int", nullable: true),
                    DOC_TYPE = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    LINE_SRL = table.Column<int>(type: "int", nullable: true),
                    LINE_TYPE = table.Column<string>(type: "varchar(3)", unicode: false, maxLength: 3, nullable: true),
                    MAIN_CODE = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    SUB_CODE = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    SRL_SORT = table.Column<long>(type: "bigint", nullable: true),
                    NAME1 = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACC_BAL_ALL", x => x.BAL_ID);
                });


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ACC_BAL_ALL");

        }
    }
}
