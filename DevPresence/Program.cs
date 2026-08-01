using DiscordRPC;
using System.IO;

var rpc = new DiscordRpcClient("1533218071148495038");

rpc.Initialize();

string statusFile = "status.txt";

while (true)
{
    string status = File.Exists(statusFile)
        ? File.ReadAllText(statusFile)
        : "Idle";

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

    Thread.Sleep(2000);
}
