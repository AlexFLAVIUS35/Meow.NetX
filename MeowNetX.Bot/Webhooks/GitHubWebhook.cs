using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace MeowNetX.Bot;

public static class GitHubWebhook
{
    private const string DiscordUrl = "https://discord.com/api/webhooks/1533250085826203789/ieJ65NA7_88XP7EpUbyFtowPsV1hPy2yEcPU0NzlnZbDXxH1pB02xF2M177vsr_LgOSU";

    public static void Start()
    {
        var builder = WebApplication.CreateBuilder();

        var app = builder.Build();

        app.MapPost("/github", async (HttpRequest request) =>
        {
            using var reader = new StreamReader(request.Body);
            var body = await reader.ReadToEndAsync();

            using var json = JsonDocument.Parse(body);

            var branch = "unknown";

            if (json.RootElement.TryGetProperty("ref", out var refValue))
            {
                branch = refValue.GetString()?
                    .Replace("refs/heads/", "") ?? "unknown";
            }

            var commits = new List<string>();

            if (json.RootElement.TryGetProperty("commits", out var commitArray))
            {
                foreach (var commit in commitArray.EnumerateArray())
                {
                    var id = commit.GetProperty("id")
                        .GetString()?
                        .Substring(0, 7);

                    var message = commit.GetProperty("message")
                        .GetString()?
                        .Split('\n')[0];

                    commits.Add($"{id} {message}");
                }
            }

            await DiscordWebhook.Send(
                DiscordUrl,
                branch,
                commits
            );

            return Results.Ok();
        });

        app.Run("http://localhost:5000");
    }
}
