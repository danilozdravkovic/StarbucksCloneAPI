using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarbucksClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IncludedAndCustomAddInsRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsCustomAddIns");

            migrationBuilder.DropTable(
                name: "ProductsIncludedAddIns");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductsCustomAddIns",
                columns: table => new
                {
                    AddInId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Pump = table.Column<int>(type: "int", nullable: true),
                    SelectedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsCustomAddIns", x => new { x.AddInId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductsCustomAddIns_AddIns_AddInId",
                        column: x => x.AddInId,
                        principalTable: "AddIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsCustomAddIns_AddIns_SelectedId",
                        column: x => x.SelectedId,
                        principalTable: "AddIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductsCustomAddIns_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductsIncludedAddIns",
                columns: table => new
                {
                    AddInId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Pump = table.Column<int>(type: "int", nullable: true),
                    SelectedId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsIncludedAddIns", x => new { x.AddInId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductsIncludedAddIns_AddIns_AddInId",
                        column: x => x.AddInId,
                        principalTable: "AddIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsIncludedAddIns_AddIns_SelectedId",
                        column: x => x.SelectedId,
                        principalTable: "AddIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductsIncludedAddIns_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCustomAddIns_ProductId",
                table: "ProductsCustomAddIns",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCustomAddIns_SelectedId",
                table: "ProductsCustomAddIns",
                column: "SelectedId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsIncludedAddIns_ProductId",
                table: "ProductsIncludedAddIns",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsIncludedAddIns_SelectedId",
                table: "ProductsIncludedAddIns",
                column: "SelectedId");
        }
    }
}
