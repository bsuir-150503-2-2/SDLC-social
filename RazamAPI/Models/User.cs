using Microsoft.AspNetCore.Identity;

namespace razam.Models
{
    public class User : IdentityUser
    {
        public Profile Profile { get; set; }
    }
}
