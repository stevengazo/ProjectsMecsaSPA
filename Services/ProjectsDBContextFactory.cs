using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProjectsMecsaSPA.Data;

namespace ProjectsMecsaSPA.Services
{
    public class ProjectsDBContextFactory : IDesignTimeDbContextFactory<ProjectsDBContext>
    {
        public ProjectsDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProjectsDBContext>();

            // Aquí estamos construyendo la configuración manualmente para el tiempo de diseño
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ProjectsConnection");
            optionsBuilder.UseSqlServer(connectionString);

            return new ProjectsDBContext(optionsBuilder.Options);
        }
    }
}
