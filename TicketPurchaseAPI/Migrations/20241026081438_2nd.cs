using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketPurchaseAPI.Migrations
{
    /// <inheritdoc />
    public partial class _2nd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "547aa55f-4da3-4034-a649-29f3d78857ed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e486a043-0ef8-4bcd-9de5-59e60fc13f3b");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "547aa55f-4da3-4034-a649-29f3d78857ed", null, "User", "USER" },
                    { "e486a043-0ef8-4bcd-9de5-59e60fc13f3b", null, "Admin", "ADMIN" }
                });
        }
    }
}
