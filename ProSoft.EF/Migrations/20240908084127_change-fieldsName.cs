using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class changefieldsName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_REGIONS_RegionsRegionCode",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_RegionsRegionCode",
            //    table: "USER_SIDE");

            //migrationBuilder.DropColumn(
            //    name: "RegionsRegionCode",
            //    table: "USER_SIDE");

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_DEPT_ID",
            //    table: "USER_SIDE",
            //    column: "DEPT_ID");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_REGIONS_DEPT_ID",
            //    table: "USER_SIDE",
            //    column: "DEPT_ID",
            //    principalTable: "REGIONS",
            //    principalColumn: "REGION_CODE",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_REGIONS_DEPT_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_DEPT_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.AddColumn<int>(
            //    name: "RegionsRegionCode",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_RegionsRegionCode",
            //    table: "USER_SIDE",
            //    column: "RegionsRegionCode");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_REGIONS_RegionsRegionCode",
            //    table: "USER_SIDE",
            //    column: "RegionsRegionCode",
            //    principalTable: "REGIONS",
            //    principalColumn: "REGION_CODE",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
