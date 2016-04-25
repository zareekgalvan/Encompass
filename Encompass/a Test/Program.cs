using System;
using System.Collections.Generic;
using TowaInfrastructure;
using System.Windows.Forms;
/* || */

namespace Encompass
{
    //[  1         2         3         4         5         6         7         8         9         0         1         2
    //]89012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890
    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        //--------------------------------------------------------------------------------------------------------------
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmPrueba());

            Environment.Exit(0);
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
