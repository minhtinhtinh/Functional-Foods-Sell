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
    /// Interaction logic for EditProduct.xaml
    /// </summary>
    public partial class EditProduct : Window
    {
        Product product;
        DatabaseSalesEntities db;
        ContentControl content;
        UpdatingListener updating;
        public EditProduct()
        {
            InitializeComponent();
        }
        public EditProduct(DatabaseSalesEntities db, Product product, ContentControl content)
        {
            InitializeComponent();
            this.content = content;
            updating = (UpdatingListener)content;
            this.db = db;
            this.product = db.Products.Where(x => x.Id == product.Id).Select(x => x).FirstOrDefault();
            imgProduct.Source = new BitmapImage(new Uri(product.Image));
            txtName.Text = product.Name;
            // txtPrice.Text = convertToStandar(product.Price.Substring(0, product.Price.Length - 3));
            txtPrice.Text = product.Price;
            txtAmount.Text = product.Amount.ToString();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if(txtPrice.Text == "" || txtName.Text == "")
            {
                MessageBox.Show("Do not empty");
            }
            else
            {
                product.Name = txtName.Text.ToString();
                // product.Price = txtPrice.Text.ToString() + "000";
                product.Price = txtPrice.Text.ToString();
                product.Amount = Int32.Parse(txtAmount.Text.ToString());
                db.SaveChanges();
                updating.updateListView();
                Close();
            }
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

        private string convertToStandar(string price)
        {
            string tmp = "";
            for (int i = 0; i < price.Length; i++)
            {
                if (price[i] != ',' && price[i] != ' ')
                    tmp += price[i];
            }
            return tmp;
        }

        private void txtPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
