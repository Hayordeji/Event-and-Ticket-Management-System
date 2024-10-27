using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketPurchaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class TicketPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4aa9565b-9394-4849-a34c-5831ffbe4ee9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59e590b6-caf1-443c-b370-ab163344ab4f");

            migrationBuilder.CreateTable(
                name: "TicketPayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPayments", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "10bea13d-5764-44b0-8b41-b9524c9d100c", null, "Admin", "ADMIN" },
                    { "c6fe1398-71fb-45ff-944d-b2c58c19ff68", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketPayments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "10bea13d-5764-44b0-8b41-b9524c9d100c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6fe1398-71fb-45ff-944d-b2c58c19ff68");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4aa9565b-9394-4849-a34c-5831ffbe4ee9", null, "User", "USER" },
                    { "59e590b6-caf1-443c-b370-ab163344ab4f", null, "Admin", "ADMIN" }
                });
        }
    }
}
