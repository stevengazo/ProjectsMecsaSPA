using DocumentFormat.OpenXml.Bibliography;
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
        public DbSet<BillFile> BillFiles { get; set; }
        public DbSet<Company> Company { get; set; }

        public DbSet<Device> Devices { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<SchDev> Schedule_Device { get; set; }
        public DbSet<SchEmpl> Schedule_Employee { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Lead> Leads { get; set; }
        public DbSet<LeadRequest> LeadsRequest { get; set; }
        public DbSet<LeadState> LeadsState { get; set; }
        public DbSet<LeadOrigin> LeadOrigins { get; set; }
        public DbSet<LeadNotes> LeadNotes { get; set; }

        public ProjectsDBContext(DbContextOptions contextOptions) : base(contextOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Company company = new Company()
            {
                CompanyId = 1,
                CompanyName = "Default"
            };
            modelBuilder.Entity<Company>().HasData(company);

            Seller seller = new()
            {
                SellerId = 1,
                SellerName = "Sample",
                Email = "",
                PhoneNumber = "1234567890",
                Bitrix24Id = 1

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

            modelBuilder.Entity<LeadOrigin>().HasData(
       new LeadOrigin
       {
           IsDeleted = false,
           LeadOriginId = 1,
           LeadOriginName = "WhatsApp"
       },
       new LeadOrigin
       {
           IsDeleted = false,
           LeadOriginId = 2,
           LeadOriginName = "Facebook"
       },
       new LeadOrigin
       {
           IsDeleted = false,
           LeadOriginId = 3,
           LeadOriginName = "Instagram"
       },
       new LeadOrigin
       {
           IsDeleted = false,
           LeadOriginId = 4,
           LeadOriginName = "Sitio Web"
       },
       new LeadOrigin
       {
           IsDeleted = false,
           LeadOriginId = 5,
           LeadOriginName = "Llamada Telefónica"
       },
       new LeadOrigin
       {
           IsDeleted = false,
           LeadOriginId = 6,
           LeadOriginName = "Correo Electrónico"
       },
       new LeadOrigin
       {
           IsDeleted = false,
           LeadOriginId = 7,
           LeadOriginName = "Referencia / Recomendación"
       },
       new LeadOrigin
       {
           IsDeleted = false,
           LeadOriginId = 8,
           LeadOriginName = "LinkedIn"
       },
       new LeadOrigin
       {
           IsDeleted = false,
           LeadOriginId = 9,
           LeadOriginName = "Google Ads"
       },
       new LeadOrigin
       {
           IsDeleted = false,
           LeadOriginId = 10,
           LeadOriginName = "Evento / Feria"
       }
   );


            modelBuilder.Entity<LeadRequest>().HasData(
                new LeadRequest()
                {
                    IsDeleted = false,
                    LeadRequestId = 1,
                    RequestName = "pararrayos"
                }
                );
            modelBuilder.Entity<LeadState>().HasData(
                  new LeadState
                  {
                      IsDeleted = false,
                      LeadStateId = 1,
                      Name = "Atendido"
                  },
                  new LeadState
                  {
                      IsDeleted = false,
                      LeadStateId = 2,
                      Name = "Nuevo"
                  },
                  new LeadState
                  {
                      IsDeleted = false,
                      LeadStateId = 3,
                      Name = "Contactado"
                  },
                  new LeadState
                  {
                      IsDeleted = false,
                      LeadStateId = 4,
                      Name = "En Seguimiento"
                  },
                  new LeadState
                  {
                      IsDeleted = false,
                      LeadStateId = 5,
                      Name = "Cotización Enviada"
                  },
                  new LeadState
                  {
                      IsDeleted = false,
                      LeadStateId = 6,
                      Name = "Negociación"
                  },
                  new LeadState
                  {
                      IsDeleted = false,
                      LeadStateId = 7,
                      Name = "Ganado"
                  },
                  new LeadState
                  {
                      IsDeleted = false,
                      LeadStateId = 8,
                      Name = "Perdido"
                  },
                  new LeadState
                  {
                      IsDeleted = false,
                      LeadStateId = 9,
                      Name = "No Calificado"
                  },
                  new LeadState
                  {
                      IsDeleted = false,
                      LeadStateId = 10,
                      Name = "Pendiente de Respuesta"
                  }
              );


            modelBuilder.Entity<State>().HasData(states);
        }
    }
}
