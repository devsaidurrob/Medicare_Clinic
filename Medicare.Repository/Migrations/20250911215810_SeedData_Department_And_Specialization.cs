using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Medicare.Repository.Migrations
{
    public partial class SeedData_Department_And_Specialization : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Cardiology" },
                    { 2, "Neurology" },
                    { 3, "Orthopedics" },
                    { 4, "Pediatrics" },
                    { 5, "Dermatology" },
                    { 6, "ENT (Ear, Nose, Throat)" },
                    { 7, "Gynecology & Obstetrics" },
                    { 8, "Oncology" },
                    { 9, "Ophthalmology" },
                    { 10, "Psychiatry" },
                    { 11, "Pulmonology" },
                    { 12, "Gastroenterology" },
                    { 13, "Nephrology" },
                    { 14, "Endocrinology" },
                    { 15, "Urology" },
                    { 16, "General Surgery" },
                    { 17, "Emergency Medicine" },
                    { 18, "Radiology" },
                    { 19, "Pathology" },
                    { 20, "Dentistry" }
                });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Specializations",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Specializes in catheter-based treatment of heart diseases.", "Interventional Cardiology" },
                    { 2, "Focuses on heart problems in children.", "Pediatric Cardiology" },
                    { 3, "Surgical treatment of neurological disorders.", "Neuro Surgery" },
                    { 4, "Expert in stroke prevention and management.", "Stroke Specialist" },
                    { 5, "Surgical care of spinal disorders.", "Spine Surgery" },
                    { 6, "Specialist in hip, knee, and joint replacement.", "Joint Replacement" },
                    { 7, "Neurological care for children.", "Pediatric Neurology" },
                    { 8, "Skin disease specialist with pathology expertise.", "Dermatopathology" },
                    { 9, "Focus on aesthetic skin treatments.", "Cosmetic Dermatology" },
                    { 10, "Performs surgery on ear, nose, and throat.", "ENT Surgeon" },
                    { 11, "Specialist in fertility and hormonal disorders.", "Reproductive Endocrinology" },
                    { 12, "Expert in cancers of the female reproductive system.", "Gynecologic Oncology" },
                    { 13, "Cancer treatment using radiation.", "Radiation Oncology" },
                    { 14, "Performs cataract surgeries.", "Cataract Specialist" },
                    { 15, "Treats glaucoma and eye pressure disorders.", "Glaucoma Specialist" },
                    { 16, "Mental health care for children and adolescents.", "Child Psychiatry" },
                    { 17, "Diagnosis and treatment of sleep disorders.", "Sleep Medicine" },
                    { 18, "Specialist in ICU care for lung-related issues.", "Pulmonary Critical Care" },
                    { 19, "Focus on liver diseases.", "Liver Specialist (Hepatology)" },
                    { 20, "Surgical management of endocrine disorders.", "Endocrine Surgery" },
                    { 21, "Male reproductive health specialist.", "Andrology" },
                    { 22, "Emergency surgical care for trauma patients.", "Trauma Surgeon" }
                });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Specializations",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 23, "Interprets medical imaging like X-rays, MRI, and CT scans.", "Radiologist" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Specializations",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 24, "Performs surgical procedures on mouth and jaw.", "Oral Surgeon" });

            migrationBuilder.InsertData(
                schema: "OPD",
                table: "Specializations",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 25, "Specialist in children's dental health.", "Pediatric Dentist" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Departments",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                schema: "OPD",
                table: "Specializations",
                keyColumn: "Id",
                keyValue: 25);
        }
    }
}
