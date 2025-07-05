namespace PlacementsDriveManagementApp.DTOs
{
    public class ApplicationDetailDto
    {
        public int ApplicationId { get; set; }
        public string StudentId { get; set; } = "";
        public string StudentName { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public string JobTitle { get; set; } = "";
        public string ResumeName { get; set; } = "";
        public string ResumePath { get; set; } = "";
        public string ApplicationStatus { get; set; } = "";
        public string AppliedDate { get; set; } = "";
        public string InterviewSlot { get; set; } = "";
        public bool? StudentAppeared { get; set; }
        public decimal? Package { get; set; }
        public string JoiningDate { get; set; } = "";
        public string PlaceOfWork { get; set; } = "";
    }
}
