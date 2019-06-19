using LiveCharts;
using LiveCharts.Wpf;
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

namespace Milestone
{
    /// <summary>
    /// Interaction logic for SellReport.xaml
    /// </summary>
    public partial class SellReport : Window
    {
        public List<string> Labels { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        public ChartValues<int> values;
        public DatabaseSalesEntities db;
        private List<OrderingDetail> orderDetails;
        public Func<int, string> Formatter { get; set; }

        public SellReport()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            db = new DatabaseSalesEntities();
            SeriesCollection = new SeriesCollection();
            Formatter = value => value.ToString("N");
            Labels = new List<string>();
            values = new ChartValues<int>();
            orderDetails = new List<OrderingDetail>();
            dpStart.SelectedDate = DateTime.Now.AddDays(-3);
            dpEnd.SelectedDate = DateTime.Now;
            initChart();
        }
        
        private void initChart()
        {
            int startDate = dpStart.SelectedDate.Value.Day;
            int startMonth = dpStart.SelectedDate.Value.Month;
            int startYear = dpStart.SelectedDate.Value.Year;
            int endDate = dpEnd.SelectedDate.Value.Day;
            int endMonth = dpEnd.SelectedDate.Value.Month;
            int endYear = dpEnd.SelectedDate.Value.Year;
            if (startDate <= endDate && startMonth <= endMonth && startYear <= endYear)
            {
                columnChart.Series.Clear();
                updateChart(dpStart.SelectedDate.Value, dpEnd.SelectedDate.Value);
            }
        }

        private void updateChart(DateTime start, DateTime end)
        {
            orderDetails = db.OrderingDetails.ToList();
            Labels.Clear();
            values.Clear();
            // Labels = new List<string>();
            if (orderDetails != null)
            {
                for (DateTime date = start; date <= end; date = date.AddDays(1))
                {
                    Labels.Add(date.Date.ToString("dd/MM/yyyy"));

                }
                for (DateTime date = dpStart.SelectedDate.Value; date <= dpEnd.SelectedDate.Value; date = date.AddDays(1))
                {
                    int sellCounter = 0;
                    string orderDate = date.ToString("dd/MM/yyyy");
                    foreach (OrderingDetail od in orderDetails)
                    {
                        var order = db.Orderings.Where(w => w.Id == od.Ordering_Id && w.Order_time.Equals(orderDate) && w.Order_state.Equals("Hoàn tất")).SingleOrDefault();
                        if (order != null)
                        {
                            sellCounter += (int)od.Quantity;
                        }
                    }
                    values.Add(sellCounter);
                }
                columnChart.Series.Add(
                     new ColumnSeries
                     {
                         Title = "Số lượng đã bán",
                         Values = values
                     }
                 );
            }
            

        }

        private void dpStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpStart.SelectedDate != null && dpEnd.SelectedDate != null)
            {
                int startDate = dpStart.SelectedDate.Value.Day;
                int startMonth = dpStart.SelectedDate.Value.Month;
                int startYear = dpStart.SelectedDate.Value.Year;
                int endDate = dpEnd.SelectedDate.Value.Day;
                int endMonth = dpEnd.SelectedDate.Value.Month;
                int endYear = dpEnd.SelectedDate.Value.Year;
                if (startDate <= endDate && startMonth <= endMonth && startYear <= endYear)
                {
                    columnChart.Series.Clear();
                    updateChart(dpStart.SelectedDate.Value, dpEnd.SelectedDate.Value);
                }
                else
                {
                    MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc");
                }
            }

        }

        private void dpEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpEnd.SelectedDate != null && dpStart.SelectedDate != null)
            {
                int startDate = dpStart.SelectedDate.Value.Day;
                int startMonth = dpStart.SelectedDate.Value.Month;
                int startYear = dpStart.SelectedDate.Value.Year;
                int endDate = dpEnd.SelectedDate.Value.Day;
                int endMonth = dpEnd.SelectedDate.Value.Month;
                int endYear = dpEnd.SelectedDate.Value.Year;
                if (startDate <= endDate && startMonth <= endMonth && startYear <= endYear)
                {
                    columnChart.Series.Clear();
                    updateChart(dpStart.SelectedDate.Value, dpEnd.SelectedDate.Value);
                }
                else
                {
                    MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc");
                }
            }

        }
    }
}
