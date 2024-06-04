using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarbucksClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CartLinesAddInsCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Pump",
                table: "ProductsIncludedAddIns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Pump",
                table: "ProductsCustomAddIns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "OrderLinesAddIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderLineId = table.Column<int>(type: "int", nullable: false),
                    AddIn = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Pump = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLinesAddIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLinesAddIns_OrderLines_OrderLineId",
                        column: x => x.OrderLineId,
                        principalTable: "OrderLines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderLinesAddIns_OrderLineId",
                table: "OrderLinesAddIns",
                column: "OrderLineId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderLinesAddIns");

            migrationBuilder.AlterColumn<int>(
                name: "Pump",
                table: "ProductsIncludedAddIns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Pump",
                table: "ProductsCustomAddIns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
