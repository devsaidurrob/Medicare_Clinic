using AutoMapper;
using Medicare.Repository.Entity;
using Medicare.Repository.Interfaces;
using Medicare.Repository.Repository;
using Medicare.Utility;
using Medicare.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Medicare.Controllers
{
    public class DropdownController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ISpecializationRepository _specializationRepository;
        private readonly IDoctorRepository _doctorRepository;
        private IMapper _mapper;
        public DropdownController(IDepartmentRepository departmentRepository,ISpecializationRepository specializationRepository,
            IDoctorRepository doctorRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _specializationRepository = specializationRepository;
            _doctorRepository = doctorRepository;
            _mapper = mapper;   
        }
        public async Task<JsonResult> DoctorSpecialization()
        {
            var specializations = await _specializationRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<SpecializationViewModel>>(specializations);
            return JsonResponseHelper.CreateSuccessResponse(viewModels);
        }
        public async Task<JsonResult> Department()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var viewModels = _mapper.Map<List<DepartmentViewModel>>(departments);
            return JsonResponseHelper.CreateSuccessResponse(viewModels);
        }
        public async Task<JsonResult> GetDoctorsWithDetails()
        {
            var doctors = await _doctorRepository.GetDoctorsWithDetailsAsync();
            var viewModels = _mapper.Map<List<DoctorsWithDetailsViewModel>>(doctors);
            return JsonResponseHelper.CreateSuccessResponse(viewModels);
        }
    }
}
