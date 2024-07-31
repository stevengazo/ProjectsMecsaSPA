using Microsoft.EntityFrameworkCore;
using ProjectsMecsaSPA.Model;

namespace ProjectsMecsaSPA.Data
{
    public class ProjectsDBContext : DbContext
    {
        public DbSet<Commentary> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FileModel> Files { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<TypeModel> Types { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<Bill> Bill { get; set; }

        public ProjectsDBContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Seller seller = new()
            {
                SellerId = 1,
                SellerName = "Sample",
                Email = ""

            };
            modelBuilder.Entity<Seller>().HasData(seller);
            List<TypeModel> typeModels = new List<TypeModel>() {
                new TypeModel()
                {
                    TypeId = 1,
                    Name = "No Establecido",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 2,
                    Name = "Mantenimiento DDCE",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 3,
                    Name = "Mantenimiento Ionizante",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 4,
                    Name = "Mantenimiento Torre",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 5,
                    Name = "Instalación DDCE",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 6,
                    Name = "Instalación Ionizante",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 7,
                    Name = "Instalación Torre",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 8,
                    Name = "Instalación SPAT",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 9,
                    Name = "Instalación Supresores",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 10,
                    Name = "Certificación SPAT",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 11,
                    Name = "Eléctricos",
                    IsDeleted = false
                },
                new TypeModel()
                {
                    TypeId = 12,
                    Name = "Otros",
                    IsDeleted = false
                }
            };

            modelBuilder.Entity<TypeModel>().HasData(typeModels);

            Customer customer = new Customer()
            {
                CustomerId = 1,
                Name = "Default",
                Type = "Publico",
                DNI = 1
            };

            modelBuilder.Entity<Customer>().HasData(customer);

            List<State> states = new List<State>()
            {
                new()
                {
                    StateId = 1,
                    StateName = "Pendiente",
                    OrderPriority = 1,
                    IsDeleted = false
                },
                new()
                {
                    StateId = 2,
                    StateName = "Coordinado",
                    OrderPriority = 2,
                    IsDeleted = false
                },
                new()
                {
                    StateId = 3,
                    StateName = "En ejecución",
                    OrderPriority = 3,
                    IsDeleted = false
                },
                new()
                {
                    StateId = 4,
                    StateName = "Pendiente Informe",
                    OrderPriority = 4,
                    IsDeleted = false
                },
                new()
                {
                    StateId = 5,
                    StateName = "Informe en proceso",
                    OrderPriority = 5,
                    IsDeleted = false
                },
                new()
                {
                    StateId = 6,
                    StateName = "Ofertando",
                    OrderPriority = 6,
                    IsDeleted = false
                },
                new()
                {
                    StateId = 7,
                    StateName = "Finalizado",
                    OrderPriority = 7,
                    IsDeleted = false
                }
            };

            modelBuilder.Entity<State>().HasData(states);
        }
    }
}
