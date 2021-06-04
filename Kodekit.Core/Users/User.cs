using System;
using System.Collections.Generic;
using Sparc.Core;

namespace Kodekit.Core
{
    public class User : Root<string>
    {
        public User()
        {
            Id = Guid.NewGuid().ToString();
            UserId = Id;
            //Kits = new List<Kit>();
        }

        public string UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Kit> Kits { get; set; }
    }
}
