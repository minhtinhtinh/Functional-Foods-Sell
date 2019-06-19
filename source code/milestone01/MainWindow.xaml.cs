using Aspose.Cells;
using Fluent;
using Microsoft.Win32;
using Milestone;
using milestone01;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

namespace milestone01
{    
    public class OrderDetail : INotifyPropertyChanged
    {
        private int SKU;
        private String _name;
        private String _price;
        private int _quantity;
        private int _total;

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public String Price
        {
            get
            {
                return _price;
            }

            set
            {
                _price = value;
            }
        }

        public int Quantity
        {
            get
            {
                return _quantity;
            }

            set
            {
                _quantity = value;
                NotifyChange("Quantity");
                NotifyChange("Total");
            }
        }

        private void NotifyChange(string v)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(v));
            }
        }

        public int SKU1
        {
            get
            {
                return SKU;
            }

            set
            {
                SKU = value;
            }
        }

        public int Total
        {
            get
            {
                return Quantity * int.Parse(Price);
            }

            set
            {
                _total = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, UpdatingListener
    {        
        BindingList<OrderingDetail> listCart;

        BindingList<Product> listProducts;
        BindingList<Category> listCategory;       
        DatabaseSalesEntities db;

        int currentPage = 1;
        int itemsPerPage = 0;
        int totalPage = 0;

        public MainWindow()
        {
            InitializeComponent();
            db = new DatabaseSalesEntities();          
        }       

        private void click_Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void click_About(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Cửa Hàng Thực Phẩm Chức Năng \n Phiên bản 1.2.0", "Thông tin", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void click_Import_Excel(object sender, RoutedEventArgs e)
        {
            //get absolute path from debug file excute program           
            string currentFolder = AppDomain.CurrentDomain.BaseDirectory;
            // Remove the last /
            var baseFolder = currentFolder.Substring(0, currentFolder.Length - 1);
            var screen = new OpenFileDialog();

            if (screen.ShowDialog() == true)
            {
                var filename = screen.FileName;
                var info = new FileInfo(filename);
                var folder = info.Directory;//path of excel file

                var workbook = new Workbook(filename);
                var sheetProduct = workbook.Worksheets["Product"];//sheetProduct name import data
                var columnProduct = 'C';//product read data
                var rowProduct = 7;

                var sheetCategory = workbook.Worksheets["Category"];//sheet category name import data               
                var columnCategory = 'C';//column read data
                var rowCategory = 7;

                var cellProduct = sheetProduct.Cells[$"{columnProduct}{rowProduct}"];
                var cellCategory = sheetCategory.Cells[$"{columnCategory}{rowCategory}"];

                //product to list            
                while (cellProduct.Value != null)
                {
                    var name = sheetProduct.Cells[$"C{rowProduct}"].StringValue;
                    //var price = doublePriceToStringPriceConverter.convert(sheetProduct.Cells[$"D{rowProduct}"].StringValue);
                    var price = sheetProduct.Cells[$"D{rowProduct}"].StringValue;
                    var imagePath = sheetProduct.Cells[$"E{rowProduct}"].StringValue;
                    var cateID = sheetProduct.Cells[$"F{rowProduct}"].IntValue;
                    var amount = sheetProduct.Cells[$"G{rowProduct}"].IntValue;

                    imagePath = folder + @"\Images\" + imagePath;
                    //var imgInfo = new FileInfo(imagePath);

                    //var newName = Guid.NewGuid() + "." + imgInfo.Extension;//create new name

                    //string AbsolutePath = baseFolder + @"\Images\" + newName;//Absolute path where the image is pasted
                    //imgInfo.CopyTo(AbsolutePath);//Copy image to debug folder
                    //Debug.WriteLine($"{name} -{imagePath}- {price}");
                    Debug.WriteLine($"{name} -{imagePath}- {price}");
                    var product = new Product()
                    {
                        Name = name,
                        Price = price,
                        Image = imagePath,    
                        CateID = cateID,
                        Amount = amount,
                        InDate = DateTime.Now.ToString("dd/MM/yyyy")                        
                    };

                    listProducts.Add(product);
                    db.Products.Add(product);
                    rowProduct++;
                    cellProduct = sheetProduct.Cells[$"{columnProduct}{rowProduct}"];
                }

                //category to combobox
                while (cellCategory.Value != null)
                {
                    var name = sheetCategory.Cells[$"C{rowCategory}"].StringValue;
                    var descript = sheetCategory.Cells[$"D{rowCategory}"].StringValue;
                    var cate = new Category()
                    {
                        Name = name,
                        Descript = descript
                    };
                    listCategory.Add(cate);
                    db.Categories.Add(cate);
                    rowCategory++;
                    cellCategory = sheetCategory.Cells[$"{columnCategory}{rowCategory}"];
                }

                db.SaveChanges();                
                comboboxCategory.ItemsSource = db.Categories.ToList();
                comboboxCategory.SelectedIndex = 0;

                //todo
                totalPage = countTotalPage(listProducts);
                LoadPage(currentPage, totalPage);
            }
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
           
            listProducts = new BindingList<Product>();
            listCategory = new BindingList<Category>();
            listCart = new BindingList<OrderingDetail>();       
            comboboxCategory.ItemsSource = db.Categories.ToList();
            comboboxCategory.SelectedIndex = 0;

            System.Windows.Controls.ComboBox cb = sender as System.Windows.Controls.ComboBox;
            if (cb != null && cb.SelectedValue != null)
            {
                int id = (cb.SelectedValue as Category).Id;
                listProduct.ItemsSource = db.Products.Where(w => w.CateID == id).Select(w => w).ToList();
            }

            PreviousButton.Visibility = Visibility.Hidden;
            foreach (var cate in db.Categories)
            {
                listCategory.Add(cate);
            }

            foreach (var product in db.Products)
            {
                listProducts.Add(product);
            }

            totalPage = countTotalPage(listProducts);
            LoadPage(currentPage, totalPage);            
        }

        private void comboboxCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox cb = sender as System.Windows.Controls.ComboBox;
            if (cb != null && cb.SelectedValue != null)
            {
                int id = (cb.SelectedValue as Category).Id;
                listProduct.ItemsSource = db.Products.Where(w => w.CateID == id).Select(w => w).ToList();
            }
            // Minh's code below
            listProducts.Clear();
            var control = sender as System.Windows.Controls.ComboBox;
            var cate = control.SelectedItem as Category;
            foreach (var p in db.Products.Where(w => w.CateID == cate.Id).Select(w => w).ToList())
            {
                if (p.CateID == cate.Id)
                {
                    listProducts.Add(p);
                }
            }

            listProduct.ItemsSource = listProducts;

            //todo
            totalPage = countTotalPage(listProducts);
            currentPage = 1;
            PreviousButton.Visibility = Visibility.Hidden;
            NextButton.Visibility = Visibility.Visible;
            LoadPage(currentPage, totalPage);
        }

        private void addCategory_Click(object sender, RoutedEventArgs e)
        {
            if(db.Categories.ToList().Count > 0 && comboboxCategory.SelectedItem!=null)
            {
                AddCategory addCgr = new AddCategory(db, comboboxCategory);
                addCgr.Show();
            }
            else
            {
                MessageBox.Show("Chưa imported dữ liệu");
            }
        }

        public void refreshCombobox()
        {
            comboboxCategory.ItemsSource = db.Categories.ToList();            
        }

        public void refreshListView()
        {
            
            int id = (comboboxCategory.SelectedValue as Category).Id;
            //listProduct.ItemsSource = db.Products.Where(w => w.CateID == id).Select(w => w).ToList();
            listProducts.Clear();
            listProducts = new BindingList<Product>(db.Products.Where(w => w.CateID == id).Select(w => w).ToList());
            totalPage = countTotalPage(listProducts);
            LoadPage(currentPage, totalPage);
            NextButton.Visibility = Visibility.Visible;
        }

        private void deleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (comboboxCategory != null && comboboxCategory.SelectedValue != null)
            {
                int id = (comboboxCategory.SelectedValue as Category).Id;
                if (db.Products.Where(w => w.CateID == id).Select(w => w).ToList().Count == 0)
                {
                    if(MessageBox.Show("Bạn muốn xóa loại sản phẩm này?", "Thông tin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        var row = db.Categories.Where(w => w.Id == id).FirstOrDefault();
                        db.Categories.Remove(row);                        
                        db.SaveChanges();
                        comboboxCategory.SelectedIndex = 0;
                        refreshCombobox();
                    }
                }
                else
                {
                    MessageBox.Show("Không thể xóa, sản phẩm thuộc loại này còn tồn tại");
                }
            }
            else
            {
                MessageBox.Show("Chưa imported dữ liệu");
            }
            
        }

        private void editCategory_Click(object sender, RoutedEventArgs e)
        {
            if (db.Categories.ToList().Count > 0 && comboboxCategory.SelectedItem != null)
            {
                EditCategory editCgr = new EditCategory(db, comboboxCategory, this);
                editCgr.Show();
            }
            else
            {
                MessageBox.Show("Chưa imported dữ liệu");
            }
            listProducts.Clear();
            
        }

        private void itemsPerPageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = itemsPerPageComboBox.SelectedItem as ComboBoxItem;
            var i = item.Content.ToString();
            itemsPerPage = int.Parse(i);    
            if (listProducts != null)
            {
                totalPage = countTotalPage(listProducts);
                currentPage = 1;
                PreviousButton.Visibility = Visibility.Hidden;
                NextButton.Visibility = Visibility.Visible;
                LoadPage(currentPage, totalPage);
            }
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

        /// <summary>
        /// total page of a category with quantity itemPerPage
        /// </summary>
        /// <param name="list">list products</param>
        /// <returns>total page</returns>
        private int countTotalPage(BindingList<Product> list)
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

        /// <summary>
        /// load list itemPerPage
        /// </summary>
        /// <param name="currentPage">position of page current</param>
        /// <param name="totalpage">total page</param>
        private void LoadPage(int currentPage, int totalpage)
        {
            listProduct.ItemsSource = listProducts.Skip((currentPage - 1) * itemsPerPage).Take(itemsPerPage);
            pagingInfoLabel.Content = $"Trang {currentPage} đến {totalPage}";
        }

        private void editProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = listProduct.SelectedValue as Product;
            if(product != null)
            {
                EditProduct edprd = new EditProduct(db, product, this);
                edprd.Show();
            }
            else
            {
                MessageBox.Show("Chưa imported dữ liệu");
            }

        }

        public void updateListView()
        {
            refreshListView();
        }

        public void updateCombobox()
        {
            comboboxCategory.SelectedIndex = 0;
            refreshCombobox();
        }

        private void addProduct_Click(object sender, RoutedEventArgs e)
        {
            if(comboboxCategory.SelectedValue != null)
            {
                AddProduct ap = new AddProduct(db, comboboxCategory, this);
                ap.Show();
            }
            else
            {
                MessageBox.Show("Chưa imported dữ liệu");
            }
        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if(listProduct.SelectedValue != null)
            {
                if (MessageBox.Show("Bạn muốn xóa sản phẩm này?", "Thông tin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var p = (listProduct.SelectedValue as Product);
                    var row = db.Products.Where(w => w.Id == p.Id).FirstOrDefault();
                    db.Products.Remove(row);
                    db.SaveChanges();                          
                    refreshListView();
                }
            }
            else
            {
                MessageBox.Show("Chưa imported dữ liệu");
            }
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var item = sender as ListViewItem;
            if (item != null && item.IsSelected)
            {
                MessageBox.Show("Chọn sản phẩm 2 lần");
            }
        }

        private void listProduct_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listProduct.SelectedItems.Count > 0)
            {
                grid_productDetail.Visibility = Visibility.Visible;
            }
            else
            {
                grid_productDetail.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonAddCart_Click(object sender, RoutedEventArgs e)
        {            
            Product product = listProduct.SelectedItem as Product;
            if(product != null && product.Amount > 0)
            {
                int amount = int.Parse(txtAmount.Text);
                var foundProduct = listCart.FirstOrDefault(f => f.Product_Id == product.Id);
                if (foundProduct != null)
                {
                    foundProduct.Quantity++;
                }
                else
                {
                    var _orderDetail = new OrderingDetail()
                    {
                        Product_Id = product.Id,
                        Name = product.Name,
                        Price = decimal.Parse(product.Price),
                        Quantity = 1,
                        Total = decimal.Parse(product.Price)
                    };
                    listCart.Add(_orderDetail);
                    
                }
                var p = db.Products.SingleOrDefault(prod => prod.Id == product.Id);
                p.Amount--;
                db.SaveChanges();
                amount--;
                txtAmount.Text = amount.ToString();

                MessageBox.Show("Đã thêm vào giỏ hàng");
            }
            else
            {
                MessageBox.Show("Sản phẩm tạm thời hết hàng");
            }
          
                  
        }      

        private void Ribbon_SelectedTabChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = sender as Ribbon;
            if (item.SelectedTabItem.KeyTip=="T")
            {              
                
            }
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow(listCart, db, this);
            cartWindow.ShowDialog();                   
        }

        public void deleteFromId(int id)
        {
            throw new NotImplementedException();
        }       
       
        private void TextBoxFillProduct_TextChanged(object sender, TextChangedEventArgs e)
        {                        
            var keyWords = textBoxFillProduct.Text;            
            if(keyWords.Length > 0)
            {
                db.Products.Load();
                listProduct.ItemsSource = db.Products.Local.Where(p => p.Name.ToLower().Contains(keyWords.ToLower())).ToList();
                statusBar.Visibility = Visibility.Hidden;
            }
            else
            {
                statusBar.Visibility = Visibility.Visible;
                totalPage = countTotalPage(listProducts);
                LoadPage(currentPage, totalPage);
            }
        }

        private void btnRevenue_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow report = new ReportWindow();
            report.ShowDialog();
        }

        private void btnInventory_Click(object sender, RoutedEventArgs e)
        {
            PieReport pie = new PieReport();
            pie.ShowDialog();
        }

        private void btnSellSum_Click(object sender, RoutedEventArgs e)
        {

            SellReport sr = new SellReport();
            sr.ShowDialog();
        }

        private void RibbonWindow_Closed(object sender, EventArgs e)
        {
            
        }

        private void RibbonWindow_Closing(object sender, CancelEventArgs e)
        {
            if (listCart.Count > 0)
            {
                if (MessageBox.Show("Đơn hàng chưa được lưu, bạn có muốn thoát?", "Thông tin", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    foreach (OrderingDetail od in listCart)
                    {
                        int returnMount = (int)od.Quantity;
                        Product product = db.Products.SingleOrDefault(prod => prod.Id == od.Product_Id);
                        product.Amount += returnMount;
                        db.SaveChanges();
                    }
                    // updateListView();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            
        }
    }
}
