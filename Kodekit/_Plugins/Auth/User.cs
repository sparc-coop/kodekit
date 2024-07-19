using Sparc.Blossom.Authentication;

namespace Kodekit._Plugins.Auth;

public class User : BlossomUser
{
    public User() : base()
    {
        UserId = Id;
    }
    
    public string UserId { get; private set; }
    public string? Email { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
}
