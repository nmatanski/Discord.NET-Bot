using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DAL.Database.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DateChanged = table.Column<DateTime>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    ImgUrl = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserRole = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    ValidationCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
