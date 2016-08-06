using System.ServiceModel;
using System.Windows;
using DiskVolumesExplorer.Client.VmWareServices;

namespace DiskVolumesExplorer.Client
{
    class ServerServiceProvider
    {
        public void Connect(string connectionUrl)
        {
            var client = new HypervisorClient(new WSHttpBinding(), new EndpointAddress(connectionUrl));
            MessageBox.Show(client.IsVirtualMachineExist("secret").ToString());
            client.Close();
        }
    }
}
