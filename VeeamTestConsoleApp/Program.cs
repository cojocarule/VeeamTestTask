// See https://aka.ms/new-console-template for more information
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

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
int y = 1; int.TryParse(lifetime, out y);

var timer = new PeriodicTimer(TimeSpan.FromMinutes(x)); //Convert declared frequency to periodic timer

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
    FindAndKillProcess();
}
while (await timer.WaitForNextTickAsync() & !exit);

void FindAndKillProcess()
{
    Process[] pname = Process.GetProcessesByName(processName: name);
    if (pname.Length > 0)
    {
        Console.WriteLine($"Process {name} found");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        TimeSpan runtime = DateTime.Now - pname.LastOrDefault().StartTime;
        Console.WriteLine($"Process {name} has been running for {runtime.TotalMinutes} minutes");
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        if (runtime.TotalMinutes >= y)
        {
            Console.WriteLine($"Closing process {name} ");
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            pname.LastOrDefault().Kill();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            //Environment.Exit(0);
        }
    }
    else
    {
        Console.WriteLine($"Process {name} not found");
    }
        
}