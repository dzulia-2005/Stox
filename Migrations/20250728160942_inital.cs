using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class inital : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "048f15fc-425d-4de7-9245-d02ebb4b30e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d8dcbf57-8cd6-4a7e-9e30-c49df64d3d85");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0664517d-4005-4e64-a6ce-5df5733f55d9", null, "admin", "ADMIN" },
                    { "428a8be7-bdcc-43c7-9754-82e81573ed7e", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0664517d-4005-4e64-a6ce-5df5733f55d9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "428a8be7-bdcc-43c7-9754-82e81573ed7e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "048f15fc-425d-4de7-9245-d02ebb4b30e6", null, "admin", "ADMIN" },
                    { "d8dcbf57-8cd6-4a7e-9e30-c49df64d3d85", null, "User", "USER" }
                });
        }
    }
}
