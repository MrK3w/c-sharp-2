using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading;
using Threads;

namespace Synchronization
{
    class Program
    {
        private static object lockObject = new object();
        static void Main(string[] args)
        {
            Random rnd = new Random();
            List<Thread> threads = new List<Thread>();
            SimpleStack<int> stack = new SimpleStack<int>();
            Thread t1 = new Thread(() =>
            {
                while (true)
                {
                    stack.Push(rnd.Next());
                    Thread.Sleep(100);
                }
            });
            t1.Start();

            for (int i = 0; i < 5; i++)
            {
                Thread t2 = new Thread(() =>
                {
                    while (true)
                    {
                        int val;
                        lock (lockObject)
                        {
                            if (stack.IsEmpty)
                            {
                                Console.WriteLine($"Empty: {Thread.CurrentThread.ManagedThreadId}");
                                continue;
                            }
                        }

                        lock (lockObject)
                        {
                            val = stack.Pop();
                        }

                        Console.WriteLine($"Value: {val} | Thread: {Thread.CurrentThread.ManagedThreadId}");
                        

                        Thread.Sleep(rnd.Next(40,1001));
                    }
                });
                t2.Start();
                threads.Add(t2);
            }
            t1.Join();
            foreach (var t in threads)
            {
                t.Join();
            }
        }
    }
}