using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicare.Repository.Migrations
{
    public partial class department_specialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Department_DepartmentId",
                schema: "OPD",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDepartment_Department_DepartmentId",
                schema: "OPD",
                table: "DoctorDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDepartment_Doctors_DoctorId",
                schema: "OPD",
                table: "DoctorDepartment");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialization_Doctors_DoctorId",
                schema: "OPD",
                table: "DoctorSpecialization");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecialization_Specializations_SpecializationId",
                schema: "OPD",
                table: "DoctorSpecialization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSpecialization",
                schema: "OPD",
                table: "DoctorSpecialization");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorDepartment",
                schema: "OPD",
                table: "DoctorDepartment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Department",
                schema: "OPD",
                table: "Department");

            migrationBuilder.RenameTable(
                name: "DoctorSpecialization",
                schema: "OPD",
                newName: "DoctorSpecializations",
                newSchema: "OPD");

            migrationBuilder.RenameTable(
                name: "DoctorDepartment",
                schema: "OPD",
                newName: "DoctorDepartments",
                newSchema: "OPD");

            migrationBuilder.RenameTable(
                name: "Department",
                schema: "OPD",
                newName: "Departments",
                newSchema: "OPD");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSpecialization_SpecializationId",
                schema: "OPD",
                table: "DoctorSpecializations",
                newName: "IX_DoctorSpecializations_SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSpecialization_DoctorId",
                schema: "OPD",
                table: "DoctorSpecializations",
                newName: "IX_DoctorSpecializations_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorDepartment_DoctorId",
                schema: "OPD",
                table: "DoctorDepartments",
                newName: "IX_DoctorDepartments_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorDepartment_DepartmentId",
                schema: "OPD",
                table: "DoctorDepartments",
                newName: "IX_DoctorDepartments_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSpecializations",
                schema: "OPD",
                table: "DoctorSpecializations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorDepartments",
                schema: "OPD",
                table: "DoctorDepartments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                schema: "OPD",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                schema: "OPD",
                table: "Appointments",
                column: "DepartmentId",
                principalSchema: "OPD",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDepartments_Departments_DepartmentId",
                schema: "OPD",
                table: "DoctorDepartments",
                column: "DepartmentId",
                principalSchema: "OPD",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDepartments_Doctors_DoctorId",
                schema: "OPD",
                table: "DoctorDepartments",
                column: "DoctorId",
                principalSchema: "OPD",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecializations_Doctors_DoctorId",
                schema: "OPD",
                table: "DoctorSpecializations",
                column: "DoctorId",
                principalSchema: "OPD",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecializations_Specializations_SpecializationId",
                schema: "OPD",
                table: "DoctorSpecializations",
                column: "SpecializationId",
                principalSchema: "OPD",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Departments_DepartmentId",
                schema: "OPD",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDepartments_Departments_DepartmentId",
                schema: "OPD",
                table: "DoctorDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorDepartments_Doctors_DoctorId",
                schema: "OPD",
                table: "DoctorDepartments");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecializations_Doctors_DoctorId",
                schema: "OPD",
                table: "DoctorSpecializations");

            migrationBuilder.DropForeignKey(
                name: "FK_DoctorSpecializations_Specializations_SpecializationId",
                schema: "OPD",
                table: "DoctorSpecializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorSpecializations",
                schema: "OPD",
                table: "DoctorSpecializations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorDepartments",
                schema: "OPD",
                table: "DoctorDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                schema: "OPD",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "DoctorSpecializations",
                schema: "OPD",
                newName: "DoctorSpecialization",
                newSchema: "OPD");

            migrationBuilder.RenameTable(
                name: "DoctorDepartments",
                schema: "OPD",
                newName: "DoctorDepartment",
                newSchema: "OPD");

            migrationBuilder.RenameTable(
                name: "Departments",
                schema: "OPD",
                newName: "Department",
                newSchema: "OPD");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSpecializations_SpecializationId",
                schema: "OPD",
                table: "DoctorSpecialization",
                newName: "IX_DoctorSpecialization_SpecializationId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorSpecializations_DoctorId",
                schema: "OPD",
                table: "DoctorSpecialization",
                newName: "IX_DoctorSpecialization_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorDepartments_DoctorId",
                schema: "OPD",
                table: "DoctorDepartment",
                newName: "IX_DoctorDepartment_DoctorId");

            migrationBuilder.RenameIndex(
                name: "IX_DoctorDepartments_DepartmentId",
                schema: "OPD",
                table: "DoctorDepartment",
                newName: "IX_DoctorDepartment_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorSpecialization",
                schema: "OPD",
                table: "DoctorSpecialization",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorDepartment",
                schema: "OPD",
                table: "DoctorDepartment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Department",
                schema: "OPD",
                table: "Department",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Department_DepartmentId",
                schema: "OPD",
                table: "Appointments",
                column: "DepartmentId",
                principalSchema: "OPD",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDepartment_Department_DepartmentId",
                schema: "OPD",
                table: "DoctorDepartment",
                column: "DepartmentId",
                principalSchema: "OPD",
                principalTable: "Department",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorDepartment_Doctors_DoctorId",
                schema: "OPD",
                table: "DoctorDepartment",
                column: "DoctorId",
                principalSchema: "OPD",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialization_Doctors_DoctorId",
                schema: "OPD",
                table: "DoctorSpecialization",
                column: "DoctorId",
                principalSchema: "OPD",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DoctorSpecialization_Specializations_SpecializationId",
                schema: "OPD",
                table: "DoctorSpecialization",
                column: "SpecializationId",
                principalSchema: "OPD",
                principalTable: "Specializations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
