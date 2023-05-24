using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactMicroservice.Migrations
{
    /// <inheritdoc />
    public partial class removeCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Companies_CompanyUUID",
                table: "People");

            migrationBuilder.DropIndex(
                name: "IX_People_CompanyUUID",
                table: "People");

            migrationBuilder.DropColumn(
                name: "CompanyUUID",
                table: "People");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyUUID",
                table: "People",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_People_CompanyUUID",
                table: "People",
                column: "CompanyUUID");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Companies_CompanyUUID",
                table: "People",
                column: "CompanyUUID",
                principalTable: "Companies",
                principalColumn: "UUID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
