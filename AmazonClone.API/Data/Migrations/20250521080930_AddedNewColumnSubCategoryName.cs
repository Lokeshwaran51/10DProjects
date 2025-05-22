using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewColumnSubCategoryName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubCategoryName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategoryName",
                table: "Products");
        }
    }
}
