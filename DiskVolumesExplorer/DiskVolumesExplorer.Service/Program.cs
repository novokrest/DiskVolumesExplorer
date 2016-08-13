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
                service.Start();
                Console.WriteLine("Hypervisor service started ...");

                var pressedKey = Console.ReadKey();

                service.Stop();
                Console.WriteLine("Hypervisor service stopped ...");

                return pressedKey;
            }
        }
    }
}
