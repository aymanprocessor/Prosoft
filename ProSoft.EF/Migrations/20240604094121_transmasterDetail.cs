using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class transmasterDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TRANS_ID",
                table: "ACC_TRANS_DETAIL",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ACC_TRANS_DETAIL_TRANS_ID",
                table: "ACC_TRANS_DETAIL",
                column: "TRANS_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_ACC_TRANS_DETAIL_ACC_TRANS_MASTER_TRANS_ID",
                table: "ACC_TRANS_DETAIL",
                column: "TRANS_ID",
                principalTable: "ACC_TRANS_MASTER",
                principalColumn: "TRANS_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ACC_TRANS_DETAIL_ACC_TRANS_MASTER_TRANS_ID",
                table: "ACC_TRANS_DETAIL");

            migrationBuilder.DropIndex(
                name: "IX_ACC_TRANS_DETAIL_TRANS_ID",
                table: "ACC_TRANS_DETAIL");

            migrationBuilder.DropColumn(
                name: "TRANS_ID",
                table: "ACC_TRANS_DETAIL");
        }
    }
}
