using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicare.Repository.Migrations
{
    public partial class alter_appointment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "PrescriptionContent",
                schema: "OPD",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrescriptionFile",
                schema: "OPD",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("c4623da6-a758-4359-bc7d-0c99955bdc61"), "Receptionist" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("ef591562-61c9-4c4f-9352-df5bce617f8a"), "Doctor" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("fe201f89-7a43-4534-9e41-3be098cce3d4"), "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c4623da6-a758-4359-bc7d-0c99955bdc61"));

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("ef591562-61c9-4c4f-9352-df5bce617f8a"));

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fe201f89-7a43-4534-9e41-3be098cce3d4"));

            migrationBuilder.DropColumn(
                name: "PrescriptionContent",
                schema: "OPD",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PrescriptionFile",
                schema: "OPD",
                table: "Appointments");

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
    }
}
