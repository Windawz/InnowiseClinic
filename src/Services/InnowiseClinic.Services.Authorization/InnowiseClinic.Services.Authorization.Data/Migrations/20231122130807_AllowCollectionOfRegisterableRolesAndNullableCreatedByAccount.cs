using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnowiseClinic.Services.Authorization.Data.Migrations
{
    public partial class AllowCollectionOfRegisterableRolesAndNullableCreatedByAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_CreatedById",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CanRegisterOthers",
                table: "Roles");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Accounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_RoleId",
                table: "Roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById",
                unique: true,
                filter: "[CreatedById] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Roles_RoleId",
                table: "Roles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_CreatedById",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Roles_RoleId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_RoleId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Roles");

            migrationBuilder.AddColumn<bool>(
                name: "CanRegisterOthers",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "CreatedById",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
