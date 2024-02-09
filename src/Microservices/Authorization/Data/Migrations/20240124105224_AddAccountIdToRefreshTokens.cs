using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnowiseClinic.Microservices.Authorization.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountIdToRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "RefreshTokens",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AccountId",
                table: "RefreshTokens",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Accounts_AccountId",
                table: "RefreshTokens",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Accounts_AccountId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_AccountId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "RefreshTokens");
        }
    }
}
