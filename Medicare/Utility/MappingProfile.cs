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
            CreateMap(typeof(PagingResult<>), typeof(PagingResult<>));

            #region Doctor

            CreateMap<Doctor, DoctorViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Departments, opt => opt.MapFrom(src =>
                                        src.DoctorDepartments.Select(dd => dd.Department)))
                .ForMember(dest => dest.Specializations, opt => opt.MapFrom(src =>
                                        src.DoctorSpecializations.Select(dd => dd.Specialization)))
                .ReverseMap();

            CreateMap<DoctorsWithDetailsModel, DoctorsWithDetailsViewModel>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => $"{src.DoctorName} ({src.Specializations})"));

            CreateMap<DoctorViewModel, User>()
                 .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone));

            CreateMap<DoctorsEducation, DoctorsEducationViewModel>();

            #endregion

            CreateMap<Department, DepartmentViewModel>().ReverseMap();
            CreateMap<Specialization, SpecializationViewModel>().ReverseMap();


            CreateMap<Appointment, AppointmentViewModel>()
                .ForMember(dest => dest.DoctorName,
                           opt => opt.MapFrom(src => src.Doctor != null
                                          ? src.Doctor.FirstName + " " + src.Doctor.LastName
                                          : string.Empty))
                .ForMember(dest => dest.PatientName,
                           opt => opt.MapFrom(src => src.Patient != null
                                                      ? src.Patient.FirstName + " " + src.Patient.LastName
                                                      : string.Empty))
                .ForMember(dest => dest.DepartmentName,
                           opt => opt.MapFrom(src => src.Department != null
                                                      ? src.Department.Name
                                                      : string.Empty))
                .ForMember(dest => dest.AppointmentDate,
                           opt => opt.MapFrom(src => src.AppointmentDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.AppointmentTime,
                           opt => opt.MapFrom(src => src.AppointmentTime.HasValue
                                                      ? src.AppointmentTime.Value.ToString(@"hh\:mm")
                                                      : string.Empty));

            CreateMap<Appointment, DoctorsAppointmentViewModel>()
                .ForMember(dest => dest.DoctorName,
                           opt => opt.MapFrom(src => src.Doctor != null
                                          ? src.Doctor.FirstName + " " + src.Doctor.LastName
                                          : string.Empty))
                .ForMember(dest => dest.PatientName,
                           opt => opt.MapFrom(src => src.Patient != null
                                                      ? src.Patient.FirstName + " " + src.Patient.LastName
                                                      : string.Empty))
                .ForMember(dest => dest.DepartmentName,
                           opt => opt.MapFrom(src => src.Department != null
                                                      ? src.Department.Name
                                                      : string.Empty))
                .ForMember(dest => dest.AppointmentDate,
                           opt => opt.MapFrom(src => src.AppointmentDate.ToString("yyyy-MM-dd")))
                .ForMember(dest => dest.AppointmentTime,
                           opt => opt.MapFrom(src => src.AppointmentTime.HasValue
                                                      ? src.AppointmentTime.Value.ToString(@"hh\:mm")
                                                      : string.Empty))
                .ForMember(dest => dest.PatientGender, opt => opt.MapFrom(src => src.Patient.Gender));

            CreateMap<CreateAppointmentViewModel, Appointment>();

            CreateMap<CreateAppointmentViewModel, Patient>();

            CreateMap<User, UserViewModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Roles,
                    opt => opt.MapFrom(src => string.Join("|",
                        src.Roles.Select(ur => ur.Role.Name))));
            CreateMap<CreateUserViewModel, User>();
            
        }
    }
}
