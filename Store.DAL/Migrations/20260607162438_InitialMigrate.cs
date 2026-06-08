using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    ImgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpriryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Computer Peripherals", null },
                    { 2, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Office Furniture & Setup", null },
                    { 3, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smart Home & Lifestyle", null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Count", "CreatedAt", "Description", "ExpriryDate", "ImgUrl", "Price", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, 1, 12, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wireless RGB with tactile switches.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 85.00m, "Mechanical Keyboard", null },
                    { 2, 2, 5, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-back mesh chair with lumbar support.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 210.50m, "Ergonomic Office Chair", null },
                    { 3, 3, 20, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Self-watering kit with LED grow lights for mint and basil.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 45.99m, "Smart Indoor Herb Garden", null },
                    { 4, 1, 8, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "High-speed external storage for developers.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 115.00m, "Portable SSD 1TB", null },
                    { 5, 2, 15, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Heavy-duty aluminum arm for two 27-inch screens.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 65.25m, "Dual Monitor Stand", null },
                    { 6, 1, 40, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "7-in-1 connectivity for modern laptops.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 35.00m, "USB-C Hub Adapter", null },
                    { 7, 1, 25, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Reduces wrist strain during long coding sessions.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 29.99m, "Vertical Mouse", null },
                    { 8, 1, 10, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active noise cancellation for focused work.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 199.00m, "Noise Cancelling Headphones", null },
                    { 9, 2, 50, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Large felt mat for mouse and keyboard stability.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 18.50m, "Desk Pad Protector", null },
                    { 10, 1, 6, new DateTime(2026, 5, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wide-angle lens with built-in dual microphones.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 89.90m, "Webcam 4K Ultra HD", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
