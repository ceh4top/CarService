using System.Collections.Generic;
using System.Linq;
using CarService.DAL.Models;

namespace CarService.PL.ViewModels
{
    public class StatisticViewModel : BaseViewModel
    {
        Order order;

        public StatisticViewModel()
        {
            this.order = new Order();
        }

        public StatisticViewModel(Order order)
        {
            this.order = order;
        }

        public double Cost
        {
            get { return order.Cost; }
        }

        public List<string> Works
        {
            get { return order.WorksList.Select(x => x.Name).ToList(); }
        }

        public string Brand
        {
            get { return order.Car.Brand.Name; }
        }

        public string Model
        {
            get { return order.Car.Model.Name; }
        }

        public int CountWorks
        {
            get { return order.WorksList.Count; }
        }

        public string Month
        {
            get { return string.Format("{0:MMMM}", order.Start); }
        }

        public string Clent
        {
            get { return string.Format("{0} {1} {2}", order.Car.CarOwner.LastName, order.Car.CarOwner.FirstName, order.Car.CarOwner.MiddleName); }
        }
    }
}
