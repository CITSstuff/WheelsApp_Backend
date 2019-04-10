using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WheelsApp_Backend.Migrations
{
    public partial class UserModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Address_Id",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Next_of_kinKin_id",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Token",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Work_telephone",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Users",
                nullable: false,
                defaultValue: "");

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
                    Postal_code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Address_Id);
                });

            migrationBuilder.CreateTable(
                name: "NextOfKin",
                columns: table => new
                {
                    Kin_id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Telephone = table.Column<string>(nullable: true),
                    Work_telephone = table.Column<string>(nullable: true),
                    Address_Id = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NextOfKin", x => x.Kin_id);
                    table.ForeignKey(
                        name: "FK_NextOfKin_Address_Address_Id",
                        column: x => x.Address_Id,
                        principalTable: "Address",
                        principalColumn: "Address_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Address_Id",
                table: "Users",
                column: "Address_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Next_of_kinKin_id",
                table: "Users",
                column: "Next_of_kinKin_id");

            migrationBuilder.CreateIndex(
                name: "IX_NextOfKin_Address_Id",
                table: "NextOfKin",
                column: "Address_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Address_Address_Id",
                table: "Users",
                column: "Address_Id",
                principalTable: "Address",
                principalColumn: "Address_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_NextOfKin_Next_of_kinKin_id",
                table: "Users",
                column: "Next_of_kinKin_id",
                principalTable: "NextOfKin",
                principalColumn: "Kin_id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Address_Address_Id",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_NextOfKin_Next_of_kinKin_id",
                table: "Users");

            migrationBuilder.DropTable(
                name: "NextOfKin");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropIndex(
                name: "IX_Users_Address_Id",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Next_of_kinKin_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Address_Id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Next_of_kinKin_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Work_telephone",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Users");
        }
    }
}
