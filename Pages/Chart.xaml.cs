using PermDynamics_Тепляков.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
using System.Windows.Threading;

namespace PermDynamics_Тепляков.Pages
{
    /// <summary>
    /// Логика взаимодействия для Chart.xaml
    /// </summary>
    public partial class Chart : Page
    {
        public double maxValue = 0;
        double averageValue = 0;
        private Line averageLine;
        private double AverageValue = 0;
        private double newValue = 0;

        public double maxValue2 = 0;
        double averageValue2 = 0;
        private Line averageLine2;
        private double AverageValue2 = 0;
        private double newValue2 = 0;

        public MainWindow mainWindow;
        public double actualHeightCanvas = 0;
        public DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public ChartContext chartContext;

        public Chart(MainWindow mainWindow, ChartContext _chartContext)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            chartContext = _chartContext;
            actualHeightCanvas = (mainWindow.Height / 2) - 50d;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Tick += CreateNewValue;
            dispatcherTimer.Start();
            CreateChart();
            ColorChart();
        }

        public void infoDataForSave(string name1, double value1, double avg1, string name2, double value2, double avg2)
        {
            var chartsData = new ChartsData
            {
                Name1 = name1,
                Value1 = value1,
                Avg1 = avg1,
                Name2 = name2,
                Value2 = value2,
                Avg2 = avg2
            };
            chartContext.ChartsData.Add(chartsData);
            chartContext.SaveChanges();
        }

        private void CreateNewValue(object sender, EventArgs e)
        {
            Random random = new Random();
            double value = mainWindow.pointsInfo[mainWindow.pointsInfo.Count - 1].value;
            newValue = value * (random.NextDouble() + 0.5d);
            double sum = mainWindow.pointsInfo.Sum(p => p.value);
            AverageValue = sum / mainWindow.pointsInfo.Count;
            mainWindow.pointsInfo.Add(new Classes.PointInfo(newValue));

            double value2 = mainWindow.pointsInfo2[mainWindow.pointsInfo2.Count - 1].value;
            newValue2 = value2 * (random.NextDouble() + 0.5d);
            double sum2 = mainWindow.pointsInfo2.Sum(y => y.value);
            AverageValue2 = sum2 / mainWindow.pointsInfo2.Count;
            mainWindow.pointsInfo2.Add(new Classes.PointInfo2(newValue2));
            ControlCreateChart();
        }

        public void CreateChart()
        {
            canvas.Children.Clear();
            for (int i = 0; i < mainWindow.pointsInfo.Count; i++) if (mainWindow.pointsInfo[i].value > maxValue) maxValue = mainWindow.pointsInfo[i].value;
            for (int i = 0; i < mainWindow.pointsInfo.Count; i++)
            {
                Line line = new Line();
                line.X1 = i * 20;
                line.X2 = (i + 1) * 20;
                if (i == 0) line.Y1 = actualHeightCanvas;
                else line.Y1 = actualHeightCanvas - ((mainWindow.pointsInfo[(i - 1)].value / maxValue) * actualHeightCanvas);
                line.Y2 = actualHeightCanvas - ((mainWindow.pointsInfo[i].value / maxValue) * actualHeightCanvas);
                line.StrokeThickness = 2;
                mainWindow.pointsInfo[i].line = line;
                canvas.Children.Add(line);
            }
            if (averageLine != null) canvas.Children.Remove(averageLine);
            averageLine = new Line
            {
                X1 = 0,
                X2 = mainWindow.pointsInfo.Count * 20,
                Y1 = actualHeightCanvas - ((averageValue / maxValue) * actualHeightCanvas),
                Y2 = actualHeightCanvas - ((averageValue / maxValue) * actualHeightCanvas),
                Stroke = Brushes.Yellow,
                StrokeThickness = 2,
                StrokeDashArray = new DoubleCollection { 4, 2 }
            };
            canvas.Children.Add(averageLine);

            canvas2.Children.Clear();
            for (int i = 0; i < mainWindow.pointsInfo2.Count; i++) if (mainWindow.pointsInfo2[i].value > maxValue2) maxValue2 = mainWindow.pointsInfo2[i].value;
            for (int i = 0; i < mainWindow.pointsInfo2.Count; i++)
            {
                Line line = new Line();
                line.X1 = i * 20;
                line.X2 = (i + 1) * 20;
                if (i == 0) line.Y1 = actualHeightCanvas;
                else line.Y1 = actualHeightCanvas - ((mainWindow.pointsInfo2[(i - 1)].value / maxValue2) * actualHeightCanvas);
                line.Y2 = actualHeightCanvas - ((mainWindow.pointsInfo2[i].value / maxValue2) * actualHeightCanvas);
                line.StrokeThickness = 2;
                mainWindow.pointsInfo2[i].line = line;
                canvas2.Children.Add(line);
            }
            if (averageLine2 != null) canvas2.Children.Remove(averageLine2);
            averageLine2 = new Line
            {
                X1 = 0,
                X2 = mainWindow.pointsInfo2.Count * 20,
                Y1 = actualHeightCanvas - ((averageValue2 / maxValue2) * actualHeightCanvas),
                Y2 = actualHeightCanvas - ((averageValue2 / maxValue2) * actualHeightCanvas),
                Stroke = Brushes.Yellow,
                StrokeThickness = 2,
                StrokeDashArray = new DoubleCollection { 4, 2 }
            };
            canvas2.Children.Add(averageLine2);
        }

