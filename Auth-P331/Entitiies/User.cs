using Microsoft.AspNetCore.Identity;

namespace Auth_P331.Entitiies
{
    public class User : IdentityUser
    {
        public string Fullname { get; set; }
        public string Country { get; set; }
    }
}
