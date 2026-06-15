using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Product.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddFeaturesValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeaturesValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductFeaturesCategoryId = table.Column<long>(type: "bigint", nullable: true),
                    ProductFeaturesId = table.Column<long>(type: "bigint", nullable: true),
                    FeatureValue = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturesValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FeaturesValues_ProductFeaturesCategories_ProductFeaturesCategoryId",
                        column: x => x.ProductFeaturesCategoryId,
                        principalTable: "ProductFeaturesCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeaturesValues_ProductFeatures_ProductFeaturesId",
                        column: x => x.ProductFeaturesId,
                        principalTable: "ProductFeatures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesValues_ProductFeaturesCategoryId",
                table: "FeaturesValues",
                column: "ProductFeaturesCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FeaturesValues_ProductFeaturesId",
                table: "FeaturesValues",
                column: "ProductFeaturesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturesValues");
        }
    }
}
