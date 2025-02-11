using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface ICompanyRepo
    {
        ICollection<Company> GetCompanies();
        Company GetCompany(int id);
        ICollection<Opening> GetCompanyOpenings(int companyId);
    }
}
