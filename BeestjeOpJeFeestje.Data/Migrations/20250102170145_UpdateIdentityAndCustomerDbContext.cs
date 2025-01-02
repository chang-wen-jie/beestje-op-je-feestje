using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeOpJeFeestje.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIdentityAndCustomerDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_customer_types_customer_type_id",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_AspNetUsers_CustomerId1",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_CustomerId1",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CustomerId1",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "customer_email_address",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "customers");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_customer_type_id",
                table: "customers",
                newName: "IX_customers_customer_type_id");

            migrationBuilder.AlterColumn<string>(
                name: "booking_customer_id",
                table: "bookings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_booking_customer_id",
                table: "bookings",
                column: "booking_customer_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_customers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_customers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_customers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_customers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_customers_booking_customer_id",
                table: "bookings",
                column: "booking_customer_id",
                principalTable: "customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_customers_customer_types_customer_type_id",
                table: "customers",
                column: "customer_type_id",
                principalTable: "customer_types",
                principalColumn: "customer_type_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_customers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_customers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_customers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_customers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_customers_booking_customer_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "fk_customers_customer_types_customer_type_id",
                table: "customers");

            migrationBuilder.DropIndex(
                name: "IX_bookings_booking_customer_id",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "AspNetUsers");

            migrationBuilder.RenameIndex(
                name: "IX_customers_customer_type_id",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_customer_type_id");

            migrationBuilder.AlterColumn<int>(
                name: "booking_customer_id",
                table: "bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_CustomerId1",
                table: "bookings",
                column: "CustomerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_customer_types_customer_type_id",
                table: "AspNetUsers",
                column: "customer_type_id",
                principalTable: "customer_types",
                principalColumn: "customer_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_AspNetUsers_CustomerId1",
                table: "bookings",
                column: "CustomerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
