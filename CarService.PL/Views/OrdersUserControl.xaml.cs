using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarService.BL.ViewModels;

namespace CarService.PL.Views
{
    /// <summary>
    /// Логика взаимодействия для OrdersUserControl.xaml
    /// </summary>
    public partial class OrdersUserControl : UserControl
    {
        private static OrdersUserControl instance;
        public static OrdersUserControl I => instance ?? (instance = new OrdersUserControl());

        OrdersViewModel OrdersVM;

        string[,] SortFilterSearch;

        protected OrdersUserControl()
        {
            OrdersVM = new OrdersViewModel();

            this.InitializeComponent();
            this.DataContext = OrdersVM;

            this.SortList.ItemsSource = this.OrdersVM.getPropertiesNames();
            this.SortList.SelectedIndex = 0;

            this.FilterList.ItemsSource = this.OrdersVM.getPropertiesNames();
            this.FilterList.SelectedIndex = 0;

            this.SearchList.ItemsSource = this.OrdersVM.getPropertiesNames();
            this.SearchList.SelectedIndex = 0;

            this.SortFilterSearch = new string[3, 2];
            this.SortFilterSearch[0, 0] = (string)this.SortList.SelectedItem;
            this.SortFilterSearch[1, 0] = (string)this.FilterList.SelectedItem;
            this.SortFilterSearch[2, 0] = (string)this.SearchList.SelectedItem;
        }

        private void FilterLoad(object sender, SelectionChangedEventArgs e)
        {
            ComboBox senderCB = (ComboBox)sender;
            this.Filter.ItemsSource = this.OrdersVM.getFilterLoad((string) senderCB.SelectedItem);
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch (button.Name)
            {
                case "SortClear":
                    this.SortFilterSearch[0, 1] = null;
                    this.Sort.SelectedItem = null;
                    break;
                case "FilterClear":
                    this.SortFilterSearch[1, 1] = null;
                    this.Filter.SelectedItem = null;
                    break;
                case "SearchClear":
                    this.SortFilterSearch[2, 1] = null;
                    this.Search.Text = "";
                    break;
            }
            UpdateData();
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            switch(button.Name)
            {
                case "SortButton":
                    this.SortFilterSearch[0, 0] = (string)this.SortList.SelectedItem;
                    this.SortFilterSearch[0, 1] = ((TextBlock)Sort.SelectedItem).Text;
                    break;
                case "FilterButton":
                    this.SortFilterSearch[1, 0] = (string)this.FilterList.SelectedItem;
                    this.SortFilterSearch[1, 1] = Filter.SelectedItem.ToString();
                    break;
                case "SearchButton":
                    this.SortFilterSearch[2, 0] = (string)this.SearchList.SelectedItem;
                    this.SortFilterSearch[2, 1] = Search.Text;
                    break;
            }
            UpdateData();
        }

        private void UpdateData()
        {
            this.OrdersVM.UpdateOrders(this.SortFilterSearch);
            this.Table.DataContext = null;
            this.Table.DataContext = this.OrdersVM;
        }
    }
}
