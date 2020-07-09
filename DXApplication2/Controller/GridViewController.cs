using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DXApplication2.Model;
using DXApplication2.View;

namespace DXApplication2.Controller
{
    public class GridViewController
    {
        IMainView view;
        GridDataModel model;

        public GridViewController(IMainView view, GridDataModel model)
        {
            this.view = view;
            this.model = model;

            model.GetDataTableBySource(view.GetDataSource());
            ConnectModelAndView();
        }
        public void ConnectModelAndView()
        {
            view.SetGridDataSource(model.LoadedTable);
        }
        public void AddNewRow()
        {
            model.AddRow();
        }
        public void RemoveRow()
        {
            model.DeleteRow();
        }
        public void SaveData()
        {
            model.UpdateDataChanged();
        }
    }
}
