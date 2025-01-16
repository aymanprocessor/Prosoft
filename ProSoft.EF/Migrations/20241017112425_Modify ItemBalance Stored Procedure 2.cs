using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class ModifyItemBalanceStoredProcedure2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchID",
                table: "ItemBalances");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "ItemBalances");

            migrationBuilder.DropColumn(
                name: "ItemCode",
                table: "ItemBalances");

            migrationBuilder.DropColumn(
                name: "StockID",
                table: "ItemBalances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchID",
                table: "ItemBalances",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "ItemBalances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ItemCode",
                table: "ItemBalances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StockID",
                table: "ItemBalances",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
