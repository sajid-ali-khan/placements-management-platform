namespace PlacementsDriveManagementApp.DTOs
{
    public class ApplicationDetailDto
    {
        public int ApplicationId { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string ApplicationStatus { get; set; }
        public String AppliedDate { get; set; }
        public DateTime? InterviewSlot { get; set; }
        public bool? StudentAppeared { get; set; }
        public decimal? Package { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? PlaceOfWork { get; set; }
    }
}
