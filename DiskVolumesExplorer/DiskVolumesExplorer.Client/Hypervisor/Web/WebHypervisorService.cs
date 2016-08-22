using DiskVolumesExplorer.Core.Extensions;
using DiskVolumesExplorer.Client.Services;
using DiskVolumesExplorer.Core.Configs;
using System;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace DiskVolumesExplorer.Client.Hypervisor
{
    internal class WebHypervisorService : IHypervisorService
    {
        private readonly ISecureConnectionConfig _connectionConfig;

        private const string GetVirtualMachinesUrlFormat = @"http://{0}/Design_Time_Addresses/hypervisor/vms";
        private const string GetDrivesUrlFormat = @"http://{0}/Design_Time_Addresses/hypervisor/disks/{1}";

        public WebHypervisorService(ISecureConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
        }

        public DriveData[] GetDrives(string vmName)
        {
            string getVirtualMachineDrivesUrl = string.Format(GetDrivesUrlFormat, _connectionConfig.ServerAddress, vmName);
            return GetData<DriveData[]>(getVirtualMachineDrivesUrl);
        }

        public Task<DriveData[]> GetDrivesAsync(string vmName)
        {
            return Task.Run(() => GetDrives(vmName));
        }

        public string[] GetVirtualMachines()
        {
            var getVirtualMachinesUrl = string.Format(GetVirtualMachinesUrlFormat, _connectionConfig.ServerAddress);
            return GetData<string[]>(getVirtualMachinesUrl);
        }

        public Task<string[]> GetVirtualMachinesAsync()
        {
            return Task.Run((Func<string[]>)GetVirtualMachines);
        }

        private T GetData<T>(string url)
        {
            HttpWebRequest request = CreateWebRequest(url);

            using (WebResponse ws = request.GetResponse())
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

                return (T)serializer.ReadObject(ws.GetResponseStream());
            }
        }

        private HttpWebRequest CreateWebRequest(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            string credentials = string.Concat(_connectionConfig.User, ":", _connectionConfig.Password.ConvertToString());
            string encodedCredentials = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(credentials));
            request.Headers.Add("Authorization", "Basic " + encodedCredentials);

            return request;
        }
    }
}
