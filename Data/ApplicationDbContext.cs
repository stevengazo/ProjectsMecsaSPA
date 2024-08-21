using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectsMecsaSPA.Model;

namespace ProjectsMecsaSPA.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserIdentityEx>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
