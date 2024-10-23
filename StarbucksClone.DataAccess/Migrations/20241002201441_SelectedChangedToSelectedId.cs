using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarbucksClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SelectedChangedToSelectedId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Selected",
                table: "ProductsIncludedAddIns");

            migrationBuilder.DropColumn(
                name: "Selected",
                table: "ProductsCustomAddIns");

            migrationBuilder.AddColumn<int>(
                name: "SelectedId",
                table: "ProductsIncludedAddIns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SelectedId",
                table: "ProductsCustomAddIns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductsIncludedAddIns_SelectedId",
                table: "ProductsIncludedAddIns",
                column: "SelectedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCustomAddIns_SelectedId",
                table: "ProductsCustomAddIns",
                column: "SelectedId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCustomAddIns_AddIns_SelectedId",
                table: "ProductsCustomAddIns",
                column: "SelectedId",
                principalTable: "AddIns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsIncludedAddIns_AddIns_SelectedId",
                table: "ProductsIncludedAddIns",
                column: "SelectedId",
                principalTable: "AddIns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCustomAddIns_AddIns_SelectedId",
                table: "ProductsCustomAddIns");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsIncludedAddIns_AddIns_SelectedId",
                table: "ProductsIncludedAddIns");

            migrationBuilder.DropIndex(
                name: "IX_ProductsIncludedAddIns_SelectedId",
                table: "ProductsIncludedAddIns");

            migrationBuilder.DropIndex(
                name: "IX_ProductsCustomAddIns_SelectedId",
                table: "ProductsCustomAddIns");

            migrationBuilder.DropColumn(
                name: "SelectedId",
                table: "ProductsIncludedAddIns");

            migrationBuilder.DropColumn(
                name: "SelectedId",
                table: "ProductsCustomAddIns");

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
    }
}
