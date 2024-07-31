using Microsoft.AspNetCore.Identity;

namespace ProjectsMecsaSPA.Model
{
    public class UserIdentityEx : IdentityUser
    {
        public int DNI { get; set; }
    }
}
