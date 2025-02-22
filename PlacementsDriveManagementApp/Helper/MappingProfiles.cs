using AutoMapper;
using PlacementsDriveManagementApp.Dto;
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
            CreateMap<ResumeUpdateDto, Resume>();
        }
    }
}
