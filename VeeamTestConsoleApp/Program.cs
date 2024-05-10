// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using VeeamTestConsoleApp;

//Start. declare arguments and variables
Console.WriteLine("MONITOR AND CLOSE A PROCESS");
string name = Environment.GetCommandLineArgs()[1];
Console.WriteLine($"Process Name: {name}");
string lifetime = Environment.GetCommandLineArgs()[2];
Console.WriteLine($"Process Lifetime(minutes): {lifetime}");
string frequency = Environment.GetCommandLineArgs()[3];
Console.WriteLine($"Monitoring Frequency(minutes): {frequency}");
Console.WriteLine($"Type 'q' to exit");
Console.WriteLine($"============================================================");

bool exit = false;
int x = 1; int.TryParse(frequency, out x);
var timer = new PeriodicTimer(TimeSpan.FromMinutes(x)); //Convert declared frequency to periodic timer
var findAndKillProcess = new FindAndKillProcess();

//Monitor for exit command 
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
Task.Factory.StartNew(() =>
{
    while (Console.ReadKey().Key == ConsoleKey.Q) ;
    exit = true;
    Environment.Exit(1);
});
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

//Run the loop on timer
do
{
    Console.WriteLine($"Checking now");
    FindAndKillProcess.DoFindAndKillProcess(name, lifetime);
}
while (await timer.WaitForNextTickAsync() & !exit);
