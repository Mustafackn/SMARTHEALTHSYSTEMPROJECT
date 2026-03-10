using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHealthSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CancerTestDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestResultId = table.Column<int>(type: "int", nullable: false),
                    RadiusMean = table.Column<double>(type: "float", nullable: false),
                    TextureMean = table.Column<double>(type: "float", nullable: false),
                    PerimeterMean = table.Column<double>(type: "float", nullable: false),
                    AreaMean = table.Column<double>(type: "float", nullable: false),
                    SmoothnessMean = table.Column<double>(type: "float", nullable: false),
                    CompactnessMean = table.Column<double>(type: "float", nullable: false),
                    ConcavityMean = table.Column<double>(type: "float", nullable: false),
                    ConcavePointsMean = table.Column<double>(type: "float", nullable: false),
                    SymmetryMean = table.Column<double>(type: "float", nullable: false),
                    FractalDimensionMean = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CancerTestDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CancerTestDatas_TestResults_TestResultId",
                        column: x => x.TestResultId,
                        principalTable: "TestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CancerTestDatas_TestResultId",
                table: "CancerTestDatas",
                column: "TestResultId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CancerTestDatas");
        }
    }
}
