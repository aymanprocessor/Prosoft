using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class CompanyProfilee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COMPANY_PROFILE",
                columns: table => new
                {
                    CO_CODE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CO_NAME_A = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CO_NAME_E = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    F_YEAR = table.Column<double>(type: "float", nullable: true),
                    TYPE_DB = table.Column<double>(type: "float", nullable: true),
                    CO_PROFILE_DB = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    USER_NAME_DB = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PASS_WORD_DB = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SERVICE_DB = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    SERIAL_DB = table.Column<double>(type: "float", nullable: true),
                    COMPANY_LOGO = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    COMPANY_LOGO_DTL = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    ARSHIVE_FILE = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    LOGO_H = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LOGO_D = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LOGO = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    LOGO_HEADER = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    LAST_UP_DATE = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY_PROFILE", x => x.CO_CODE);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "COMPANY_PROFILE");
        }
    }
}
