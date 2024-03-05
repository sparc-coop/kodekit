using System.Security.Claims;

namespace Kodekit;

public class GetUser(IRepository<User> users) : Feature<User>
{
    public IRepository<User> Users { get; } = users;

    public override Task<User> ExecuteAsync()
    {
        var claims = User.Claims;
        User user = new()
        {
            Id = User.Id(),
            FirstName = claims.Single(x => x.Type == ClaimTypes.GivenName).Value,
            LastName = claims.Single(x => x.Type == ClaimTypes.Surname).Value,
            Email = claims.Single(x => x.Type == "emails").Value
        };

        return Task.FromResult(user);
    }
}
