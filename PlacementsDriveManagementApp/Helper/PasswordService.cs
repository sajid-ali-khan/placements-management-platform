namespace PlacementsDriveManagementApp.Helper;
using BCrypt.Net;

public class PasswordService
{
    public string HashPassword(string password)
    {
        return BCrypt.EnhancedHashPassword(password);
    }

    public bool VerifyPassword(string enteredPassword, string storedHashedPassword)
    {
        return BCrypt.Verify(enteredPassword, storedHashedPassword);
    }
}

