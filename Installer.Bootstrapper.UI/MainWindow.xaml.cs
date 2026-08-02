using System.Windows;

namespace Installer.Bootstrapper.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Browse_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new Microsoft.Win32.OpenFolderDialog();

        if (dialog.ShowDialog() == true)
        {
            FolderBox.Text = dialog.FolderName;

            bool valid = LauncherDetector.IsValid(dialog.FolderName);

            InstallButton.IsEnabled = valid;
        }
    }

    private void Install_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.MessageBox.Show("Ready to install Meow.NetX!");
    }
}
