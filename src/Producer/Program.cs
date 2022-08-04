// See https://aka.ms/new-console-template for more information
using System.CommandLine;

Console.WriteLine("Hello, World!");

var fileOption = new Option<string>(
            name: "--file",
            description: "The file to read and display on the console.");

var rootCommand = new RootCommand("Sample app for System.CommandLine");
rootCommand.AddOption(fileOption);

rootCommand.SetHandler((fileName) => {
  Console.WriteLine(fileName);
}, fileOption);

return await rootCommand.InvokeAsync(args);