using AutoMapper;
using Medicare.Models;
using Medicare.Repository.Entity;
using Medicare.Repository.Interfaces;
using Medicare.Repository.Utility;
using Medicare.Utility;
using Medicare.ViewModels;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using System.Security.Claims;

namespace Medicare.Controllers
{

    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _repo;
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IUserRepository _userRepo;
        private readonly IDoctorsEducationRepository _edicationRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public DoctorController(IDoctorRepository repo, IUserRepository userRepo, IAppointmentRepository appointmentRepository,
            IDoctorsEducationRepository doctorsEducationRepository, IUnitOfWork unitOfWork,
            EmailService emailService, IConfiguration configuration, IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
            _appointmentRepo = appointmentRepository;
            _edicationRepo = doctorsEducationRepository;
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult DoctorList()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetDoctors(DoctorFilter filter)
        {
            try
            {
                var doctors = await _repo.GetAllAsync(filter);
                var viewModels = _mapper.Map<PagingResult<DoctorViewModel>>(doctors);
                return JsonResponseHelper.CreateSuccessResponse(viewModels);
            }
            catch (Exception ex)
            {
                return JsonResponseHelper.CreateFailureResponse(ex.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddDoctor(DoctorViewModel doctorViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return JsonResponseHelper.CreateFailureResponse("Validation failed", errors);
            }
            try
            {
                var user = new User();
                // Map Doctor
                var doctor = _mapper.Map<Doctor>(doctorViewModel);
                doctor.Id = Guid.NewGuid();
                doctor.CreatedAt = DateTime.UtcNow;



                // Map Departments
                if (doctorViewModel.Departments != null && doctorViewModel.Departments.Any())
                {
                    doctor.DoctorDepartments = doctorViewModel.Departments
                        .Select(d => new DoctorDepartment { DoctorId = doctor.Id, DepartmentId = d.Id })
                        .ToList();
                }

                // Map Specializations
                if (doctorViewModel.Specializations != null && doctorViewModel.Specializations.Any())
                {
                    doctor.DoctorSpecializations = doctorViewModel.Specializations
                        .Select(s => new DoctorSpecialization { DoctorId = doctor.Id, SpecializationId = s.Id })
                        .ToList();
                }

                // Add doctor via repository
                await _repo.AddAsync(doctor);

                if (doctorViewModel.CreateLogin)
                {
                    user = _mapper.Map<User>(doctorViewModel);
                    user.Id = doctor.Id;
                    user.UserName = user.Email.Split('@')[0].ToString();
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(_configuration["AppSettings:DefaultPassword"]);
                    user.Roles = new List<UserRole>()
                    {
                        new UserRole{ RoleId =  new Guid("EF591562-61C9-4C4F-9352-DF5BCE617F8A"), UserId = user.Id}
                    };
                    var userresult = await _userRepo.AddAsync(user);
                }

                int retVal = await _unitOfWork.SaveChangesAsync();
                if (retVal > 0)
                {
                    string subject = "Your Medicare Doctor Login";
                    string body = $@"
                            <h2>Welcome to Medicare</h2>
                            <p>Your account has been created.</p>
                            <p><b>Username:</b> {user.UserName}</p>
                            <p><b>Password:</b> {user.PasswordHash}</p>
                            <p>Please change your password after first login.</p>";

                    //await _emailService.SendEmailAsync(user.Email, subject, body);

                    return JsonResponseHelper.CreateSuccessResponse(_mapper.Map<DoctorViewModel>(doctor), "Doctor added successfully");
                }
                else
                    return JsonResponseHelper.CreateFailureResponse("An Error Occured");
            }
            catch (Exception e)
            {
                return JsonResponseHelper.CreateFailureResponse(e.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteDoctor(Guid id)
        {
            try
            {
                var success = await _repo.DeleteAsync(id);
                if (success)
                {
                    return JsonResponseHelper.CreateSuccessResponse(success, "Doctor deleted successfully");
                }
                else
                {
                    return JsonResponseHelper.CreateFailureResponse("Doctor not found");
                }
            }
            catch (Exception e)
            {
                return JsonResponseHelper.CreateFailureResponse(e.Message);
            }
        }

        [HttpGet]
        public IActionResult Appointments()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAppointments(AppointmentFilter filter)
        {
            try
            {
                // Check if current user has Doctor role
                if (User.IsInRole("Doctor"))
                {
                    filter.DoctorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                }
                var appoitments = await _appointmentRepo.GetAllAsync(filter);
                var viewModels = _mapper.Map<PagingResult<AppointmentViewModel>>(appoitments);


                return JsonResponseHelper.CreateSuccessResponse(viewModels);
            }
            catch (Exception ex)
            {
                return JsonResponseHelper.CreateFailureResponse(ex.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddPrescription(CreatePrescriptionViewModel prescriptionViewModel)
        {
            try
            {
                var appointment = await _appointmentRepo.GetByIdAsync(prescriptionViewModel.AppointmentId);
                if (appointment != null/* && appointment.Status == "Scheduled"*/)
                {
                    // Create PDF doc
                    var document = new PrescriptionDocument(prescriptionViewModel.PrescriptionContent);

                    appointment.PrescriptionContent = prescriptionViewModel.PrescriptionContent;
                    appointment.Status = "Completed";

                    await _appointmentRepo.UpdateAsync(appointment);

                    int retVal = await _unitOfWork.SaveChangesAsync();

                    if (retVal > 0)
                    {
                        // Directory check
                        var folderPath = Path.Combine("wwwroot", "prescriptions");
                        if (!Directory.Exists(folderPath))
                            Directory.CreateDirectory(folderPath);

                        // Generate & save
                        var filePath = Path.Combine(folderPath, $"Prescription_{DateTime.Now:yyyyMMddHHmmss}.pdf");
                        document.GeneratePdf(filePath);

                        return JsonResponseHelper.CreateSuccessResponse(_mapper.Map<AppointmentViewModel>(appointment));
                    }
                }
                return JsonResponseHelper.CreateFailureResponse("An Error Occured");
            }
            catch (Exception ex)
            {
                return JsonResponseHelper.CreateFailureResponse(ex.Message);
            }
        }

        public IActionResult Profile()
        {
            return View();
        }
        public async Task<JsonResult> GetProfile()
        {
            try
            {
                var doctorId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var doctor = await _repo.GetByIdAsync(doctorId);
                if (doctor != null)
                {
                    var viewModel = _mapper.Map<DoctorViewModel>(doctor);
                    return JsonResponseHelper.CreateSuccessResponse(viewModel);
                }
                else
                {
                    return JsonResponseHelper.CreateFailureResponse("Doctor not found");
                }
            }
            catch (Exception ex)
            {
                return JsonResponseHelper.CreateFailureResponse(ex.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddEducation(DoctorsEducationViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    return JsonResponseHelper.CreateFailureResponse("Validation failed", errors);
                }
                var educationEntity = _mapper.Map<DoctorsEducation>(model);
                educationEntity.FieldOfStudy = educationEntity.FieldOfStudy == null ? "" : educationEntity.FieldOfStudy;
                var education = await _edicationRepo.AddAsync(educationEntity);
                if (education != null)
                {
                    var viewModel = _mapper.Map<DoctorsEducation>(education);
                    return JsonResponseHelper.CreateSuccessResponse(viewModel);
                }
                else
                {
                    return JsonResponseHelper.CreateFailureResponse("Education not found");
                }
            }
            catch (Exception ex)
            {
                return JsonResponseHelper.CreateFailureResponse(ex.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteEducation(int id)
        {
            try
            {
                var result = await _edicationRepo.DeleteAsync(id);
                if (result)
                {
                    return JsonResponseHelper.CreateSuccessResponse(true);
                }
                else
                {
                    return JsonResponseHelper.CreateFailureResponse("Education not found");
                }
            }
            catch (Exception ex)
            {
                return JsonResponseHelper.CreateFailureResponse(ex.Message);
            }
        }

        public IActionResult Schedule()
        {
            return View();
        }
    }
}
