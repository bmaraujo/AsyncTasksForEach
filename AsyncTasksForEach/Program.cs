using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace AsyncTasksForEach
{
    class Program
    {
        static void Main(string[] args)
        {
            var doThese = new[] {1,2,3,4,5,6,7,8,9,10 };

            RunNoListAsync(doThese).Wait();

            RunListAsync(doThese).Wait();           
        }

        private static async Task RunNoListAsync(int[] tasks)
        {
            Console.WriteLine("Waiting inside a foreach.");
            Stopwatch stopwatch = Stopwatch.StartNew();
            foreach(int x in tasks)
            {
                await DoSomethingAsync(x);
            }
            stopwatch.Stop();
            Console.WriteLine($"It took {stopwatch.Elapsed.Seconds} seconds to execute all tasks.");
        }

        private static async Task RunListAsync(int[] tasks)
        {
            Console.WriteLine("Waiting all tasks at once after the foreach.");
            Stopwatch stopwatch = Stopwatch.StartNew();
            List<Task> tasks_ = new List<Task>();
            foreach (int x in tasks)
            {
                tasks_.Add(DoSomethingAsync(x));
            }
            await Task.WhenAll(tasks_);
            stopwatch.Stop();
            Console.WriteLine($"It took {stopwatch.Elapsed.Seconds} seconds to execute all tasks.");
        }

        private static bool CheckForList(string[] args)
        {
            bool list = false;
            foreach (string argument in args)
            {
                if (argument.Equals("-list"))
                {
                    list = true;
                }
            }

            return list;
        }

        private static async Task DoSomethingAsync(int x) {
            Console.WriteLine($"Doing {x}... ({DateTime.Now:hh:mm:ss})");
            await Task.Delay(1000);
            Console.WriteLine($"{x} done.    ({DateTime.Now:hh:mm:ss})");
        }

    }
}
