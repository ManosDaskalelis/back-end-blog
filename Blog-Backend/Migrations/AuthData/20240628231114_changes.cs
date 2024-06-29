using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog_Backend.Migrations.AuthData
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dd3fa83-5ea9-4fbf-b6a1-a312dfe22ba6",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "62cc1973-bd49-46a6-9f8f-d291186b4464", "ADMIN@GMAIL.COM", "ADMIN@GMAIL.COM", "AQAAAAIAAYagAAAAED5gtJmHbWY5OQNZUpIyIGtYIQcVc9JTLFUWE7N0YoHClvcDv7/HtllO6ydaMYdO6w==", "65ce00bd-bd47-4f99-9ee9-76c3ac3a9401", "admin@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dd3fa83-5ea9-4fbf-b6a1-a312dfe22ba6",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "112e46af-16a5-4a3a-8cf2-c9cfa4e15865", "ADMIN", "ADMIN", "AQAAAAIAAYagAAAAEL0bbXd7TW3tWvl75eIusEujeGXKrS7nb0RD+lSNZZmZdXAteMPlmAHk50oEO05F3A==", "fd636085-37cc-4cec-ba4e-da9888fe5641", "admin" });
        }
    }
}
