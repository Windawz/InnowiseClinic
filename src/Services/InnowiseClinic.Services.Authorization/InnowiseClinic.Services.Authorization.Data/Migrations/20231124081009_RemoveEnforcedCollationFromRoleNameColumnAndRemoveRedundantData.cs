using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnowiseClinic.Services.Authorization.Data.Migrations
{
    public partial class RemoveEnforcedCollationFromRoleNameColumnAndRemoveRedundantData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Accounts");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldCollation: "Latin1_General_CI_AS");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: false,
                collation: "Latin1_General_CI_AS",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Accounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Accounts",
                type: "int",
                nullable: true);
        }
    }
}
