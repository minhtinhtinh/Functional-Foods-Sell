using milestone01;
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
    /// Interaction logic for OrderingEdit.xaml
    /// </summary>
    public partial class OrderingEdit : Window
    {
        string[] states = { "Mới", "Hoàn tất", "Đã hủy" };
        public string[] States
        {
            get
            {
                return states;
            }
        }

        DatabaseSalesEntities db;
        ContentControl content;
        Ordering order;
        UpdatingListener updating;

        string current_State;
        public OrderingEdit()
        {
            InitializeComponent();
        }

        public OrderingEdit(DatabaseSalesEntities db, ContentControl content, int order_Id)
        {
            InitializeComponent();
            this.db = db;
            this.content = content;
            updating = (UpdatingListener)content;
            order = db.Orderings.Where(x => x.Id == order_Id).Select(x => x).Single();
            current_State = order.Order_state;
            cbbState.ItemsSource = States;
            cbbState.SelectedValue = current_State;
            lblId.Content = lblId.Content.ToString() + order_Id;
            lblTotal.Content = lblTotal.Content.ToString() + order.Total + " VNĐ";
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!cbbState.SelectedValue.Equals(current_State))
            {
                if(current_State.Equals("Đã hủy"))
                {
                    MessageBox.Show("Không thể khôi phục đơn hàng đã hủy, hãy tạo đơn hàng mới");
                }
                else
                {
                    order.Order_state = (string)cbbState.SelectedValue;
                    order.Order_time = DateTime.Now.ToString("dd/MM/yyyy");
                    db.Orderings.Add(order);
                    db.Orderings.Attach(order);
                    db.Entry(order).State = System.Data.Entity.EntityState.Modified;
                    //db.SaveChangesAsync();
                    db.SaveChanges();
                    current_State = order.Order_state;
                    if (current_State.Equals("Đã hủy"))
                    {
                        var orderingDetail = db.OrderingDetails.ToList();
                        foreach (OrderingDetail od in orderingDetail)
                        {
                            if (od.Ordering_Id == order.Id)
                            {
                                var product = db.Products.Where(p => p.Id == od.Product_Id).FirstOrDefault();
                                product.Amount += od.Quantity;
                                db.SaveChanges();
                            }
                        }

                    }
                }
                
                updating.updateListView();
                Close();
                
            }
            else
            {
                MessageBox.Show("Order state never changed yet");
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you really want remove it?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                if (current_State.Equals("Mới"))
                {
                    var orderingDetail = db.OrderingDetails.ToList();
                    foreach (OrderingDetail od in orderingDetail)
                    {
                        if (od.Ordering_Id == order.Id)
                        {
                            var product = db.Products.Where(p => p.Id == od.Product_Id).FirstOrDefault();
                            product.Amount += od.Quantity;
                            db.SaveChanges();
                        }
                    }
                }
                
                updating.deleteFromId(order.Id);
                Close();
            }
        }
    }
}
