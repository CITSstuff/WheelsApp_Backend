using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WheelsApp_Backend.Migrations
{
    public partial class Cars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Car_Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date_Added = table.Column<string>(nullable: true),
                    Make = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true),
                    Colour = table.Column<string>(nullable: true),
                    Kms = table.Column<string>(nullable: true),
                    Registration = table.Column<string>(nullable: true),
                    Tank = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Car_Id);
                });

            migrationBuilder.CreateTable(
                name: "Passwords",
                columns: table => new
                {
                    Contact = table.Column<string>(nullable: false),
                    Reminder = table.Column<string>(nullable: true),
                    OfKinContact = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    New_Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passwords", x => x.Contact);
                });

            migrationBuilder.CreateTable(
                name: "Damages",
                columns: table => new
                {
                    Damage_Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Weigh = table.Column<string>(nullable: true),
                    DateOfDamage = table.Column<string>(nullable: true),
                    DateOfFix = table.Column<string>(nullable: true),
                    Damage = table.Column<string>(nullable: true),
                    Driver = table.Column<int>(nullable: false),
                    IsFixed = table.Column<bool>(nullable: false),
                    Car_Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Damages", x => x.Damage_Id);
                    table.ForeignKey(
                        name: "FK_Damages_Cars_Car_Id",
                        column: x => x.Car_Id,
                        principalTable: "Cars",
                        principalColumn: "Car_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Damages_Car_Id",
                table: "Damages",
                column: "Car_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Damages");

            migrationBuilder.DropTable(
                name: "Passwords");

            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
