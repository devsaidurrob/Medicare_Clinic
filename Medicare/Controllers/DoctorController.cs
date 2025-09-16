using AutoMapper;
using Medicare.Models;
using Medicare.Repository.Entity;
using Medicare.Repository.Interfaces;
using Medicare.Repository.Utility;
using Medicare.Utility;
using Medicare.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Controllers
{

    public class DoctorController : Controller
    {
        private readonly IDoctorRepository _repo;
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public DoctorController(IDoctorRepository repo, IUserRepository userRepo, IUnitOfWork unitOfWork,
            EmailService emailService, IConfiguration configuration, IMapper mapper)
        {
            _repo = repo;
            _userRepo = userRepo;
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
                        new UserRole{ RoleId =  new Guid("1F0555AF-5D23-4A78-9173-F67050CC2464"), UserId = user.Id}
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
    }
}
