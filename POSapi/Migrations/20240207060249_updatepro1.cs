using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POSapi.Migrations
{
    /// <inheritdoc />
    public partial class updatepro1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "ProImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProImages",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "ProImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "UnitCost",
                table: "ProImages",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "UnitPrice",
                table: "ProImages",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "VendorID",
                table: "ProImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "isActive",
                table: "ProImages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "skuID",
                table: "ProImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "ProImages");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProImages");

            migrationBuilder.DropColumn(
                name: "Qty",
                table: "ProImages");

            migrationBuilder.DropColumn(
                name: "UnitCost",
                table: "ProImages");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "ProImages");

            migrationBuilder.DropColumn(
                name: "VendorID",
                table: "ProImages");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "ProImages");

            migrationBuilder.DropColumn(
                name: "skuID",
                table: "ProImages");
        }
    }
}
