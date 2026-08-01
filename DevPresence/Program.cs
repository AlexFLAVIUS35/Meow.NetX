using DiscordRPC;
using System.IO;

var rpc = new DiscordRpcClient("1533218071148495038");

rpc.Initialize();

string currentStatus = "Idle";

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
        currentStatus = $"Edited {Path.GetFileName(e.FullPath)}";
    };

    watcher.Created += (s, e) =>
    {
        currentStatus = $"Created {Path.GetFileName(e.FullPath)}";
    };

    watcher.Renamed += (s, e) =>
    {
        currentStatus = $"Renamed {Path.GetFileName(e.FullPath)}";
    };

    watcher.EnableRaisingEvents = true;
}

while (true)
{
    rpc.SetPresence(new RichPresence
    {
        Details = "Working on Meow.NetX/",
        State = currentStatus,
        Assets = new Assets
        {
            LargeImageKey = "meowmeowlogo",
            LargeImageText = "meowmeowlogo"
        },
    Buttons = new[]
    {
        new Button
        {
            Label = "Check Out The PROJECT!!!",
            Url = "https://github.com/AlexFLAVIUS35/Meow.NetX/"
        }
    });

    Thread.Sleep(2000);
}
