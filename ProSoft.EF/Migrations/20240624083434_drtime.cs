using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProSoft.EF.Migrations
{
    /// <inheritdoc />
    public partial class drtime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DRTIMSHEET",
                columns: table => new
                {
                    DR_TIM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BRANCH_ID = table.Column<int>(type: "int", nullable: true),
                    DR_ID = table.Column<int>(type: "int", nullable: true),
                    DAY_NUMBER = table.Column<int>(type: "int", nullable: true),
                    TIMFROM = table.Column<DateTime>(type: "datetime", nullable: true),
                    TIMTO = table.Column<DateTime>(type: "datetime", nullable: true),
                    EX_PERIOD = table.Column<int>(type: "int", nullable: true),
                    REPLCATE = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DRTIMSHEET", x => x.DR_TIM_ID);
                });
 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DRTIMSHEET");
        }
    }
}
