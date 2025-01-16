using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class adduserSideTable3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_USERS_GROUP_UserGroupsG_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_USERS_UsersId",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_UserGroupsG_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_UsersId",
            //    table: "USER_SIDE");

            //migrationBuilder.DropColumn(
            //    name: "UserGroupsG_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropColumn(
            //    name: "UsersId",
            //    table: "USER_SIDE");

            //migrationBuilder.RenameIndex(
            //    name: "IX_USER_SIDE_SIDE_ID",
            //    table: "USER_SIDE",
            //    newName: "IX_UserSides_SideId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_USER_SIDE_EIS_SYS_ID",
            //    table: "USER_SIDE",
            //    newName: "IX_UserSides_EisSectionTypeId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_USER_SIDE_DEPT_ID",
            //    table: "USER_SIDE",
            //    newName: "IX_UserSides_RegionId");

            //migrationBuilder.RenameIndex(
            //    name: "IX_USER_SIDE_BRANCH_ID",
            //    table: "USER_SIDE",
            //    newName: "IX_UserSides_BranchId");

            //migrationBuilder.AddUniqueConstraint(
            //    name: "AK_USERS_USER_CODE",
            //    table: "USERS",
            //    column: "USER_CODE");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserSides_UserGroupId",
            //    table: "USER_SIDE",
            //    column: "USER_G_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_UserSides_UserId",
            //    table: "USER_SIDE",
            //    column: "USER_CODE");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_USERS_GROUP_USER_G_ID",
            //    table: "USER_SIDE",
            //    column: "USER_G_ID",
            //    principalTable: "USERS_GROUP",
            //    principalColumn: "G_ID",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_USERS_USER_CODE",
            //    table: "USER_SIDE",
            //    column: "USER_CODE",
            //    principalTable: "USERS",
            //    principalColumn: "USER_CODE",
            //    onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_USERS_GROUP_USER_G_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_USERS_USER_CODE",
            //    table: "USER_SIDE");

            //migrationBuilder.DropUniqueConstraint(
            //    name: "AK_USERS_USER_CODE",
            //    table: "USERS");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserSides_UserGroupId",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_UserSides_UserId",
            //    table: "USER_SIDE");

            //migrationBuilder.RenameIndex(
            //    name: "IX_UserSides_SideId",
            //    table: "USER_SIDE",
            //    newName: "IX_USER_SIDE_SIDE_ID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_UserSides_RegionId",
            //    table: "USER_SIDE",
            //    newName: "IX_USER_SIDE_DEPT_ID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_UserSides_EisSectionTypeId",
            //    table: "USER_SIDE",
            //    newName: "IX_USER_SIDE_EIS_SYS_ID");

            //migrationBuilder.RenameIndex(
            //    name: "IX_UserSides_BranchId",
            //    table: "USER_SIDE",
            //    newName: "IX_USER_SIDE_BRANCH_ID");

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

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_UserGroupsG_ID",
            //    table: "USER_SIDE",
            //    column: "UserGroupsG_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_UsersId",
            //    table: "USER_SIDE",
            //    column: "UsersId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_USERS_GROUP_UserGroupsG_ID",
            //    table: "USER_SIDE",
            //    column: "UserGroupsG_ID",
            //    principalTable: "USERS_GROUP",
            //    principalColumn: "G_ID",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_USERS_UsersId",
            //    table: "USER_SIDE",
            //    column: "UsersId",
            //    principalTable: "USERS",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
