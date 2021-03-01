using System;
using System.Collections.Generic;
using System.Threading;

namespace Threads
{
    class Program
    {
        private static SimpleStack<int> stack = new SimpleStack<int>();
        private static object lockObject = new object();

        static void Main()
        {
            Random rand = new Random();
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 5; i++)
            {
                Thread t = new Thread(Execute);
                t.IsBackground = true;
                t.Start(rand);
                threads.Add(t);
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }
        }

        public static void Execute(object randomGenerator)
        {
            Random rand = (Random)randomGenerator;
            while (true)
            {

                if (rand.NextDouble() > 0.6)
                {
                    stack.Push(rand.Next());
                }
                else
                {
                    lock (lockObject)
                    {
                        if (!stack.IsEmpty)
                        {
                            stack.Pop();
                        }
                    }
                }
            }
        }
    }
}
