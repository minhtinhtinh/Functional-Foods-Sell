using Microsoft.Win32;
using Milestone;
using milestone01;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        DatabaseSalesEntities db;
        ComboBox cb;
        ContentControl content;
        UpdatingListener update;
        Product product;
        public AddProduct()
        {
            InitializeComponent();
        }

        public AddProduct(DatabaseSalesEntities db, ComboBox cb, ContentControl content)
        {
            InitializeComponent();
            this.db = db;
            this.cb = cb;
            this.content = content;
            update = (UpdatingListener)content;
            product = new Product();
            // add blank image
            string currentFolder = AppDomain.CurrentDomain.BaseDirectory;
            // Remove the last /
            var baseFolder = currentFolder.Substring(0, currentFolder.Length - 1);
            string absolutePath = baseFolder + @"\BlankImage\blank.jpg";
            imgProduct.Source = new BitmapImage(new Uri(absolutePath));
            product.Image = absolutePath;
            lbType.Content = (cb.SelectedValue as Category).Name;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            product.Name = txtName.Text.ToString();
            product.CateID = (cb.SelectedValue as Category).Id;
            //product.Price = doublePriceToStringPriceConverter.convert(txtPrice.Text.ToString());
            product.Price = txtPrice.Text.ToString();
            product.Amount = Int32.Parse(txtAmount.Text.ToString());
            db.Products.Add(product);
            db.SaveChanges();
            update.updateListView();                        
            Close();
        }

        private void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            string currentFolder = AppDomain.CurrentDomain.BaseDirectory;
            // Remove the last /
            var baseFolder = currentFolder.Substring(0, currentFolder.Length - 1);
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                var filename = screen.FileName;
                var info = new FileInfo(filename);
                var folder = info.Directory;//path of image file
                var newName = Guid.NewGuid() + "." + info.Extension;
                string absolutePath = baseFolder + @"\Images\" + newName;
                info.CopyTo(absolutePath);
                imgProduct.Source = new BitmapImage(new Uri(absolutePath));
                product.Image = absolutePath;
            }
        }
    }
}
