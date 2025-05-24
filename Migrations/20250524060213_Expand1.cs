using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVStation.Migrations
{
    /// <inheritdoc />
    public partial class Expand1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.RenameColumn(
                name: "EpisodeCount",
                table: "Programme",
                newName: "EpisodeNumber");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Programme",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Programme");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Programme");

            migrationBuilder.RenameColumn(
                name: "EpisodeNumber",
                table: "Programme",
                newName: "EpisodeCount");

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
                    { "1f988cff-ceec-4d63-be0c-ec702ba2f633", "e80a02be-1a1c-47fd-a400-8057c4ffbc22", "SCREENWRITER", "SCREENWRITER" },
                    { "26b8d4ff-e4d5-494b-bcb1-00bb1564470c", "5a61d634-46bf-4f72-b397-992a945fd220", "MANAGER", "MANAGER" },
                    { "7c6fa34c-288b-4b98-befb-76593a9f04a7", "9f7ed717-4a7e-49a9-8cea-fea582d7a864", "REPORTER", "REPORTER" },
                    { "bb125071-5113-4a72-923b-5de644366b0b", "7f441a08-f815-46e1-b348-8ff0159a46db", "VIDEOEDITOR", "VIDEOEDITOR" },
                    { "bd9d1a62-7c4d-43be-b056-b15f434c126f", "d4201906-634b-4c74-8f6e-d9a0855bd4b3", "ADMIN", "ADMIN" },
                    { "e7ed1537-fc70-44ab-a570-c9f334d987ee", "3d96b739-e31e-4321-bcdb-9478ee60eddf", "DIRECTOR", "DIRECTOR" }
                });
        }
    }
}
