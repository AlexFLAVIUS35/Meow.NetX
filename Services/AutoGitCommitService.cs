using System.Diagnostics;

namespace MeowNetX.Bot.Services;

public class AutoGitCommitService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("?? Auto Git Commit started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var status = RunGit("status --porcelain");

                if (!string.IsNullOrWhiteSpace(status))
                {
                    Console.WriteLine("?? Changes detected");

                    RunGit("add .");

                    RunGit($"commit -m \"Auto commit {DateTime.Now:yyyy-MM-dd HH:mm:ss}\"");

                    RunGit("push");

                    Console.WriteLine("? Changes committed and pushed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"? Git error: {ex.Message}");
            }

            await Task.Delay(30000, stoppingToken);
        }
    }

    private static string RunGit(string args)
    {
        using var p = Process.Start(new ProcessStartInfo
        {
            FileName = "git",
            Arguments = args,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        });

        return p!.StandardOutput.ReadToEnd();
    }
}
