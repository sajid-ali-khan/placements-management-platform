using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class ApplicationRepo : IApplicationRepo
    {
        private readonly DataContext _context;

        public ApplicationRepo(DataContext context)
        {
            _context = context;
        }
        public Application GetApplication(int applicationId)
        {
            return _context.Applications.Where(ap => ap.Id == applicationId).FirstOrDefault();
        }

        public ICollection<Application> GetApplications()
        {
            return _context.Applications.OrderBy(ap => ap.AppliedDate).ToList();
        }
    }
}
