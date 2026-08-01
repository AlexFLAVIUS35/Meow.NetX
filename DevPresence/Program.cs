using DiscordRPC;
using System.IO;

var rpc = new DiscordRpcClient("1533218071148495038");

rpc.Initialize();

string currentStatus = "Idle";
System.Threading.Timer? updateTimer = null;

void UpdateStatus(string action, string file)
{
    currentStatus = $"{action} {file}";

    updateTimer?.Dispose();

    updateTimer = new System.Threading.Timer(_ =>
    {
        currentStatus = "Idle";
    }, null, 10000, System.Threading.Timeout.Infinite);
}

string[] watchPaths =
{
    @"C:\Users\User\Meow.NetX\Installer.Wix",
    @"C:\Users\User\Meow.NetX\PluginLoader",
    @"C:\Users\User\Meow.NetX\Meow.NetX",
    @"C:\Users\User\Meow.NetX\Patcher",
    @"C:\Users\User\Meow.NetX\TestPlugin"
};

foreach (var path in watchPaths)
{
    if (!Directory.Exists(path))
        continue;

    var watcher = new FileSystemWatcher(path)
    {
        IncludeSubdirectories = true,
        NotifyFilter = NotifyFilters.FileName |
                       NotifyFilters.LastWrite
    };

    watcher.Changed += (s, e) =>
    {
        UpdateStatus("Editing", Path.GetFileName(e.FullPath));
    };

    watcher.Created += (s, e) =>
    {
        UpdateStatus("Created", Path.GetFileName(e.FullPath));
    };

    watcher.Renamed += (s, e) =>
    {
        UpdateStatus("Renamed", Path.GetFileName(e.FullPath));
    };

    watcher.EnableRaisingEvents = true;
}

while (true)
{
    rpc.SetPresence(new RichPresence
    {
        Details = "Working on Meow.NetX",
        State = currentStatus,
        Assets = new Assets
        {
            LargeImageKey = "meownet",
            LargeImageText = "Meow.NetX"
        }
    });

    Thread.Sleep(2000);
}

