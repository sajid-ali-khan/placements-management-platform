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

        public bool CompanyExists(int companyId)
        {
            return _context.Companies.Any(c => c.Id == companyId);
        }

        public bool CreateCompany(Company company)
        {
            _context.Companies.Add(company);
            return Save();
        }

        public ICollection<Application> GetApplicationsByCompany(int companyId)
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

        public Company GetCompanyByEmail(string companyEmail)
        {
            return _context.Companies.Where(c => c.Email == companyEmail).FirstOrDefault();
        }

        public Company GetCompanyById(int companyId)
        {
            return _context.Companies.Where(c => c.Id == companyId).FirstOrDefault();
        }

        public ICollection<Opening> GetCompanyOpenings(int companyId)
        {
            //return _context.Openings.Where(op => op.CompanyId == companyId).ToList();
            return _context.Companies.Where(c => c.Id == companyId).Select(c => c.Openings).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
