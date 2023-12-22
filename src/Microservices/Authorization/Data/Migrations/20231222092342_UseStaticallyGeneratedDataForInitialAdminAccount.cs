using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InnowiseClinic.Microservices.Authorization.Data.Migrations
{
    /// <inheritdoc />
    public partial class UseStaticallyGeneratedDataForInitialAdminAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("a8275ec5-2367-49fe-b824-a753e2ad3302"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "CreatedByEmail", "Email", "IsEmailVerified", "Password", "Role", "UpdatedAt", "UpdatedByEmail" },
                values: new object[] { new Guid("71992c68-c246-49d5-a22f-84fae24cba89"), new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@mydomain.com", true, "AQAAAAIAAYagAAAAEAsfZPi+5Ij52HHpQMwi5cOWHuShAyGhZ/QG34onfHAjDUuYYZWM94ARK2EM1LRgwQ==", "receptionist", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: new Guid("71992c68-c246-49d5-a22f-84fae24cba89"));

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "CreatedAt", "CreatedByEmail", "Email", "IsEmailVerified", "Password", "Role", "UpdatedAt", "UpdatedByEmail" },
                values: new object[] { new Guid("a8275ec5-2367-49fe-b824-a753e2ad3302"), new DateTime(2023, 12, 22, 8, 36, 31, 129, DateTimeKind.Utc).AddTicks(5290), null, "admin@mydomain.com", true, "AQAAAAIAAYagAAAAEOuknF0esqDT2vsfd0OkImuBWb6Yidjhzgheno+c6P23+IYeyPBF2C5Ml9bj4Sj0Yg==", "receptionist", null, null });
        }
    }
}
