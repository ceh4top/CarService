using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CarService.DAL.Repositories;
using CarService.DAL.Models;

namespace CarService.PL.ViewModels
{
    public class StatisticsViewModal : BaseViewModel
    {
        public struct Element
        {
            public string Name { get; set; }
            public double Count { get; set; }
        }

        private Dictionary<string, string[]> properties;
        public string[] getProperties(string Name) => properties[Name];
        public string[] getPropertiesNames => properties.Keys.ToArray();

        private List<StatisticViewModel> statisticList;
        private ObservableCollection<StatisticViewModel> statistic;
        public ObservableCollection<StatisticViewModel> Statistic
        {
            get { return this.statistic; }
        }

        public StatisticsViewModal()
        {
            this.properties = new Dictionary<string, string[]>();
            this.properties.Add("Месяцы", new string[] { "Заказы", "Работы", "Деньги", "Марки", "Модели", "Клиенты" });
            this.properties.Add("Работы", new string[] { "Заказы", "Деньги", "Марки", "Модели" });
            this.properties.Add("Марки", new string[] { "Заказы", "Деньги", "Работы", "Модели" });
            this.properties.Add("Модели", new string[] { "Заказы", "Деньги", "Работы" });
            this.properties.Add("Клиенты", new string[] { "Заказы", "Деньги", "Работы", "Машины" });

            IEnumerable<Order> orders = EFUnitOfWork.I.Orders.GetAll();
            this.statisticList = orders.OrderBy(x => x.Id).Select(x => new StatisticViewModel(x)).ToList();
            this.statistic = new ObservableCollection<StatisticViewModel>(this.statisticList);
        }

        public List<Element> getList(string Type, string Subtype)
        {
            Dictionary<string, List<StatisticViewModel>> dictionary;
            switch (Type)
            {
                case "Месяцы":
                    dictionary = statistic.GroupBy(x => x.Month).ToDictionary(x => x.Key, s => s.ToList());
                    break;
                case "Работы":
                    List<string> workList = new List<string>();
                    foreach (StatisticViewModel item in statistic)
                        workList.AddRange(item.Works);
                    dictionary = workList.Distinct().ToDictionary(x => x, s => statistic.Where(c => c.Works.Any(p => p == s)).ToList());
                    break;
                case "Марки":
                    dictionary = statistic.GroupBy(x => x.Brand).ToDictionary(x => x.Key, s => s.ToList());
                    break;
                case "Модели":
                    dictionary = statistic.GroupBy(x => x.Model).ToDictionary(x => x.Key, s => s.ToList());
                    break;
                case "Клиенты":
                    dictionary = statistic.GroupBy(x => x.Clent).ToDictionary(x => x.Key, s => s.ToList());
                    break;
                default:
                    dictionary = new Dictionary<string, List<StatisticViewModel>>();
                    break;
            }

            List<Element> list;

            switch (Subtype)
            {
                case "Заказы":
                    list = dictionary.Select(x => new Element() { Name = x.Key, Count = x.Value.Count() }).ToList();
                    break;
                case "Работы":
                    list = dictionary.Select(x => new Element() { Name = x.Key, Count = x.Value.Sum(s => s.Works.Count()) }).ToList();
                    break;
                case "Деньги":
                    list = dictionary.Select(x => new Element() { Name = x.Key, Count = x.Value.Sum(s => s.Cost) }).ToList();
                    break;
                case "Марки":
                    list = dictionary.Select(x => new Element() { Name = x.Key, Count = x.Value.Select(s => s.Brand).Distinct().Count() }).ToList();
                    break;
                case "Машины":
                case "Модели":
                    list = dictionary.Select(x => new Element() { Name = x.Key, Count = x.Value.Select(s => s.Model).Distinct().Count() }).ToList();
                    break;
                case "Клиенты":
                    list = dictionary.Select(x => new Element() { Name = x.Key, Count = x.Value.Select(s => s.Clent).Distinct().Count() }).ToList();
                    break;
                default:
                    list = new List<Element>();
                    break;
            }

            return list;
        }
    }
}
