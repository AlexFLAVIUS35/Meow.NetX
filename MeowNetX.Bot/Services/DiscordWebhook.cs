using System.Net.Http.Json;

namespace MeowNetX.Bot;

public static class DiscordWebhook
{
    private static readonly HttpClient Client = new();

    public static async Task Send(
        string webhookUrl,
        string branch,
        List<string> commits)
    {
        var payload = new
        {
            username = "Meow.NetX BOT",
            embeds = new[]
            {
                new
                {
                    title = $"[Meow.NetX:{branch}] {commits.Count} new commits",
                    description = string.Join("\n", commits),
                    footer = new
                    {
                        text = "Meow.NetX GitHub"
                    }
                }
            }
        };

        await Client.PostAsJsonAsync(
            webhookUrl,
            payload
        );
    }
}
