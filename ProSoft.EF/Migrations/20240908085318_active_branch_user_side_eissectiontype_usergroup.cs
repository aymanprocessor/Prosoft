using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class active_branch_user_side_eissectiontype_usergroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "EisSectionTypesSecCode",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "UserGroupsG_ID",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<string>(
            //    name: "UsersId",
            //    table: "USER_SIDE",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_BRANCH_ID",
                table: "USER_SIDE",
                column: "BRANCH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_EIS_SYS_ID",
                table: "USER_SIDE",
                column: "EIS_SYS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_SIDE_ID",
                table: "USER_SIDE",
                column: "SIDE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_USER_G_ID",
                table: "USER_SIDE",
                column: "USER_G_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_USER_CODE",
                table: "USER_SIDE",
                column: "USER_CODE");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_BRANCHS_BRANCH_ID",
                table: "USER_SIDE",
                column: "BRANCH_ID",
                principalTable: "BRANCHS",
                principalColumn: "BRANCH_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_EIS_SECTION_TYPES_EIS_SYS_ID",
                table: "USER_SIDE",
                column: "EIS_SYS_ID",
                principalTable: "EIS_SECTION_TYPES",
                principalColumn: "SEC_CODE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_SIDES_SIDE_ID",
                table: "USER_SIDE",
                column: "SIDE_ID",
                principalTable: "SIDES",
                principalColumn: "SIDE_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_USERS_GROUP_USER_G_ID",
                table: "USER_SIDE",
                column: "USER_G_ID",
                principalTable: "USERS_GROUP",
                principalColumn: "G_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_USERS_USER_CODE",
                table: "USER_SIDE",
                column: "USER_CODE",
                principalTable: "USERS",
                principalColumn: "USER_CODE",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_BRANCHS_BRANCH_ID",
                table: "USER_SIDE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_EIS_SECTION_TYPES_EIS_SYS_ID",
                table: "USER_SIDE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_SIDES_SIDE_ID",
                table: "USER_SIDE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_USERS_GROUP_USER_G_ID",
                table: "USER_SIDE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_USERS_USER_CODE",
                table: "USER_SIDE");

            migrationBuilder.DropIndex(
                name: "IX_USER_SIDE_BRANCH_ID",
                table: "USER_SIDE");

            migrationBuilder.DropIndex(
                name: "IX_USER_SIDE_EIS_SYS_ID",
                table: "USER_SIDE");

            migrationBuilder.DropIndex(
                name: "IX_USER_SIDE_SIDE_ID",
                table: "USER_SIDE");

            migrationBuilder.DropIndex(
                name: "IX_USER_SIDE_USER_G_ID",
                table: "USER_SIDE");

            migrationBuilder.DropIndex(
                name: "IX_USER_SIDE_USER_CODE",
                table: "USER_SIDE");

            //migrationBuilder.DropColumn(
            //    name: "EisSectionTypesSecCode",
            //    table: "USER_SIDE");

            //migrationBuilder.DropColumn(
            //    name: "UserGroupsG_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropColumn(
            //    name: "UsersId",
            //    table: "USER_SIDE");
        }
    }
}
