using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Payment.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addReturnURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReturnUrl",
                table: "Payments",
                newName: "SuccessReturnUrl");

            migrationBuilder.AddColumn<string>(
                name: "FailedReturnUrl",
                table: "Payments",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedReturnUrl",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "SuccessReturnUrl",
                table: "Payments",
                newName: "ReturnUrl");
        }
    }
}
