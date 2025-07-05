using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface ICompanyRepo
    {
        ICollection<Company> GetCompanies();
        Company GetCompanyById(int companyId);
        ICollection<Opening> GetCompanyOpenings(int companyId);
        ICollection<Application> GetApplicationByCompanyEmail(string companyEmail);
        ICollection<Application> GetApplicationsByCompany(int companyId);
        bool CompanyExists(int companyId);
        Company GetCompanyByEmail(string companyEmail);
        bool CreateCompany(Company company);
        bool Save();
        bool CompanyExistsByEmail(string companyEmail);
        string GetHashedPassword(string companyEmail);

        void Dispose();
    }
}
