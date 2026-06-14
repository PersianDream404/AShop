using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Product.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class chnageTitleFeaturesCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FeatureCategoryTitle",
                table: "ProductFeaturesCategories",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFeaturesCategories_FeatureCategoryTitle",
                table: "ProductFeaturesCategories",
                newName: "IX_ProductFeaturesCategories_Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "ProductFeaturesCategories",
                newName: "FeatureCategoryTitle");

            migrationBuilder.RenameIndex(
                name: "IX_ProductFeaturesCategories_Title",
                table: "ProductFeaturesCategories",
                newName: "IX_ProductFeaturesCategories_FeatureCategoryTitle");
        }
    }
}
