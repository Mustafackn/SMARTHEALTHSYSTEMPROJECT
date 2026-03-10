using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartHealthSystem.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedRolesFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("a1234567-b123-c123-d123-e12345678910"), "1", null, "Admin", "ADMIN" },
                    { new Guid("b1234567-c123-d123-e123-f12345678911"), "2", null, "Doctor", "DOCTOR" },
                    { new Guid("c1234567-d123-e123-f123-a12345678912"), "3", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a1234567-b123-c123-d123-e12345678910"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b1234567-c123-d123-e123-f12345678911"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c1234567-d123-e123-f123-a12345678912"));
        }
    }
}
