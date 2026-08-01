using System.Windows;
using Forms = System.Windows.Forms;

namespace Installer.Bootstrapper.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Browse_Click(object sender, RoutedEventArgs e)
    {
        using var dialog = new Forms.FolderBrowserDialog();

        if (dialog.ShowDialog() == Forms.DialogResult.OK)
        {
            FolderBox.Text = dialog.SelectedPath;

            bool valid = LauncherDetector.IsValid(dialog.SelectedPath);

            InstallButton.IsEnabled = valid;

            StatusText.Text = valid
                ? "? Launcher.exe found"
                : "? Launcher.exe missing";
        }
    }

    private void Install_Click(object sender, RoutedEventArgs e)
    {
        System.Windows.MessageBox.Show(
            "Installing to:\n" + FolderBox.Text
        );
    }
}
