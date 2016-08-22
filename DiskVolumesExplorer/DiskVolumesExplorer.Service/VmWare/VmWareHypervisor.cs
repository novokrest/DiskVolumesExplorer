using System.Collections.Generic;
using DiskVolumesExplorer.Core.Extensions;
using Vim25Api;
using System;
using DiskVolumesExplorer.Core;
using DiskVolumesExplorer.Native.Wrappers;
using DiskVolumesExplorer.Service.Core.Data;
using DiskVolumesExplorer.Service.Core.Configs;
using DiskVolumesExplorer.Service.Configs.VmWare;

namespace DiskVolumesExplorer.Service.VmWare
{
    internal sealed class VmWareHypervisor : IDisposable
    {
        private readonly VmWareServiceConnection _connection = new VmWareServiceConnection();
        private readonly IVmWareConnectionConfig _connectionConfig;
        private bool _connected;

        public VmWareHypervisor(IVmWareConnectionConfig connectionConfig)
        {
            _connectionConfig = connectionConfig;
        }

        public IReadOnlyCollection<string> GetVirtualMachineNames()
        {
            EnsureConnected();

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

        public DiskData[] GetDisks(string virtualMachineName)
        {
            EnsureConnected();

            VmWareServiceUtil serviceUtil = new VmWareServiceUtil(_connection);
            ManagedObjectReference virtualMachineRef = serviceUtil.GetDecendentMoRef(null, "VirtualMachine", virtualMachineName);
            Verifiers.VerifyNotNull(virtualMachineRef, "Cannot find virtual machine with specified name: {0}", virtualMachineName);

            String dataCenterName = serviceUtil.GetDataCenter(virtualMachineRef);
            String[] vmDirectory = serviceUtil.GetVmDirectory(virtualMachineRef);

            var virtualDiskPaths = serviceUtil.GetVirtualDiskPaths(virtualMachineRef);

            return new DiskData[0];
        }

        private DiskData LoadDiskData(string dataCenterName, string virtualDiskPath)
        {
            var diskConfig = new VmWareDiskConfig
            {
                Datacenter = dataCenterName,
                VirtualDiskFilePath = virtualDiskPath
            };

            using (var diskVolumesManager = new DiskVolumesManager(_connectionConfig))
            {
                return diskVolumesManager.GetDiskData(diskConfig);
            }
        }

        private void EnsureConnected()
        {
            if (!_connected)
            {
                Connect();
            }
        }

        private void Connect()
        {
            _connection.Connect(_connectionConfig.Server, _connectionConfig.User, _connectionConfig.Password);
            _connected = true;
        }

        public void Dispose()
        {
            if (_connected)
            {
                Disconnect();
            }
        }

        private void Disconnect()
        {
            _connection.Disconnect();
        }
    }
}
