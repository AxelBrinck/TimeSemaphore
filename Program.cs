using System;

namespace TimeSemaphore
{
    class Program
    {
        static void Main(string[] args)
        {
            var semaphore = new TimeSemaphore(1200, TimeSpan.FromSeconds(60));

            for (var i = 0; i < 2000; i++)
            {
                semaphore.Wait();
                Console.WriteLine($"{i} going!");
            }
        }
    }
}
