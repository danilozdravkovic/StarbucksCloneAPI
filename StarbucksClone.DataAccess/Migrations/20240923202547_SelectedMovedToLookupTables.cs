using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarbucksClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SelectedMovedToLookupTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selected",
                table: "AddIns");

            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "ProductsIncludedAddIns",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "ProductsCustomAddIns",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selected",
                table: "ProductsIncludedAddIns");

            migrationBuilder.DropColumn(
                name: "Selected",
                table: "ProductsCustomAddIns");

            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "AddIns",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
