using DiscordRPC;

var discord = new DiscordRpcClient("YOUR_DISCORD_APPLICATION_ID");

discord.Initialize();

discord.SetPresence(new RichPresence
{
    Details = "Running Meow.NetX",
    State = "Launcher online",
    Assets = new Assets
    {
        LargeImageKey = "meownet",
        LargeImageText = "Meow.NetX"
    }
});

Console.WriteLine("Meow.NetX Launcher running.");
Console.WriteLine("Press CTRL+C to exit.");

while (true)
{
    Thread.Sleep(1000);
}
