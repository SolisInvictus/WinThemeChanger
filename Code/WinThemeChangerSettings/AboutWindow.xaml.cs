using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WinThemeChangerSettings
{
    /// <summary>
    /// Interaction logic for AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        private void GitHubImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Uri uri = new Uri("https://github.com/SolisInvictus/WinThemeChanger");
            System.Diagnostics.Process.Start(uri.AbsoluteUri);
        }

        private void MPLLicense_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Uri uri = new Uri("https://github.com/SolisInvictus/WinThemeChanger/blob/master/LICENSE.md");
            System.Diagnostics.Process.Start(uri.AbsoluteUri);
        }

        private void SolisInvictusImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Uri uri = new Uri("https://github.com/SolisInvictus");
            System.Diagnostics.Process.Start(uri.AbsoluteUri);
        }
    }
}
