using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedNamingConventionIdtoProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategory_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategory",
                principalColumn: "SubCategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategory_SubCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");
        }
    }
}
