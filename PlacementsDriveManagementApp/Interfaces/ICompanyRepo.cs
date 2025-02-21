using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface ICompanyRepo
    {
        ICollection<Company> GetCompanies();
        Company GetCompanyById(int companyId);
        ICollection<Opening> GetCompanyOpenings(int companyId);
        ICollection<Application> GetApplicationsByCompany(int companyId);
        bool CompanyExists(int companyId);
        Company GetCompanyByEmail(string companyEmail);
        bool CreateCompany(Company company);
        bool Save();
    }
}
