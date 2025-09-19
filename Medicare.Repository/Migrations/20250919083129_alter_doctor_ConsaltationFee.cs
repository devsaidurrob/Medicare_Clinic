using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicare.Repository.Migrations
{
    public partial class alter_doctor_ConsaltationFee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("002080b9-8e43-4849-a85a-2b99d3092ae1"));

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("52e9cf06-6339-4a35-9a5b-94270adeddff"));

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("d907fb83-8a18-494b-a7f9-e91bd99e93c0"));

            migrationBuilder.AddColumn<decimal>(
                name: "ConsultationFee",
                schema: "OPD",
                table: "Doctors",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a500e1f5-b86a-44ab-83b2-dfac4d667ab4"), "Admin" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a7c0e233-4375-403b-bd86-6ef7f6b8cfdf"), "Receptionist" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("f907bf9d-19e1-42da-9539-280cbe67103a"), "Doctor" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a500e1f5-b86a-44ab-83b2-dfac4d667ab4"));

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a7c0e233-4375-403b-bd86-6ef7f6b8cfdf"));

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("f907bf9d-19e1-42da-9539-280cbe67103a"));

            migrationBuilder.DropColumn(
                name: "ConsultationFee",
                schema: "OPD",
                table: "Doctors");

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("002080b9-8e43-4849-a85a-2b99d3092ae1"), "Doctor" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("52e9cf06-6339-4a35-9a5b-94270adeddff"), "Receptionist" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("d907fb83-8a18-494b-a7f9-e91bd99e93c0"), "Admin" });
        }
    }
}
