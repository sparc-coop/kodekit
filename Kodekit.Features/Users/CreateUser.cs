using System.Linq;
using System.Threading.Tasks;
using Sparc.Authentication.AzureADB2C;
using Sparc.Features;
using Sparc.Core;
using Kodekit.Core;
using Microsoft.EntityFrameworkCore;

namespace Kodekit.Features
{
    public class CreateUser : Feature<string>
    {
        public CreateUser(IRepository<User> users)
        {
            Users = users;
        }

        public IRepository<User> Users { get; }

        public override async Task<string> ExecuteAsync()
        {
            return "test123";
            //try
            //{
            //    User user =  new User();
            //    user.FirstName = "NewTest";// User.Identity.Name;
            //    user.LastName = "Last";
            //    user.Email = "test@gmail.com";
            //    await Users.AddAsync(user);
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
        }
    }
}
