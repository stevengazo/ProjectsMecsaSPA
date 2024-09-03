using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Identity;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            List<IdentityRole> roles = new List<IdentityRole>()
           {
               new()
               {
                   Id="1",
                   Name="administrador",
                   NormalizedName="ADMINISTRADOR",
                   ConcurrencyStamp= Guid.NewGuid().ToString(),
               },
                new()
               {
                   Id="2",
                   Name="ingeniero",
                   NormalizedName="INGENIERO",
                   ConcurrencyStamp= Guid.NewGuid().ToString(),
               },
               new()
               {
                   Id="3",
                   Name="asistente",
                   NormalizedName="asistente",
                   ConcurrencyStamp= Guid.NewGuid().ToString(),
               },
                new()
               {
                   Id="4",
                   Name="vendedor",
                   NormalizedName="VENDEDOR",
                   ConcurrencyStamp= Guid.NewGuid().ToString(),
               },
                new()
               {
                   Id="5",
                   Name="contabilidad",
                   NormalizedName="CONTABILIDAD",
                   ConcurrencyStamp= Guid.NewGuid().ToString(),
               }

           };
            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Agregar usuario por defecto
            var user = new UserIdentityEx
            {
                Id = "1",
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@admin.com",
                PhoneNumber = "",
                Name = "Administrator",
                LastName = "",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Admin@123"), // Contraseña por defecto
                SecurityStamp = Guid.NewGuid().ToString()
            };

            modelBuilder.Entity<UserIdentityEx>().HasData(user);

            // Asignar roles al usuario por defecto
            var userRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = "1",
                    RoleId = "1" // Administrador
                }
            };

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }

}
