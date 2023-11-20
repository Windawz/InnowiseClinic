using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnowiseClinic.Services.Authorization.Data.Migrations
{
    public partial class TrimDownDataAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UpdatedById",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UpdatedById",
                table: "Accounts",
                column: "UpdatedById",
                unique: true,
                filter: "[UpdatedById] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_UpdatedById",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_UpdatedById",
                table: "Accounts",
                column: "UpdatedById");
        }
    }
}
