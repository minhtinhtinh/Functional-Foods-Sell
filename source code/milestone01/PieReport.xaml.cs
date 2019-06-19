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
    /// Interaction logic for PieReport.xaml
    /// </summary>
    public partial class PieReport : Window
    {

        public Func<ChartPoint, string> PointLabel { get; set; }

        DatabaseSalesEntities db;
        List<Ordering> orders;
        List<OrderingDetail> orderDetails;
        List<Category> cates;


        public PieReport()
        {
            InitializeComponent();
            
            
            // PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            

        }

        private void updateChart(int startDate, int startMonth, int startYear, int endDate, int endMonth, int endYear)
        {
            if(cates != null)
            {
                foreach (Category cate in cates)
                {
                    double value = 0;
                    foreach (OrderingDetail ordDetail in orderDetails)
                    {
                        Ordering o = db.Orderings.Where(w => w.Id == ordDetail.Ordering_Id && w.Order_state.Equals("Hoàn tất")).FirstOrDefault();
                        if (o != null)
                        {
                            string[] orderTime = o.Order_time.Split('/');
                            // string[] nowTime = DateTime.Now.ToString("dd/MM/yyyy").Split('/');
                            //if ()
                            // o.Order_time.Equals(DateTime.Now.ToString("dd/MM/yyyy"))
                            if ((int.Parse(orderTime[0]) >= startDate && int.Parse(orderTime[0]) <= endDate)
                                && (int.Parse(orderTime[1]) >= startMonth && int.Parse(orderTime[1]) <= endMonth)
                                && (int.Parse(orderTime[2]) >= startYear && int.Parse(orderTime[2]) <= endYear))
                            {
                                Product p = db.Products.Where(w => w.Id == ordDetail.Product_Id).FirstOrDefault();
                                if (p.CateID == cate.Id)
                                {
                                    value += (double)ordDetail.Total;
                                }
                            }
                        }


                    }
                    pieChart.Series.Add(new PieSeries { Title = cate.Name, Values = new ChartValues<double> { value } });

                }
            }
            
        }

       

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            dpStart.SelectedDate = DateTime.Now.AddDays(-1);
            dpEnd.SelectedDate = DateTime.Now;
            db = new DatabaseSalesEntities();
            orders = new List<Ordering>();
            orderDetails = new List<OrderingDetail>();
            cates = new List<Category>();
            cates = db.Categories.ToList();
            orderDetails = db.OrderingDetails.ToList();
            orders = db.Orderings.ToList();
            triggerChart();
        }

        private void triggerChart()
        {
            int startDate = dpStart.SelectedDate.Value.Day;
            int startMonth = dpStart.SelectedDate.Value.Month;
            int startYear = dpStart.SelectedDate.Value.Year;
            int endDate = dpEnd.SelectedDate.Value.Day;
            int endMonth = dpEnd.SelectedDate.Value.Month;
            int endYear = dpEnd.SelectedDate.Value.Year;
            if (startDate <= endDate && startMonth <= endMonth && startYear <= endYear)
            {
                pieChart.Series.Clear();
                updateChart(startDate, startMonth, startYear, endDate, endMonth, endYear);
            }
        }

        private void dpStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dpStart.SelectedDate != null && dpEnd.SelectedDate != null)
            {
                int startDate = dpStart.SelectedDate.Value.Day;
                int startMonth = dpStart.SelectedDate.Value.Month;
                int startYear = dpStart.SelectedDate.Value.Year;
                int endDate = dpEnd.SelectedDate.Value.Day;
                int endMonth = dpEnd.SelectedDate.Value.Month;
                int endYear = dpEnd.SelectedDate.Value.Year;
                if (startDate <= endDate && startMonth <= endMonth && startYear <= endYear)
                {
                    pieChart.Series.Clear();
                    updateChart(startDate, startMonth, startYear, endDate, endMonth, endYear);
                }
                else
                {
                    MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc");
                }
            }
            
        }

        private void dpEnd_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dpEnd.SelectedDate != null && dpStart.SelectedDate != null)
            {
                int startDate = dpStart.SelectedDate.Value.Day;
                int startMonth = dpStart.SelectedDate.Value.Month;
                int startYear = dpStart.SelectedDate.Value.Year;
                int endDate = dpEnd.SelectedDate.Value.Day;
                int endMonth = dpEnd.SelectedDate.Value.Month;
                int endYear = dpEnd.SelectedDate.Value.Year;
                if (startDate <= endDate && startMonth <= endMonth && startYear <= endYear)
                {
                    pieChart.Series.Clear();
                    updateChart(startDate, startMonth, startYear, endDate, endMonth, endYear);
                }
                else
                {
                    MessageBox.Show("Ngày bắt đầu phải trước ngày kết thúc");
                }
            }
            
        }
    }
}
