using System.Reflection;

namespace PluginLoader;

public static class PluginLoader
{
    public static void LoadPlugins()
    {
        string pluginDirectory = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Plugins");

        Directory.CreateDirectory(pluginDirectory);

        foreach (string file in Directory.GetFiles(pluginDirectory, "*.dll"))
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(file);

                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IPlugin).IsAssignableFrom(type) &&
                        Activator.CreateInstance(type) is IPlugin plugin)
                    {
                        plugin.Initialize();
                    }
                }
            }
            catch
            {
                // Ignore broken plugins
            }
        }
    }
}
