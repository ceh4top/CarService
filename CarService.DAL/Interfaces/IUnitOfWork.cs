using System;
using CarService.DAL.Models;

namespace CarService.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<CarModel> CarModels { get; }
        IRepository<CarBrand> CarBrands { get; }
        IRepository<CarTransmissionType> CarTransmissionTypes { get; }
        IRepository<Human> Humans { get; }
        IRepository<CarOwner> CarOwners { get; }
        IRepository<Car> Cars { get; }
        IRepository<Order> Orders { get; }
        IRepository<Work> Works { get; }
        IRepository<OrderWorks> OrderWorks { get; }

        void Save();
    }
}
