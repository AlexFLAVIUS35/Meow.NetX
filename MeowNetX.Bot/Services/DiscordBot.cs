using Discord;
using Discord.WebSocket;

namespace MeowNetX.Bot;

public class DiscordBot
{
    private readonly DiscordSocketClient _client = new();

    public async Task Start()
    {
        _client.Log += Log;

        await _client.LoginAsync(
            TokenType.Bot,
            "PUT_TOKEN_HERE"
        );

        await _client.StartAsync();

        await Task.Delay(-1);
    }

    private Task Log(LogMessage msg)
    {
        Console.WriteLine(msg);
        return Task.CompletedTask;
    }
}
