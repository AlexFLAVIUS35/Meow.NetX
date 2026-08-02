using DiscordRPC;
using System.IO;

var rpc = new DiscordRpcClient("1533218071148495038");

rpc.Initialize();

string currentStatus = "Idle";

string[] watchPaths =
{
    @"C:\Users\User\Meow.NetX\",
};

string[] ignoredPaths =
{
    @"\.git\",
    "obj",
    "auto-git-sync.ps1",
    "auto-watch-sync.ps1"
};

bool ShouldIgnore(string path)
{
    foreach (var ignored in ignoredPaths)
    {
        if (path.Contains(ignored, StringComparison.OrdinalIgnoreCase))
            return true;
    }

    return false;
}

foreach (var path in watchPaths)
{
    if (!Directory.Exists(path))
        continue;

    var watcher = new FileSystemWatcher(path)
    {
        IncludeSubdirectories = true,
        NotifyFilter = NotifyFilters.FileName |
                       NotifyFilters.LastWrite |
                       NotifyFilters.Size
    };

    watcher.Changed += (s, e) =>
    {
        if (ShouldIgnore(e.FullPath))
            return;

        currentStatus = $"Edited {Path.GetFileName(e.FullPath)}";
    };

    watcher.Created += (s, e) =>
    {
        if (ShouldIgnore(e.FullPath))
            return;

        currentStatus = $"Created {Path.GetFileName(e.FullPath)}";
    };

    watcher.Renamed += (s, e) =>
    {
        if (ShouldIgnore(e.FullPath))
            return;

        currentStatus = $"Renamed {Path.GetFileName(e.FullPath)}";
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
            LargeImageKey = "meowmeowlogo",
            LargeImageText = "Meow.NetX"
        },

        Buttons = new[]
        {
            new DiscordRPC.Button
            {
                Label = "Check Out The PROJECT!!!",
                Url = "https://github.com/AlexFLAVIUS35/Meow.NetX/"
            }
        }
    });

    Thread.Sleep(2000);
}