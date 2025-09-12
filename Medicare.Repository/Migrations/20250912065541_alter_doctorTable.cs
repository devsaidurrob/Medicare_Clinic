using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicare.Repository.Migrations
{
    public partial class alter_doctorTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                schema: "OPD",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_Deleted",
                schema: "OPD",
                table: "Doctors",
                column: "Deleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Doctors_Deleted",
                schema: "OPD",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "Deleted",
                schema: "OPD",
                table: "Doctors");
        }
    }
}
