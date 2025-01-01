using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeestjeOpJeFeestje.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_animals_bookings_booking_id",
                table: "animals_bookings");

            migrationBuilder.RenameColumn(
                name: "booking_discount_amount",
                table: "bookings",
                newName: "booking_total_discount_percentage");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "booking_date",
                table: "bookings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddForeignKey(
                name: "fk_bookings_animals_booking_id",
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
                name: "fk_bookings_animals_booking_id",
                table: "animals_bookings");

            migrationBuilder.RenameColumn(
                name: "booking_total_discount_percentage",
                table: "bookings",
                newName: "booking_discount_amount");

            migrationBuilder.AlterColumn<DateTime>(
                name: "booking_date",
                table: "bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "fk_animals_bookings_booking_id",
                table: "animals_bookings",
                column: "booking_id",
                principalTable: "bookings",
                principalColumn: "booking_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
