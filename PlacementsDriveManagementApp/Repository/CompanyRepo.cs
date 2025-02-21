using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class CompanyRepo : ICompanyRepo
    {
        private readonly DataContext _context;
        private readonly IOpeningRepo _openingRepo;

        public CompanyRepo(DataContext context, IOpeningRepo openingRepo)
        {
            _context = context;
            _openingRepo = openingRepo;
        }

        public bool CompanyExists(string companyId)
        {
            return _context.Companies.Any(c => c.Id == companyId);
        }

        public ICollection<Application> GetApplicationsByCompany(string companyId)
        {
            var companyOpenings = GetCompanyOpenings(companyId);

            var companyApplications = new List<Application>();

            foreach (var opening in companyOpenings)
            {
                companyApplications.AddRange(_openingRepo.GetApplicationsByOpening(opening.Id));
            }

            return companyApplications;
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
