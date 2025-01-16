using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class removeUserSideTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER_SIDE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "USER_SIDE",
                columns: table => new
                {
                    USER_CODE = table.Column<int>(type: "int", nullable: false),
                    SIDE_ID = table.Column<int>(type: "int", nullable: false),
                    DEPT_ID = table.Column<int>(type: "int", nullable: false),
                    BRANCH_ID = table.Column<int>(type: "int", nullable: false),
                    BranchsBranchId = table.Column<int>(type: "int", nullable: false),
                    EisSectionTypesSecCode = table.Column<int>(type: "int", nullable: false),
                    RegionsRegionCode = table.Column<int>(type: "int", nullable: false),
                    SidesSideId = table.Column<int>(type: "int", nullable: false),
                    UserGroupsG_ID = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EIS_SYS_ID = table.Column<int>(type: "int", nullable: false),
                    FLAG1 = table.Column<int>(type: "int", nullable: false),
                    USER_G_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_SIDE", x => new { x.USER_CODE, x.SIDE_ID, x.DEPT_ID, x.BRANCH_ID });
                    table.ForeignKey(
                        name: "FK_USER_SIDE_BRANCHS_BranchsBranchId",
                        column: x => x.BranchsBranchId,
                        principalTable: "BRANCHS",
                        principalColumn: "BRANCH_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_SIDE_EIS_SECTION_TYPES_EisSectionTypesSecCode",
                        column: x => x.EisSectionTypesSecCode,
                        principalTable: "EIS_SECTION_TYPES",
                        principalColumn: "SEC_CODE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_SIDE_REGIONS_RegionsRegionCode",
                        column: x => x.RegionsRegionCode,
                        principalTable: "REGIONS",
                        principalColumn: "REGION_CODE",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_SIDE_SIDES_SidesSideId",
                        column: x => x.SidesSideId,
                        principalTable: "SIDES",
                        principalColumn: "SIDE_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_SIDE_USERS_GROUP_UserGroupsG_ID",
                        column: x => x.UserGroupsG_ID,
                        principalTable: "USERS_GROUP",
                        principalColumn: "G_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_USER_SIDE_USERS_UsersId",
                        column: x => x.UsersId,
                        principalTable: "USERS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_BranchsBranchId",
                table: "USER_SIDE",
                column: "BranchsBranchId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_EisSectionTypesSecCode",
                table: "USER_SIDE",
                column: "EisSectionTypesSecCode");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_RegionsRegionCode",
                table: "USER_SIDE",
                column: "RegionsRegionCode");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_SidesSideId",
                table: "USER_SIDE",
                column: "SidesSideId");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_UserGroupsG_ID",
                table: "USER_SIDE",
                column: "UserGroupsG_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SIDE_UsersId",
                table: "USER_SIDE",
                column: "UsersId");
        }
    }
}
