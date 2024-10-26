using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketPurchaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class _3rd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_HostId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_HostId",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98423e99-86c1-4bdc-aee7-de6fb6d298e9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a169154f-8cbd-4d7a-8aa2-06ca7c9b02e3");

            migrationBuilder.DropColumn(
                name: "HostId",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "BoughtBy",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Host",
                table: "Events",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "78648f25-65e3-4e05-940e-6f8a3ef49538", null, "Admin", "ADMIN" },
                    { "930dcdfa-8174-4102-a792-348241f565b8", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "78648f25-65e3-4e05-940e-6f8a3ef49538");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "930dcdfa-8174-4102-a792-348241f565b8");

            migrationBuilder.DropColumn(
                name: "BoughtBy",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Host",
                table: "Events");

            migrationBuilder.AddColumn<string>(
                name: "HostId",
                table: "Events",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "98423e99-86c1-4bdc-aee7-de6fb6d298e9", null, "User", "USER" },
                    { "a169154f-8cbd-4d7a-8aa2-06ca7c9b02e3", null, "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_HostId",
                table: "Events",
                column: "HostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_HostId",
                table: "Events",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
