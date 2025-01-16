using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class userSecc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USERS_SECTION",
                columns: table => new
                {
                    U_SEC_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_CODE = table.Column<int>(type: "int", nullable: true),
                    CLINIC_ID = table.Column<int>(type: "int", nullable: true),
                    DEFAULT_ID = table.Column<int>(type: "int", nullable: true),
                    BRANCH_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS_SECTION", x => x.U_SEC_ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "USERS_SECTION");
        }
    }
}
