using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHealthSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDiabetesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiabetesTestDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestResultId = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<float>(type: "real", nullable: false),
                    Hypertension = table.Column<int>(type: "int", nullable: false),
                    HeartDisease = table.Column<int>(type: "int", nullable: false),
                    SmokingHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BMI = table.Column<float>(type: "real", nullable: false),
                    HbA1cLevel = table.Column<float>(type: "real", nullable: false),
                    BloodGlucoseLevel = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiabetesTestDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiabetesTestDatas_TestResults_TestResultId",
                        column: x => x.TestResultId,
                        principalTable: "TestResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiabetesTestDatas_TestResultId",
                table: "DiabetesTestDatas",
                column: "TestResultId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiabetesTestDatas");
        }
    }
}
