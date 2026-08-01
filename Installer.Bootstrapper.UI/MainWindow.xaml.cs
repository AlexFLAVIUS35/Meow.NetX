using System.Windows;
using System.Windows.Forms;

namespace Installer.Bootstrapper.UI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Browse_Click(object sender, RoutedEventArgs e)
    {
        using var dialog = new FolderBrowserDialog();

        if (dialog.ShowDialog() == DialogResult.OK)
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
        MessageBox.Show(
            "Installing to:\n" + FolderBox.Text
        );
    }
}
