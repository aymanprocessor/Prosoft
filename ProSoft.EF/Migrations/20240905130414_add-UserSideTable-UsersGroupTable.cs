using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class addUserSideTableUsersGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersGroups",
                columns: table => new
                {
                    G_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    G_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FLAG = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersGroups", x => x.G_ID);
                });

            migrationBuilder.CreateTable(
                name: "UserSides",
                columns: table => new
                {
                    USER_CODE = table.Column<int>(type: "int", nullable: false),
                    SIDE_ID = table.Column<int>(type: "int", nullable: false),
                    DEPT_ID = table.Column<int>(type: "int", nullable: false),
                    BRANCH_ID = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SidesSideId = table.Column<int>(type: "int", nullable: false),
                    RegionsRegionCode = table.Column<int>(type: "int", nullable: false),
                    BranchsBranchId = table.Column<int>(type: "int", nullable: false),
                    USER_G_ID = table.Column<int>(type: "int", nullable: false),
                    UserGroupId = table.Column<int>(type: "int", nullable: false),
                    FLAG1 = table.Column<int>(type: "int", nullable: false),
                    EIS_SYS_ID = table.Column<int>(type: "int", nullable: false),
                    EisSectionTypesSecCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSides", x => new { x.USER_CODE, x.SIDE_ID, x.DEPT_ID, x.BRANCH_ID });
                    table.ForeignKey(
                        name: "FK_UserSides_BRANCHS_BranchsBranchId",
                        column: x => x.BranchsBranchId,
                        principalTable: "BRANCHS",
                        principalColumn: "BRANCH_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSides_EIS_SECTION_TYPES_EisSectionTypesSecCode",
                        column: x => x.EisSectionTypesSecCode,
                        principalTable: "EIS_SECTION_TYPES",
                        principalColumn: "SEC_CODE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSides_REGIONS_RegionsRegionCode",
                        column: x => x.RegionsRegionCode,
                        principalTable: "REGIONS",
                        principalColumn: "REGION_CODE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSides_SIDES_SidesSideId",
                        column: x => x.SidesSideId,
                        principalTable: "SIDES",
                        principalColumn: "SIDE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSides_USERS_UsersId",
                        column: x => x.UsersId,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSides_UsersGroups_UserGroupId",
                        column: x => x.UserGroupId,
                        principalTable: "UsersGroups",
                        principalColumn: "G_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_BranchsBranchId",
                table: "UserSides",
                column: "BranchsBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_EisSectionTypesSecCode",
                table: "UserSides",
                column: "EisSectionTypesSecCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_RegionsRegionCode",
                table: "UserSides",
                column: "RegionsRegionCode");

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_SidesSideId",
                table: "UserSides",
                column: "SidesSideId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_UserGroupId",
                table: "UserSides",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_UsersId",
                table: "UserSides",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSides");

            migrationBuilder.DropTable(
                name: "UsersGroups");
        }
    }
}
