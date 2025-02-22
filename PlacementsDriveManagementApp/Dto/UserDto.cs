namespace PlacementsDriveManagementApp.Dto
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role {  get; set; }
    }

    public enum UserRole
    {
        STUDENT = 1,
        ADMIN = 2, 
        HR = 3
    }
}
