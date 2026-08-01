using System;

string? folder = Environment.GetEnvironmentVariable("INSTALLFOLDER");

if (string.IsNullOrEmpty(folder))
{
    return 1;
}

return File.Exists(Path.Combine(folder, "Launcher.exe"))
    ? 0
    : 1;
