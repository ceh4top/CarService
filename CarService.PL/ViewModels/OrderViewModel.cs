using System;
using System.Linq;
using System.Collections.Generic;
using CarService.DAL.Models;

namespace CarService.PL.ViewModels
{
    public class OrderViewModel : BaseViewModel
    {
        private Order order;

        public OrderViewModel()
        {
            this.order = new Order();
            this.carOwner = new CarOwner();
        }

        public OrderViewModel(Order order)
        {
            this.order = order;
            this.carOwner = this.order.Car.CarOwner;
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
                    return order.Start?.ToString("dd.MM.yyyy");
                else return "Не начата!";
            }
        }

        public string StartSort
        {
            get
            {
                if (order.Start != null)
                    return order.Start?.ToString("yyyy.MM.dd");
                else return "Не начата!";
            }
        }

        public string End
        {
            get
            {
                if (order.End != null)
                    return order.End?.ToString("dd.MM.yyyy");
                else return "Не готова!";
            }
        }

        public string EndSort
        {
            get
            {
                if (order.End != null)
                    return order.End?.ToString("yyyy.MM.dd");
                else return "Не начата!";
            }
        }

        public string Cost
        {
            get { return string.Format("{0:c}", order.Cost); }
        }

        public double CostSort
        {
            get { return order.Cost; }
        }

        CarOwner carOwner;

        public string FirstName
        {
            get { return carOwner.FirstName; }
            set
            {
                carOwner.FirstName = value;
                OnPropertyChanged("CarOwner.FirstName");
            }
        }

        public string LastName
        {
            get { return carOwner.LastName; }
            set
            {
                carOwner.LastName = value;
                OnPropertyChanged("CarOwner.LastName");
            }
        }

        public string MiddleName
        {
            get { return carOwner.MiddleName; }
            set
            {
                carOwner.MiddleName = value;
                OnPropertyChanged("CarOwner.MiddleName");
            }
        }

        public string YearOfBirth
        {
            get { return carOwner.YearOfBirth.ToString("dd.MM.yyyy"); }
        }

        public string PhoneNumber
        {
            get { return carOwner.PhoneNumber; }
            set
            {
                carOwner.PhoneNumber = value;
                OnPropertyChanged("CarOwner.PhoneNumber");
            }
        }
    }
}
