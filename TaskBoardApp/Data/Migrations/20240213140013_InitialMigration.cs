using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Board identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "The name of the board")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Task Identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "The title of the task"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "The description of the task"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "The date time when the task has been created"),
                    BoardId = table.Column<int>(type: "int", nullable: false, comment: "Reference to the Board of which the Task is part of"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "The identifier of the user who has created the task")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Represents a Task");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99", 0, "10f45110-485f-4125-8fdc-8cc7cd90ad47", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEOBR3ARieGpGIC9fWphqWsibt7n0Xvtp08BII3yRi4x1NpD8vhZ/IlrEUeE9N/1SXw==", null, false, "57cdaa15-b2a8-4b15-ac5e-e0f8c614efa9", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 28, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1794), "Implement better styling for all public pages", "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 13, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1829), "Create Android client app for the TaskBoard RESTful API", "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 13, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1832), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 13, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1834), "Implement [Create Task] page for adding new tasks", "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99", "Create Tasks" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99");
        }
    }
}
