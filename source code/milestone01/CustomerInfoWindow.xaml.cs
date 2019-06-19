using milestone01;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for CustomerInfoWindow.xaml
    public partial class CustomerInfoWindow : Window
    /// </summary>
    {
        BindingList<OrderingDetail> listOrder = new BindingList<OrderingDetail>();
        DatabaseSalesEntities db;
        public CustomerInfoWindow(BindingList<OrderingDetail> Cart)
        {
            InitializeComponent();
            listOrder = Cart;
            db = new DatabaseSalesEntities();
        }

        private void saveButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            //if (listOrder.Count > 0)
            //{
            //    try
            //    {
            //        if(textBoxCustomerName.Text.Length == 0 || 
            //            textBoxCustomerAddress.Text.Length == 0 || 
            //            textBoxCustomerPhone.Text.Length == 0)
            //        {
            //            MessageBox.Show("Nhập lại thông tin khách hàng");
            //            return;
            //        }
            //        var customer = new Customer()
            //        {
            //            FullName = textBoxCustomerName.Text,
            //            Phone = textBoxCustomerPhone.Text,
            //            Address = textBoxCustomerAddress.Text,
            //        };
            //        db.Customers.Add(customer);

            //        var ordering = new Ordering()
            //        {
            //            Order_state = "Mới",
            //            Customer_Id = customer.Id,
            //            Total = listOrder.Sum(o => o.Total),
            //        };
            //        db.Orderings.Add(ordering);

            //        foreach (var o in listOrder)
            //        {
            //            var orderingDetail = new OrderingDetail()
            //            {
            //                Name = o.Name,
            //                Price = decimal.Parse(o.Price),
            //                Quantity = o.Quantity,
            //                Total = o.Total,
            //                Ordering_Id = ordering.Id,
            //            };
            //            db.OrderingDetails.Add(orderingDetail);
            //        }

            //        db.SaveChanges();                    
            //        MessageBox.Show("Đã lưu đơn hàng");
            //        this.Close();
            //    }
            //    catch (Exception)
            //    {
            //        MessageBox.Show("Nhập lại dữ liệu");
            //        return;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Chưa có đơn hàng");
            //}   
            try
            {               
                var customer = new Customer()
                {
                    FullName = textBoxCustomerName.Text,
                    Phone = textBoxCustomerPhone.Text,
                    Address = textBoxCustomerAddress.Text,
                };
                db.Customers.Add(customer);

                var ordering = new Ordering()
                {
                    Order_state = "Mới",
                    Customer_Id = customer.Id,
                    Total = listOrder.Sum(o => o.Total),
                    Order_time = DateTime.Now.ToString("dd/MM/yyyy")
                };
                db.Orderings.Add(ordering);

                foreach (var o in listOrder)
                {
                    var orderingDetail = new OrderingDetail()
                    {
                        Name = o.Name,
                        Price = o.Price,
                        Quantity = o.Quantity,
                        Total = o.Total,
                        Ordering_Id = ordering.Id,
                        Product_Id = o.Product_Id
                    };
                    db.OrderingDetails.Add(orderingDetail);

                    //update amount of product
                    //foreach (var p in db.Products)
                    //{
                    //    if(p.Id == orderingDetail.Product_Id)
                    //    {
                    //        p.Amount -= orderingDetail.Quantity;
                    //    }                        
                    //}
                }
                db.SaveChanges();                
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Nhập lại dữ liệu");
                return;
            }
        }

        private void cancelSaveButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
