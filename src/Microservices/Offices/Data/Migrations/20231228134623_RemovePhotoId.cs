using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnowiseClinic.Microservices.Offices.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePhotoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Offices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PhotoId",
                table: "Offices",
                type: "uuid",
                nullable: true);
        }
    }
}
