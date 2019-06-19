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
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {

        List<Product> products;
        List<Ordering> orders;
        DatabaseSalesEntities db;
        ChartValues<double> values;
        ChartValues<double> orderValues;

        public ReportWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = this;
            products = new List<Product>();
            orders = new List<Ordering>();
            db = new DatabaseSalesEntities();
            products = db.Products.ToList();
            orders = db.Orderings.ToList();
            values = new ChartValues<double>();
            orderValues = new ChartValues<double>();
            inSummary();
            inOrderSummary();
        }

        private void inOrderSummary()
        {
            for (int i = 1; i <= 12; i++)
            {
                double sum = 0;
                foreach(Ordering order in orders)
                {
                    string[] date = order.Order_time.Split('/');
                    int month = int.Parse(date[1]);
                    int year = int.Parse(date[2]);
                    int yearNow = int.Parse(DateTime.Now.ToString("yyyy"));
                    if (month == i && year == yearNow && order.Order_state.Equals("Hoàn tất"))
                    {
                        sum += (double)order.Total;
                    }
                }
                orderValues.Add(sum);
            }
        }

        private void inSummary()
        {
            
            for(int i = 1; i <= 12; i++)
            {

                double sum = 0;
                foreach (Product p in products)
                {
                    string[] date = p.InDate.Split('/');
                    int month = int.Parse(date[1]);
                    int year = int.Parse(date[2]);
                    int yearNow = int.Parse(DateTime.Now.ToString("yyyy"));
                    if (month == i && year == yearNow)
                    {
                        sum += double.Parse(p.Price) * (double)p.Amount;
                    }
                }
                values.Add(sum);
            }
        }

        public string[] Labels { get; set; } = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        public SeriesCollection SeriesCollection => new SeriesCollection
        {
            
            
            new LineSeries
            {
                Title = "Bán ra (VNĐ)",
                Values = orderValues
                
            },
            new LineSeries
            {
                Title = "Nhập vào (VNĐ)",
                Values = values
            }
        };
    }
}
