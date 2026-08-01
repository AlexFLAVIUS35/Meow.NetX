using System.Windows.Forms;
using PluginLoader;

public class TestPlugin : IPlugin
{
    public string Name => "Test Plugin";

    public void Initialize()
    {
        MessageBox.Show("Meow.NetX plugin loaded!");
    }
}
