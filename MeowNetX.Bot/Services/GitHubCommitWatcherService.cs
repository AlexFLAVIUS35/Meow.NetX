using System.Text.Json;

namespace MeowNetX.Bot.Services;

public class GitHubCommitWatcherService : BackgroundService
{
    private const string Repo =
        "https://api.github.com/repos/AlexFLAVIUS35/Meow.NetX/commits?sha=main";

    private readonly DiscordWebhookService discord = new();

    private readonly string SaveFile = "lastcommit.txt";

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("?? GitHub commit watcher started");
        Console.WriteLine("Watching AlexFLAVIUS35/Meow.NetX main commits");

        using var client = new HttpClient();

        client.DefaultRequestHeaders.UserAgent.ParseAdd("MeowNetX-Bot");

        string? lastCommit = null;

        if (File.Exists(SaveFile))
            lastCommit = await File.ReadAllTextAsync(SaveFile);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var json = await client.GetStringAsync(Repo, stoppingToken);

                using var doc = JsonDocument.Parse(json);

                var commits = doc.RootElement;

                if (commits.GetArrayLength() > 0)
                {
                    var newestSha = commits[0]
                        .GetProperty("sha")
                        .GetString();

                    if (newestSha != lastCommit)
                    {
                        Console.WriteLine("?? New commits detected!");

                        foreach (var commit in commits.EnumerateArray())
                        {
                            var sha = commit
                                .GetProperty("sha")
                                .GetString();

                            if (sha == lastCommit)
                                break;

                            var shortSha = sha?.Substring(0, 7);

                            var message = commit
                                .GetProperty("commit")
                                .GetProperty("message")
                                .GetString();

                            var author = commit
                                .GetProperty("commit")
                                .GetProperty("author")
                                .GetProperty("name")
                                .GetString();

                            var output =
$"""
?? Meow.NetX/main

{shortSha} {message}
by {author}

https://github.com/AlexFLAVIUS35/Meow.NetX/commit/{sha}
""";

                            Console.WriteLine(output);

                            await discord.Send(output);
                        }

                        lastCommit = newestSha;

                        await File.WriteAllTextAsync(
                            SaveFile,
                            lastCommit);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? GitHub watcher error: {ex.Message}");
            }

            await Task.Delay(
                TimeSpan.FromSeconds(30),
                stoppingToken);
        }
    }
}
