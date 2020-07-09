using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.DataAccess.Sql;
using System.Data;
using DXApplication2.Controller;

namespace DXApplication2.View
{
    public interface IMainView
    {
        void ConnectWithController(GridViewController controller);
        SqlDataSource GetDataSource();
        void SetGridDataSource(DataTable table);
    }
}
