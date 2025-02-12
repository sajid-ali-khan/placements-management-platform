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
        }
    }
}
