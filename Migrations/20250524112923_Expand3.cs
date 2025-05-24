using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVStation.Migrations
{
    /// <inheritdoc />
    public partial class Expand3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0fab86f2-fd26-41d0-b528-86e3e91e666d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13c67d19-11c2-48a9-8991-2a5a34f1ebf9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7916f41b-8435-47d1-8c68-b5e1088fbd48");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7bd0d8d9-1bc5-4cc4-affa-0c721549ef3b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8506600b-d901-4f88-90cf-e0a0a2192e5f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e8ae0640-0c58-4803-84f0-4d95604aa5d0");

            migrationBuilder.AddColumn<float>(
                name: "Duration",
                table: "Programme",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08fd045c-3a82-4927-b7f7-8805514be98f", "01a6d727-56c5-4790-b46f-102abbc4dca1", "REPORTER", "REPORTER" },
                    { "21a677e8-9947-46ea-99f1-381ad73d8e7a", "e680841d-9cdf-453e-8666-38c822bfe2bb", "DIRECTOR", "DIRECTOR" },
                    { "28e116a2-a7d2-4389-b64f-fde6be3c8024", "ac5d0163-2174-41e2-8f97-554838b68fc7", "VIDEOEDITOR", "VIDEOEDITOR" },
                    { "538480c1-47fc-4c26-b4fd-c437fb711954", "883a1a7d-0060-4ccb-9e0a-a85ea649d535", "SCREENWRITER", "SCREENWRITER" },
                    { "7b57e19b-2797-4de7-bc41-49aa17d16243", "916db8e0-8d41-434d-891b-4a197e134b9e", "MANAGER", "MANAGER" },
                    { "b5af321f-63e1-4e0c-8ad6-f0086b10e264", "2d3b27d8-5f21-46e4-bbf5-556200192581", "ADMIN", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08fd045c-3a82-4927-b7f7-8805514be98f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21a677e8-9947-46ea-99f1-381ad73d8e7a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28e116a2-a7d2-4389-b64f-fde6be3c8024");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "538480c1-47fc-4c26-b4fd-c437fb711954");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7b57e19b-2797-4de7-bc41-49aa17d16243");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b5af321f-63e1-4e0c-8ad6-f0086b10e264");

            migrationBuilder.AddColumn<float>(
                name: "Duration",
                table: "Programme",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0fab86f2-fd26-41d0-b528-86e3e91e666d", "253375d9-4455-49f0-8d90-85dadb9cfb3d", "DIRECTOR", "DIRECTOR" },
                    { "13c67d19-11c2-48a9-8991-2a5a34f1ebf9", "b714c1dd-c543-42ed-815f-f042ed5bd3ca", "ADMIN", "ADMIN" },
                    { "7916f41b-8435-47d1-8c68-b5e1088fbd48", "a74a8f94-299d-4b7a-958a-213017cb3874", "MANAGER", "MANAGER" },
                    { "7bd0d8d9-1bc5-4cc4-affa-0c721549ef3b", "86840765-8dad-4826-84fc-4d7412e08671", "SCREENWRITER", "SCREENWRITER" },
                    { "8506600b-d901-4f88-90cf-e0a0a2192e5f", "f36df1ce-a76a-43f1-aefc-5c0076c07017", "REPORTER", "REPORTER" },
                    { "e8ae0640-0c58-4803-84f0-4d95604aa5d0", "4f54f19f-a421-4087-9292-950d99cb09d7", "VIDEOEDITOR", "VIDEOEDITOR" }
                });
        }
    }
}
