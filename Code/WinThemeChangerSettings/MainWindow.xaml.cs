using System;
using System.Windows;
using WinThemeChangerLib;

namespace WinThemeChangerSettings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            if (!Settings.GetInstance().LoadSettings())
                MessageBox.Show("Ocorreu um erro ao carregar as configurações", "Erro de Sistema",
                    MessageBoxButton.OK, MessageBoxImage.Error);

            mtbLightScheduledTime.Text = Settings.GetInstance().LightScheduledTime;
            chkLightChangeWindowMode.IsChecked = Settings.GetInstance().LightChangeWindowMode;
            chkLightChangeApplicationMode.IsChecked = Settings.GetInstance().LightChangeApplicationMode;
            mtbDarkScheduledTime.Text = Settings.GetInstance().DarkScheduledTime;
            chkDarkChangeWindowMode.IsChecked = Settings.GetInstance().DarkChangeWindowMode;
            chkDarkChangeApplicationMode.IsChecked = Settings.GetInstance().DarkChangeApplicationMode;
        }

        private void AboutMenuItem_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow window = new AboutWindow();
            window.ShowDialog();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate())
                return;

            Settings.GetInstance().LightScheduledTime = mtbLightScheduledTime.Text;
            Settings.GetInstance().LightChangeWindowMode = (bool)chkLightChangeWindowMode.IsChecked;
            Settings.GetInstance().LightChangeApplicationMode = (bool)chkLightChangeApplicationMode.IsChecked;
            Settings.GetInstance().DarkScheduledTime = mtbDarkScheduledTime.Text;
            Settings.GetInstance().DarkChangeWindowMode = (bool)chkDarkChangeWindowMode.IsChecked;
            Settings.GetInstance().DarkChangeApplicationMode = (bool)chkDarkChangeApplicationMode.IsChecked;

            if (!Settings.GetInstance().SaveSettings())
                MessageBox.Show("Falha ao salvar as configurações.", "Erro de Sistema",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                MessageBox.Show("Configurações salvas com sucesso.", "Sucesso!",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }

        private bool Validate()
        {
            if (!mtbLightScheduledTime.IsMaskCompleted)
            {
                MessageBox.Show("No grupo \"Tema Claro\" insira um horário para iniciar o tema.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            if (!(bool)chkLightChangeApplicationMode.IsChecked && !(bool)chkLightChangeWindowMode.IsChecked)
            {
                MessageBox.Show("No grupo \"Tema Claro\" selecione pelo menos uma das caixas de seleção.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            if (!mtbDarkScheduledTime.IsMaskCompleted)
            {
                MessageBox.Show("No grupo \"Tema Escuro\" insira um horário para iniciar o tema.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            if (!(bool)chkDarkChangeApplicationMode.IsChecked && !(bool)chkDarkChangeWindowMode.IsChecked)
            {
                MessageBox.Show("No grupo \"Tema Escuro\" selecione pelo menos uma das caixas de seleção.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);

                return false;
            }

            if (mtbLightScheduledTime.Text.Equals(mtbDarkScheduledTime.Text))
            {
                MessageBox.Show("O horário de início dos temas não podem ser os mesmos.", "Atenção",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return true;
        }
    }
}
