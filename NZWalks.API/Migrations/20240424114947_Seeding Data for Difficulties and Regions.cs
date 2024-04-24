using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataforDifficultiesandRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("5619c829-cb70-4f87-960d-e4683d48e89b"), "Easy" },
                    { new Guid("8b7fbc50-39b2-4b0b-b39b-c8d334accb60"), "Medium" },
                    { new Guid("9e752efc-bf7c-45a7-b1ff-9c9d230a9a04"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("6816f730-820d-43d6-abd0-949b36cea722"), "NSN", "Nelson", "https://www.aucklandnz.com/sites/all/themes/custom/aucklandnz/images/auckland-city.jpg" },
                    { new Guid("7e22f497-a464-48f9-baba-36c8cefacf00"), "WGN", "Wellington", "https://www.aucklandnz.com/sites/all/themes/custom/aucklandnz/images/auckland-city.jpg" },
                    { new Guid("b50c9999-2350-476a-8221-e59fb0496079"), "BOP", "Bay Of Plenty", "https://www.aucklandnz.com/sites/all/themes/custom/aucklandnz/images/auckland-city.jpg" },
                    { new Guid("b99f4482-fdc7-4cae-be91-6970aacb9f1e"), "STL", "Southland", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("5619c829-cb70-4f87-960d-e4683d48e89b"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8b7fbc50-39b2-4b0b-b39b-c8d334accb60"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9e752efc-bf7c-45a7-b1ff-9c9d230a9a04"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6816f730-820d-43d6-abd0-949b36cea722"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7e22f497-a464-48f9-baba-36c8cefacf00"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b50c9999-2350-476a-8221-e59fb0496079"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("b99f4482-fdc7-4cae-be91-6970aacb9f1e"));
        }
    }
}
