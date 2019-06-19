using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace milestone01
{
    interface UpdatingListener
    {
        void updateListView();
        void updateCombobox();

        void deleteFromId(int id);
    }
}
