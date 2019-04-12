using Microsoft.EntityFrameworkCore.Migrations;

namespace WheelsApp_Backend.Migrations
{
    public partial class UpdateAlph : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Users_ClientUser_Id",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_NextOfs_Users_ClientUser_Id",
                table: "NextOfs");

            migrationBuilder.AlterColumn<long>(
                name: "ClientUser_Id",
                table: "NextOfs",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<long>(
                name: "ClientUser_Id",
                table: "Address",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Users_ClientUser_Id",
                table: "Address",
                column: "ClientUser_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_NextOfs_Users_ClientUser_Id",
                table: "NextOfs",
                column: "ClientUser_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Users_ClientUser_Id",
                table: "Address");

            migrationBuilder.DropForeignKey(
                name: "FK_NextOfs_Users_ClientUser_Id",
                table: "NextOfs");

            migrationBuilder.AlterColumn<long>(
                name: "ClientUser_Id",
                table: "NextOfs",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ClientUser_Id",
                table: "Address",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Users_ClientUser_Id",
                table: "Address",
                column: "ClientUser_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NextOfs_Users_ClientUser_Id",
                table: "NextOfs",
                column: "ClientUser_Id",
                principalTable: "Users",
                principalColumn: "User_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
