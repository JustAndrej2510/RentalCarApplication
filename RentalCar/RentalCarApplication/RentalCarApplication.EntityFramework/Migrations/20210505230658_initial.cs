using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalCarApplication.EntityFramework.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", maxLength: 8, nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EngineCapacity = table.Column<double>(type: "float", nullable: false),
                    GearBox = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BodyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seats = table.Column<int>(type: "int", nullable: false),
                    Consumption = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(22)", maxLength: 22, nullable: false),
                    Passport = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DriverLicense = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TelNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                    table.UniqueConstraint("AK_Users_DriverLicense", x => x.DriverLicense);
                    table.UniqueConstraint("AK_Users_Passport", x => x.Passport);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "CarId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_Email",
                        column: x => x.Email,
                        principalTable: "Users",
                        principalColumn: "Email",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Email", "DriverLicense", "IsAdmin", "Name", "Passport", "Password", "Surname", "TelNumber" },
                values: new object[] { "andrey.pisaryk@gmail.com", "AA5678934", true, "Андрей", "AB1234567", "admin", "Писарик", "+375297294012" });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CarId",
                table: "Orders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Email",
                table: "Orders",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
