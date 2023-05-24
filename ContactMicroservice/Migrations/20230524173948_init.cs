using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    CompanyUUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_People_Companies_CompanyUUID",
                        column: x => x.CompanyUUID,
                        principalTable: "Companies",
                        principalColumn: "UUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    PersonUUID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.UUID);
                    table.ForeignKey(
                        name: "FK_ContactInfos_People_PersonUUID",
                        column: x => x.PersonUUID,
                        principalTable: "People",
                        principalColumn: "UUID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfos_PersonUUID",
                table: "ContactInfos",
                column: "PersonUUID");

            migrationBuilder.CreateIndex(
                name: "IX_People_CompanyUUID",
                table: "People",
                column: "CompanyUUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactInfos");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
