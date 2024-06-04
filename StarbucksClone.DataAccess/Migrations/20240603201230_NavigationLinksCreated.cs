using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarbucksClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NavigationLinksCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LinkPositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkPositions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NavigationLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LinkHref = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    LinkPositionId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavigationLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NavigationLinks_LinkPositions_LinkPositionId",
                        column: x => x.LinkPositionId,
                        principalTable: "LinkPositions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NavigationLinks_NavigationLinks_ParentId",
                        column: x => x.ParentId,
                        principalTable: "NavigationLinks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LinkPositions_Name",
                table: "LinkPositions",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NavigationLinks_LinkPositionId",
                table: "NavigationLinks",
                column: "LinkPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_NavigationLinks_Name",
                table: "NavigationLinks",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NavigationLinks_ParentId",
                table: "NavigationLinks",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NavigationLinks");

            migrationBuilder.DropTable(
                name: "LinkPositions");
        }
    }
}
