namespace PluginLoader;

public interface IPlugin
{
    string Name { get; }

    void Initialize();
}
