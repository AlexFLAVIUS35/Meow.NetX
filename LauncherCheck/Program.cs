using System;
using System.IO;

if (args.Length == 0)
{
    Console.WriteLine("No folder provided.");
    return 1;
}

string folder = args[0];

string launcher = Path.Combine(folder, "Launcher.exe");

if (File.Exists(launcher))
{
    Console.WriteLine("Launcher.exe found.");
    return 0;
}

Console.WriteLine("Launcher.exe missing.");
return 1;
