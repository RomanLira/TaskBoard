using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskBoard.Migrations
{
    /// <inheritdoc />
    public partial class update1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "01eb55b7-4828-41e6-9130-7e308f64f152");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "09fbfda8-f74e-4e0b-9f9c-02e803d955c2");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "2c1e7148-85bd-4c71-9edf-cfb972ee0903");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "9fe07867-e47b-411c-a61d-c1e113df3795");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Files = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sprints_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SprintUser",
                columns: table => new
                {
                    SprintsId = table.Column<string>(type: "text", nullable: false),
                    UsersId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintUser", x => new { x.SprintsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_SprintUser_Sprints_SprintsId",
                        column: x => x.SprintsId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SprintUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Files = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    SprintId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Sprints_SprintId",
                        column: x => x.SprintId,
                        principalTable: "Sprints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { "e37c4ec8-e5e6-44f1-9bd0-e828fe9b806d", "Description of First Project. Have a nice day!", "First Project" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password", "Role" },
                values: new object[,]
                {
                    { "0ab8f3b4-3647-4f03-af59-9d95c65b9c71", "manager", "2", 1 },
                    { "6c67f22b-6731-4927-a57c-6297d9b1a892", "user1", "3", 2 },
                    { "ba3fedc2-c08e-4222-b51c-c3f2e911df8f", "user2", "3", 2 },
                    { "ee0cd788-cb4a-44b0-bfc2-f693d37c5dcf", "admin", "1", 0 }
                });

            migrationBuilder.InsertData(
                table: "Sprints",
                columns: new[] { "Id", "Comment", "Description", "EndDate", "Files", "Name", "ProjectId", "StartDate" },
                values: new object[,]
                {
                    { "15888b6d-df67-4814-a464-950f86f75fde", "Second Comment", "Description of Second Sprint", new DateTime(2024, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Second Sprint", "e37c4ec8-e5e6-44f1-9bd0-e828fe9b806d", new DateTime(2024, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { "27b42fa3-ab04-4772-a2a8-d881c7185524", "First Comment", "Description of First Sprint", new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "First Sprint", "e37c4ec8-e5e6-44f1-9bd0-e828fe9b806d", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Comment", "Description", "Files", "Name", "SprintId", "Status", "UserId" },
                values: new object[,]
                {
                    { "38c96dee-3d08-4d89-b4d7-df3f70b3c922", "First Comment", "Description of First Task", null, "First Task", "27b42fa3-ab04-4772-a2a8-d881c7185524", 0, "6c67f22b-6731-4927-a57c-6297d9b1a892" },
                    { "9e5dfd49-4dbf-47d7-9e19-6d7f67017e83", "Fourth Comment", "Description of Fourth Task", null, "Fourth Task", "15888b6d-df67-4814-a464-950f86f75fde", 0, "ba3fedc2-c08e-4222-b51c-c3f2e911df8f" },
                    { "a7dcfe99-e1f2-4900-a759-b740a9554e50", "Third Comment", "Description of Third Task", null, "Third Task", "15888b6d-df67-4814-a464-950f86f75fde", 0, "6c67f22b-6731-4927-a57c-6297d9b1a892" },
                    { "b46492d2-5b1a-4f4d-9c29-771e3cbd1e0c", "Second Comment", "Description of Second Task", null, "Second Task", "27b42fa3-ab04-4772-a2a8-d881c7185524", 0, "ba3fedc2-c08e-4222-b51c-c3f2e911df8f" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_ProjectId",
                table: "Sprints",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SprintUser_UsersId",
                table: "SprintUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SprintId",
                table: "Tasks",
                column: "SprintId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SprintUser");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "0ab8f3b4-3647-4f03-af59-9d95c65b9c71");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "6c67f22b-6731-4927-a57c-6297d9b1a892");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ba3fedc2-c08e-4222-b51c-c3f2e911df8f");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ee0cd788-cb4a-44b0-bfc2-f693d37c5dcf");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Login", "Password", "Role" },
                values: new object[,]
                {
                    { "01eb55b7-4828-41e6-9130-7e308f64f152", "user2", "3", 2 },
                    { "09fbfda8-f74e-4e0b-9f9c-02e803d955c2", "user1", "3", 2 },
                    { "2c1e7148-85bd-4c71-9edf-cfb972ee0903", "manager", "2", 1 },
                    { "9fe07867-e47b-411c-a61d-c1e113df3795", "admin", "1", 0 }
                });
        }
    }
}
