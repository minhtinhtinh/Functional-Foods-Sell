using Milestone;
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

namespace milestone01
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    /// 

    public partial class Ordering1
    {
        private int id, customer_id;
        private double total;
        private string order_state;
        private DateTime order_time;

        public int Id1
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int Customer_id
        {
            get
            {
                return customer_id;
            }

            set
            {
                customer_id = value;
            }
        }

        public double Total1
        {
            get
            {
                return total;
            }

            set
            {
                total = value;
            }
        }

        public string Order_state
        {
            get
            {
                return order_state;
            }

            set
            {
                order_state = value;
            }
        }

        public DateTime Order_time
        {
            get
            {
                return order_time;
            }

            set
            {
                order_time = value;
            }
        }
    }
    public partial class CartWindow : Window, INotifyPropertyChanged, UpdatingListener
    {
        BindingList<OrderingDetail> Cart = new BindingList<OrderingDetail>();
        BindingList<Ordering> Ordering = new BindingList<Ordering>();
        DatabaseSalesEntities db;

        bool isSaved = false;
        int currentPage = 1;
        int itemsPerPage = 0;
        int totalPage = 0;
        UpdatingListener update;
        ContentControl content;

        public CartWindow()
        {
            InitializeComponent();            
        }
        public CartWindow(BindingList<OrderingDetail> cart, DatabaseSalesEntities db, ContentControl content)
        {
            InitializeComponent();
            update = (UpdatingListener)content;
            this.content = content;
            this.db = db;
            Cart = cart;          
            this.listCart.ItemsSource = Cart;
            this.DataContext = this;//binding for Total            
            if (db.Orderings.ToList().Count > 0)
            {               
                foreach(var or in db.Orderings)
                {
                    Ordering.Add(or);
                }
                listOrdering.ItemsSource = Ordering;
            }

            totalPage = countTotalPage(Ordering);
            LoadPage(currentPage, totalPage);
        }     

        public decimal FinalTotal
        {
            get {
                return Cart.Sum(detail => decimal.Parse(detail.Total.ToString()));
            }

            set
            {
                FinalTotal = value;
                NotifyChange("FinalTotal");
            }         
        }      

        public void deleteFromId(int id)
        {           
            var listRow = db.OrderingDetails.Where(w => w.Ordering_Id == id).ToList();
            db.OrderingDetails.RemoveRange(listRow);
            db.SaveChanges();

            var row = db.Orderings.Where(w => w.Id == id).FirstOrDefault();

            db.Orderings.Remove(row);
            db.SaveChanges();
            listOrdering.ItemsSource = db.Orderings.ToList();
        }

        public void updateCombobox()
        {
            throw new NotImplementedException();
        }

        public void updateListView()
        {
            //listOrdering.Items.Clear();
            listOrdering.ItemsSource = db.Orderings.ToList();
        }       

        private void NotifyChange(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void ButtonSaveOrder_Click(object sender, RoutedEventArgs e)
        {
            if (Cart.Count > 0)
            {
                CustomerInfoWindow customerWindow = new CustomerInfoWindow(Cart);
                customerWindow.ShowDialog();
                listOrdering.ItemsSource = db.Orderings.ToList();
                isSaved = true;
                Cart.Clear();
            }
            
        }                                         

        private void listViewCart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sender as ListView;
            var _orderDetail = item.SelectedItem as OrderingDetail;
            if(_orderDetail != null)
            {
                CartChangeWindow cartChangeWindow = new CartChangeWindow(db, content, _orderDetail, Cart, FinalTotal);
                cartChangeWindow.ShowDialog();
            }
            listCart.ItemsSource = null;
            listCart.ItemsSource = Cart;
            finalTotal.Text = doublePriceToStringPriceConverter.convert(FinalTotal.ToString());
        }      

        private void ButtonRemoveAllCart_Click(object sender, RoutedEventArgs e)
        {
            if(Cart.Count > 0)
            {
                if (!isSaved)
                {
                    foreach (OrderingDetail od in Cart)
                    {
                        int returnMount = (int)od.Quantity;
                        Product product = db.Products.SingleOrDefault(prod => prod.Id == od.Product_Id);
                        product.Amount += returnMount;
                        db.SaveChanges();
                    }
                    update.updateListView();
                }
                Cart.Clear();
                MessageBox.Show("Đã xóa toàn bộ giỏ hàng");
            }
                      
        }

        private void listOrdering_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var senderOrdering = sender as DataGrid;

            var item = senderOrdering.SelectedItem as Ordering;
            if (item != null && item.Order_state != null)
            {
                OrderingEdit oed = new OrderingEdit(db, this, item.Id);
                oed.ShowDialog();
            }
        }      
        
        public class infomation
        {
            public String info_name;
            public int info_quantity;            
        }       

        private void listOrdering_MouseLeftButtonDown(object sender, SelectionChangedEventArgs e)
        {
            //var item = sender as DataGrid;
            //var order = item.SelectedItem as Ordering;

            //BindingList<OrderingDetail> infoOrder = new BindingList<OrderingDetail>();
            //try
            //{
            //    foreach (var od in db.OrderingDetails)
            //    {
            //        if (od.Ordering_Id == order.Id)
            //        {
            //            infoOrder.Add(od);
            //        }
            //    }
            //    InfomationOrderDetailList infoWindow = new InfomationOrderDetailList(infoOrder);
            //    infoWindow.ShowDialog();
            //}
            //catch (Exception)
            //{

            //}
            
        }

        //TODO
        private void itemsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = itemsPerPageComboBox.SelectedItem as ComboBoxItem;
            var i = item.Content.ToString();
            itemsPerPage = int.Parse(i);
            if (Cart != null)
            {
                totalPage = countTotalPage(Ordering);
                currentPage = 1;
                PreviousButton.Visibility = Visibility.Hidden;
                NextButton.Visibility = Visibility.Visible;
                LoadPage(currentPage, totalPage);
            }
        }

        private void LoadPage(int currentPage, int totalpage)
        {
           
            listOrdering.ItemsSource = Ordering.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);
            pagingInfoLabel.Content = $"Trang {currentPage} đến {totalPage}";                   
        }

        private int countTotalPage(BindingList<Ordering> list)
        {
            var count = list.Count;
            if (count % itemsPerPage != 0)
            {
                totalPage = count / itemsPerPage + 1;
            }
            else
            {
                totalPage = count / itemsPerPage;
            }

            return totalPage;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                LoadPage(currentPage, totalPage);
                NextButton.Visibility = Visibility.Visible;
            }

            if (currentPage == 1)
            {
                PreviousButton.Visibility = Visibility.Hidden;
            }
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage < totalPage)
            {
                currentPage++;
                LoadPage(currentPage, totalPage);
                PreviousButton.Visibility = Visibility.Visible;
            }

            if (currentPage == totalPage)
            {
                NextButton.Visibility = Visibility.Hidden;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PreviousButton.Visibility = Visibility.Hidden;            
        }

        private void listOrdering_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sender as DataGrid;
            var order = item.SelectedItem as Ordering;

            BindingList<OrderingDetail> infoOrder = new BindingList<OrderingDetail>();
            try
            {
                foreach (var od in db.OrderingDetails)
                {
                    if (od.Ordering_Id == order.Id)
                    {
                        infoOrder.Add(od);
                    }
                }
                InfomationOrderDetailList infoWindow = new InfomationOrderDetailList(infoOrder);
                infoWindow.ShowDialog();
            }
            catch (Exception)
            {

            }
        }
    }
    
}
