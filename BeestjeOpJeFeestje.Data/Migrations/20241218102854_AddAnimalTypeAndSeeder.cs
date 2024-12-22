using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeOpJeFeestje.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAnimalTypeAndSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalBooking_Animals_AnimalsId",
                table: "AnimalBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimalBooking_Bookings_BookingsId",
                table: "AnimalBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Accounts_AccountId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Animals",
                table: "Animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AnimalBooking",
                table: "AnimalBooking");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Animals");

            migrationBuilder.RenameTable(
                name: "Bookings",
                newName: "bookings");

            migrationBuilder.RenameTable(
                name: "Animals",
                newName: "animals");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "accounts");

            migrationBuilder.RenameTable(
                name: "AnimalBooking",
                newName: "animals_bookings");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "bookings",
                newName: "booking_total_price");

            migrationBuilder.RenameColumn(
                name: "IsConfirmed",
                table: "bookings",
                newName: "booking_is_confirmed");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "bookings",
                newName: "booking_date");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "bookings",
                newName: "booking_account_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "bookings",
                newName: "booking_id");

            migrationBuilder.RenameIndex(
                name: "IX_Bookings_AccountId",
                table: "bookings",
                newName: "IX_bookings_booking_account_id");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "animals",
                newName: "animal_price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "animals",
                newName: "animal_name");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "animals",
                newName: "animal_image_url");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "animals",
                newName: "animal_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "accounts",
                newName: "account_id");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalBooking_BookingsId",
                table: "animals_bookings",
                newName: "IX_animals_bookings_BookingsId");

            migrationBuilder.AlterColumn<decimal>(
                name: "booking_total_price",
                table: "bookings",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "AccountId1",
                table: "bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "animal_price",
                table: "animals",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "animal_type_id",
                table: "animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "account_email_address",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "account_house_number",
                table: "accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "account_name",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "account_password",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "account_phone_number",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "account_type",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "account_zip_code",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_bookings",
                table: "bookings",
                column: "booking_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_animals",
                table: "animals",
                column: "animal_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_accounts",
                table: "accounts",
                column: "account_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_animals_bookings",
                table: "animals_bookings",
                columns: new[] { "AnimalsId", "BookingsId" });

            migrationBuilder.CreateTable(
                name: "animal_types",
                columns: table => new
                {
                    animal_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    animal_type_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_animal_types", x => x.animal_type_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookings_AccountId1",
                table: "bookings",
                column: "AccountId1");

            migrationBuilder.CreateIndex(
                name: "IX_animals_animal_type_id",
                table: "animals",
                column: "animal_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_animals_animal_types_animal_type_id",
                table: "animals",
                column: "animal_type_id",
                principalTable: "animal_types",
                principalColumn: "animal_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_animals_bookings_animals_AnimalsId",
                table: "animals_bookings",
                column: "AnimalsId",
                principalTable: "animals",
                principalColumn: "animal_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_animals_bookings_bookings_BookingsId",
                table: "animals_bookings",
                column: "BookingsId",
                principalTable: "bookings",
                principalColumn: "booking_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_accounts_AccountId1",
                table: "bookings",
                column: "AccountId1",
                principalTable: "accounts",
                principalColumn: "account_id");

            migrationBuilder.AddForeignKey(
                name: "FK_bookings_accounts_booking_account_id",
                table: "bookings",
                column: "booking_account_id",
                principalTable: "accounts",
                principalColumn: "account_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_animals_animal_types_animal_type_id",
                table: "animals");

            migrationBuilder.DropForeignKey(
                name: "FK_animals_bookings_animals_AnimalsId",
                table: "animals_bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_animals_bookings_bookings_BookingsId",
                table: "animals_bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_accounts_AccountId1",
                table: "bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_accounts_booking_account_id",
                table: "bookings");

            migrationBuilder.DropTable(
                name: "animal_types");

            migrationBuilder.DropPrimaryKey(
                name: "PK_bookings",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_AccountId1",
                table: "bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_animals",
                table: "animals");

            migrationBuilder.DropIndex(
                name: "IX_animals_animal_type_id",
                table: "animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_accounts",
                table: "accounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_animals_bookings",
                table: "animals_bookings");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "animal_type_id",
                table: "animals");

            migrationBuilder.DropColumn(
                name: "account_email_address",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "account_house_number",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "account_name",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "account_password",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "account_phone_number",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "account_type",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "account_zip_code",
                table: "accounts");

            migrationBuilder.RenameTable(
                name: "bookings",
                newName: "Bookings");

            migrationBuilder.RenameTable(
                name: "animals",
                newName: "Animals");

            migrationBuilder.RenameTable(
                name: "accounts",
                newName: "Accounts");

            migrationBuilder.RenameTable(
                name: "animals_bookings",
                newName: "AnimalBooking");

            migrationBuilder.RenameColumn(
                name: "booking_total_price",
                table: "Bookings",
                newName: "TotalPrice");

            migrationBuilder.RenameColumn(
                name: "booking_is_confirmed",
                table: "Bookings",
                newName: "IsConfirmed");

            migrationBuilder.RenameColumn(
                name: "booking_date",
                table: "Bookings",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "booking_account_id",
                table: "Bookings",
                newName: "AccountId");

            migrationBuilder.RenameColumn(
                name: "booking_id",
                table: "Bookings",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_bookings_booking_account_id",
                table: "Bookings",
                newName: "IX_Bookings_AccountId");

            migrationBuilder.RenameColumn(
                name: "animal_price",
                table: "Animals",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "animal_name",
                table: "Animals",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "animal_image_url",
                table: "Animals",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "animal_id",
                table: "Animals",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "account_id",
                table: "Accounts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_animals_bookings_BookingsId",
                table: "AnimalBooking",
                newName: "IX_AnimalBooking_BookingsId");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Bookings",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Animals",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Animals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bookings",
                table: "Bookings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Animals",
                table: "Animals",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AnimalBooking",
                table: "AnimalBooking",
                columns: new[] { "AnimalsId", "BookingsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalBooking_Animals_AnimalsId",
                table: "AnimalBooking",
                column: "AnimalsId",
                principalTable: "Animals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalBooking_Bookings_BookingsId",
                table: "AnimalBooking",
                column: "BookingsId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Accounts_AccountId",
                table: "Bookings",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
