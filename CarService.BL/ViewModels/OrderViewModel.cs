using System;
using System.Linq;
using System.Collections.Generic;
using CarService.DAL.Models;

namespace CarService.BL.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        private Order order;
        private CarOwnerViewModel carOwner;

        public OrderViewModel()
        {
            this.order = new Order();
            this.carOwner = new CarOwnerViewModel();
        }

        public OrderViewModel(Order order)
        {
            this.order = order;
            this.carOwner = new CarOwnerViewModel(this.order.Car.CarOwner);
        }

        public CarOwnerViewModel CarOwner
        {
            get { return this.carOwner; }
        }

        public int Id
        {
            get { return order.Id; }
        }

        public string Brand
        {
            get { return order.Car.Brand.Name; }
        }

        public string Model
        {
            get { return order.Car.Model.Name; }
        }

        public string YearOfManufacture
        {
            get { return order.Car.YearOfManufacture.ToString("dd.MM.yyyy"); }
            set
            {
                order.Car.YearOfManufacture = DateTime.ParseExact(value, "dd.MM.yyyy", null);
                OnPropertyChanged("Car.YearOfManufacture");
            }
        }

        public string YearOfManufactureSort
        {
            get { return order.Car.YearOfManufacture.ToString("yyyy.MM.dd"); }
        }

        public string TransmissionType
        {
            get { return order.Car.TransmissionType.Name; }
        }

        public int EnginePower
        {
            get { return order.Car.EnginePower; }
            set
            {
                order.Car.EnginePower = value;
                OnPropertyChanged("Car.EnginePower");
            }
        }

        public string Works
        {
            get { return order.Works; }
        }

        public List<string> WorksSort
        {
            get { return order.WorksList.Select(x => x.Name).ToList(); }
        }

        public string Start
        {
            get
            {
                if (order.Start != null)
                    return order.Start?.ToString("dd.MM.yyyy HH:mm");
                else return "Не начата!";
            }
        }

        public string StartSort
        {
            get
            {
                if (order.Start != null)
                    return order.Start?.ToString("yyyy.MM.dd HH:mm");
                else return "Не начата!";
            }
        }

        public string End
        {
            get
            {
                if (order.End != null)
                    return order.End?.ToString("dd.MM.yyyy HH:mm");
                else return "Не готова!";
            }
        }

        public string EndSort
        {
            get
            {
                if (order.End != null)
                    return order.End?.ToString("yyyy.MM.dd HH:mm");
                else return "Не начата!";
            }
        }

        public double Cost
        {
            get { return order.Cost; }
        }
    }
}
