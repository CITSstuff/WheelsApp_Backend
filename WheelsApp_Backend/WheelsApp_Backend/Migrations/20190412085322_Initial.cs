using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WheelsApp_Backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    First_Name = table.Column<string>(nullable: true),
                    Last_Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Telephone = table.Column<string>(nullable: true),
                    Id_number = table.Column<long>(nullable: false),
                    Telephone_2 = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Date_created = table.Column<string>(nullable: true),
                    Account_status = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    Token = table.Column<int>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Work_telephone = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Address_Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Building_name = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Postal_code = table.Column<string>(nullable: true),
                    ClientUser_Id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Address_Id);
                    table.ForeignKey(
                        name: "FK_Address_Users_ClientUser_Id",
                        column: x => x.ClientUser_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NextOfs",
                columns: table => new
                {
                    OfKin_ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Telephone = table.Column<string>(nullable: true),
                    Work_telephone = table.Column<string>(nullable: true),
                    Address_Id = table.Column<long>(nullable: true),
                    ClientUser_Id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextOfs", x => x.OfKin_ID);
                    table.ForeignKey(
                        name: "FK_NextOfs_Address_Address_Id",
                        column: x => x.Address_Id,
                        principalTable: "Address",
                        principalColumn: "Address_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NextOfs_Users_ClientUser_Id",
                        column: x => x.ClientUser_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_ClientUser_Id",
                table: "Address",
                column: "ClientUser_Id");

            migrationBuilder.CreateIndex(
                name: "IX_NextOfs_Address_Id",
                table: "NextOfs",
                column: "Address_Id");

            migrationBuilder.CreateIndex(
                name: "IX_NextOfs_ClientUser_Id",
                table: "NextOfs",
                column: "ClientUser_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NextOfs");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
