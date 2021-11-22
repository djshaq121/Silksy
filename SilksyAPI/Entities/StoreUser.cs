using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SilksyAPI.Entities
{
    public class StoreUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<UserAddress> Addresses { get; set; }
    }
}
