using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSapi.Migrations
{
    /// <inheritdoc />
    public partial class updatePO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryID",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_PODetails_PoId",
                table: "PODetails",
                column: "PoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PODetails_PurchaseOrders_PoId",
                table: "PODetails",
                column: "PoId",
                principalTable: "PurchaseOrders",
                principalColumn: "PoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PODetails_PurchaseOrders_PoId",
                table: "PODetails");

            migrationBuilder.DropIndex(
                name: "IX_PODetails_PoId",
                table: "PODetails");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryID",
                table: "Products",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
