using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeOpJeFeestje.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookings_customers_booking_customer_id",
                table: "bookings");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropIndex(
                name: "IX_bookings_booking_customer_id",
                table: "bookings");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "AspNetUsers",
                newName: "customer_phone_number");

            migrationBuilder.AddColumn<string>(
                name: "CustomerId1",
                table: "bookings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "customer_email_address",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "customer_house_number",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "customer_name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "customer_type_id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "customer_zip_code",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_CustomerId1",
                table: "bookings",
                column: "CustomerId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_customer_type_id",
                table: "AspNetUsers",
                column: "customer_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_customer_types_customer_type_id",
                table: "AspNetUsers",
                column: "customer_type_id",
                principalTable: "customer_types",
                principalColumn: "customer_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_AspNetUsers_CustomerId1",
                table: "bookings",
                column: "CustomerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_customer_types_customer_type_id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_AspNetUsers_CustomerId1",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_CustomerId1",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_customer_type_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "customer_email_address",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "customer_house_number",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "customer_name",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "customer_type_id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "customer_zip_code",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "customer_phone_number",
                table: "AspNetUsers",
                newName: "PhoneNumber");

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_type_id = table.Column<int>(type: "int", nullable: true),
                    customer_email_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_house_number = table.Column<int>(type: "int", nullable: false),
                    customer_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customer_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    customer_phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_zip_code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.customer_id);
                    table.ForeignKey(
                        name: "fk_customers_customer_types_customer_type_id",
                        column: x => x.customer_type_id,
                        principalTable: "customer_types",
                        principalColumn: "customer_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_booking_customer_id",
                table: "bookings",
                column: "booking_customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_customers_customer_type_id",
                table: "customers",
                column: "customer_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_customers_booking_customer_id",
                table: "bookings",
                column: "booking_customer_id",
                principalTable: "customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
