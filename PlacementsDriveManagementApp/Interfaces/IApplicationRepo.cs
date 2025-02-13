using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface IApplicationRepo
    {
        ICollection<Application> GetApplications();
        Application GetApplication(int applicationId);
        
    }
}
