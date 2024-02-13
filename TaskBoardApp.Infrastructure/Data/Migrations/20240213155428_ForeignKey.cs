using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskBoardApp.Data.Migrations
{
    public partial class ForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3d1ee843-2ab7-4f18-a5b9-b775be1d6ab3", 0, "5bc8d407-06ba-45b6-938a-b3abc4d91e9c", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEGlHSU0CRJROCgy6N1aTqndceMd4J/j6ou9KZVURkmhz4SV4DDG5bkEquEMbpNbQqA==", null, false, "da8909a1-26bc-4f0f-a266-6ba3a3caa00e", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 7, 28, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1794), "Implement better styling for all public pages", "3d1ee843-2ab7-4f18-a5b9-b775be1d6ab3", "Improve CSS styles" },
                    { 2, 1, new DateTime(2023, 9, 13, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1829), "Create Android client app for the TaskBoard RESTful API", "3d1ee843-2ab7-4f18-a5b9-b775be1d6ab3", "Android Client App" },
                    { 3, 2, new DateTime(2024, 1, 13, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1832), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "3d1ee843-2ab7-4f18-a5b9-b775be1d6ab3", "Desktop Client App" },
                    { 4, 3, new DateTime(2023, 2, 13, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1834), "Implement [Create Task] page for adding new tasks", "3d1ee843-2ab7-4f18-a5b9-b775be1d6ab3", "Create Tasks" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d1ee843-2ab7-4f18-a5b9-b775be1d6ab3");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99", 0, "10f45110-485f-4125-8fdc-8cc7cd90ad47", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAEAACcQAAAAEOBR3ARieGpGIC9fWphqWsibt7n0Xvtp08BII3yRi4x1NpD8vhZ/IlrEUeE9N/1SXw==", null, false, "57cdaa15-b2a8-4b15-ac5e-e0f8c614efa9", false, "test@softuni.bg" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 7, 28, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1794), "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 9, 13, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1829), "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2024, 1, 13, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1832), "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedOn", "OwnerId" },
                values: new object[] { new DateTime(2023, 2, 13, 16, 0, 13, 552, DateTimeKind.Local).AddTicks(1834), "bc965a3c-bfc0-40ba-a6ad-0bb76e73bb99" });
        }
    }
}
