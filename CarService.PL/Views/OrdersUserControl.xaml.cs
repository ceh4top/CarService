using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CarService.PL.ViewModels;

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

            LinesCount.Text = "9";
            UpdateDataPage();
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

                    if (Sort.SelectedItem == null) MessageBox.Show("Выберете порядок сортировки");
                    else this.SortFilterSearch[0, 1] = ((TextBlock)Sort.SelectedItem).Text;

                    break;
                case "FilterButton":
                    this.SortFilterSearch[1, 0] = (string)this.FilterList.SelectedItem;

                    if (Filter.SelectedItem == null) MessageBox.Show("Выберете значение для фильтрации");
                    else this.SortFilterSearch[1, 1] = Filter.SelectedItem.ToString();
                    
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
            UpdateDataPage();
        }

        private bool ChangeLinesCount = false;
        private int LinesCountInt = 9;
        private int PageNumberInt = 1;
        private void NumberOnly(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            e.Handled = !(Char.IsDigit(e.Text, 0));
            if (!e.Handled)
                e.Handled = (textBox.Text.Length > 5);

            if (textBox.Name == "LinesCount")
                ChangeLinesCount = true;
        }

        private void CheckKey(object sender, KeyEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            switch(e.Key)
            {
                case Key.Return:
                    UnfocusElement.Focus();
                    return;
            }
        }

        private void LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateDataPage();
        }

        private void CheckCount()
        {
            LinesCount.Text = (LinesCount.Text == "") ? "0" : LinesCount.Text;

            int LinesCountInt = this.LinesCountInt;

            try
            {
                LinesCountInt = Convert.ToInt32(LinesCount.Text);
                if (LinesCountInt > 999999)
                {
                    MessageBox.Show("Число должно быть меньше 1000000!");
                    LinesCount.Text = this.LinesCountInt.ToString();
                    return;
                }
            } catch
            {
                MessageBox.Show("В поле \"Количество элементов на странице\" должно быть введено только целочисленное значение!");
                LinesCount.Text = this.LinesCountInt.ToString();
                return;
            }

            if (LinesCountInt < this.LinesCountInt && !ChangeLinesCount)
            {
                LinesCountInt = this.LinesCountInt;
                LinesCount.Text = LinesCountInt.ToString();
            }
            else if (ChangeLinesCount)
                this.LinesCountInt = LinesCountInt;

            if (LinesCountInt > OrdersVM.Count)
            {
                LinesCountInt = OrdersVM.Count;
                LinesCount.Text = LinesCountInt.ToString();
            }

            PageNumber.Text = (PageNumber.Text == "") ? "0" : PageNumber.Text;

            int PageNumberInt = 1;

            try
            {
                PageNumberInt = Convert.ToInt32(PageNumber.Text);
                if (PageNumberInt > 999999)
                {
                    MessageBox.Show("Число должно быть меньше 1000000!");
                    PageNumber.Text = this.PageNumberInt.ToString();
                    return;
                }
            }
            catch
            {
                MessageBox.Show("В поле \"Номер страницы\" должно быть введено только целочисленное значение!");
                PageNumber.Text = this.PageNumberInt.ToString();
                return;
            }

            this.PageNumberInt = PageNumberInt;

            int PagesCountInt = (int)Math.Ceiling((double)OrdersVM.Count / ((LinesCountInt == 0) ? 1 : LinesCountInt));

            if (PageNumberInt < 1 && PagesCountInt >= 1)
            {
                PageNumberInt = 1;
                PageNumber.Text = PageNumberInt.ToString();
            }

            if (PageNumberInt > PagesCountInt)
            {
                PageNumberInt = PagesCountInt;
                PageNumber.Text = PageNumberInt.ToString();
            }

            PagesCount.Text = PagesCountInt.ToString();
        }

        private void UpdateDataPage()
        {
            CheckCount();

            int LinesCountInt = Convert.ToInt32(LinesCount.Text);
            int PageNumberInt = Convert.ToInt32(PageNumber.Text);

            this.Table.DataContext = null;
            this.OrdersVM.UpdateOrdersPage(LinesCountInt, PageNumberInt);
            this.Table.DataContext = this.OrdersVM;

            ChangeLinesCount = false;
        }

        private void GoToStatistics(object sender, RoutedEventArgs e)
        {
            MainWindow.I.ChangeView(StatisticsUserControl.I);
        }
    }
}
