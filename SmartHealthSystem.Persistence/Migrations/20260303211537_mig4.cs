using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHealthSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KidneyTestDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestResultId = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<double>(type: "float", nullable: false),
                    Bp = table.Column<double>(type: "float", nullable: false),
                    Sg = table.Column<double>(type: "float", nullable: false),
                    Al = table.Column<double>(type: "float", nullable: false),
                    Su = table.Column<double>(type: "float", nullable: false),
                    Rbc = table.Column<int>(type: "int", nullable: false),
                    Pc = table.Column<int>(type: "int", nullable: false),
                    Pcc = table.Column<int>(type: "int", nullable: false),
                    Ba = table.Column<int>(type: "int", nullable: false),
                    Bgr = table.Column<double>(type: "float", nullable: false),
                    Bu = table.Column<double>(type: "float", nullable: false),
                    Sc = table.Column<double>(type: "float", nullable: false),
                    Sod = table.Column<double>(type: "float", nullable: false),
                    Pot = table.Column<double>(type: "float", nullable: false),
                    Hemo = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KidneyTestDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KidneyTestDatas_TestResults_TestResultId",
                        column: x => x.TestResultId,
                        principalTable: "TestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KidneyTestDatas_TestResultId",
                table: "KidneyTestDatas",
                column: "TestResultId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KidneyTestDatas");
        }
    }
}
