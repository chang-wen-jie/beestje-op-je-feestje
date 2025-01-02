using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeOpJeFeestje.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePhoneNumberFromCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "customer_phone_number",
                table: "customers",
                newName: "PhoneNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "customers",
                newName: "customer_phone_number");
        }
    }
}
