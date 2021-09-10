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

        public override async Task<User> ExecuteAsync()
        {
            var claims = User.Claims;
            User user = new User();
            user.Id = User.Id();
            user.FirstName = claims.SingleOrDefault(x => x.Type == ClaimTypes.GivenName).Value;
            user.LastName = claims.SingleOrDefault(x => x.Type == ClaimTypes.Surname).Value;
            user.Email = claims.SingleOrDefault(x => x.Type == "emails").Value;
            return user;
        }
    }
}
