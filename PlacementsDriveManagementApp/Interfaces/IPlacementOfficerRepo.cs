using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Interfaces
{
    public interface IPlacementOfficerRepo
    {
        ICollection<PlacementOfficer> GetPlacementOfficers();
        PlacementOfficer GetPlacementOfficerById(int placementOfficerId);
        PlacementOfficer GetPlacementOfficerByUserName(string userName);
        bool CreatePlacementOfficer(PlacementOfficer placementOfficer);
        bool PlacementOfficerExists(int placementOfficerId);
        bool PlacementOfficerExistsByUserName(string userName);
        bool Save();
    }
}
