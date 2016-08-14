using System;
using System.ServiceProcess;

namespace DiskVolumesExplorer.Service
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                RunServiceLoop();
            }
            else
            {
                ServiceBase.Run(new HypervisorServiceWindowsHost());
            }
        }

        private static void RunServiceLoop()
        {
            ConsoleKeyInfo pressedKey;

            do
            {
                pressedKey = RunService();
            } while (pressedKey.Key == ConsoleKey.R);
        }

        private static ConsoleKeyInfo RunService()
        {
            using (var service = new HypervisorServiceWindowsHost())
            {
                Console.WriteLine("Starting Hypervisor service...");
                service.Start();
                Console.WriteLine("Hypervisor service has been started.");

                var pressedKey = Console.ReadKey();

                Console.WriteLine("Stopping Hypervisor service...");
                service.Stop();
                Console.WriteLine("Hypervisor service has been stopped.");

                return pressedKey;
            }
        }
    }
}
