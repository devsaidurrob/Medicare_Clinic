using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicare.Repository.Migrations
{
    public partial class alter_doctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "DisplayTitle",
                schema: "OPD",
                table: "Doctors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProfessionStartDate",
                schema: "OPD",
                table: "Doctors",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DoctorsEducations",
                schema: "OPD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Institution = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FieldOfStudy = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    YearOfCompletion = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorsEducations_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "OPD",
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsEducations_DoctorId",
                schema: "OPD",
                table: "DoctorsEducations",
                column: "DoctorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorsEducations",
                schema: "OPD");

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

            migrationBuilder.DropColumn(
                name: "DisplayTitle",
                schema: "OPD",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "ProfessionStartDate",
                schema: "OPD",
                table: "Doctors");

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
    }
}
