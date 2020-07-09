using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DXApplication2.Controller;
using DXApplication2.Model;
using DevExpress.DataAccess.Sql;
using DXApplication2.View;
namespace DXApplication2
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm, IMainView
    {
        private GridViewController controller;

        public Form1()
        {
            InitializeComponent();
            sqlDataSource1.Fill();
        }
        public void ConnectWithController(GridViewController controller)
        {
            this.controller = controller;
        }
        public SqlDataSource GetDataSource()
        {
            return sqlDataSource1;
        }
        public void SetGridDataSource(DataTable table)
        {
            gridControl1.DataSource = table;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            controller.SaveData();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            controller.AddNewRow();
        }
    }
}
