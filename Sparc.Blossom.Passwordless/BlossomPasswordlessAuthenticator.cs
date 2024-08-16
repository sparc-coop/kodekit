using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sparc.Blossom.Authentication;
using Sparc.Blossom.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sparc.Blossom.Passwordless
{
    public class BlossomPasswordlessAuthenticator<T> : BlossomDefaultAuthenticator<T>
        where T : BlossomUser, new()
    {
        public BlossomPasswordlessAuthenticator(
            IRepository<T> users,
            ILoggerFactory loggerFactory,
            IServiceScopeFactory scopeFactory,
            PersistentComponentState state)
            : base(users, loggerFactory, scopeFactory, state)
        {
        }

        public override async Task<BlossomUser?> GetAsync(ClaimsPrincipal principal)
        {
            if (principal?.Identity?.IsAuthenticated != true)
            {
                var user = new T();
                await Users.AddAsync(user);
                User = user;
                LoginState = LoginStates.LoggedIn;
                return user;
            }

            User = await Users.FindAsync(principal.Id());
            if (User != null)
                LoginState = LoginStates.LoggedIn;
            return User;
        }

        public async IAsyncEnumerable<LoginStates> LoginAsync(string? emailOrToken = null)
        {
            var test = User;
            
            LoginState = LoginStates.LoggedIn;
            yield return LoginState;
        }
    }
}
