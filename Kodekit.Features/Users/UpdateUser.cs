using System;
using System.Threading.Tasks;
using Sparc.Features;
using Sparc.Core;

namespace Kodekit.Features
{
    public class UpdateUser : Feature<User, bool>
    {
        public UpdateUser(IRepository<User> users)
        {
            Users = users;
        }

        public IRepository<User> Users { get; }

        public override async Task<bool> ExecuteAsync(User user)
        {
            try
            {
                await Users.UpdateAsync(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
