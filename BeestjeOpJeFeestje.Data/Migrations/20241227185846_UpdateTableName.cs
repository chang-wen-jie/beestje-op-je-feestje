using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeOpJeFeestje.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accounts_account_types_account_type_id",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_accounts_booking_account_id",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accounts",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_account_types",
                table: "account_types");

            migrationBuilder.RenameTable(
                name: "accounts",
                newName: "customers");

            migrationBuilder.RenameTable(
                name: "account_types",
                newName: "customer_types");

            migrationBuilder.RenameColumn(
                name: "booking_account_id",
                table: "bookings",
                newName: "booking_customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_booking_account_id",
                table: "bookings",
                newName: "IX_bookings_booking_customer_id");

            migrationBuilder.RenameColumn(
                name: "account_zip_code",
                table: "customers",
                newName: "customer_zip_code");

            migrationBuilder.RenameColumn(
                name: "account_type_id",
                table: "customers",
                newName: "customer_type_id");

            migrationBuilder.RenameColumn(
                name: "account_phone_number",
                table: "customers",
                newName: "customer_phone_number");

            migrationBuilder.RenameColumn(
                name: "account_password",
                table: "customers",
                newName: "customer_password");

            migrationBuilder.RenameColumn(
                name: "account_name",
                table: "customers",
                newName: "customer_name");

            migrationBuilder.RenameColumn(
                name: "account_house_number",
                table: "customers",
                newName: "customer_house_number");

            migrationBuilder.RenameColumn(
                name: "account_email_address",
                table: "customers",
                newName: "customer_email_address");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "customers",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_accounts_account_type_id",
                table: "customers",
                newName: "IX_customers_customer_type_id");

            migrationBuilder.RenameColumn(
                name: "account_type_name",
                table: "customer_types",
                newName: "customer_type_name");

            migrationBuilder.RenameColumn(
                name: "account_type_id",
                table: "customer_types",
                newName: "customer_type_id");

            migrationBuilder.AddColumn<decimal>(
                name: "booking_discount_amount",
                table: "bookings",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_customers",
                table: "customers",
                column: "customer_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customer_types",
                table: "customer_types",
                column: "customer_type_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_customers_booking_customer_id",
                table: "bookings",
                column: "booking_customer_id",
                principalTable: "customers",
                principalColumn: "customer_id",
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
                name: "FK_bookings_customers_booking_customer_id",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "fk_customers_customer_types_customer_type_id",
                table: "customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customers",
                table: "customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customer_types",
                table: "customer_types");

            migrationBuilder.DropColumn(
                name: "booking_discount_amount",
                table: "bookings");

            migrationBuilder.RenameTable(
                name: "customers",
                newName: "accounts");

            migrationBuilder.RenameTable(
                name: "customer_types",
                newName: "account_types");

            migrationBuilder.RenameColumn(
                name: "booking_customer_id",
                table: "bookings",
                newName: "booking_account_id");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_booking_customer_id",
                table: "bookings",
                newName: "IX_bookings_booking_account_id");

            migrationBuilder.RenameColumn(
                name: "customer_zip_code",
                table: "accounts",
                newName: "account_zip_code");

            migrationBuilder.RenameColumn(
                name: "customer_type_id",
                table: "accounts",
                newName: "account_type_id");

            migrationBuilder.RenameColumn(
                name: "customer_phone_number",
                table: "accounts",
                newName: "account_phone_number");

            migrationBuilder.RenameColumn(
                name: "customer_password",
                table: "accounts",
                newName: "account_password");

            migrationBuilder.RenameColumn(
                name: "customer_name",
                table: "accounts",
                newName: "account_name");

            migrationBuilder.RenameColumn(
                name: "customer_house_number",
                table: "accounts",
                newName: "account_house_number");

            migrationBuilder.RenameColumn(
                name: "customer_email_address",
                table: "accounts",
                newName: "account_email_address");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "accounts",
                newName: "account_id");

            migrationBuilder.RenameIndex(
                name: "IX_customers_customer_type_id",
                table: "accounts",
                newName: "IX_accounts_account_type_id");

            migrationBuilder.RenameColumn(
                name: "customer_type_name",
                table: "account_types",
                newName: "account_type_name");

            migrationBuilder.RenameColumn(
                name: "customer_type_id",
                table: "account_types",
                newName: "account_type_id");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accounts",
                table: "accounts",
                column: "account_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_account_types",
                table: "account_types",
                column: "account_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_accounts_account_types_account_type_id",
                table: "accounts",
                column: "account_type_id",
                principalTable: "account_types",
                principalColumn: "account_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_accounts_booking_account_id",
                table: "bookings",
                column: "booking_account_id",
                principalTable: "accounts",
                principalColumn: "account_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
