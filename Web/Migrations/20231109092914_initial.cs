using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskBoard.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
