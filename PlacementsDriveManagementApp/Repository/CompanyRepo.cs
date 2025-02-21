using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class CompanyRepo : ICompanyRepo
    {
        private readonly DataContext _context;

        public CompanyRepo(DataContext context)
        {
            _context = context;
        }

        public bool CompanyExists(string companyId)
        {
            return _context.Companies.Any(c => c.Id == companyId);
        }

        public ICollection<Company> GetCompanies()
        {
            return _context.Companies.OrderBy(c => c.Name).ToList();
        }

        public Company GetCompanyById(string companyId)
        {
            return _context.Companies.Where(c => c.Id == companyId).FirstOrDefault();
        }

        public ICollection<Opening> GetCompanyOpenings(string companyId)
        {
            //return _context.Openings.Where(op => op.CompanyId == companyId).ToList();
            return _context.Companies.Where(c => c.Id == companyId).Select(c => c.Openings).FirstOrDefault();
        }
    }
}
