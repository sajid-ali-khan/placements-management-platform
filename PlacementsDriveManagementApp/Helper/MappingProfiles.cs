using AutoMapper;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.DTOs;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Helper
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<Company, CompanyDto>();
            CreateMap<Opening, OpeningDto>();
            CreateMap<OpeningUpdateDto, Opening>();
            CreateMap<Application, ApplicationDto>();
            CreateMap<ApplicationUpdateDto, Application>();
            CreateMap<Student, StudentDto>();
            CreateMap<StudentDto, Student>();
            CreateMap<StudentUpdateDto, Student>();
            CreateMap<ResumeUpdateDto, Resume>();
            CreateMap<Application, ApplicationDetailDto>()
                .ForMember(dest => dest.ApplicationId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Student.Name))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Opening.Company.Name))
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Opening.JobTitle))
                .ForMember(dest => dest.ApplicationStatus, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.AppliedDate, opt => opt.MapFrom(src => src.AppliedDate))
                .ForMember(dest => dest.InterviewSlot, opt => opt.MapFrom(src => src.InterviewSlot))
                .ForMember(dest => dest.StudentAppeared, opt => opt.MapFrom(src => src.StudentAppeared))
                .ForMember(dest => dest.Package, opt => opt.MapFrom(src => src.Package))
                .ForMember(dest => dest.JoiningDate, opt => opt.MapFrom(src => src.JoiningDate))
                .ForMember(dest => dest.PlaceOfWork, opt => opt.MapFrom(src => src.PlaceOfWork));
        }
    }
}
