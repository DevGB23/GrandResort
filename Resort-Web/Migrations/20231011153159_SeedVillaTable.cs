using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Resort_Web.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedAt", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "SqFt", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 10, 11, 15, 31, 59, 387, DateTimeKind.Utc).AddTicks(959), "Etiam auctor pellentesque metus sit amet accumsan. Proin laoreet viverra nibh, eget eleifend dui condimentum pulvinar. Duis auctor in ipsum ut rutrum. Aliquam posuere sollicitudin quam. In in lobortis felis. Fusce ultricies, eros nec vestibulum pulvinar, ex massa interdum nunc, at pretium ipsum justo ac ex. Suspendisse ut luctus lectus. Proin congue sodales porta. Proin eleifend semper neque eu iaculis. Donec feugiat risus enim, vitae consectetur mauris porta id. Donec euismod, massa non porttitor tincidunt, diam dolor dignissim nisl", "", "Royal Villa", 5, "200", 580, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(2023, 10, 11, 15, 31, 59, 387, DateTimeKind.Utc).AddTicks(963), "Sed mollis odio in justo volutpat semper. Etiam eget interdum ipsum, id placerat dui. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus", "", "SciFi", 7, "170", 480, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", new DateTime(2023, 10, 11, 15, 31, 59, 387, DateTimeKind.Utc).AddTicks(965), "Donec est lacus, pharetra sagittis eleifend ac, sodales sit amet tortor. Aliquam massa odio, ullamcorper sit amet tristique in, facilisis at libero. Aenean in magna mi. ", "", "Anime", 9, "400", 660, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
