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

        public bool ApplicationExists(int applicationId)
        {
            return _context.Applications.Any(ap => ap.Id == applicationId);
        }

        public bool CreateApplication(Application application)
        {
            _context.Applications.Add(application);
            return Save();
        }

        public Application GetApplicationById(int applicationId)
        {
            return _context.Applications.Where(ap => ap.Id == applicationId).FirstOrDefault();
        }

        public Opening GetApplicationOpening(int applicationId)
        {
            return _context.Applications.Where(ap => ap.Id == applicationId).Select(ap => ap.Opening).FirstOrDefault();
        }

        public Resume GetApplicationResume(int applicationId)
        {
            return _context.Applications.Where(ap => ap.Id == applicationId).Select(ap => ap.Resume).FirstOrDefault();
        }

        public ICollection<Application> GetApplications()
        {
            return _context.Applications.OrderBy(ap => ap.AppliedDate).ToList();
        }

        public Student GetStudentByApplication(int applicationId)
        {
            return _context.Applications.Where(ap => ap.Id == applicationId).Select(ap => ap.Student).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }
    }
}
