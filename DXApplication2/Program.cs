using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using DevExpress.LookAndFeel;
using DXApplication2.Controller;
using DXApplication2.Model;
using DXApplication2.View;

namespace DXApplication2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            BonusSkins.Register();

            // View
            Form1 mainForm = new Form1();

            // Model
            GridDataModel dataModel = new GridDataModel();

            // Controller
            GridViewController controller = new GridViewController(mainForm, dataModel);
            mainForm.ConnectWithController(controller);

            Application.Run(mainForm);
        }
    }
}
