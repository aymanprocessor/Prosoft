using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class renameFieldsUserSideTableUsersGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSides_BRANCHS_BranchsBranchId",
                table: "UserSides");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSides_EIS_SECTION_TYPES_EisSectionTypesSecCode",
                table: "UserSides");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSides_REGIONS_RegionsRegionCode",
                table: "UserSides");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSides_SIDES_SidesSideId",
                table: "UserSides");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSides_USERS_UsersId",
                table: "UserSides");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSides_UsersGroups_UserGroupId",
                table: "UserSides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSides",
                table: "UserSides");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersGroups",
                table: "UsersGroups");

            migrationBuilder.RenameTable(
                name: "UserSides",
                newName: "USER_SIDE");

            migrationBuilder.RenameTable(
                name: "UsersGroups",
                newName: "USERS_GROUP");

            migrationBuilder.RenameIndex(
                name: "IX_UserSides_UsersId",
                table: "USER_SIDE",
                newName: "IX_USER_SIDE_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSides_UserGroupId",
                table: "USER_SIDE",
                newName: "IX_USER_SIDE_UserGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSides_SidesSideId",
                table: "USER_SIDE",
                newName: "IX_USER_SIDE_SidesSideId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSides_RegionsRegionCode",
                table: "USER_SIDE",
                newName: "IX_USER_SIDE_RegionsRegionCode");

            migrationBuilder.RenameIndex(
                name: "IX_UserSides_EisSectionTypesSecCode",
                table: "USER_SIDE",
                newName: "IX_USER_SIDE_EisSectionTypesSecCode");

            migrationBuilder.RenameIndex(
                name: "IX_UserSides_BranchsBranchId",
                table: "USER_SIDE",
                newName: "IX_USER_SIDE_BranchsBranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_USER_SIDE",
                table: "USER_SIDE",
                columns: new[] { "USER_CODE", "SIDE_ID", "DEPT_ID", "BRANCH_ID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_USERS_GROUP",
                table: "USERS_GROUP",
                column: "G_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_BRANCHS_BranchsBranchId",
                table: "USER_SIDE",
                column: "BranchsBranchId",
                principalTable: "BRANCHS",
                principalColumn: "BRANCH_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_EIS_SECTION_TYPES_EisSectionTypesSecCode",
                table: "USER_SIDE",
                column: "EisSectionTypesSecCode",
                principalTable: "EIS_SECTION_TYPES",
                principalColumn: "SEC_CODE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_REGIONS_RegionsRegionCode",
                table: "USER_SIDE",
                column: "RegionsRegionCode",
                principalTable: "REGIONS",
                principalColumn: "REGION_CODE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_SIDES_SidesSideId",
                table: "USER_SIDE",
                column: "SidesSideId",
                principalTable: "SIDES",
                principalColumn: "SIDE_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_USERS_GROUP_UserGroupsG_ID",
                table: "USER_SIDE",
                column: "UserGroupsG_ID",
                principalTable: "USERS_GROUP",
                principalColumn: "G_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_USER_SIDE_USERS_UsersId",
                table: "USER_SIDE",
                column: "UsersId",
                principalTable: "USERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_BRANCHS_BranchsBranchId",
                table: "USER_SIDE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_EIS_SECTION_TYPES_EisSectionTypesSecCode",
                table: "USER_SIDE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_REGIONS_RegionsRegionCode",
                table: "USER_SIDE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_SIDES_SidesSideId",
                table: "USER_SIDE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_USERS_GROUP_UserGroupsG_ID",
                table: "USER_SIDE");

            migrationBuilder.DropForeignKey(
                name: "FK_USER_SIDE_USERS_UsersId",
                table: "USER_SIDE");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USERS_GROUP",
                table: "USERS_GROUP");

            migrationBuilder.DropPrimaryKey(
                name: "PK_USER_SIDE",
                table: "USER_SIDE");

            migrationBuilder.RenameTable(
                name: "USERS_GROUP",
                newName: "UsersGroups");

            migrationBuilder.RenameTable(
                name: "USER_SIDE",
                newName: "UserSides");

            migrationBuilder.RenameIndex(
                name: "IX_USER_SIDE_UsersId",
                table: "UserSides",
                newName: "IX_UserSides_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_USER_SIDE_UserGroupsG_ID",
                table: "UserSides",
                newName: "IX_UserSides_UserGroupsG_ID");

            migrationBuilder.RenameIndex(
                name: "IX_USER_SIDE_SidesSideId",
                table: "UserSides",
                newName: "IX_UserSides_SidesSideId");

            migrationBuilder.RenameIndex(
                name: "IX_USER_SIDE_RegionsRegionCode",
                table: "UserSides",
                newName: "IX_UserSides_RegionsRegionCode");

            migrationBuilder.RenameIndex(
                name: "IX_USER_SIDE_EisSectionTypesSecCode",
                table: "UserSides",
                newName: "IX_UserSides_EisSectionTypesSecCode");

            migrationBuilder.RenameIndex(
                name: "IX_USER_SIDE_BranchsBranchId",
                table: "UserSides",
                newName: "IX_UserSides_BranchsBranchId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersGroups",
                table: "UsersGroups",
                column: "G_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSides",
                table: "UserSides",
                columns: new[] { "USER_CODE", "SIDE_ID", "DEPT_ID", "BRANCH_ID" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserSides_BRANCHS_BranchsBranchId",
                table: "UserSides",
                column: "BranchsBranchId",
                principalTable: "BRANCHS",
                principalColumn: "BRANCH_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSides_EIS_SECTION_TYPES_EisSectionTypesSecCode",
                table: "UserSides",
                column: "EisSectionTypesSecCode",
                principalTable: "EIS_SECTION_TYPES",
                principalColumn: "SEC_CODE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSides_REGIONS_RegionsRegionCode",
                table: "UserSides",
                column: "RegionsRegionCode",
                principalTable: "REGIONS",
                principalColumn: "REGION_CODE",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSides_SIDES_SidesSideId",
                table: "UserSides",
                column: "SidesSideId",
                principalTable: "SIDES",
                principalColumn: "SIDE_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSides_USERS_UsersId",
                table: "UserSides",
                column: "UsersId",
                principalTable: "USERS",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSides_UsersGroups_UserGroupsG_ID",
                table: "UserSides",
                column: "UserGroupsG_ID",
                principalTable: "UsersGroups",
                principalColumn: "G_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
