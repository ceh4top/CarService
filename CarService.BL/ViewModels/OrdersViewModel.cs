using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using CarService.DAL.Models;
using CarService.DAL.Repositories;

namespace CarService.BL.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private ObservableCollection<OrderViewModel> orders;
        public ObservableCollection<OrderViewModel> Orders
        {
            get { return this.orders; }
        }

        private OrderViewModel selectOrder;
        public OrderViewModel SelectedOrder
        {
            get { return selectOrder; }
            set
            {
                selectOrder = value;
                OnPropertyChanged("SelectedOrder");
            }
        }

        private string[] propertiesNames;
        public string[] getPropertiesNames() => propertiesNames;

        public OrdersViewModel()
        {
            LoadOrders();
            propertiesNames = new string[] { "Номер заказа", "Марка", "Модель", "Год выпуска авто", "Тип трансмиссии", "Мощность двигателя", "Наименование работ", "Начало", "Конец", "Стоимость" };
        }

        private void LoadOrders()
        {
            IEnumerable<Order> orders = EFUnitOfWork.I.Orders.GetAll();
            List<OrderViewModel> orderViewModels = orders.Select(x => new OrderViewModel(x)).ToList();
            this.orders = new ObservableCollection<OrderViewModel>(orderViewModels);
        }
        
        public object[] getFilterLoad(string FilterItem)
        {
            IEnumerable<object> Result;

            switch (FilterItem)
            {
                case "Марка":
                    Result = this.Orders.Select(x => x.Brand);
                    break;
                case "Модель":
                    Result = this.Orders.Select(x => x.Model);
                    break;
                case "Год выпуска авто":
                    Result = this.Orders.Select(x => x.YearOfManufactureSort);
                    break;
                case "Тип трансмиссии":
                    Result = this.Orders.Select(x => x.TransmissionType);
                    break;
                case "Мощность двигателя":
                    Result = this.Orders.Select(x => (object)x.EnginePower);
                    break;
                case "Наименование работ":
                    List<string> Temp = new List<string>();
                    foreach (List<string> list in this.Orders.Select(x => x.WorksSort))
                        foreach (string element in list)
                            Temp.Add(element);
                    Result = Temp;
                    break;
                case "Начало":
                    Result = this.Orders.Select(x => x.StartSort);
                    break;
                case "Конец":
                    Result = this.Orders.Select(x => x.EndSort);
                    break;
                case "Стоимость":
                    Result = this.Orders.Select(x => (object)x.Cost);
                    break;
                default:
                    Result = this.Orders.Select(x => (object)x.Id);
                    break;
            }

            Result = Result.Distinct();
            int Count = Result.Count();

            if (Count > 0 && Result.First() is string)
            {
                Result = Result.OrderBy(x => (string)x);
            }
            else if (Count > 0)
            {
                Result = Result.OrderBy(x => (int)x);
            }

            return Result.ToArray();
        }

        public void UpdateOrders(string[,] Property)
        {
            LoadOrders();
            List<OrderViewModel> orders;
            for(int i = 0; i < Property.GetLength(0); ++i)
            {
                if (Property[i, 1] == null) continue;

                switch (Property[i, 0])
                {
                    case "Марка":
                        switch(i)
                        {
                            case 0:
                                orders = ((Property[i, 1] == "По возрастанию")
                                    ? this.orders.OrderBy(x => x.Brand)
                                    : this.orders.OrderByDescending(x => x.Brand))
                                    .ToList(); break;
                            case 1: orders = this.orders.Where(x => x.Brand == Property[i, 1]).ToList(); break;
                            default: orders = this.orders.Where(x => x.Brand.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Модель":
                        switch (i)
                        {
                            case 0:
                                orders = ((Property[i, 1] == "По возрастанию")
                                    ? this.orders.OrderBy(x => x.Model)
                                    : this.orders.OrderByDescending(x => x.Model))
                                    .ToList(); break;
                            case 1: orders = this.orders.Where(x => x.Model == Property[i, 1]).ToList(); break;
                            default: orders = this.orders.Where(x => x.Model.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Год выпуска авто":
                        switch (i)
                        {
                            case 0:
                                orders = ((Property[i, 1] == "По возрастанию")
                                    ? this.orders.OrderBy(x => x.YearOfManufactureSort)
                                    : this.orders.OrderByDescending(x => x.YearOfManufactureSort))
                                    .ToList(); break;
                            case 1: orders = this.orders.Where(x => x.YearOfManufacture == Property[i, 1]).ToList(); break;
                            default: orders = this.orders.Where(x => x.YearOfManufacture.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Тип трансмиссии":
                        switch (i)
                        {
                            case 0:
                                orders = ((Property[i, 1] == "По возрастанию")
                                    ? this.orders.OrderBy(x => x.TransmissionType)
                                    : this.orders.OrderByDescending(x => x.TransmissionType))
                                    .ToList(); break;
                            case 1: orders = this.orders.Where(x => x.TransmissionType == Property[i, 1]).ToList(); break;
                            default: orders = this.orders.Where(x => x.TransmissionType.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Мощность двигателя":
                        switch (i)
                        {
                            case 0:
                                orders = ((Property[i, 1] == "По возрастанию")
                                    ? this.orders.OrderBy(x => x.EnginePower)
                                    : this.orders.OrderByDescending(x => x.EnginePower))
                                    .ToList(); break;
                            default: orders = this.orders.Where(x => x.EnginePower == Convert.ToInt32(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Наименование работ":
                        switch (i)
                        {
                            case 0:
                                orders = ((Property[i, 1] == "По возрастанию")
                                    ? this.orders.OrderBy(x => x.Works)
                                    : this.orders.OrderByDescending(x => x.Works))
                                    .ToList(); break;
                            default: orders = this.orders.Where(x => x.Works.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Начало":
                        switch (i)
                        {
                            case 0:
                                orders = ((Property[i, 1] == "По возрастанию")
                                    ? this.orders.OrderBy(x => x.StartSort)
                                    : this.orders.OrderByDescending(x => x.StartSort))
                                    .ToList(); break;
                            default: orders = this.orders.Where(x => x.Start.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Конец":
                        switch (i)
                        {
                            case 0:
                                orders = ((Property[i, 1] == "По возрастанию")
                                    ? this.orders.OrderBy(x => x.EndSort)
                                    : this.orders.OrderByDescending(x => x.EndSort))
                                    .ToList(); break;
                            default: orders = this.orders.Where(x => x.End.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Стоимость":
                        switch (i)
                        {
                            case 0:
                                orders = ((Property[i, 1] == "По возрастанию")
                                    ? this.orders.OrderBy(x => x.Cost)
                                    : this.orders.OrderByDescending(x => x.Cost))
                                    .ToList(); break;
                            default: orders = this.orders.Where(x => x.Cost == Convert.ToInt32(Property[i, 1])).ToList(); break;
                        }
                        break;
                    default:
                        switch (i)
                        {
                            case 0:
                                orders = ((Property[i, 1] == "По возрастанию")
                                    ? this.orders.OrderBy(x => x.Id)
                                    : this.orders.OrderByDescending(x => x.Id))
                                    .ToList(); break;
                            default: orders = this.orders.Where(x => x.Id == Convert.ToInt32(Property[i, 1])).ToList(); break;
                        }
                        break;
                }

                this.orders = new ObservableCollection<OrderViewModel>(orders);
            }
        }
    }
}
