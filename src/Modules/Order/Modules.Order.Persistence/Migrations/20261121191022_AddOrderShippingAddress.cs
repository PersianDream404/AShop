using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Order.Persistence.Migrations
{
    public partial class AddOrderShippingAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ShippingAddressId",
                table: "Orders",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingCity",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingPostalCode",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingProvince",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingRecipientName",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "ShippingAddressId", table: "Orders");
            migrationBuilder.DropColumn(name: "ShippingCity", table: "Orders");
            migrationBuilder.DropColumn(name: "ShippingPostalCode", table: "Orders");
            migrationBuilder.DropColumn(name: "ShippingProvince", table: "Orders");
            migrationBuilder.DropColumn(name: "ShippingRecipientName", table: "Orders");
        }
    }
}
