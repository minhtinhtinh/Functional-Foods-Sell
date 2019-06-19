using Milestone;
using milestone01;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace milestone01
{
    class idProductToNameCategoryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var db = new DatabaseSalesEntities();
            var category = db.Categories;
            var categoryID = int.Parse(value.ToString());
            foreach (var cate in category)
            {
                if (cate.Id == categoryID)
                {
                    return cate.Name;
                }
            }

            return "Unknow";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
