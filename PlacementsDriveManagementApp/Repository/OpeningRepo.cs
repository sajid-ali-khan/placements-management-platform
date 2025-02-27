using Microsoft.EntityFrameworkCore;
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

        public bool CreateOpening(Opening opening)
        {
            _context.Openings.Add(opening);
            return Save();
        }

        public ICollection<Application> GetApplicationsByOpening(int openingId)
        {
            return _context.Applications
                .Where(ap => ap.OpeningId == openingId)
                .Include(ap => ap.Student)
                .Include(ap => ap.Opening)
                    .ThenInclude(o => o.Company)
                .ToList();
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
            return _context.Openings
                .OrderBy(op => op.Id).ToList();
        }

        public bool OpeningExists(int openingId)
        {
            return _context.Openings.Any(op => op.Id == openingId);
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateOpening(Opening opening)
        {
            _context.Update(opening);
            return Save();
        }
    }
}
