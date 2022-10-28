using Microsoft.EntityFrameworkCore.Migrations;

namespace SkynetzMVC.Migrations
{
    public partial class SeedDataPlansAndTariff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Plans",
                columns: new[] { "Id", "FreeMinutes", "Name" },
                values: new object[,]
                {
                    { 2, 60, "FaleMais 60" },
                    { 3, 120, "FaleMais 120" }
                });

            migrationBuilder.InsertData(
                table: "Tariffs",
                columns: new[] { "Id", "Destination", "MinuteValue", "Source" },
                values: new object[,]
                {
                    { 2, "011", 2.9, "016" },
                    { 3, "017", 1.7, "011" },
                    { 4, "011", 2.7, "017" },
                    { 5, "018", 0.9, "011" },
                    { 6, "011", 1.9, "018" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Plans",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tariffs",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
