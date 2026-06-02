using OrderBurger.API.Enums;

namespace OrderBurger.API.Models;

public class User
{
    public Guid Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.Employee;

    public User() { }

    public User(string userName, string email, string passwordHash, UserRole role)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
}
