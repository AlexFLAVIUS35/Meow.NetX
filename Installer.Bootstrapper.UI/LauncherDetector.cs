using System.IO;

public static class LauncherDetector
{
    public static bool IsValid(string folder)
    {
        return File.Exists(
            Path.Combine(folder, "Launcher.exe")
        );
    }
}
