using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Patcher;

public static class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage: Patcher <input.dll> <output.dll>");
            return;
        }

        string input = args[0];
        string output = args[1];

        var resolver = new DefaultAssemblyResolver();

        resolver.AddSearchDirectory(
            Path.GetDirectoryName(input)!);

        resolver.AddSearchDirectory(
            Path.GetDirectoryName(typeof(PluginLoader.PluginLoader).Assembly.Location)!);

        var parameters = new ReaderParameters
        {
            AssemblyResolver = resolver
        };

        var assembly = AssemblyDefinition.ReadAssembly(input, parameters);
        var module = assembly.MainModule;

        var program = module.Types
            .First(t => t.Name == "Program");

        var main = program.Methods
            .First(m => m.Name == "Main");

        var loaderAssembly = AssemblyDefinition.ReadAssembly(
            typeof(PluginLoader.PluginLoader).Assembly.Location);

        var loaderType = loaderAssembly.MainModule.Types
            .First(t => t.Name == "PluginLoader");

        var loadMethod = loaderType.Methods
            .First(m => m.Name == "LoadPlugins");

        var imported = module.ImportReference(loadMethod);

        var processor = main.Body.GetILProcessor();

        var target = main.Body.Instructions
            .First(i =>
                i.OpCode == OpCodes.Call &&
                i.Operand.ToString()!.Contains("Application::Run"));

        processor.InsertBefore(
            target,
            processor.Create(OpCodes.Call, imported));

        assembly.Write(output);

        Console.WriteLine("Plugin loader injected successfully.");
    }
}
