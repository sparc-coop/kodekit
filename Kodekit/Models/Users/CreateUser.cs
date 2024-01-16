using Sparc.Blossom.Authentication;
using System.Security.Claims;

namespace Kodekit;

public class CreateUser : PublicFeature<bool>
{
    public CreateUser(IRepository<User> users)
    {
        Users = users;
    }

    public IRepository<User> Users { get; }

    public override async Task<bool> ExecuteAsync()
    {
        try
        {
            if (!UserExists())
            {
                var claims = User.Claims;
                User user = new User();

                user.Id = User.Id();
                user.FirstName = claims.Single(x => x.Type == ClaimTypes.GivenName).Value;
                user.LastName = claims.Single(x => x.Type == ClaimTypes.Surname).Value;
                user.Email = claims.Single(x => x.Type == "emails").Value;
                await Users.AddAsync(user);

                return true;
            }
        }
        catch
        {
            //return false;
        }
        return false;
    }

    private bool UserExists()
    {
        if (User.Id() != null)
        {
            if (Users.Query.Where(x => x.Id == User.Id()).Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
