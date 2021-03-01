using System;
using System.IO;
using System.Threading.Tasks;

namespace AsyncProgramming
{
    class Program
    {
        static async Task Main()
        {
            await Experiment();
        }

        public static async Task Experiment()
        {
            Console.WriteLine("Start");
            await Task.Delay(1000);
            using (StreamWriter writer = new StreamWriter("temp.txt"))
            {
                await writer.WriteAsync("Hello world it's me\nI am dimitri");
            }
            await Task.Delay(1000);
            Console.WriteLine("end");
        }
            



    }
}
