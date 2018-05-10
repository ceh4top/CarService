using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarService.DAL.Models;

namespace CarService.BL.ViewModels
{
    public class CarOwnerViewModel : BaseViewModel
    {
        CarOwner carOwner;

        public CarOwnerViewModel()
        {
            this.carOwner = new CarOwner();
        }

        public CarOwnerViewModel(CarOwner carOwner)
        {
            this.carOwner = carOwner;
        }

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
