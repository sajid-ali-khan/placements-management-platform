namespace PlacementsDriveManagementApp.Dto
{
    public class OpeningDetailDto
    {
        public int Id { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public String LastDate { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
    }

}
