using Microsoft.AspNetCore.Identity;

namespace SilksyAPI.Entities
{
    public class StoreUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
