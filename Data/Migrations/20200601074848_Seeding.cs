using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1, "Mobile" },
                    { 2, "TV" },
                    { 3, "Audio" }
                });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "Discount", "Price", "Type" },
                values: new object[,]
                {
                    { 1, 0.050000000000000003, 25.0, "Bronze" },
                    { 2, 0.10000000000000001, 35.0, "Silver" },
                    { 3, 0.14999999999999999, 45.0, "Gold" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Memberships",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
