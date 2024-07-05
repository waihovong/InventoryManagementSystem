using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalInfo4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
            name: "AdditionalInfo",
            table: "Products",
            nullable: true);

            migrationBuilder.RenameColumn(
            name: "Name",
            table: "Products",
            newName: "ProductName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "AdditionalInfo",
            table: "Products");

            migrationBuilder.RenameColumn(
            name: "ProductName",
            table: "Products",
            newName: "Name");
        }
    }
}
