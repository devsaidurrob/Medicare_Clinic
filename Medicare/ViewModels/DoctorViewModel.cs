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
        public System.DateTime CreatedAt { get; set; }
        public Nullable<System.DateTime> UpdatedAt { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }
        public List<SpecializationViewModel> Specializations { get; set; }
    }
    public class DoctorsWithDetailsViewModel
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