        public void CreatePoint()
        {
            Line line = new Line();
            line.X1 = (mainWindow.pointsInfo.Count - 1) * 20;
            line.X2 = mainWindow.pointsInfo.Count * 20;
            line.Y1 = actualHeightCanvas - ((mainWindow.pointsInfo[(mainWindow.pointsInfo.Count - 2)].value / maxValue) * actualHeightCanvas);
            line.Y2 = actualHeightCanvas - ((mainWindow.pointsInfo[(mainWindow.pointsInfo.Count - 1)].value / maxValue) * actualHeightCanvas);
            line.StrokeThickness = 2;
            mainWindow.pointsInfo[(mainWindow.pointsInfo.Count - 1)].line = line;
            canvas.Children.Add(line);

            Line line2 = new Line();
            line2.X1 = (mainWindow.pointsInfo2.Count - 1) * 20;
            line2.X2 = mainWindow.pointsInfo2.Count * 20;
            line2.Y1 = actualHeightCanvas - ((mainWindow.pointsInfo2[(mainWindow.pointsInfo2.Count - 2)].value / maxValue2) * actualHeightCanvas);
            line2.Y2 = actualHeightCanvas - ((mainWindow.pointsInfo2[(mainWindow.pointsInfo2.Count - 1)].value / maxValue2) * actualHeightCanvas);
            line2.StrokeThickness = 2;
            mainWindow.pointsInfo2[(mainWindow.pointsInfo2.Count - 1)].line = line2;
            canvas2.Children.Add(line2);
        }

        public void ControlCreateChart()
        {
            double value = mainWindow.pointsInfo[mainWindow.pointsInfo.Count - 1].value;
            double value2 = mainWindow.pointsInfo2[mainWindow.pointsInfo2.Count - 1].value;
            if (value < maxValue && value2 < maxValue2) CreatePoint();
            else CreateChart();
            ColorChart();
        }

        public void ColorChart()
        {
            double value = mainWindow.pointsInfo[mainWindow.pointsInfo.Count - 1].value;
            averageValue = mainWindow.pointsInfo.Average(p => p.value);
            for (int i = 0; i < mainWindow.pointsInfo.Count; i++)
            {
                if (value < averageValue)
                {
                    if (mainWindow.pointsInfo[i].line == null) mainWindow.pointsInfo[i].line = new Line();
                    mainWindow.pointsInfo[i].line.Stroke = Brushes.Red;
                }
                else
                {
                    if (mainWindow.pointsInfo[i].line == null) mainWindow.pointsInfo[i].line = new Line();
                    mainWindow.pointsInfo[i].line.Stroke = Brushes.Green;
                }
            }
            if (averageLine != null) canvas.Children.Remove(averageLine);
            averageLine = new Line
            {
                X1 = 0,
                X2 = mainWindow.pointsInfo.Count * 20,
                Y1 = actualHeightCanvas - ((averageValue / maxValue) * actualHeightCanvas),
                Y2 = actualHeightCanvas - ((averageValue / maxValue) * actualHeightCanvas),
                Stroke = Brushes.Yellow,
                StrokeThickness = 2,
                StrokeDashArray = new DoubleCollection { 4, 2 }
            };
            canvas.Children.Add(averageLine);
            canvas.Width = mainWindow.pointsInfo.Count * 20 + 300;
            scroll.ScrollToHorizontalOffset(canvas.Width);
            current_value.Content = "Тек. знач: " + Math.Round(value, 2);
            average_value.Content = "Сред. знач: " + Math.Round(averageValue, 2);

            double value2 = mainWindow.pointsInfo2[mainWindow.pointsInfo2.Count - 1].value;
            averageValue2 = mainWindow.pointsInfo2.Average(y => y.value);
            for (int i = 0; i < mainWindow.pointsInfo2.Count; i++)
            {
                if (value2 < averageValue2)
                {
                    if (mainWindow.pointsInfo2[i].line == null) mainWindow.pointsInfo2[i].line = new Line();
                    mainWindow.pointsInfo2[i].line.Stroke = Brushes.Red;
                }
                else
                {
                    if (mainWindow.pointsInfo2[i].line == null) mainWindow.pointsInfo2[i].line = new Line();
                    mainWindow.pointsInfo2[i].line.Stroke = Brushes.Green;
                }
            }
            if (averageLine2 != null) canvas2.Children.Remove(averageLine2);
            averageLine2 = new Line
            {
                X1 = 0,
                X2 = mainWindow.pointsInfo2.Count * 20,
                Y1 = actualHeightCanvas - ((averageValue2 / maxValue2) * actualHeightCanvas),
                Y2 = actualHeightCanvas - ((averageValue2 / maxValue2) * actualHeightCanvas),
                Stroke = Brushes.Yellow,
                StrokeThickness = 2,
                StrokeDashArray = new DoubleCollection { 4, 2 }
            };
            canvas2.Children.Add(averageLine2);
            canvas2.Width = mainWindow.pointsInfo2.Count * 20 + 300;
            scroll2.ScrollToHorizontalOffset(canvas2.Width);
            current_value2.Content = "Тек. знач: " + Math.Round(value2, 2);
            average_value2.Content = "Сред. знач: " + Math.Round(averageValue2, 2);
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            actualHeightCanvas = (mainWindow.Height / 2) - 50d;
            CreateChart();
            ColorChart();
        }

        private void SaveChartsClick(object sender, RoutedEventArgs e)
        {
            infoDataForSave("Charts1", newValue, averageValue, "Charts2", newValue2, averageValue2);
            MessageBox.Show("Данные сохранены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}