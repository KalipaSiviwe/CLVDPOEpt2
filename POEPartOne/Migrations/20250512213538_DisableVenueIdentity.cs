using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POEPartOne.Migrations
{
    /// <inheritdoc />
    public partial class DisableVenueIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Location = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Venues__3C57E5D22B98C052", x => x.VenueID);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VenueID = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Events__7944C8709433D734", x => x.EventID);
                    table.ForeignKey(
                        name: "FK__Events__VenueID__4BAC3F29",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "VenueID");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    EventID = table.Column<int>(type: "int", nullable: true),
                    BookingsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VenueID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookings__73951ACD9FF6FE77", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK__Bookings__EventI__4E88ABD4",
                        column: x => x.EventID,
                        principalTable: "Events",
                        principalColumn: "EventID");
                    table.ForeignKey(
                        name: "FK__Bookings__VenueI__5165187F",
                        column: x => x.VenueID,
                        principalTable: "Venues",
                        principalColumn: "VenueID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EventID",
                table: "Bookings",
                column: "EventID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VenueID",
                table: "Bookings",
                column: "VenueID");

            migrationBuilder.CreateIndex(
                name: "IX_Events_VenueID",
                table: "Events",
                column: "VenueID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
