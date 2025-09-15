using AutoMapper;
using Medicare.Repository.Entity;
using Medicare.Repository.Interfaces;
using Medicare.Repository.Utility;
using Medicare.Utility;
using Medicare.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _repo;
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AppointmentController(IAppointmentRepository repo, IPatientRepository patientRepository,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAppointments(AppointmentFilter filter)
        {
            try
            {
                var appoitments = await _repo.GetAllAsync(filter);
                var viewModels = _mapper.Map<PagingResult<AppointmentViewModel>>(appoitments);
                return JsonResponseHelper.CreateSuccessResponse(viewModels);
            }
            catch (Exception ex)
            {
                return JsonResponseHelper.CreateFailureResponse(ex.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> AddAppointment(CreateAppointmentViewModel appoitmentViewModel)
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
                var patient = _mapper.Map<Patient>(appoitmentViewModel);
                var patientResult = await _patientRepository.AddAsync(patient);


                // Map Doctor
                var appoitment = _mapper.Map<Appointment>(appoitmentViewModel);
                appoitment.Id = Guid.NewGuid();
                appoitment.PatientId = patientResult.Id;
                appoitment.Status = "Scheduled";

                // Add doctor via repository
                await _repo.AddAsync(appoitment);

                int retVal = await _unitOfWork.SaveChangesAsync();
                if (retVal > 0)
                    return JsonResponseHelper.CreateSuccessResponse(appoitmentViewModel, "Appointment Scheduled successfully");
                else
                    return JsonResponseHelper.CreateFailureResponse("Failed to Schedule Appointment");
            }
            catch (Exception e)
            {
                return JsonResponseHelper.CreateFailureResponse(e.Message);
            }
        }
        [HttpPost]
        public async Task<JsonResult> DeleteAppointment(Guid id)
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
    }
}
