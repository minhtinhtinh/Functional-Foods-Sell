using Milestone;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace milestone01
{
    class idCustomerOrderingToNameCustomerConverter : IValueConverter
    {
        DatabaseSalesEntities db;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            db = new DatabaseSalesEntities();
            try
            {
                foreach (var ctm in db.Customers)
                {
                    if (ctm.Id == int.Parse(value.ToString()))
                    {
                        return ctm.FullName;
                    }
                }
                return "Unknown";
            }
            catch (Exception)
            {

            }
            return null;           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
