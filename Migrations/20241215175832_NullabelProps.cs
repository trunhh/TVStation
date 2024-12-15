using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVStation.Migrations
{
    /// <inheritdoc />
    public partial class NullabelProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "081f3269-cbf5-42bd-a1c5-bbfa1cbda067");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7f73d0e5-acf7-4f17-a906-5d4cddeeaa1a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce59bb62-6358-45cd-bb6f-737170595397");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eb07efd6-aa8f-4a27-89bd-44bb9cf16c33");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "ScriptProgram",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "ProductionRegistration",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c999fbd-0ece-4bdb-8eb0-8c006724dae8", "fe069c86-5f91-4526-84ba-c39da5260e57", "DIRECTOR", "DIRECTOR" },
                    { "6da5e403-4b3c-4207-9762-2c744c0cdbd1", "3fe3fb61-bcf5-4496-b29c-62613ecebbc7", "ADMIN", "ADMIN" },
                    { "9395fb16-307b-4d65-92ad-5135ef6e4503", "67bf85ab-490e-4667-b327-2ceed85b3a4d", "REPORTER", "REPORTER" },
                    { "97f3b661-9411-4140-876f-9bb212568139", "66f800bd-7284-4e56-a0a9-2fdf7fe48f6d", "MANAGER", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c999fbd-0ece-4bdb-8eb0-8c006724dae8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6da5e403-4b3c-4207-9762-2c744c0cdbd1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9395fb16-307b-4d65-92ad-5135ef6e4503");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97f3b661-9411-4140-876f-9bb212568139");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "ScriptProgram",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Genre",
                table: "ProductionRegistration",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "081f3269-cbf5-42bd-a1c5-bbfa1cbda067", "94b0c874-daae-46ac-8491-acb3212b339d", "REPORTER", "REPORTER" },
                    { "7f73d0e5-acf7-4f17-a906-5d4cddeeaa1a", "b9352574-1ecd-4bf6-8640-a655e40a213c", "MANAGER", "MANAGER" },
                    { "ce59bb62-6358-45cd-bb6f-737170595397", "79debbaf-7c6b-4d48-a1f4-c94aa99d8403", "ADMIN", "ADMIN" },
                    { "eb07efd6-aa8f-4a27-89bd-44bb9cf16c33", "fabe7d60-2e3b-402d-a427-cdd8dd5e1899", "DIRECTOR", "DIRECTOR" }
                });
        }
    }
}
