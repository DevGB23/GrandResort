using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resort_Web.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 11, 15, 35, 42, 124, DateTimeKind.Utc).AddTicks(1911));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2023, 10, 11, 15, 35, 42, 124, DateTimeKind.Utc).AddTicks(1914), "Diamond Villa" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2023, 10, 11, 15, 35, 42, 124, DateTimeKind.Utc).AddTicks(1916), "Safire Royal Villa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2023, 10, 11, 15, 31, 59, 387, DateTimeKind.Utc).AddTicks(959));

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2023, 10, 11, 15, 31, 59, 387, DateTimeKind.Utc).AddTicks(963), "SciFi" });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "Name" },
                values: new object[] { new DateTime(2023, 10, 11, 15, 31, 59, 387, DateTimeKind.Utc).AddTicks(965), "Anime" });
        }
    }
}
