using DiskVolumesExplorer.Service.Configs.VmWare;
using System.ServiceModel;
using System.ServiceProcess;

namespace DiskVolumesExplorer.Service
{
    internal class HypervisorWindowsService : ServiceBase
    {
        public const string HypervisorServiceName = "HypervisorService";

        public ServiceHost serviceHost = null;

        public HypervisorWindowsService()
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
            var vmWareConfigLoader = new VmWareConfigLoader();
            var hypervisorService = new HypervisorService(vmWareConfigLoader);
            serviceHost = new ServiceHost(hypervisorService);
            serviceHost.Description.Behaviors.Find<ServiceBehaviorAttribute>().InstanceContextMode = InstanceContextMode.Single;
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
