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
    /// Interaction logic for EditCategory.xaml
    /// </summary>
    public partial class EditCategory : Window
    {
        DatabaseSalesEntities db;
        ComboBox cb;
        int currID;
        Category cgr;
        ContentControl content;
        UpdatingListener updating;
        public EditCategory()
        {
            InitializeComponent();
        }

        public EditCategory(DatabaseSalesEntities db, ComboBox cb, ContentControl content)
        {
            InitializeComponent();
            this.content = content;
            updating = (UpdatingListener)content;
            this.db = db;
            this.cb = cb;
            this.currID = (cb.SelectedValue as Category).Id;
            this.cgr = db.Categories.Where(x => x.Id == currID).Select(x => x).FirstOrDefault();
            txtName.Text = cgr.Name;
            txtDetail.Text = cgr.Descript;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (txtName.Text.ToString() == "")
            {
                lbError.Content = "Không được để trống";
            }
            else
            {

                cgr.Name = txtName.Text.ToString();
                cgr.Descript = txtDetail.Text.ToString();
                db.SaveChanges();                
                updating.updateCombobox();
                this.Close();
            }
        }
    }
}
