using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class OpeningRepo : IOpeningRepo
    {
        private readonly DataContext _context;

        public OpeningRepo(DataContext context)
        {
            _context = context;
        }
        public ICollection<Application> GetApplicationsByOpening(int openingId)
        {
            return _context.Openings.Where(op => op.Id == openingId).SelectMany(op => op.Applications).ToList();
        }

        public Company GetCompanyByOpening(int openingId)
        {
            return _context.Openings.Where(op => op.Id == openingId).Select(op => op.Company).FirstOrDefault();
        }

        public Opening GetOpeningById(int openingId)
        {
            return _context.Openings.Where(op => op.Id == openingId).FirstOrDefault();
        }

        public ICollection<Opening> GetOpenings()
        {
            return _context.Openings.OrderBy(op => op.Id).ToList();
        }

        public bool OpeningExists(int openingId)
        {
            return _context.Openings.Any(op => op.Id == openingId);
        }
    }
}
