using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POEPartOne.Migrations
{
    /// <inheritdoc />
    public partial class DisableBookingIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Events__VenueID__4BAC3F29",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "VenueID",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Events__VenueID__4BAC3F29",
                table: "Events",
                column: "VenueID",
                principalTable: "Venues",
                principalColumn: "VenueID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Events__VenueID__4BAC3F29",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "VenueID",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Events",
                type: "varchar(255)",
                unicode: false,
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldUnicode: false,
                oldMaxLength: 255);

            migrationBuilder.AddForeignKey(
                name: "FK__Events__VenueID__4BAC3F29",
                table: "Events",
                column: "VenueID",
                principalTable: "Venues",
                principalColumn: "VenueID");
        }
    }
}
