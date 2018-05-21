using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using CarService.PL.ViewModels;

namespace CarService.PL.Views
{
    /// <summary>
    /// Логика взаимодействия для StatisticsUserControl.xaml
    /// </summary>
    public partial class StatisticsUserControl : UserControl
    {
        private static StatisticsUserControl instance;
        public static StatisticsUserControl I => instance ?? (instance = new StatisticsUserControl());

        private StatisticsViewModel statistics;

        public StatisticsUserControl()
        {
            InitializeComponent();
            this.SeriesCollection = new SeriesCollection();
            this.ColumnSeriesCollection = new SeriesCollection();

            this.statistics = new StatisticsViewModel();

            this.TypeChart.ItemsSource = new string[] { "Круговая", "В виде столбцов" };
            this.TypeChart.SelectedIndex = 0;

            this.NamePoints.ItemsSource = this.statistics.getPropertiesNames;
            this.NamePoints.SelectedIndex = 0;

            this.ValuePoints.ItemsSource = this.statistics.getProperties((string) this.NamePoints.SelectedItem);
            this.ValuePoints.SelectedIndex = 0;

            this.SeriesCollection = new SeriesCollection();

            foreach (StatisticsViewModel.Element element in statistics
                .getList((string)this.NamePoints.SelectedItem, (string)this.ValuePoints.SelectedItem))
                this.SeriesCollection.Add(new PieSeries {
                    Title = element.Name,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(element.Count) },
                    DataLabels = true
                });
            
            foreach (StatisticsViewModel.Element element in statistics
                .getList((string)this.NamePoints.SelectedItem, (string)this.ValuePoints.SelectedItem))
                this.ColumnSeriesCollection.Add(new ColumnSeries
                {
                    Title = element.Name,
                    Values = new ChartValues<double> { element.Count },
                    DataLabels = true
                });
            
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        private void Apply(object sender, RoutedEventArgs e)
        {
            this.SeriesCollection.Clear();
            foreach (StatisticsViewModel.Element element in statistics
                .getList((string)this.NamePoints.SelectedItem, (string)this.ValuePoints.SelectedItem))
                this.SeriesCollection.Add(new PieSeries
                {
                    Title = element.Name,
                    Values = new ChartValues<ObservableValue> { new ObservableValue(element.Count) },
                    DataLabels = true
                });

            this.ColumnSeriesCollection.Clear();
            foreach (StatisticsViewModel.Element element in statistics
                .getList((string)this.NamePoints.SelectedItem, (string)this.ValuePoints.SelectedItem))
                this.ColumnSeriesCollection.Add(new ColumnSeries
                {
                    Title = element.Name,
                    Values = new ChartValues<double> { element.Count },
                    DataLabels = true
                });

            this.pieChart.Update();
            this.columnChart.Update();
        }

        public SeriesCollection SeriesCollection { get; set; }

        public SeriesCollection ColumnSeriesCollection { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void ChartClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }

        private void GoToData(object sender, RoutedEventArgs e)
        {
            MainWindow.I.ChangeView(OrdersUserControl.I);
        }

        private void SelectedType(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.Name == "NamePoints")
            {
                this.ValuePoints.ItemsSource = this.statistics.getProperties((string)this.NamePoints.SelectedItem);
                this.ValuePoints.SelectedIndex = 0;
            }
        }

        private void SelectChart(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            if (comboBox.Name == "TypeChart")
            {
                switch ((string)comboBox.SelectedItem)
                {
                    case "Круговая":
                        this.pieChart.Visibility = Visibility.Visible;
                        this.columnChart.Visibility = Visibility.Hidden;
                        break;
                    case "В виде столбцов":
                        this.pieChart.Visibility = Visibility.Hidden;
                        this.columnChart.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
    }
}
