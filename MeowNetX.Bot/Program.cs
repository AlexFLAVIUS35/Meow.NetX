namespace MeowNetX.Bot;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("?? Meow.NetX BOT starting...");
        Console.WriteLine("?? GitHub Commit Watcher starting...");

        var builder = WebApplication.CreateBuilder();

        builder.Services.AddHostedService<MeowNetX.Bot.Services.GitHubCommitWatcherService>();

        var app = builder.Build();

        app.Run();
    }
}
