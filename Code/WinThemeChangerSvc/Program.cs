using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace WinThemeChangerSvc
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var assembly = typeof(Program).Assembly;
            var attribute = (GuidAttribute)assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0];
            var id = attribute.Value;
            using (Mutex mutex = new Mutex(false, id))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("O serviço do Windows 10 Theme Changer já está em execução.", "Serviço já em execução",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                GC.Collect();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainAppContext());
            }
        }
    }
}
