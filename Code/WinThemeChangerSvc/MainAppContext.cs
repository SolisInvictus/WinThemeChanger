using System;
using System.Windows.Forms;
using WinThemeChangerLib;

namespace WinThemeChangerSvc
{
    class MainAppContext : ApplicationContext
    {
        private NotifyIcon AppNotifyIcon;

        public MainAppContext()
        {
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            AppNotifyIcon = new NotifyIcon
            {
                BalloonTipIcon = ToolTipIcon.Info,
                BalloonTipTitle = "Windows 10 Theme Changer",
                BalloonTipText = "O Windows 10 Theme Changer está rodando em segundo plano.",
                Text = "Windows 10 Theme Changer",
                Icon = Properties.Resources.Icon,
                Visible = true
            };

            AppNotifyIcon.Click += AppNotifyIcon_Click;

            ContextMenuStrip AppNotifyIconContextMenu = new ContextMenuStrip();
            ToolStripMenuItem ExitMenuItem = new ToolStripMenuItem();
            AppNotifyIconContextMenu.SuspendLayout();

            AppNotifyIconContextMenu.Items.AddRange(new ToolStripItem[] { ExitMenuItem });

            ExitMenuItem.Text = "Sair";
            ExitMenuItem.Click += new EventHandler(ExitMenuItem_Click);

            AppNotifyIconContextMenu.ResumeLayout(false);
            AppNotifyIcon.ContextMenuStrip = AppNotifyIconContextMenu;

            if (!Settings.GetInstance().LoadSettings())
            {
                MessageBox.Show("Falha ao carregar as configurações e por isto o serviço será interrompido", "Problema ao carregar configurações",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExitThread();
            }

            new JobScheduler().Schedule();
        }

        private void OnApplicationExit(object sender, EventArgs e)
        {
            AppNotifyIcon.Visible = false;
        }

        private void AppNotifyIcon_Click(object sender, EventArgs e)
        {
            if (((MouseEventArgs)e).Button == MouseButtons.Left)
                AppNotifyIcon.ShowBalloonTip(2000);
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja fechar o Windows 10 Theme Changer? Se você fechar este aplicativo não será possível mudar o tema no horário especificado.", "Sair do Windows 10 Theme Changer",
                MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ExitThread();
            }
        }
    }
}
