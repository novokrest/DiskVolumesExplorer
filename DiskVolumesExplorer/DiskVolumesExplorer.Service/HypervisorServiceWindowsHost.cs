using DiskVolumesExplorer.Service.Mocks;
using System.ServiceModel;
using System.ServiceProcess;

namespace DiskVolumesExplorer.Service
{
    internal class HypervisorServiceWindowsHost : ServiceBase
    {
        public const string HypervisorServiceName = "HypervisorService";

        public ServiceHost serviceHost = null;

        public HypervisorServiceWindowsHost()
        {
            ServiceName = HypervisorServiceName;
        }

        public void Start()
        {
            CloseServiceHost();
            OpenServiceHost();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            Start();
        }

        private void OpenServiceHost()
        {
            serviceHost = new ServiceHost(typeof(MockHypervisorService));
            serviceHost.Open();
        }

        protected override void OnStop()
        {
            base.OnStop();
            CloseServiceHost();
        }

        private void CloseServiceHost()
        {
            if (serviceHost != null)
            {
                serviceHost.Close();
                serviceHost = null;
            }
        }
    }
}
