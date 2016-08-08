namespace DiskVolumesExplorer.Core
{
    interface IVmWareService
    {
        
    }

    class HttpsVmWareSdkService : IVmWareService
    {
        private readonly string _serverAddress;
        private readonly string _userName;
        private readonly string _password;

        public HttpsVmWareSdkService(string serverAddress, string userName, string password)
        {
            _serverAddress = serverAddress;
            _userName = userName;
            _password = password;
        }

        public void GetVirtualMachines()
        {
            
        }
    }
}
