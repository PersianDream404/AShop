using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Product.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class removeColor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductColors");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "ProductColors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
