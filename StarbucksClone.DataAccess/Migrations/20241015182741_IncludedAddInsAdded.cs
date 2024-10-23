using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarbucksClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IncludedAddInsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncludedProductAddIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddInId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Pump = table.Column<int>(type: "int", nullable: true),
                    SelectedId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncludedProductAddIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncludedProductAddIns_AddIns_AddInId",
                        column: x => x.AddInId,
                        principalTable: "AddIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IncludedProductAddIns_AddIns_SelectedId",
                        column: x => x.SelectedId,
                        principalTable: "AddIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IncludedProductAddIns_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncludedProductAddIns_AddInId",
                table: "IncludedProductAddIns",
                column: "AddInId");

            migrationBuilder.CreateIndex(
                name: "IX_IncludedProductAddIns_ProductId",
                table: "IncludedProductAddIns",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_IncludedProductAddIns_SelectedId",
                table: "IncludedProductAddIns",
                column: "SelectedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncludedProductAddIns");
        }
    }
}
