using System.Collections.Generic;
using DiskVolumesExplorer.Core.Configs;
using DiskVolumesExplorer.Core.Extensions;
using Vim25Api;

namespace DiskVolumesExplorer.Core.VmWare
{
    public class VmWareHypervisor
    {
        private readonly VmWareServiceConnection _connection = new VmWareServiceConnection();
        private readonly ISecureConnectionConfig _connectionConfig;

        public VmWareHypervisor(ISecureConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
        }

        public void Connect()
        {
            _connection.Connect(_connectionConfig.ServerAddress, _connectionConfig.User, _connectionConfig.Password.ConvertToString());
        }

        public IReadOnlyCollection<string> GetVirtualMachineNames()
        {
            ServiceContent serviceContent = _connection.ServiceContent;
            ManagedObjectReference viewManager = serviceContent.viewManager;
            ManagedObjectReference propertyCollector = serviceContent.propertyCollector;

            ManagedObjectReference containerView = _connection.Service.CreateContainerView(viewManager,
                serviceContent.rootFolder, new[] {"VirtualMachine"}, true);

            TraversalSpec traversalSpec = new TraversalSpec()
            {
                name = "traverseEntities",
                type = "ContainerView",
                path = "view",
                skip = false
            };

            ObjectSpec objectSpec = new ObjectSpec
            {
                obj = containerView,
                skip = true
            };
            objectSpec.selectSet = new SelectionSpec[] {traversalSpec};

            PropertySpec propertySpec = new PropertySpec()
            {
                type = "VirtualMachine",
                pathSet = new []{"name"}
            };

            PropertyFilterSpec filter = new PropertyFilterSpec()
            {
                objectSet = new[] {objectSpec},
                propSet = new[] {propertySpec}
            };

            RetrieveOptions retrieveOptions = new RetrieveOptions();
            RetrieveResult properties = _connection.Service.RetrievePropertiesEx(propertyCollector, new[] {filter},
                retrieveOptions);


            var virtualMachineNames = new List<string>();
            if (properties != null)
            {
                foreach (ObjectContent objectContent in properties.objects)
                {
                    DynamicProperty[] dynamicProperties = objectContent.propSet;
                    if (dynamicProperties != null)
                    {
                        foreach (DynamicProperty dynamicProperty in dynamicProperties)
                        {
                            string virtualMachineName = (string) dynamicProperty.val;
                            string path = dynamicProperty.name;
                            virtualMachineNames.Add(virtualMachineName);
                        }
                    }
                }
            }

            return virtualMachineNames;
        }
    }
}
