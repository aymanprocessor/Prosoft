using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class adduserSideTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "USER_SIDE",
               columns: table => new
               {
                   userId = table.Column<int>(type: "int", nullable: false),
                   SideId = table.Column<int>(type: "int", nullable: false),
                   RegionId = table.Column<int>(type: "int", nullable: false),
                   BranchId = table.Column<int>(type: "int", nullable: false),
                   UserGroupId = table.Column<int>(type: "int", nullable: false),
                   Flag = table.Column<int>(type: "int", nullable: false),
                   EisSectionTypeId = table.Column<int>(type: "int", nullable: false),
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_UserSides", x => new { x.userId, x.SideId, x.RegionId, x.BranchId });
                   table.ForeignKey(
                       name: "FK_UserSides_BRANCHS_BranchId",
                       column: x => x.BranchId,
                       principalTable: "BRANCHS",
                       principalColumn: "BRANCH_ID",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_UserSides_EIS_SECTION_TYPES_EisSectionTypeId",
                       column: x => x.EisSectionTypeId,
                       principalTable: "EIS_SECTION_TYPES",
                       principalColumn: "SEC_CODE",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_UserSides_REGIONS_RegionId",
                       column: x => x.RegionId,
                       principalTable: "REGIONS",
                       principalColumn: "REGION_CODE",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_UserSides_SIDES_SideId",
                       column: x => x.SideId,
                       principalTable: "SIDES",
                       principalColumn: "SIDE_ID",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_UserSides_USERS_userId",
                       column: x => x.userId,
                       principalTable: "USERS",
                       principalColumn: "USER_CODE",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_UserSides_UsersGroups_UserGroupId",
                       column: x => x.UserGroupId,
                       principalTable: "USERS_GROUP",
                       principalColumn: "G_ID",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_BranchId",
                table: "USER_SIDE",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_EisSectionTypeId",
                table: "USER_SIDE",
                column: "EisSectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_RegionId",
                table: "USER_SIDE",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_SideId",
                table: "USER_SIDE",
                column: "SideId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_UserGroupId",
                table: "USER_SIDE",
                column: "UserGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserSides_UserId",
                table: "USER_SIDE",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                   name: "USER_SIDE"
               );
        }
    }
}
