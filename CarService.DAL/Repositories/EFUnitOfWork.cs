using System;
using System.Linq;
using CarService.DAL.Interfaces;
using CarService.DAL.Models;
using CarService.DAL.EF;

namespace CarService.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private static EFUnitOfWork instance;
        public static EFUnitOfWork I => instance ?? (instance = new EFUnitOfWork());

        private Context DB;

        protected EFUnitOfWork()
        {
            this.DB = new Context();
        }

        private Repository<Car> cars;
        public IRepository<Car> Cars => cars ?? (cars = new Repository<Car>(DB));

        private Repository<CarBrand> carBrands;
        public IRepository<CarBrand> CarBrands => carBrands ?? (carBrands = new Repository<CarBrand>(DB));

        private Repository<CarModel> carModels;
        public IRepository<CarModel> CarModels => carModels ?? (carModels = new Repository<CarModel>(DB));

        private Repository<CarOwner> carOwners;
        public IRepository<CarOwner> CarOwners => carOwners ?? (carOwners = new Repository<CarOwner>(DB));

        private Repository<CarTransmissionType> carTransmissionTypes;
        public IRepository<CarTransmissionType> CarTransmissionTypes => carTransmissionTypes ?? (carTransmissionTypes = new Repository<CarTransmissionType>(DB));

        private Repository<Order> orders;
        public IRepository<Order> Orders => orders ?? (orders = new Repository<Order>(DB));

        private Repository<OrderWorks> orderWorks;
        public IRepository<OrderWorks> OrderWorks => orderWorks ?? (orderWorks = new Repository<OrderWorks>(DB));

        private Repository<Work> works;
        public IRepository<Work> Works => works ?? (works = new Repository<Work>(DB));

        public void Save()
        {
            this.DB.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.DB.SaveChanges();
                    this.DB.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
