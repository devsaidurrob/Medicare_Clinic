using System.ComponentModel.DataAnnotations;

namespace Medicare.ViewModels
{
    public class DoctorViewModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string RegistrationNumber { get; set; }
        public string? DisplayTitle { get; set; }
        public string? Experience { get; set; }
        public decimal? ConsultationFee { get; set; }
        public bool CreateLogin { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }
        public List<SpecializationViewModel> Specializations { get; set; }
        public List<DoctorsEducationViewModel> Educations { get; set; }
    }
    public class DoctorsEducationViewModel
    {
        public int? Id { get; set; }
        public Guid? DoctorId { get; set; }
        [Required]
        public string? Degree { get; set; }
        [Required]
        public string? Institution { get; set; }
        public string? FieldOfStudy { get; set; }
        [Required]
        public int? YearOfCompletion { get; set; }
        public string? Notes { get; set; }
    }
    public class DoctorsWithDetailsViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
    public class CreatePrescriptionViewModel
    {
        public Guid AppointmentId { get; set; }
        public DoctorViewModel Doctor { get; set; }
        public ClinicDetails Clinic { get; set; }
        public string? PrescriptionContent { get; set; }
    }
    public class ClinicDetails
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
