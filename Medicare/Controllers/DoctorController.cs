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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DoctorController(IDoctorRepository repo, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repo = repo;
            _unitOfWork = unitOfWork;
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

                return JsonResponseHelper.CreateSuccessResponse(doctor, "Doctor added successfully");

            }
            catch(Exception e)
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
    }
}
