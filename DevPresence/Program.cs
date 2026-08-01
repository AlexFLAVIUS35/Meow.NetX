using DiscordRPC;

var rpc = new DiscordRpcClient("1533218071148495038");

rpc.Initialize();

rpc.SetPresence(new RichPresence
{
    Details = "Working on Meow.NetX",
    State = "Editing code"
});

Console.WriteLine("Discord Rich Presence running.");
Console.WriteLine("Press ENTER to exit.");

Console.ReadLine();
