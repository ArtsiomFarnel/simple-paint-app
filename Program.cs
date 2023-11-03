using System;
using System.Windows.Forms;

namespace Paint
{
    class Program
    {
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Paint());
        }
    }
}
