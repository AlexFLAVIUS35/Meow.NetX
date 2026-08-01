using DiscordRPC;
using System.IO;

var rpc = new DiscordRpcClient("1533218071148495038");

rpc.Initialize();

string repoPath = @"C:\Users\User\Meow.NetX";

string currentStatus = "Idle";

using var watcher = new FileSystemWatcher(repoPath)
{
    IncludeSubdirectories = true,
    NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite
};

watcher.Changed += (s, e) =>
{
    currentStatus = $"Editing {Path.GetFileName(e.FullPath)}";
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
