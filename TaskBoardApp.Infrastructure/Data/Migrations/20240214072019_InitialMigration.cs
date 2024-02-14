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
                values: new object[] { "ef6eb4c1-4539-4176-9952-0164a6d464d6", 0, "351cbd01-2cf5-4823-b5fa-e56fed5397b0", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEEXYMuLbqZfUKQuuo7kk8ufS4WnWtIssebZpS2L9NPVA064w/W4gTutc2Ru6NE4R7Q==", null, false, "f97bb193-bfc6-449f-b43d-9eef9e183286", false, "test@softuni.bg" });

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
                    { 1, 1, new DateTime(2023, 7, 29, 9, 20, 19, 643, DateTimeKind.Local).AddTicks(1036), "Implement better styling for all public pages", "ef6eb4c1-4539-4176-9952-0164a6d464d6", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 14, 9, 20, 19, 643, DateTimeKind.Local).AddTicks(1065), "Create Android client app for the TaskBoard RESTful API", "ef6eb4c1-4539-4176-9952-0164a6d464d6", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 14, 9, 20, 19, 643, DateTimeKind.Local).AddTicks(1068), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "ef6eb4c1-4539-4176-9952-0164a6d464d6", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 14, 9, 20, 19, 643, DateTimeKind.Local).AddTicks(1071), "Implement [Create Task] page for adding new tasks", "ef6eb4c1-4539-4176-9952-0164a6d464d6", "Create Tasks" }
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
                keyValue: "ef6eb4c1-4539-4176-9952-0164a6d464d6");
        }
    }
}
