using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnowiseClinic.Microservices.Authorization.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialAdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "CreatedByEmail", "Email", "IsEmailVerified", "Password", "Role", "UpdatedAt", "UpdatedByEmail" },
                values: new object[] { new Guid("7156e4cc-afa3-4fc7-a0fd-44070c27dfee"), new DateTime(2023, 12, 22, 8, 29, 35, 790, DateTimeKind.Utc).AddTicks(1544), null, "admin@mydomain.com", true, "12345678", "receptionist", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("7156e4cc-afa3-4fc7-a0fd-44070c27dfee"));
        }
    }
}
