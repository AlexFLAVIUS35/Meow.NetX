using System.Net.Http.Json;

namespace MeowNetX.Bot.Services;

public class DiscordWebhookService
{
    private readonly HttpClient client = new();

    private readonly string webhook =
        Environment.GetEnvironmentVariable("MEOW_DISCORD_WEBHOOK")
        ?? "";

    public async Task Send(string message)
    {
        if (string.IsNullOrWhiteSpace(webhook))
        {
            Console.WriteLine("? Discord webhook missing");
            return;
        }

        await client.PostAsJsonAsync(
            webhook,
            new
            {
                content = message
            });
    }
}
