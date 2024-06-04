using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarbucksClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddInsCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AddIn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddIn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddIn_AddIn_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AddIn",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductsCustomAddIns",
                columns: table => new
                {
                    AddInId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Pump = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsCustomAddIns", x => new { x.AddInId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductsCustomAddIns_AddIn_AddInId",
                        column: x => x.AddInId,
                        principalTable: "AddIn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    Pump = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsIncludedAddIns", x => new { x.AddInId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ProductsIncludedAddIns_AddIn_AddInId",
                        column: x => x.AddInId,
                        principalTable: "AddIn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductsIncludedAddIns_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddIn_ParentId",
                table: "AddIn",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCustomAddIns_ProductId",
                table: "ProductsCustomAddIns",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsIncludedAddIns_ProductId",
                table: "ProductsIncludedAddIns",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsCustomAddIns");

            migrationBuilder.DropTable(
                name: "ProductsIncludedAddIns");

            migrationBuilder.DropTable(
                name: "AddIn");
        }
    }
}
