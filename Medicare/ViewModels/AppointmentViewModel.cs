namespace Medicare.ViewModels
{
    public class AppointmentViewModel
    {
        public Guid Id { get; set; }
        public Guid? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? AppointmentDate { get; set; }
        public string? AppointmentTime { get; set; }
        public string? Remarks { get; set; }
        public string? Status { get; set; }

        public Guid? PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? PatientGender { get; set; }
        public int? PatientAge { get; set; }


    }
    public class CreateAppointmentViewModel
    {
        public Guid Id { get; set; }
        public Guid DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public System.TimeSpan AppointmentTime { get; set; }
        public int? DepartmentId { get; set; }
        public string? ReasonForAppointment { get; set; }
        public string? Remarks { get; set; }
        public string? Status { get; set; }

        #region Patient Details
        public Guid? PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string? BloodGroup { get; set; }
        public string? MaritalStatus { get; set; }
        public string? MobileNumber { get; set; }
        public string? AlternatePhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }
        public string? Occupation { get; set; }
        public string? NationalId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }

        #endregion
    }

    public class DoctorsAppointmentViewModel
    {
        public Guid Id { get; set; }
        public Guid? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorSpecialization { get; set; }
        public string? DoctorEmail { get; set; }
        public string? DoctorMobile { get; set; }
        public int? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? AppointmentDate { get; set; }
        public string? AppointmentTime { get; set; }
        public string? Remarks { get; set; }
        public string? Status { get; set; }

        public Guid? PatientId { get; set; }
        public string? PatientName { get; set; }
        public string? PatientGender { get; set; }
        public int? PatientAge { get; set; }


    }
}
