using Medicare.Repository.Entity;
using Medicare.Repository.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medicare.Repository.DbContext
{
    public class MedicareDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public MedicareDbContext(DbContextOptions<MedicareDbContext> options)
            : base(options)
        {
        }

        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<DoctorDepartment> DoctorDepartments { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<DoctorSpecialization> DoctorSpecializations { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set default schema
            modelBuilder.HasDefaultSchema("OPD");

            // Doctor
            modelBuilder.Entity<Doctor>()
                .HasMany(d => d.Appointments)
                .WithOne(a => a.Doctor)
                .HasForeignKey(a => a.DoctorId)
                ;

            // Apply global query filter (ignores Deleted = true records)
            modelBuilder.Entity<Doctor>().HasQueryFilter(d => !d.Deleted);

            // Add index for Deleted property
            modelBuilder.Entity<Doctor>()
                .HasIndex(d => d.Deleted);

            // Patient
            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Appointments)
                .WithOne(a => a.Patient)
                .HasForeignKey(a => a.PatientId);

            // Appointment
            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Department)
                .WithMany()
                .HasForeignKey(a => a.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Doctor ↔ Department
            modelBuilder.Entity<DoctorDepartment>()
                .HasOne(dd => dd.Doctor)
                .WithMany(d => d.DoctorDepartments)
                .HasForeignKey(dd => dd.DoctorId);

            modelBuilder.Entity<DoctorDepartment>()
                .HasOne(dd => dd.Department)
                .WithMany(dep => dep.DoctorDepartments)
                .HasForeignKey(dd => dd.DepartmentId);

            // Doctor ↔ Specialization
            modelBuilder.Entity<DoctorSpecialization>()
                .HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorSpecializations)
                .HasForeignKey(ds => ds.DoctorId);

            modelBuilder.Entity<DoctorSpecialization>()
                .HasOne(ds => ds.Specialization)
                .WithMany(s => s.DoctorSpecializations)
                .HasForeignKey(ds => ds.SpecializationId);


            #region Stored procedure

            modelBuilder.Entity<DoctorsWithDetailsModel>().HasNoKey();

            #endregion

            #region Seed Data

            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "Cardiology" },
                new Department { Id = 2, Name = "Neurology" },
                new Department { Id = 3, Name = "Orthopedics" },
                new Department { Id = 4, Name = "Pediatrics" },
                new Department { Id = 5, Name = "Dermatology" },
                new Department { Id = 6, Name = "ENT (Ear, Nose, Throat)" },
                new Department { Id = 7, Name = "Gynecology & Obstetrics" },
                new Department { Id = 8, Name = "Oncology" },
                new Department { Id = 9, Name = "Ophthalmology" },
                new Department { Id = 10, Name = "Psychiatry" },
                new Department { Id = 11, Name = "Pulmonology" },
                new Department { Id = 12, Name = "Gastroenterology" },
                new Department { Id = 13, Name = "Nephrology" },
                new Department { Id = 14, Name = "Endocrinology" },
                new Department { Id = 15, Name = "Urology" },
                new Department { Id = 16, Name = "General Surgery" },
                new Department { Id = 17, Name = "Emergency Medicine" },
                new Department { Id = 18, Name = "Radiology" },
                new Department { Id = 19, Name = "Pathology" },
                new Department { Id = 20, Name = "Dentistry" }

            );

            modelBuilder.Entity<Specialization>().HasData(
                new Specialization { Id = 1, Name = "Interventional Cardiology", Description = "Specializes in catheter-based treatment of heart diseases." },
                new Specialization { Id = 2, Name = "Pediatric Cardiology", Description = "Focuses on heart problems in children." },
                new Specialization { Id = 3, Name = "Neuro Surgery", Description = "Surgical treatment of neurological disorders." },
                new Specialization { Id = 4, Name = "Stroke Specialist", Description = "Expert in stroke prevention and management." },
                new Specialization { Id = 5, Name = "Spine Surgery", Description = "Surgical care of spinal disorders." },
                new Specialization { Id = 6, Name = "Joint Replacement", Description = "Specialist in hip, knee, and joint replacement." },
                new Specialization { Id = 7, Name = "Pediatric Neurology", Description = "Neurological care for children." },
                new Specialization { Id = 8, Name = "Dermatopathology", Description = "Skin disease specialist with pathology expertise." },
                new Specialization { Id = 9, Name = "Cosmetic Dermatology", Description = "Focus on aesthetic skin treatments." },
                new Specialization { Id = 10, Name = "ENT Surgeon", Description = "Performs surgery on ear, nose, and throat." },
                new Specialization { Id = 11, Name = "Reproductive Endocrinology", Description = "Specialist in fertility and hormonal disorders." },
                new Specialization { Id = 12, Name = "Gynecologic Oncology", Description = "Expert in cancers of the female reproductive system." },
                new Specialization { Id = 13, Name = "Radiation Oncology", Description = "Cancer treatment using radiation." },
                new Specialization { Id = 14, Name = "Cataract Specialist", Description = "Performs cataract surgeries." },
                new Specialization { Id = 15, Name = "Glaucoma Specialist", Description = "Treats glaucoma and eye pressure disorders." },
                new Specialization { Id = 16, Name = "Child Psychiatry", Description = "Mental health care for children and adolescents." },
                new Specialization { Id = 17, Name = "Sleep Medicine", Description = "Diagnosis and treatment of sleep disorders." },
                new Specialization { Id = 18, Name = "Pulmonary Critical Care", Description = "Specialist in ICU care for lung-related issues." },
                new Specialization { Id = 19, Name = "Liver Specialist (Hepatology)", Description = "Focus on liver diseases." },
                new Specialization { Id = 20, Name = "Endocrine Surgery", Description = "Surgical management of endocrine disorders." },
                new Specialization { Id = 21, Name = "Andrology", Description = "Male reproductive health specialist." },
                new Specialization { Id = 22, Name = "Trauma Surgeon", Description = "Emergency surgical care for trauma patients." },
                new Specialization { Id = 23, Name = "Radiologist", Description = "Interprets medical imaging like X-rays, MRI, and CT scans." },
                new Specialization { Id = 24, Name = "Oral Surgeon", Description = "Performs surgical procedures on mouth and jaw." },
                new Specialization { Id = 25, Name = "Pediatric Dentist", Description = "Specialist in children's dental health." }

            );

            #endregion
        }
    }
}
