using System;

namespace TimeSemaphore
{
    class Program
    {
        static void Main(string[] args)
        {
            var semaphore = new TimeSemaphore(1, TimeSpan.FromSeconds(1));

            for (var i = 0; i < 20; i++)
            {
                semaphore.Wait();
                Console.WriteLine("Green!");
            }
        }
    }
}
