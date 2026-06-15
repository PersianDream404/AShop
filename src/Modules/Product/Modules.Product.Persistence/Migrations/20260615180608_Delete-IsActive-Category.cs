using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Product.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DeleteIsActiveCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "ProductCategories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "ProductCategories",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }
    }
}
