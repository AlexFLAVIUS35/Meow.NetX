using DiscordRPC;

var rpc = new DiscordRpcClient("YOUR_DISCORD_APPLICATION_ID");

rpc.Initialize();

rpc.SetPresence(new RichPresence
{
    Details = "Working on Meow.NetX",
    State = "Editing code",
    Assets = new Assets
    {
        LargeImageKey = "meownet",
        LargeImageText = "Meow.NetX"
    }
});

Console.WriteLine("Discord Rich Presence running.");
Console.ReadLine();
