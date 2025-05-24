using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVStation.Migrations
{
    /// <inheritdoc />
    public partial class Expand2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "043db6a0-815f-415b-a85e-19e22c73f6cd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16e9589a-2162-493e-9d9b-3d26408acebc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c6b51c6-e89b-40be-af70-45d0890a27b6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc980900-2916-418a-9eed-b89fa7704750");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eceff517-0d8e-48d8-9609-e6b3bb4bccc5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1dfc0a2-c3ed-415e-8007-d03454e21c62");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Programme");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Programme");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "StartTime",
                table: "Programme",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Programme",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duration",
                table: "Programme",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Programme");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartTime",
                table: "Programme",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "Programme",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Programme",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Programme",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "043db6a0-815f-415b-a85e-19e22c73f6cd", "9ec726bf-6ec1-4c1c-8ca7-7522e1140cba", "ADMIN", "ADMIN" },
                    { "16e9589a-2162-493e-9d9b-3d26408acebc", "c12d3e4f-279b-4f5d-b7fa-35d7e9602ce2", "REPORTER", "REPORTER" },
                    { "6c6b51c6-e89b-40be-af70-45d0890a27b6", "63559965-f8e1-4a7c-8558-4cd3cddad5d8", "DIRECTOR", "DIRECTOR" },
                    { "dc980900-2916-418a-9eed-b89fa7704750", "7b5ab685-383c-4286-ab8f-02baa0017858", "MANAGER", "MANAGER" },
                    { "eceff517-0d8e-48d8-9609-e6b3bb4bccc5", "cb18ae89-404e-4fcd-994f-ebcc387a836e", "VIDEOEDITOR", "VIDEOEDITOR" },
                    { "f1dfc0a2-c3ed-415e-8007-d03454e21c62", "96a7f603-7f16-4b10-ac7a-fe5966b299fa", "SCREENWRITER", "SCREENWRITER" }
                });
        }
    }
}
