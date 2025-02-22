using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface IPlacementOfficerRepo
    {
        ICollection<PlacementOfficer> GetPlacementOfficers();
        PlacementOfficer GetPlacementOfficerById(int placementOfficerId);
        PlacementOfficer GetPlacementOfficerByEmail(string email);
        bool CreatePlacementOfficer(PlacementOfficer placementOfficer);
        bool PlacementOfficerExists(int placementOfficerId);
        bool PlacementOfficerExistsByEmail(string email);
        bool Save();

        string GetPlacementOfficerPasswordHash(string userName);
    }
}
