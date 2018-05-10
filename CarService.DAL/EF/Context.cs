using System.Data.Entity;
using CarService.DAL.Models;

namespace CarService.DAL.EF
{
    public class Context : DbContext
    {
        public Context() : base("name=CarService") {}

        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarTransmissionType> CarTransmissionTypes { get; set; }
        public DbSet<Human> Humens { get; set; }
        public DbSet<CarOwner> CarOwners { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<OrderWorks> OrderWorks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            modelBuilder.Entity<OrderWorks>().HasKey(x => new { x.OrderId, x.WorkId });
            base.OnModelCreating(modelBuilder);
        }
    }
}
