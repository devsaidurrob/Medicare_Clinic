using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicare.Repository.Migrations
{
    public partial class Role_Seed_Data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("1f0555af-5d23-4a78-9173-f67050cc2464"), "Doctor" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("43e4423f-dbb1-4e6d-a87d-a596d53e2b06"), "Admin" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("4c1e5cc9-aa1b-4133-9db0-81707ad7e3cd"), "Receptionist" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("1f0555af-5d23-4a78-9173-f67050cc2464"));

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("43e4423f-dbb1-4e6d-a87d-a596d53e2b06"));

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("4c1e5cc9-aa1b-4133-9db0-81707ad7e3cd"));
        }
    }
}
