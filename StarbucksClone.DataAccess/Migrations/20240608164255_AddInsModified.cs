using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarbucksClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddInsModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddIn_AddIn_ParentId",
                table: "AddIn");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCustomAddIns_AddIn_AddInId",
                table: "ProductsCustomAddIns");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsIncludedAddIns_AddIn_AddInId",
                table: "ProductsIncludedAddIns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddIn",
                table: "AddIn");

            migrationBuilder.RenameTable(
                name: "AddIn",
                newName: "AddIns");

            migrationBuilder.RenameIndex(
                name: "IX_AddIn_ParentId",
                table: "AddIns",
                newName: "IX_AddIns_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddIns",
                table: "AddIns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddIns_AddIns_ParentId",
                table: "AddIns",
                column: "ParentId",
                principalTable: "AddIns",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCustomAddIns_AddIns_AddInId",
                table: "ProductsCustomAddIns",
                column: "AddInId",
                principalTable: "AddIns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsIncludedAddIns_AddIns_AddInId",
                table: "ProductsIncludedAddIns",
                column: "AddInId",
                principalTable: "AddIns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AddIns_AddIns_ParentId",
                table: "AddIns");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsCustomAddIns_AddIns_AddInId",
                table: "ProductsCustomAddIns");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductsIncludedAddIns_AddIns_AddInId",
                table: "ProductsIncludedAddIns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AddIns",
                table: "AddIns");

            migrationBuilder.RenameTable(
                name: "AddIns",
                newName: "AddIn");

            migrationBuilder.RenameIndex(
                name: "IX_AddIns_ParentId",
                table: "AddIn",
                newName: "IX_AddIn_ParentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AddIn",
                table: "AddIn",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AddIn_AddIn_ParentId",
                table: "AddIn",
                column: "ParentId",
                principalTable: "AddIn",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsCustomAddIns_AddIn_AddInId",
                table: "ProductsCustomAddIns",
                column: "AddInId",
                principalTable: "AddIn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductsIncludedAddIns_AddIn_AddInId",
                table: "ProductsIncludedAddIns",
                column: "AddInId",
                principalTable: "AddIn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
