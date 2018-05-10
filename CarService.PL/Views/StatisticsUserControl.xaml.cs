using System.Windows.Controls;

namespace CarService.PL.Views
{
    /// <summary>
    /// Логика взаимодействия для StatisticsUserControl.xaml
    /// </summary>
    public partial class StatisticsUserControl : UserControl
    {
        private static StatisticsUserControl instance;
        public static StatisticsUserControl I => instance ?? (instance = new StatisticsUserControl());

        protected StatisticsUserControl()
        {
            InitializeComponent();
        }
    }
}
