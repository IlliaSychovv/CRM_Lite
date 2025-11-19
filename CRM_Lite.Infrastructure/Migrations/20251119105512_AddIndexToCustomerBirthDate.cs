using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM_Lite.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToCustomerBirthDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_BirthDate",
                table: "Customers",
                column: "BirthDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_BirthDate",
                table: "Customers");
        }
    }
}
