using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class adduserSideTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserSide_BRANCHS_BranchId",
            //    table: "UserSide");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserSide_EIS_SECTION_TYPES_EisSectionTypeId",
            //    table: "UserSide");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserSide_REGIONS_RegionId",
            //    table: "UserSide");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserSide_SIDES_SideId",
            //    table: "UserSide");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserSide_USERS_GROUP_UserGroupsG_ID",
            //    table: "UserSide");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_UserSide_USERS_UsersId",
            //    table: "UserSide");

            //migrationBuilder.RenameTable(
            //    name: "UserSide",
            //    newName: "USER_SIDE");

            //migrationBuilder.RenameColumn(
            //    name: "SideId",
            //    table: "USER_SIDE",
            //    newName: "SIDE_ID");

            //migrationBuilder.RenameColumn(
            //    name: "RegionId",
            //    table: "USER_SIDE",
            //    newName: "DEPT_ID");

            //migrationBuilder.RenameColumn(
            //    name: "EisSectionTypeId",
            //    table: "USER_SIDE",
            //    newName: "EIS_SYS_ID");

            //migrationBuilder.RenameColumn(
            //    name: "BranchId",
            //    table: "USER_SIDE",
            //    newName: "BRANCH_ID");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UsersId",
            //    table: "USER_SIDE",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    defaultValue: "",
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "UserGroupsG_ID",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "SIDE_ID",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "DEPT_ID",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "EIS_SYS_ID",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<int>(
            //    name: "BRANCH_ID",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0,
            //    oldClrType: typeof(int),
            //    oldType: "int",
            //    oldNullable: true);

            //migrationBuilder.AddColumn<int>(
            //    name: "USER_CODE",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "FLAG1",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "USER_G_ID",
            //    table: "USER_SIDE",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddPrimaryKey(
            //    name: "PK_USER_SIDE",
            //    table: "USER_SIDE",
            //    columns: new[] { "USER_CODE", "SIDE_ID", "DEPT_ID", "BRANCH_ID" });

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_BRANCH_ID",
            //    table: "USER_SIDE",
            //    column: "BRANCH_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_DEPT_ID",
            //    table: "USER_SIDE",
            //    column: "DEPT_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_EIS_SYS_ID",
            //    table: "USER_SIDE",
            //    column: "EIS_SYS_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_SIDE_ID",
            //    table: "USER_SIDE",
            //    column: "SIDE_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_UserGroupsG_ID",
            //    table: "USER_SIDE",
            //    column: "UserGroupsG_ID");

            //migrationBuilder.CreateIndex(
            //    name: "IX_USER_SIDE_UsersId",
            //    table: "USER_SIDE",
            //    column: "UsersId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_BRANCHS_BRANCH_ID",
            //    table: "USER_SIDE",
            //    column: "BRANCH_ID",
            //    principalTable: "BRANCHS",
            //    principalColumn: "BRANCH_ID",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_EIS_SECTION_TYPES_EIS_SYS_ID",
            //    table: "USER_SIDE",
            //    column: "EIS_SYS_ID",
            //    principalTable: "EIS_SECTION_TYPES",
            //    principalColumn: "SEC_CODE",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_REGIONS_DEPT_ID",
            //    table: "USER_SIDE",
            //    column: "DEPT_ID",
            //    principalTable: "REGIONS",
            //    principalColumn: "REGION_CODE",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_USER_SIDE_SIDES_SIDE_ID",
            //    table: "USER_SIDE",
            //    column: "SIDE_ID",
            //    principalTable: "SIDES",
            //    principalColumn: "SIDE_ID",
            //    onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_BRANCHS_BRANCH_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_EIS_SECTION_TYPES_EIS_SYS_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_REGIONS_DEPT_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_SIDES_SIDE_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_USERS_GROUP_UserGroupsG_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_USER_SIDE_USERS_UsersId",
            //    table: "USER_SIDE");

            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_USER_SIDE",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_BRANCH_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_DEPT_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_EIS_SYS_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_SIDE_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_UserGroupsG_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.DropIndex(
            //    name: "IX_USER_SIDE_UsersId",
            //    table: "USER_SIDE");

            //migrationBuilder.DropColumn(
            //    name: "USER_CODE",
            //    table: "USER_SIDE");

            //migrationBuilder.DropColumn(
            //    name: "FLAG1",
            //    table: "USER_SIDE");

            //migrationBuilder.DropColumn(
            //    name: "USER_G_ID",
            //    table: "USER_SIDE");

            //migrationBuilder.RenameTable(
            //    name: "USER_SIDE",
            //    newName: "UserSide");

            //migrationBuilder.RenameColumn(
            //    name: "EIS_SYS_ID",
            //    table: "UserSide",
            //    newName: "EisSectionTypeId");

            //migrationBuilder.RenameColumn(
            //    name: "BRANCH_ID",
            //    table: "UserSide",
            //    newName: "BranchId");

            //migrationBuilder.RenameColumn(
            //    name: "DEPT_ID",
            //    table: "UserSide",
            //    newName: "RegionId");

            //migrationBuilder.RenameColumn(
            //    name: "SIDE_ID",
            //    table: "UserSide",
            //    newName: "SideId");

            //migrationBuilder.AlterColumn<string>(
            //    name: "UsersId",
            //    table: "UserSide",
            //    type: "nvarchar(450)",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "nvarchar(450)");

            //migrationBuilder.AlterColumn<int>(
            //    name: "UserGroupsG_ID",
            //    table: "UserSide",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AlterColumn<int>(
            //    name: "EisSectionTypeId",
            //    table: "UserSide",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AlterColumn<int>(
            //    name: "BranchId",
            //    table: "UserSide",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AlterColumn<int>(
            //    name: "RegionId",
            //    table: "UserSide",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AlterColumn<int>(
            //    name: "SideId",
            //    table: "UserSide",
            //    type: "int",
            //    nullable: true,
            //    oldClrType: typeof(int),
            //    oldType: "int");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserSide_BRANCHS_BranchId",
            //    table: "UserSide",
            //    column: "BranchId",
            //    principalTable: "BRANCHS",
            //    principalColumn: "BRANCH_ID",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserSide_EIS_SECTION_TYPES_EisSectionTypeId",
            //    table: "UserSide",
            //    column: "EisSectionTypeId",
            //    principalTable: "EIS_SECTION_TYPES",
            //    principalColumn: "SEC_CODE",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserSide_REGIONS_RegionId",
            //    table: "UserSide",
            //    column: "RegionId",
            //    principalTable: "REGIONS",
            //    principalColumn: "REGION_CODE",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserSide_SIDES_SideId",
            //    table: "UserSide",
            //    column: "SideId",
            //    principalTable: "SIDES",
            //    principalColumn: "SIDE_ID",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserSide_USERS_GROUP_UserGroupsG_ID",
            //    table: "UserSide",
            //    column: "UserGroupsG_ID",
            //    principalTable: "USERS_GROUP",
            //    principalColumn: "G_ID",
            //    onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddForeignKey(
            //    name: "FK_UserSide_USERS_UsersId",
            //    table: "UserSide",
            //    column: "UsersId",
            //    principalTable: "USERS",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
