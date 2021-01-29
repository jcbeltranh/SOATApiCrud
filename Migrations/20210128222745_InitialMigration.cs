using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SOATApiReact.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Document = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DocumentType = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Surname = table.Column<string>(maxLength: 50, nullable: false),
                    Genre = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Document);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Plate = table.Column<string>(nullable: false),
                    Color = table.Column<string>(nullable: false),
                    Engine = table.Column<string>(nullable: false),
                    Axles = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Plate);
                });

            migrationBuilder.CreateTable(
                name: "SOATs",
                columns: table => new
                {
                    OwnerDocument = table.Column<int>(nullable: false),
                    VehiclePlate = table.Column<string>(nullable: false),
                    Year = table.Column<DateTime>(type: "Date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SOATs", x => new { x.OwnerDocument, x.VehiclePlate, x.Year });
                    table.ForeignKey(
                        name: "FK_SOATs_Users_OwnerDocument",
                        column: x => x.OwnerDocument,
                        principalTable: "Users",
                        principalColumn: "Document",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SOATs_Vehicles_VehiclePlate",
                        column: x => x.VehiclePlate,
                        principalTable: "Vehicles",
                        principalColumn: "Plate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SOATs_VehiclePlate",
                table: "SOATs",
                column: "VehiclePlate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SOATs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
