namespace WinThemeChangerSvc
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.MainServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // MainServiceProcessInstaller
            // 
            this.MainServiceProcessInstaller.Password = null;
            this.MainServiceProcessInstaller.Username = null;
            // 
            // MainServiceInstaller
            // 
            this.MainServiceInstaller.Description = "Serviço que faz a alteração do tema conforme definido nas configurações do App.";
            this.MainServiceInstaller.DisplayName = "Windows 10 Theme Changer Service";
            this.MainServiceInstaller.ServiceName = "WinThemeChangerSvc";
            this.MainServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.MainServiceProcessInstaller,
            this.MainServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller MainServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller MainServiceInstaller;
    }
}