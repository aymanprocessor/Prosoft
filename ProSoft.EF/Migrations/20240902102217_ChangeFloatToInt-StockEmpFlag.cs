using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFloatToIntStockEmpFlag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey("PK_STOCK_EMP_FLAG", "STOCK_EMP_FLAG");

            migrationBuilder.AlterColumn<int>(
                name: "K_ID",
                table: "STOCK_EMP_FLAG",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "BRANCH_ID",
                table: "STOCK_EMP_FLAG",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "STKCOD",
                table: "STOCK_EMP_FLAG",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<int>(
                name: "USER_ID",
                table: "STOCK_EMP_FLAG",
                type: "int",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddPrimaryKey(
          name: "PK_STOCK_EMP_FLAG",
          table: "STOCK_EMP_FLAG",
          columns: new[] { "K_ID", "BRANCH_ID", "STKCOD", "USER_ID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey("PK_STOCK_EMP_FLAG", "STOCK_EMP_FLAG");


            migrationBuilder.AlterColumn<float>(
                name: "K_ID",
                table: "STOCK_EMP_FLAG",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "BRANCH_ID",
                table: "STOCK_EMP_FLAG",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "STKCOD",
                table: "STOCK_EMP_FLAG",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<float>(
                name: "USER_ID",
                table: "STOCK_EMP_FLAG",
                type: "real",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
            migrationBuilder.AddPrimaryKey(
          name: "PK_STOCK_EMP_FLAG",
          table: "STOCK_EMP_FLAG",
          columns: new[] { "K_ID", "BRANCH_ID", "STKCOD", "USER_ID" });
        }
    }
}
