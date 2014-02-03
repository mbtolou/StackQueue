using System;
using System.Windows.Forms;

namespace StackQueue
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            RandomNumbers.Seed(DateTime.Now.TimeOfDay.Seconds);
            Application.Run(new frmMain());
        }
    }
}