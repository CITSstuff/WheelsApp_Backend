using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WheelsApp_Backend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    role = table.Column<string>(nullable: true),
                    token = table.Column<int>(nullable: false),
                    id_number = table.Column<long>(nullable: false),
                    telephone = table.Column<string>(nullable: true),
                    first_name = table.Column<string>(nullable: true),
                    lasst_name = table.Column<string>(nullable: true),
                    telephone_2 = table.Column<string>(nullable: true),
                    work_contact = table.Column<string>(nullable: true),
                    next_of_keen = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
