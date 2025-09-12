using AutoMapper;
using Medicare.Repository.Entity;
using Medicare.Repository.Utility;
using Medicare.ViewModels;

namespace Medicare.Utility
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PagingResult<Doctor>, PagingResult<DoctorViewModel>>()
                .ForMember(dest => dest.Records, opt => opt.MapFrom(src => src.Records));

            CreateMap<Doctor, DoctorViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Departments, opt => opt.MapFrom(src =>
                                        src.DoctorDepartments.Select(dd => dd.Department)))
                .ForMember(dest => dest.Specializations, opt => opt.MapFrom(src =>
                                        src.DoctorSpecializations.Select(dd => dd.Specialization)))
                .ReverseMap();

            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            CreateMap<Specialization, SpecializationViewModel>().ReverseMap();

        }
    }
}
