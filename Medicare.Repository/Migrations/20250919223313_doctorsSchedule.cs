using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicare.Repository.Migrations
{
    public partial class doctorsSchedule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorsScheduleId",
                schema: "OPD",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DoctorsSchedules",
                schema: "OPD",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AvailableDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    PatientCapacity = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorsSchedules_Doctors_DoctorId",
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
                values: new object[] { new Guid("263e1b58-7d92-49cc-96e3-d985baa11a96"), "Admin" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("33c7291c-76f9-4121-87b3-5097bf322597"), "Doctor" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("da3143fe-c5e4-4d63-8497-b929851c9835"), "Receptionist" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorsScheduleId",
                schema: "OPD",
                table: "Appointments",
                column: "DoctorsScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsSchedules_DoctorId",
                schema: "OPD",
                table: "DoctorsSchedules",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_DoctorsSchedules_DoctorsScheduleId",
                schema: "OPD",
                table: "Appointments",
                column: "DoctorsScheduleId",
                principalSchema: "OPD",
                principalTable: "DoctorsSchedules",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_DoctorsSchedules_DoctorsScheduleId",
                schema: "OPD",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "DoctorsSchedules",
                schema: "OPD");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_DoctorsScheduleId",
                schema: "OPD",
                table: "Appointments");

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("263e1b58-7d92-49cc-96e3-d985baa11a96"));

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("33c7291c-76f9-4121-87b3-5097bf322597"));

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("da3143fe-c5e4-4d63-8497-b929851c9835"));

            migrationBuilder.DropColumn(
                name: "DoctorsScheduleId",
                schema: "OPD",
                table: "Appointments");

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
    }
}
