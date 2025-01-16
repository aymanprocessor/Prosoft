using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class astesta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        //    migrationBuilder.DropForeignKey(
        //        name: "FK_USER_SIDE_EIS_SECTION_TYPES_EisSectionTypesSecCode",
        //        table: "USER_SIDE");

        //    migrationBuilder.DropIndex(
        //        name: "IX_USER_SIDE_EisSectionTypesSecCode",
        //        table: "USER_SIDE");

        //    migrationBuilder.DropColumn(
        //        name: "EisSectionTypesSecCode",
        //        table: "USER_SIDE");

        //    migrationBuilder.CreateIndex(
        //        name: "IX_USER_SIDE_EIS_SYS_ID",
        //        table: "USER_SIDE",
        //        column: "EIS_SYS_ID");

        //    migrationBuilder.AddForeignKey(
        //        name: "FK_USER_SIDE_EIS_SECTION_TYPES_EIS_SYS_ID",
        //        table: "USER_SIDE",
        //        column: "EIS_SYS_ID",
        //        principalTable: "EIS_SECTION_TYPES",
        //        principalColumn: "SEC_CODE",
        //        onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_EIS_SECTION_TYPES_EIS_SYS_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_EIS_SYS_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.AddColumn<int>(
            //    name: "EisSectionTypesSecCode",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_EisSectionTypesSecCode",
            //    table: "USER_SIDE",
            //    column: "EisSectionTypesSecCode");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_EIS_SECTION_TYPES_EisSectionTypesSecCode",
            //    table: "USER_SIDE",
            //    column: "EisSectionTypesSecCode",
            //    principalTable: "EIS_SECTION_TYPES",
            //    principalColumn: "SEC_CODE",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
