using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Dto
{
    public class ApplicationUpdateDto
    {
        public ApplicationStatus Status { get; set; }
        public DateTime? InterviewSlot { get; set; }
        public bool? StudentAppeared { get; set; }
        public decimal? Package { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string PlaceOfWork { get; set; }
    }
}
