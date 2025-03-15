using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateMSSQL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 15, 1, 17, 54, 223, DateTimeKind.Utc).AddTicks(7189), "$2a$11$DABHD98o2ejyQnILnxMX.OEFenjnmlR0I1qTk1wei7AbZfkIFlksG" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "PasswordHash" },
                values: new object[] { new DateTime(2025, 3, 14, 23, 48, 10, 68, DateTimeKind.Utc).AddTicks(5126), "$2a$11$fC9rYjAs8y2sePZtlHXMBOuhvgccKAVGECJIj0NWb6dT/vwPdw25O" });
        }
    }
}
