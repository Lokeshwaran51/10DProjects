using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmazonClone.API.Migrations
{
    /// <inheritdoc />
    public partial class RemovedProductId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "CartItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "CartItem",
                type: "int",
                nullable: true);
        }
    }
}
