using Sparc.Blossom.Authentication;

namespace Kodekit.Features;

public class User : BlossomUser
{
    public User()
    {
        Id = string.Empty;
        UserId = Id;
    }

    public string UserId { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
