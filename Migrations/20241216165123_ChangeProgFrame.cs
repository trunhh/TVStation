using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVStation.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProgFrame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Year",
                table: "ProgramFrameYear");

            migrationBuilder.DropColumn(
                name: "Week",
                table: "ProgramFrameWeek");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "ProgramFrameWeek");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "ProgramFrameBroadcast");

            migrationBuilder.AddColumn<DateTime>(
                name: "Airdate",
                table: "ProgramFrameYear",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Airdate",
                table: "ProgramFrameWeek",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1e5dd931-d1cd-4c08-955b-a8f58aac30b5", "f7a63421-ffca-4624-982f-ef728be9c334", "REPORTER", "REPORTER" },
                    { "3f5aaadf-da2b-4375-a7ce-2c09dbf27a7a", "ad0d998f-5e1f-4728-86ba-5459f8337749", "ADMIN", "ADMIN" },
                    { "6692cb9f-33b5-45ef-9508-6a992c13cd40", "da867f58-27da-4b30-8d2a-29d8782eae36", "MANAGER", "MANAGER" },
                    { "d38bf12d-a7dc-4e5b-9ab8-0b57ef9d19ab", "23c833fd-69b4-4945-a611-1c1d3e48d592", "DIRECTOR", "DIRECTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1e5dd931-d1cd-4c08-955b-a8f58aac30b5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3f5aaadf-da2b-4375-a7ce-2c09dbf27a7a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6692cb9f-33b5-45ef-9508-6a992c13cd40");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d38bf12d-a7dc-4e5b-9ab8-0b57ef9d19ab");

            migrationBuilder.DropColumn(
                name: "Airdate",
                table: "ProgramFrameYear");

            migrationBuilder.DropColumn(
                name: "Airdate",
                table: "ProgramFrameWeek");

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "ProgramFrameYear",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Week",
                table: "ProgramFrameWeek",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "ProgramFrameWeek",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "ProgramFrameBroadcast",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
    }
}
