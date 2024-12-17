using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVStation.Migrations
{
    /// <inheritdoc />
    public partial class UpgradeYear : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "ProgramFrameYear",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "01d76b2f-db7e-4032-8aeb-4ebf6a2db61e", "e9bd8ae6-66a5-4cc1-99e4-c28693b8d652", "ADMIN", "ADMIN" },
                    { "0346cd48-ce89-48a1-903c-d51fb3a34107", "d3f2665f-7d51-40ab-82e4-8e8df8150ca4", "MANAGER", "MANAGER" },
                    { "0d81a3a1-9c12-4a4c-a8b9-df2caf6dc123", "e44811f0-9562-4e79-ba95-f3e9903271e0", "DIRECTOR", "DIRECTOR" },
                    { "2c998fca-e4f9-4210-b542-48a439d4350b", "7f567fa3-1a51-49a7-9e37-f149997d680e", "REPORTER", "REPORTER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "01d76b2f-db7e-4032-8aeb-4ebf6a2db61e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0346cd48-ce89-48a1-903c-d51fb3a34107");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0d81a3a1-9c12-4a4c-a8b9-df2caf6dc123");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c998fca-e4f9-4210-b542-48a439d4350b");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "ProgramFrameYear");

            migrationBuilder.AddColumn<DateTime>(
                name: "Airdate",
                table: "ProgramFrameYear",
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
    }
}
