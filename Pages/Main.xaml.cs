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

namespace PermDynamics_Тепляков.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public MainWindow mainWindow;
        public Main(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void OpenPageChart(object sender, RoutedEventArgs e)
        {
            float value = Convert.ToInt32(tb_value.Text);
            mainWindow.pointsInfo.Add(new Classes.PointInfo(value));
            float value2 = Convert.ToInt32(tb_value2.Text);
            mainWindow.pointsInfo2.Add(new Classes.PointInfo2(value2));
            mainWindow.OpenPages(MainWindow.pages.chart);
        }
    }
}
