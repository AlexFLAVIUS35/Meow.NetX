using DiscordRPC;
using System.IO;

var rpc = new DiscordRpcClient("1533218071148495038");

rpc.Initialize();

string repoPath = @"C:\Users\User\Meow.NetX";

string[] ignored =
{
    @"\.git\",
    @"\bin\",
    @"\obj\",
    "auto-git-sync.ps1",
    "auto-watch-sync.ps1"
};

bool ShouldIgnore(string path)
{
    path = path.ToLower();

    foreach (var item in ignored)
    {
        if (path.Contains(item.ToLower()))
            return true;
    }

    return false;
}

string currentStatus = "Idle";

void Update(string path, string action)
{
    if (!ShouldIgnore(path))
    {
        currentStatus = $"{action}: {Path.GetFileName(path)}";
    }
}

using var watcher = new FileSystemWatcher(repoPath)
{
    IncludeSubdirectories = true,
    NotifyFilter = NotifyFilters.FileName |
                   NotifyFilters.LastWrite |
                   NotifyFilters.DirectoryName
};

watcher.Changed += (s,e) => Update(e.FullPath, "Edited");
watcher.Created += (s,e) => Update(e.FullPath, "Created");
watcher.Renamed += (s,e) => Update(e.FullPath, "Renamed");

watcher.EnableRaisingEvents = true;

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
