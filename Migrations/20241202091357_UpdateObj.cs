using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVStation.Migrations
{
    /// <inheritdoc />
    public partial class UpdateObj : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "49a3412f-73d7-4ef4-819b-347e125db64e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dd3a3608-f3be-4c0e-ad9c-71f3bd5c072d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f664c215-c3a4-45fd-9267-1e29dab64920");

            migrationBuilder.DropColumn(
                name: "ObjectType",
                table: "ProductionRegistration");

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "ProgramFrameWeek",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0f9dd30a-1f5c-446e-867b-3f45097e375a", "9a7a34a0-b96a-4708-9587-6fa59b256634", "EMPLOYEE", "EMPLOYEE" },
                    { "92c1bc84-22be-400d-80f2-b286f2045418", "b72dde77-2d85-4069-87f8-e62249a1e889", "MANAGER", "MANAGER" },
                    { "eba5100e-ea22-4cde-9aa8-651bfce972d1", "ba25e090-865a-4520-ad2d-749b142f2190", "ADMIN", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0f9dd30a-1f5c-446e-867b-3f45097e375a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "92c1bc84-22be-400d-80f2-b286f2045418");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eba5100e-ea22-4cde-9aa8-651bfce972d1");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "ProgramFrameWeek");

            migrationBuilder.AddColumn<string>(
                name: "ObjectType",
                table: "ProductionRegistration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "49a3412f-73d7-4ef4-819b-347e125db64e", "ad435e56-a21d-4a12-b0f7-85ccc22c06a1", "Employee", "EMPLOYEE" },
                    { "dd3a3608-f3be-4c0e-ad9c-71f3bd5c072d", "3a162ec2-f7e4-4b9e-9312-06683ef474d0", "Admin", "ADMIN" },
                    { "f664c215-c3a4-45fd-9267-1e29dab64920", "b9338db7-49ea-4943-8961-55929caee52a", "Manager", "MANAGER" }
                });
        }
    }
}
