using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TVStation.Migrations
{
    /// <inheritdoc />
    public partial class AddEventCollaborators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Event_EventId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_CreatorId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Channel_ChanelId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EventId",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0b14f78d-8ca7-4c60-8995-cfe311cd0db3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72208770-a158-47ac-8c2a-a66a356dd9b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7a7a8ea3-7e3f-49d7-8520-a8992d32bb18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7dc5a250-3f1e-4c01-ba93-c23b47a12cb3");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ChanelId",
                table: "Event",
                newName: "ChannelId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ChanelId",
                table: "Event",
                newName: "IX_Event_ChannelId");

            migrationBuilder.CreateTable(
                name: "EventCollaborators",
                columns: table => new
                {
                    CollaboratingEventsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CollaboratorsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCollaborators", x => new { x.CollaboratingEventsId, x.CollaboratorsId });
                    table.ForeignKey(
                        name: "FK_EventCollaborators_AspNetUsers_CollaboratorsId",
                        column: x => x.CollaboratorsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventCollaborators_Event_CollaboratingEventsId",
                        column: x => x.CollaboratingEventsId,
                        principalTable: "Event",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "436e7f07-9046-4e7e-8cce-783c18a28a96", "3860f20c-b31e-4a87-b341-bf9900e94922", "ADMIN", "ADMIN" },
                    { "5017f36c-705a-4b9c-82f3-3b8e2c57b065", "b5be22bd-ed22-4e05-8e1e-7124e03906a8", "REPORTER", "REPORTER" },
                    { "61b850c6-628f-44e3-9cd9-d70d6dc1729e", "1f4eb418-4bc9-487c-8021-4e132bf2d8d9", "DIRECTOR", "DIRECTOR" },
                    { "aa155b68-5163-4f67-bb69-8543fee5561b", "ceb8c229-8150-4095-a26a-93eec612cd76", "MANAGER", "MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventCollaborators_CollaboratorsId",
                table: "EventCollaborators",
                column: "CollaboratorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_CreatorId",
                table: "Event",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Channel_ChannelId",
                table: "Event",
                column: "ChannelId",
                principalTable: "Channel",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_AspNetUsers_CreatorId",
                table: "Event");

            migrationBuilder.DropForeignKey(
                name: "FK_Event_Channel_ChannelId",
                table: "Event");

            migrationBuilder.DropTable(
                name: "EventCollaborators");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "436e7f07-9046-4e7e-8cce-783c18a28a96");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5017f36c-705a-4b9c-82f3-3b8e2c57b065");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "61b850c6-628f-44e3-9cd9-d70d6dc1729e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aa155b68-5163-4f67-bb69-8543fee5561b");

            migrationBuilder.RenameColumn(
                name: "ChannelId",
                table: "Event",
                newName: "ChanelId");

            migrationBuilder.RenameIndex(
                name: "IX_Event_ChannelId",
                table: "Event",
                newName: "IX_Event_ChanelId");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0b14f78d-8ca7-4c60-8995-cfe311cd0db3", "b1c49dff-8ebf-4f25-b818-84f93a353856", "DIRECTOR", "DIRECTOR" },
                    { "72208770-a158-47ac-8c2a-a66a356dd9b3", "cf9019b9-04c1-4b2a-82ef-3247a56c4d86", "ADMIN", "ADMIN" },
                    { "7a7a8ea3-7e3f-49d7-8520-a8992d32bb18", "cd97aa9a-07f9-47f1-a58d-1b7756a80d9c", "REPORTER", "REPORTER" },
                    { "7dc5a250-3f1e-4c01-ba93-c23b47a12cb3", "2dc82365-91cc-4b27-9c9b-7aba3679f682", "MANAGER", "MANAGER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EventId",
                table: "AspNetUsers",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Event_EventId",
                table: "AspNetUsers",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_AspNetUsers_CreatorId",
                table: "Event",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_Channel_ChanelId",
                table: "Event",
                column: "ChanelId",
                principalTable: "Channel",
                principalColumn: "Id");
        }
    }
}
