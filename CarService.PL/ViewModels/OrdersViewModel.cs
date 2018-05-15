using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using CarService.DAL.Models;
using CarService.DAL.Repositories;

namespace CarService.PL.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private List<OrderViewModel> ordersList;
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
            this.ordersList = orders.OrderBy(x => x.Id).Select(x => new OrderViewModel(x)).ToList();
            this.orders = new ObservableCollection<OrderViewModel>(this.ordersList);
        }

        public int Count => this.ordersList.Count;
        
        public object[] getFilterLoad(string FilterItem)
        {
            IEnumerable<object> Result;

            switch (FilterItem)
            {
                case "Марка":
                    Result = this.ordersList.Select(x => x.Brand);
                    break;
                case "Модель":
                    Result = this.ordersList.Select(x => x.Model);
                    break;
                case "Год выпуска авто":
                    Result = this.ordersList.Select(x => x.YearOfManufactureSort);
                    break;
                case "Тип трансмиссии":
                    Result = this.ordersList.Select(x => x.TransmissionType);
                    break;
                case "Мощность двигателя":
                    Result = this.ordersList.Select(x => (object)x.EnginePower);
                    break;
                case "Наименование работ":
                    List<string> Temp = new List<string>();
                    foreach (List<string> list in this.ordersList.Select(x => x.WorksSort))
                        foreach (string element in list)
                            Temp.Add(element);
                    Result = Temp;
                    break;
                case "Начало":
                    Result = this.ordersList.Select(x => x.StartSort);
                    break;
                case "Конец":
                    Result = this.ordersList.Select(x => x.EndSort);
                    break;
                case "Стоимость":
                    Result = this.ordersList.Select(x => (object)x.CostSort);
                    break;
                default:
                    Result = this.ordersList.Select(x => (object)x.Id);
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

        public void UpdateOrdersPage(int CountItems, int PageNumber)
        {
            int Start = CountItems * (PageNumber - 1);
            int End = CountItems * PageNumber;

            List<OrderViewModel> list = new List<OrderViewModel>();
            for (int i = Start; i < End && i < this.Count; i++)
                list.Add(ordersList[i]);

            this.orders = new ObservableCollection<OrderViewModel>(list);
        }

        public void UpdateOrders(string[,] Property)
        {
            LoadOrders();
            for(int i = 0; i < Property.GetLength(0); ++i)
            {
                if (Property[i, 1] == null) continue;

                switch (Property[i, 0])
                {
                    case "Марка":
                        switch(i)
                        {
                            case 0:
                                this.ordersList = ((Property[i, 1] == "По возрастанию")
                                    ? this.ordersList.OrderBy(x => x.Brand)
                                    : this.ordersList.OrderByDescending(x => x.Brand))
                                    .ToList(); break;
                            case 1: this.ordersList = this.ordersList.Where(x => x.Brand == Property[i, 1]).ToList(); break;
                            default: this.ordersList = this.ordersList.Where(x => x.Brand.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Модель":
                        switch (i)
                        {
                            case 0:
                                this.ordersList = ((Property[i, 1] == "По возрастанию")
                                    ? this.ordersList.OrderBy(x => x.Model)
                                    : this.ordersList.OrderByDescending(x => x.Model))
                                    .ToList(); break;
                            case 1: this.ordersList = this.ordersList.Where(x => x.Model == Property[i, 1]).ToList(); break;
                            default: this.ordersList = this.ordersList.Where(x => x.Model.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Год выпуска авто":
                        switch (i)
                        {
                            case 0:
                                this.ordersList = ((Property[i, 1] == "По возрастанию")
                                    ? this.ordersList.OrderBy(x => x.YearOfManufactureSort)
                                    : this.ordersList.OrderByDescending(x => x.YearOfManufactureSort))
                                    .ToList(); break;
                            case 1: this.ordersList = this.ordersList.Where(x => x.YearOfManufacture == Property[i, 1]).ToList(); break;
                            default: this.ordersList = this.ordersList.Where(x => x.YearOfManufacture.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Тип трансмиссии":
                        switch (i)
                        {
                            case 0:
                                this.ordersList = ((Property[i, 1] == "По возрастанию")
                                    ? this.ordersList.OrderBy(x => x.TransmissionType)
                                    : this.ordersList.OrderByDescending(x => x.TransmissionType))
                                    .ToList(); break;
                            case 1: this.ordersList = this.ordersList.Where(x => x.TransmissionType == Property[i, 1]).ToList(); break;
                            default: this.ordersList = this.ordersList.Where(x => x.TransmissionType.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Мощность двигателя":
                        switch (i)
                        {
                            case 0:
                                this.ordersList = ((Property[i, 1] == "По возрастанию")
                                    ? this.ordersList.OrderBy(x => x.EnginePower)
                                    : this.ordersList.OrderByDescending(x => x.EnginePower))
                                    .ToList(); break;
                            default: this.ordersList = this.ordersList.Where(x => x.EnginePower == Convert.ToInt32(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Наименование работ":
                        switch (i)
                        {
                            case 0:
                                this.ordersList = ((Property[i, 1] == "По возрастанию")
                                    ? this.ordersList.OrderBy(x => x.Works)
                                    : this.ordersList.OrderByDescending(x => x.Works))
                                    .ToList(); break;
                            default: this.ordersList = this.ordersList.Where(x => x.Works.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Начало":
                        switch (i)
                        {
                            case 0:
                                this.ordersList = ((Property[i, 1] == "По возрастанию")
                                    ? this.ordersList.OrderBy(x => x.StartSort)
                                    : this.ordersList.OrderByDescending(x => x.StartSort))
                                    .ToList(); break;
                            default: this.ordersList = this.ordersList.Where(x => x.Start.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Конец":
                        switch (i)
                        {
                            case 0:
                                this.ordersList = ((Property[i, 1] == "По возрастанию")
                                    ? this.ordersList.OrderBy(x => x.EndSort)
                                    : this.ordersList.OrderByDescending(x => x.EndSort))
                                    .ToList(); break;
                            default: this.ordersList = this.ordersList.Where(x => x.End.Contains(Property[i, 1])).ToList(); break;
                        }
                        break;
                    case "Стоимость":
                        switch (i)
                        {
                            case 0:
                                this.ordersList = ((Property[i, 1] == "По возрастанию")
                                    ? this.ordersList.OrderBy(x => x.CostSort)
                                    : this.ordersList.OrderByDescending(x => x.CostSort))
                                    .ToList(); break;
                            default: this.ordersList = this.ordersList.Where(x => x.CostSort == Convert.ToInt32(Property[i, 1])).ToList(); break;
                        }
                        break;
                    default:
                        switch (i)
                        {
                            case 0:
                                this.ordersList = ((Property[i, 1] == "По возрастанию")
                                    ? this.ordersList.OrderBy(x => x.Id)
                                    : this.ordersList.OrderByDescending(x => x.Id))
                                    .ToList(); break;
                            default: this.ordersList = this.ordersList.Where(x => x.Id == Convert.ToInt32(Property[i, 1])).ToList(); break;
                        }
                        break;
                }

                this.orders = new ObservableCollection<OrderViewModel>(this.ordersList);
            }
        }
    }
}
