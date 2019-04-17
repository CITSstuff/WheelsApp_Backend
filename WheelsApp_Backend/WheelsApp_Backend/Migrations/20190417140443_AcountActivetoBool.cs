using Microsoft.EntityFrameworkCore.Migrations;

namespace WheelsApp_Backend.Migrations
{
    public partial class AcountActivetoBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Account_status",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Account_status",
                table: "Users",
                nullable: true,
                oldClrType: typeof(bool));
        }
    }
}
