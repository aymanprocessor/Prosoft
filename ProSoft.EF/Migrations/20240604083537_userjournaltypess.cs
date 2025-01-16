using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class userjournaltypess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "JOURNAL_CODE",
                table: "USER_JOURNAL_TYPE",
                type: "int",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldUnicode: false,
                oldMaxLength: 5,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "JOURNAL_CODE",
                table: "USER_JOURNAL_TYPE",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldUnicode: false,
                oldNullable: true);
        }
    }
}
