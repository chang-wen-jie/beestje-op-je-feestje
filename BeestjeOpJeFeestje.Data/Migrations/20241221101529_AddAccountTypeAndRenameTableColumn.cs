using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeOpJeFeestje.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountTypeAndRenameTableColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_animals_bookings_animals_AnimalsId",
                table: "animals_bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_animals_bookings_bookings_BookingsId",
                table: "animals_bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_bookings_accounts_AccountId1",
                table: "bookings");

            migrationBuilder.DropIndex(
                name: "IX_bookings_AccountId1",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "account_type",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "BookingsId",
                table: "animals_bookings",
                newName: "booking_id");

            migrationBuilder.RenameColumn(
                name: "AnimalsId",
                table: "animals_bookings",
                newName: "animal_id");

            migrationBuilder.RenameIndex(
                name: "IX_animals_bookings_BookingsId",
                table: "animals_bookings",
                newName: "IX_animals_bookings_booking_id");

            migrationBuilder.AddColumn<int>(
                name: "account_type_id",
                table: "accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "account_types",
                columns: table => new
                {
                    account_type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    account_type_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account_types", x => x.account_type_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounts_account_type_id",
                table: "accounts",
                column: "account_type_id");

            migrationBuilder.AddForeignKey(
                name: "fk_accounts_account_types_account_type_id",
                table: "accounts",
                column: "account_type_id",
                principalTable: "account_types",
                principalColumn: "account_type_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_animals_bookings_animal_id",
                table: "animals_bookings",
                column: "animal_id",
                principalTable: "animals",
                principalColumn: "animal_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_animals_bookings_booking_id",
                table: "animals_bookings",
                column: "booking_id",
                principalTable: "bookings",
                principalColumn: "booking_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_accounts_account_types_account_type_id",
                table: "accounts");

            migrationBuilder.DropForeignKey(
                name: "fk_animals_bookings_animal_id",
                table: "animals_bookings");

            migrationBuilder.DropForeignKey(
                name: "fk_animals_bookings_booking_id",
                table: "animals_bookings");

            migrationBuilder.DropTable(
                name: "account_types");

            migrationBuilder.DropIndex(
                name: "IX_accounts_account_type_id",
                table: "accounts");

            migrationBuilder.DropColumn(
                name: "account_type_id",
                table: "accounts");

            migrationBuilder.RenameColumn(
                name: "booking_id",
                table: "animals_bookings",
                newName: "BookingsId");

            migrationBuilder.RenameColumn(
                name: "animal_id",
                table: "animals_bookings",
                newName: "AnimalsId");

            migrationBuilder.RenameIndex(
                name: "IX_animals_bookings_booking_id",
                table: "animals_bookings",
                newName: "IX_animals_bookings_BookingsId");

            migrationBuilder.AddColumn<int>(
                name: "AccountId1",
                table: "bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "account_type",
                table: "accounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_AccountId1",
                table: "bookings",
                column: "AccountId1");

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
        }
    }
}
