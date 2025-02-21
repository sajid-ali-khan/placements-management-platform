using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface IResumeRepo
    {
        Resume GetResumeById(int resumeId);
        ICollection<Resume> GetResumes();

        bool ResumeExists(int resumeId);

        bool CreateResume(Resume resume);
        bool Save();
    }
}
