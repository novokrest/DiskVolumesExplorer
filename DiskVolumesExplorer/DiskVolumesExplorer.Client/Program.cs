using System;

namespace DiskVolumesExplorer.Client
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            App app = new App();
            app.InitializeComponent();

            app.Run(new MainWindow());
        }
    }
}
