using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewColumninProductsTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "SubCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "SubCategory");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Products");
        }
    }
}
