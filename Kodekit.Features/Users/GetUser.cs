using Sparc.Core;
using Sparc.Features;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Kodekit.Features
{
    public class GetUser : PublicFeature<User>
    {
        public GetUser(IRepository<User> users)
        {
            Users = users;
        }

        public IRepository<User> Users { get; }

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
}
