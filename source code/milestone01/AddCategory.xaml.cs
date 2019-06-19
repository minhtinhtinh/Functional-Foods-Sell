using Milestone;
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

namespace milestone01
{
    /// <summary>
    /// Interaction logic for AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        DatabaseSalesEntities db;
        ComboBox cb;
        public AddCategory()
        {
            InitializeComponent();
        }

        public AddCategory(DatabaseSalesEntities db, ComboBox cb)
        {
            InitializeComponent();
            this.db = db;
            this.cb = cb;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(txtName.Text.ToString() == "")
            {
                lbError.Content = "Không được để trống";
            }
            else
            {
                Category cgr = new Category();
                cgr.Name = txtName.Text.ToString();
                cgr.Descript = txtDetail.Text.ToString();
                db.Categories.Add(cgr);
                db.SaveChanges();
                int index = cb.SelectedIndex;
                cb.ItemsSource = db.Categories.ToList();
                cb.SelectedIndex = index;
                this.Close();
            }
        }
    }
}
