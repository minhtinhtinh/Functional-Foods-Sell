using Milestone;
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

namespace milestone01
{
    /// <summary>
    /// Interaction logic for CartChangeWindow.xaml
    /// </summary>
    public partial class CartChangeWindow : Window
    {
        OrderingDetail _orderDetail;
        BindingList<OrderingDetail> listCartUpdate;
        private decimal total;
        DatabaseSalesEntities db;
        UpdatingListener update;

        public decimal Total
        {
            get
            {
                return listCartUpdate.Sum(detail => decimal.Parse(detail.Total.ToString()));
            }

            set
            {
                total = value;
            }
        }

        public CartChangeWindow()
        {
            InitializeComponent();
        }

        public CartChangeWindow(DatabaseSalesEntities db, ContentControl content, OrderingDetail orderDetail, BindingList<OrderingDetail> listCart, decimal FinalTotal)
        {
            InitializeComponent();
            this.db = db;
            update = (UpdatingListener)content;
            _orderDetail = new OrderingDetail();
            _orderDetail = orderDetail;
            listCartUpdate = new BindingList<OrderingDetail>();
            listCartUpdate = listCart;
            Total = FinalTotal;
        }

        private void updateButtonQuantity_Click(object sender, RoutedEventArgs e)
        {        
            if(textBoxQuantityUpdate.Text.Length > 0)
            {
                int new_quantity = 0;
                try {
                    new_quantity = int.Parse(textBoxQuantityUpdate.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Nhập lại dữ liệu");
                    return;
                }

                Product product = db.Products.SingleOrDefault(prod => prod.Id == _orderDetail.Product_Id);
                if(product.Amount - new_quantity + 1 >= 0)
                {
                    product.Amount = product.Amount - new_quantity + 1;
                    db.SaveChanges();
                    update.updateListView();
                    _orderDetail.Quantity = new_quantity;
                    _orderDetail.Total = _orderDetail.Price * _orderDetail.Quantity;
                    MessageBox.Show("Đã cập nhật số lượng sản phẩm");
                }
                else
                {
                    MessageBox.Show("Số lượng sản phẩm không đủ");
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Nhập số lượng sản phẩm");
            }
        }

        private void deleteButtonProductDeTail_Click(object sender, RoutedEventArgs e)
        {
            var orderDatilDelete = listCartUpdate.FirstOrDefault(d => d.Id == _orderDetail.Id);
            if(orderDatilDelete != null)
            {
                int q = (int)orderDatilDelete.Quantity;
                var product = db.Products.Where(p => p.Id == orderDatilDelete.Product_Id).FirstOrDefault();
                product.Amount += q;
                db.SaveChanges();
                listCartUpdate.Remove(orderDatilDelete);
                MessageBox.Show("Đã xóa sản phẩm");
                this.Close();               
            }
        }
    }
}