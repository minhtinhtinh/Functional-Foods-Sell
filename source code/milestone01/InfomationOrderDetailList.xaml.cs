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
    /// Interaction logic for InfomationOrderDetailList.xaml
    /// </summary>
    public partial class InfomationOrderDetailList : Window
    {
        public InfomationOrderDetailList()
        {
            InitializeComponent();
        }

        public InfomationOrderDetailList(BindingList<OrderingDetail> cart)
        {
            InitializeComponent();

            listInfoOrderDetail.ItemsSource = cart;
        }
    }
}
