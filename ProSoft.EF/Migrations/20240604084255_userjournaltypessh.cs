using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class userjournaltypessh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_USER_JOURNAL_TYPE_JOURNAL_CODE",
                table: "USER_JOURNAL_TYPE",
                column: "JOURNAL_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_JOURNAL_TYPE_JOURNAL_TYPE_JOURNAL_CODE",
                table: "USER_JOURNAL_TYPE",
                column: "JOURNAL_CODE",
                principalTable: "JOURNAL_TYPE",
                principalColumn: "JOURNAL_CODE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_JOURNAL_TYPE_JOURNAL_TYPE_JOURNAL_CODE",
                table: "USER_JOURNAL_TYPE");

            migrationBuilder.DropIndex(
                name: "IX_USER_JOURNAL_TYPE_JOURNAL_CODE",
                table: "USER_JOURNAL_TYPE");
        }
    }
}
