namespace Medicare.ViewModels
{
    public class PatientViewModel
    {
        public Guid Id { get; set; }

        // Basic Info
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName => string.Join(" ", new[] { FirstName, LastName }.Where(s => !string.IsNullOrWhiteSpace(s)));

        // Contact Info
        public string? MobileNumber { get; set; }
        public string? AlternatePhoneNumber { get; set; }
        public string? Email { get; set; }

        // Demographics
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Address Info
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
        public string? Country { get; set; }

        // Emergency Contact
        public string? EmergencyContactName { get; set; }
        public string? EmergencyContactNumber { get; set; }

        // Other Info
        public string? Occupation { get; set; }
        public string? NationalId { get; set; }

        // Metadata
        //public DateTime CreatedAt { get; set; }
        //public DateTime? UpdatedAt { get; set; }
    }

}
