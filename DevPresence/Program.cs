using DiscordRPC;
using System.Diagnostics;

var rpc = new DiscordRpcClient("1533218071148495038");

rpc.Initialize();

string status = "Waiting for CMD";

while (true)
{
    try
    {
        var history = Process.Start(new ProcessStartInfo
        {
            FileName = "cmd",
            Arguments = "/c doskey /history",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        });

        string output = history?.StandardOutput.ReadToEnd() ?? "";

        var lines = output
            .Split('\n', StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length > 0)
            status = "CMD: " + lines[^1].Trim();
    }
    catch
    {
        status = "CMD active";
    }

    rpc.SetPresence(new RichPresence
    {
        Details = "Working on Meow.NetX",
        State = status,
        Assets = new Assets
        {
            LargeImageKey = "meownet",
            LargeImageText = "Meow.NetX"
        }
    });

    Thread.Sleep(3000);
}
