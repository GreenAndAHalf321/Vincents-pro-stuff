using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerSim
{
    class Program
    {
        static void Main(string[] args)
        {
            Task[] tasks = new Task[1000000];

            for(int i = 0; i < 1000000; i++)
            {
                Task t = new Task(() => new Simulation(4));
                tasks[i] = t;
                t.Start();
            }

            Task.WaitAll(tasks);

            Console.WriteLine(Simulation.Tries);

            Console.ReadKey();
        }
    }
}
