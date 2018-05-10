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
using System.Windows.Shapes;

namespace CarService.PL.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow instance;
        public static MainWindow I => instance ?? (instance = new MainWindow());

        public void ChangeView(UserControl UC)
        {
            CurrentView.Content = UC;
        }

        public MainWindow()
        {
            if (instance == null) instance = this;

            InitializeComponent();
            ChangeView(OrdersUserControl.I);
        }
    }
}
