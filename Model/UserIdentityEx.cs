using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.Pkcs;

namespace ProjectsMecsaSPA.Model
{
    public class UserIdentityEx : IdentityUser
    {
        public int DNI { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
