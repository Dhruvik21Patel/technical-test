using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductManagement.Entities.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 29, 17, 24, 39, 214, DateTimeKind.Utc).AddTicks(554), new DateTime(2025, 12, 29, 17, 24, 39, 214, DateTimeKind.Utc).AddTicks(559) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 12, 29, 9, 45, 48, 151, DateTimeKind.Utc).AddTicks(4761), new DateTime(2025, 12, 29, 9, 45, 48, 151, DateTimeKind.Utc).AddTicks(4769) });
        }
    }
}
