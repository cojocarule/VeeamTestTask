using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeeamTestConsoleApp
{
    public class FindAndKillProcess
    {
        public static List<int> DoFindAndKillProcess(string name, string lifetime)
        {
            var result = new List<int>(); //create a list to record progress

            int y = 1; int.TryParse(lifetime, out y);
            Process[] pname = Process.GetProcessesByName(processName: name);
            if (pname.Length > 0)
            {
                Console.WriteLine($"Process {name} found");
                result.Add(0);
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
                    result.Add(0);
                }
                else
                {
                    result.Add(1);
                }
            }
            else
            {
                Console.WriteLine($"Process {name} not found");
                result.Add(1);
            }
            return result;
        }

    }
}
