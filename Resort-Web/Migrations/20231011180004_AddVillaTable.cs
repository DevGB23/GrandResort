using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Resort_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddVillaTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    SqFt = table.Column<int>(type: "integer", nullable: false),
                    Occupancy = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Amenity = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreatedAt", "Details", "ImageUrl", "Name", "Occupancy", "Rate", "SqFt", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "", new DateTime(2023, 10, 11, 18, 0, 4, 65, DateTimeKind.Utc).AddTicks(3658), "Etiam auctor pellentesque metus sit amet accumsan. Proin laoreet viverra nibh, eget eleifend dui condimentum pulvinar. Duis auctor in ipsum ut rutrum. Aliquam posuere sollicitudin quam. In in lobortis felis. Fusce ultricies, eros nec vestibulum pulvinar, ex massa interdum nunc, at pretium ipsum justo ac ex. Suspendisse ut luctus lectus. Proin congue sodales porta. Proin eleifend semper neque eu iaculis. Donec feugiat risus enim, vitae consectetur mauris porta id. Donec euismod, massa non porttitor tincidunt, diam dolor dignissim nisl", "", "Royal Villa", 5, 200.0, 580, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "", new DateTime(2023, 10, 11, 18, 0, 4, 65, DateTimeKind.Utc).AddTicks(3661), "Sed mollis odio in justo volutpat semper. Etiam eget interdum ipsum, id placerat dui. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus", "", "Diamond Villa", 7, 170.0, 480, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "", new DateTime(2023, 10, 11, 18, 0, 4, 65, DateTimeKind.Utc).AddTicks(3664), "Donec est lacus, pharetra sagittis eleifend ac, sodales sit amet tortor. Aliquam massa odio, ullamcorper sit amet tristique in, facilisis at libero. Aenean in magna mi. ", "", "Safire Royal Villa", 9, 400.0, 660, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}
