using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVStation.Migrations
{
    /// <inheritdoc />
    public partial class Expand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "37d05792-ad9c-4e5f-a2b4-04f1365a3b0f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f401eb9-11ec-4596-90bd-33b3779a6ec5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9572f54d-d4c3-4dfd-90cb-dd7694c8265d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "af3d99df-b67c-4021-9fb6-60ef36be6d20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1d278ce-14a1-4de3-b11a-3fe281f2e56f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ef6c65b3-fe47-4013-8f1f-5cbeca1c4ec3");

            migrationBuilder.DropColumn(
                name: "BackgroundColor",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "BorderColor",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Channel");

            migrationBuilder.DropColumn(
                name: "DragBackgroundColor",
                table: "Channel");

            migrationBuilder.AddColumn<float>(
                name: "Duration",
                table: "Programme",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "EpisodeCount",
                table: "Programme",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Programme",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1f988cff-ceec-4d63-be0c-ec702ba2f633", "e80a02be-1a1c-47fd-a400-8057c4ffbc22", "SCREENWRITER", "SCREENWRITER" },
                    { "26b8d4ff-e4d5-494b-bcb1-00bb1564470c", "5a61d634-46bf-4f72-b397-992a945fd220", "MANAGER", "MANAGER" },
                    { "7c6fa34c-288b-4b98-befb-76593a9f04a7", "9f7ed717-4a7e-49a9-8cea-fea582d7a864", "REPORTER", "REPORTER" },
                    { "bb125071-5113-4a72-923b-5de644366b0b", "7f441a08-f815-46e1-b348-8ff0159a46db", "VIDEOEDITOR", "VIDEOEDITOR" },
                    { "bd9d1a62-7c4d-43be-b056-b15f434c126f", "d4201906-634b-4c74-8f6e-d9a0855bd4b3", "ADMIN", "ADMIN" },
                    { "e7ed1537-fc70-44ab-a570-c9f334d987ee", "3d96b739-e31e-4321-bcdb-9478ee60eddf", "DIRECTOR", "DIRECTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f988cff-ceec-4d63-be0c-ec702ba2f633");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "26b8d4ff-e4d5-494b-bcb1-00bb1564470c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c6fa34c-288b-4b98-befb-76593a9f04a7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bb125071-5113-4a72-923b-5de644366b0b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bd9d1a62-7c4d-43be-b056-b15f434c126f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e7ed1537-fc70-44ab-a570-c9f334d987ee");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Programme");

            migrationBuilder.DropColumn(
                name: "EpisodeCount",
                table: "Programme");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Programme");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundColor",
                table: "Channel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BorderColor",
                table: "Channel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Channel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DragBackgroundColor",
                table: "Channel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "37d05792-ad9c-4e5f-a2b4-04f1365a3b0f", "5151ce29-5e07-4bb2-9904-9724aa5c3dbb", "DIRECTOR", "DIRECTOR" },
                    { "4f401eb9-11ec-4596-90bd-33b3779a6ec5", "1ec6412f-fb4c-4a75-9a65-59efb2d0a3e9", "ADMIN", "ADMIN" },
                    { "9572f54d-d4c3-4dfd-90cb-dd7694c8265d", "441ae9c2-d867-489c-b68b-4d6ed8372e05", "REPORTER", "REPORTER" },
                    { "af3d99df-b67c-4021-9fb6-60ef36be6d20", "70054d8e-6900-43c6-9155-68f9ca4f9f8c", "SCREENWRITER", "SCREENWRITER" },
                    { "d1d278ce-14a1-4de3-b11a-3fe281f2e56f", "e22bb0f2-731a-44b2-b4af-0d07854844ad", "MANAGER", "MANAGER" },
                    { "ef6c65b3-fe47-4013-8f1f-5cbeca1c4ec3", "28a8a67b-9ad4-44f5-a27b-08858caa8da9", "VIDEOEDITOR", "VIDEOEDITOR" }
                });
        }
    }
}
