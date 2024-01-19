using System.Security.Claims;

namespace Kodekit
{
    public class UserRepository
    {
        public UserRepository(IRepository<User> users)
        {
            Users = users;
        }
        public IRepository<User> Users { get; }

        public ClaimsPrincipal User { get; }

        // CREATE
        public async Task<bool> CreateUserAsync()
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

        // GET
        public Task<User> GetUserAsync()
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

        // UPDATE
        public async Task<bool> UpdateUserAsync(User user)
        {
            try
            {
                await Users.UpdateAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
