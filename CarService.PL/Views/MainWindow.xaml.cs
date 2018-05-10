using System.Windows;
using System.Windows.Controls;

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
