using System;
using System.Threading.Tasks;
using Sparc.Features;
using Sparc.Core;
using Kodekit.Core;

namespace Kodekit.Features.Users
{
    public class CreateUser : Feature<bool>
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
                User user =  new User();
                user.FirstName = User.Identity.Name;

                await Users.AddAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
