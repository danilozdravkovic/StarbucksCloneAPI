using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarbucksClone.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationshipBetweenCartLineAddInsAndAddIns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddIn",
                table: "CartLinesAddIns");

            migrationBuilder.AddColumn<int>(
                name: "AddInId",
                table: "CartLinesAddIns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartLinesAddIns_AddInId",
                table: "CartLinesAddIns",
                column: "AddInId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartLinesAddIns_AddIns_AddInId",
                table: "CartLinesAddIns",
                column: "AddInId",
                principalTable: "AddIns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartLinesAddIns_AddIns_AddInId",
                table: "CartLinesAddIns");

            migrationBuilder.DropIndex(
                name: "IX_CartLinesAddIns_AddInId",
                table: "CartLinesAddIns");

            migrationBuilder.DropColumn(
                name: "AddInId",
                table: "CartLinesAddIns");

            migrationBuilder.AddColumn<string>(
                name: "AddIn",
                table: "CartLinesAddIns",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");
        }
    }
}
