using PlacementsDriveManagementApp.Data;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Repository
{
    public class ResumeRepo : IResumeRepo
    {
        private readonly DataContext _context;

        public ResumeRepo(DataContext context)
        {
            _context = context;
        }
        public Resume GetResumeById(int resumeId)
        {
            return _context.Resumes.Where(r => r.Id == resumeId).FirstOrDefault();
        }

        public ICollection<Resume> GetResumes()
        {
            return _context.Resumes.ToList();
        }

        public bool ResumeExists(int resumeId)
        {
            return _context.Resumes.Any(r => r.Id == resumeId);
        }
    }
}
