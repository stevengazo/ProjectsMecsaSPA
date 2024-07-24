using Microsoft.EntityFrameworkCore;
using ProjectsMecsaSPA.Model;

namespace ProjectsMecsaSPA.Data
{
    public class ProjectsDBContext : DbContext
    {
        public DbSet<Commentary> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberType> MemberTypes { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectMember> MembersMemberships { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<TypeModel> Types { get; set; }
        public DbSet<Seller> Seller { get; set; }

        public ProjectsDBContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<TypeModel> typeModels = new List<TypeModel>() { 
                new TypeModel()
                {
                    TypeId=1,
                    Name="No Establecido",
                    IsDeleted=false,
                }
            };
            modelBuilder.Entity<TypeModel>().HasData(typeModels);
            List<State> states = new List<State>()
            {
                new()
                {
                    StateId=1,
                    StateName="Pendiente",
                    SendNotification=false,
                    IsDeleted=false
                },
                new()
                {
                    StateId=2,
                    StateName="Coordinado",
                    SendNotification=false,
                    IsDeleted=false
                },
                new()
                {
                    StateId=3,
                    StateName="Requisición",
                    SendNotification=false,
                    IsDeleted=false
                },
                new()
                {
                    StateId=4,
                    StateName="En ejecucion",
                    SendNotification=false,
                    IsDeleted=false
                },
                new()
                {
                    StateId=6,
                    StateName="Informe",
                    SendNotification=false,
                    IsDeleted=false
                },
                new()
                {
                    StateId=5,
                    StateName="Revisión",
                    SendNotification=false,
                    IsDeleted=false
                }
            };
            modelBuilder.Entity<State>().HasData(states);
        }
    }
}
