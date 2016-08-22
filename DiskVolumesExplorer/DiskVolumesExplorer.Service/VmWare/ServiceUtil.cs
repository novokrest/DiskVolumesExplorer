using DiskVolumesExplorer.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vim25Api;

namespace DiskVolumesExplorer.Service.VmWare
{
    class VmWareServiceUtil
    {
        private static readonly string[] ManagedEntityTree =
        {
            "ManagedEntity",
            "ComputeResource",
            "ClusterComputeResource",
            "Datacenter",
            "Folder",
            "HostSystem",
            "ResourcePool",
            "VirtualMachine"
        };

        private static readonly string[] ComputeResourceTree = 
        {
            "ComputeResource",
            "ClusterComputeResource"
        };

        static String[] HistoryCollectorTree = 
        {
            "HistoryCollector",
            "EventHistoryCollector",
            "TaskHistoryCollector"
        };

        private readonly VmWareServiceConnection _connection;

        public VmWareServiceUtil(VmWareServiceConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Get an entity of specified type with the name specified
        /// If name is null, will return the 1st matching entity of the type
        /// </summary>
        /// <param name="root">a root folder if available, or null for default</param>
        /// <param name="type">the type of the entity - e.g. VirtualMachine</param>
        /// <param name="name">name to match</param>
        /// <returns>
        ///    ManagedObjectReference of 1st type found, if name is null,
        ///    null if name not matched,
        ///    a ManagedObjectReference if name not null and match found.
        /// </returns>
        public ManagedObjectReference GetDecendentMoRef(
           ManagedObjectReference root, string type, string name
        )
        {
            if (name == null || name.Length == 0)
            {
                return null;
            }

            string[][] typeinfo =
               new string[][] { new string[] { type,  "name", },
         };

            ObjectContent[] ocary =
               GetContentsRecursively(null, root, typeinfo, true);

            if (ocary == null || ocary.Length == 0)
            {
                return null;
            }

            ObjectContent oc = null;
            ManagedObjectReference mor = null;
            DynamicProperty[] propary = null;
            string propval = null;
            bool found = false;
            for (int oci = 0; oci < ocary.Length && !found; oci++)
            {
                oc = ocary[oci];
                mor = oc.obj;
                propary = oc.propSet;

                if ((type == null) || (type != null && typeIsA(type, mor.type)))
                {
                    if (propary.Length > 0)
                    {
                        propval = (string)propary[0].val;
                    }

                    found = propval != null && name.Equals(propval);
                    propval = null;
                }
            }

            if (!found)
            {
                mor = null;
            }

            return mor;
        }

        /// <summary>
        /// convenience function to retrieve content recursively with multiple properties.
        /// the typeinfo array contains typename + properties to retrieve.
        /// </summary>
        /// <param name="collector">a property collector if available or null for default</param>
        /// <param name="root">a root folder if available, or null for default</param>
        /// <param name="typeinfo">2D array of properties for each typename</param>
        /// <param name="recurse">retrieve contents recursively from the root down</param>
        /// <returns>retrieved object contents</returns>
        public ObjectContent[] GetContentsRecursively(
           ManagedObjectReference collector, ManagedObjectReference root,
           string[][] typeinfo, bool recurse
        )
        {
            if (typeinfo == null || typeinfo.Length == 0)
            {
                return null;
            }

            ManagedObjectReference usecoll = collector;
            if (usecoll == null)
            {
                usecoll = _connection.PropertyCollector;
            }

            ManagedObjectReference useroot = root;
            if (useroot == null)
            {
                useroot = _connection.RootFolder;
            }
            SelectionSpec[] selectionSpecs = null;
            // Modified by Satyendra on 19th May
            if (recurse)
            {
                selectionSpecs = buildFullTraversal();
            }

            PropertySpec[] propspecary = BuildPropertySpecArray(typeinfo);

            PropertyFilterSpec spec = new PropertyFilterSpec();
            spec.propSet = propspecary;
            spec.objectSet = new ObjectSpec[] { new ObjectSpec() };
            spec.objectSet[0].obj = useroot;
            spec.objectSet[0].skip = false;
            spec.objectSet[0].selectSet = selectionSpecs;

            ObjectContent[] retoc =
               retrievePropertiesEx(usecoll, new PropertyFilterSpec[] { spec });

            return retoc;
        }

        public Boolean typeIsA(String searchType, String foundType)
        {
            if (searchType.Equals(foundType))
            {
                return true;
            }
            else if (searchType.Equals("ManagedEntity"))
            {
                for (int i = 0; i < ManagedEntityTree.Length; ++i)
                {
                    if (ManagedEntityTree[i].Equals(foundType))
                    {
                        return true;
                    }
                }
            }
            else if (searchType.Equals("ComputeResource"))
            {
                for (int i = 0; i < ComputeResourceTree.Length; ++i)
                {
                    if (ComputeResourceTree[i].Equals(foundType))
                    {
                        return true;
                    }
                }
            }
            else if (searchType.Equals("HistoryCollector"))
            {
                for (int i = 0; i < HistoryCollectorTree.Length; ++i)
                {
                    if (HistoryCollectorTree[i].Equals(foundType))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /**
    * This method creates a SelectionSpec[] to traverses the entire
    * inventory tree starting at a Folder
    * @return The SelectionSpec[]
    */
        public SelectionSpec[] buildFullTraversal()
        {
            // Recurse through all ResourcePools

            TraversalSpec rpToVm = new TraversalSpec();
            rpToVm.name = "rpToVm";
            rpToVm.type = "ResourcePool";
            rpToVm.path = "vm";
            rpToVm.skip = false;
            rpToVm.skipSpecified = true;

            // Recurse through all ResourcePools

            TraversalSpec rpToRp = new TraversalSpec();
            rpToRp.name = "rpToRp";
            rpToRp.type = "ResourcePool";
            rpToRp.path = "resourcePool";
            rpToRp.skip = false;
            rpToRp.skipSpecified = true;

            rpToRp.selectSet = new SelectionSpec[] { new SelectionSpec(), new SelectionSpec() };
            rpToRp.selectSet[0].name = "rpToRp";
            rpToRp.selectSet[1].name = "rpToVm";


            // Traversal through ResourcePool branch
            TraversalSpec crToRp = new TraversalSpec();
            crToRp.name = "crToRp";
            crToRp.type = "ComputeResource";
            crToRp.path = "resourcePool";
            crToRp.skip = false;
            crToRp.skipSpecified = true;
            crToRp.selectSet = new SelectionSpec[] { new SelectionSpec(), new SelectionSpec() };
            crToRp.selectSet[0].name = "rpToRp";
            crToRp.selectSet[1].name = "rpToVm";


            // Traversal through host branch
            TraversalSpec crToH = new TraversalSpec();
            crToH.name = "crToH";
            crToH.type = "ComputeResource";
            crToH.path = "host";
            crToH.skip = false;
            crToH.skipSpecified = true;

            // Traversal through hostFolder branch
            TraversalSpec dcToHf = new TraversalSpec();
            dcToHf.name = "dcToHf";
            dcToHf.type = "Datacenter";
            dcToHf.path = "hostFolder";
            dcToHf.skip = false;
            dcToHf.selectSet = new SelectionSpec[] { new SelectionSpec() };
            dcToHf.selectSet[0].name = "visitFolders";


            // Traversal through vmFolder branch
            TraversalSpec dcToVmf = new TraversalSpec();
            dcToVmf.name = "dcToVmf";
            dcToVmf.type = "Datacenter";
            dcToVmf.path = "vmFolder";
            dcToVmf.skip = false;
            dcToVmf.skipSpecified = true;
            dcToVmf.selectSet = new SelectionSpec[] { new SelectionSpec() };
            dcToVmf.selectSet[0].name = "visitFolders";


            // Recurse through all Hosts
            TraversalSpec HToVm = new TraversalSpec();
            HToVm.name = "HToVm";
            HToVm.type = "HostSystem";
            HToVm.path = "vm";
            HToVm.skip = false;
            HToVm.skipSpecified = true;
            HToVm.selectSet = new SelectionSpec[] { new SelectionSpec() };
            HToVm.selectSet[0].name = "visitFolders";

            //Traversal spec from Dataceneter to Network

            TraversalSpec dctonw = new TraversalSpec();
            dctonw.name = "dctonw";
            dctonw.type = "Datacenter";
            dctonw.path = "networkFolder";
            dctonw.skip = false;
            dctonw.skipSpecified = true;
            dctonw.selectSet = new SelectionSpec[] { new SelectionSpec() };
            dctonw.selectSet[0].name = "visitFolders";

            // Recurse thriugh the folders
            TraversalSpec visitFolders = new TraversalSpec();
            visitFolders.name = "visitFolders";
            visitFolders.type = "Folder";
            visitFolders.path = "childEntity";
            visitFolders.skip = false;
            visitFolders.skipSpecified = true;
            visitFolders.selectSet = new SelectionSpec[] { new SelectionSpec(), new SelectionSpec(), new SelectionSpec(), new SelectionSpec(), new SelectionSpec(), new SelectionSpec(), new SelectionSpec(), new SelectionSpec() };
            visitFolders.selectSet[0].name = "visitFolders";
            visitFolders.selectSet[1].name = "dcToHf";
            visitFolders.selectSet[2].name = "dcToVmf";
            visitFolders.selectSet[3].name = "crToH";
            visitFolders.selectSet[4].name = "crToRp";
            visitFolders.selectSet[5].name = "HToVm";
            visitFolders.selectSet[6].name = "rpToVm";
            visitFolders.selectSet[7].name = "dctonw";
            return new SelectionSpec[] { visitFolders, dcToVmf, dctonw, dcToHf, crToH, crToRp, rpToRp, HToVm, rpToVm };
        }

        /// <summary>
        /// This code takes an array of [typename, property, property, ...]
        /// and converts it into a ContainerFilterSpec array.
        /// handles case where multiple references to the same typename
        /// are specified.
        /// </summary>
        /// <param name="typeinfo">array pf [typename, property, property, ...]</param>
        /// <returns>array of container property specs</returns>
        public PropertySpec[] BuildPropertySpecArray(
           string[][] typeinfo
        )
        {
            // Eliminate duplicates
            Hashtable tInfo = new Hashtable();
            for (int ti = 0; ti < typeinfo.Length; ++ti)
            {
                Hashtable props = (Hashtable)tInfo[typeinfo[ti][0]];
                if (props == null)
                {
                    props = new Hashtable();
                    tInfo[typeinfo[ti][0]] = props;
                }
                bool typeSkipped = false;
                for (int pi = 0; pi < typeinfo[ti].Length; ++pi)
                {
                    String prop = typeinfo[ti][pi];
                    if (typeSkipped)
                    {
                        if (!props.Contains(prop))
                        {
                            // some value, not important
                            props[prop] = String.Empty;
                        }
                    }
                    else
                    {
                        typeSkipped = true;
                    }
                }
            }

            // Create PropertySpecs
            ArrayList pSpecs = new ArrayList();
            foreach (String type in tInfo.Keys)
            {
                PropertySpec pSpec = new PropertySpec();
                Hashtable props = (Hashtable)tInfo[type];
                pSpec.type = type;
                pSpec.all = props.Count == 0 ? true : false;
                pSpec.pathSet = new String[props.Count];
                int index = 0;
                foreach (String prop in props.Keys)
                {
                    pSpec.pathSet[index++] = prop;
                }
                pSpecs.Add(pSpec);
            }

            return (PropertySpec[])pSpecs.ToArray(typeof(PropertySpec));
        }

        public ObjectContent[] retrievePropertiesEx(ManagedObjectReference propertyCollector, PropertyFilterSpec[] specs)
        {
            return retrievePropertiesEx(propertyCollector, specs, null);
        }

        public ObjectContent[] retrievePropertiesEx(ManagedObjectReference propertyCollector, PropertyFilterSpec[] specs, int? maxObjects)
        {
            List<ObjectContent> objectList = new List<ObjectContent>();
            var result =
                     _connection.Service.RetrievePropertiesEx(propertyCollector, specs, new RetrieveOptions()
                     {
                         maxObjects = maxObjects.GetValueOrDefault(),
                         maxObjectsSpecified = (maxObjects != null)
                     });
            if (result != null)
            {
                string token = result.token;
                objectList.AddRange(result.objects.AsEnumerable());
                while (token != null && !string.Empty.Equals(token))
                {
                    result = _connection.Service.ContinueRetrievePropertiesEx(propertyCollector, token);
                    if (result != null)
                    {
                        token = result.token;
                        objectList.AddRange(result.objects.AsEnumerable());
                    }
                }
            }
            return objectList.ToArray();
        }

        /// <summary>
        /// Get a MORef from the property returned.
        /// </summary>
        /// <param name="objMor">Object to get a reference property from</param>
        /// <param name="propName">name of the property that is the MORef</param>
        /// <returns>the MORef for that property.</returns>
        public ManagedObjectReference GetMoRefProp(ManagedObjectReference objMor, string propName)
        {
            if (objMor == null)
            {
                throw new Exception("Need an Object Reference to get Contents from.");
            }

            // If no property specified, assume childEntity
            if (propName == null)
            {
                propName = "childEntity";
            }

            ObjectContent[] objcontent =
               GetObjectProperties(
                  null, objMor, new string[] { propName }
               );

            ManagedObjectReference propmor = null;
            if (objcontent.Length > 0 && objcontent[0].propSet.Length > 0)
            {
                propmor = (ManagedObjectReference)objcontent[0].propSet[0].val;
            }
            else
            {
                throw new Exception("Did not find first " + propName + " in " + objMor.type);
            }

            return propmor;
        }

        /// <summary>
        /// Retrieve contents for a single object based on the property collector
        /// registered with the service.
        /// </summary>
        /// <param name="collector">Property collector registered with service</param>
        /// <param name="mobj">Managed Object Reference to get contents for</param>
        /// <param name="properties">names of properties of object to retrieve</param>
        /// <returns>retrieved object contents</returns>
        public ObjectContent[] GetObjectProperties(
           ManagedObjectReference collector,
           ManagedObjectReference mobj, string[] properties
        )
        {
            if (mobj == null)
            {
                return null;
            }

            ManagedObjectReference usecoll = collector;
            if (usecoll == null)
            {
                usecoll = _connection.PropertyCollector;
            }

            PropertyFilterSpec spec = new PropertyFilterSpec();
            spec.propSet = new PropertySpec[] { new PropertySpec() };
            spec.propSet[0].all = properties == null || properties.Length == 0;
            spec.propSet[0].allSpecified = spec.propSet[0].all;
            spec.propSet[0].type = mobj.type;
            spec.propSet[0].pathSet = properties;

            spec.objectSet = new ObjectSpec[] { new ObjectSpec() };
            spec.objectSet[0].obj = mobj;
            spec.objectSet[0].skip = false;

            return retrievePropertiesEx(usecoll, new PropertyFilterSpec[] { spec });
        }

        public String[] GetVmDirectory(ManagedObjectReference vmmor)
        {
            String[] vmDir = new String[4];
            VirtualMachineConfigInfo vmConfigInfo
               = (VirtualMachineConfigInfo)GetDynamicProperty(vmmor, "config");
            if (vmConfigInfo != null)
            {
                vmDir[0] = vmConfigInfo.files.vmPathName;
                vmDir[1] = vmConfigInfo.files.snapshotDirectory;
                vmDir[2] = vmConfigInfo.files.suspendDirectory;
                vmDir[3] = vmConfigInfo.files.logDirectory;
            }
            else
            {
                Console.WriteLine("Cannot Restore VM. Not Able " + "To Find The Virtual Machine Config Info");
            }
            return vmDir;
        }

        public String GetDataCenter(ManagedObjectReference vmmor)
        {
            ManagedObjectReference morParent = GetMoRefProp(vmmor, "parent");
            morParent = GetMoRefProp(morParent, "parent");
            if (!morParent.type.Equals("Datacenter"))
            {
                morParent = GetMoRefProp(morParent, "parent");
            }
            Object objdcName = GetDynamicProperty(morParent, "name");
            String dcName = objdcName.ToString();
            return dcName;
        }

        public Object GetDynamicProperty(ManagedObjectReference mor, String propertyName)
        {
            ObjectContent[] objContent = GetObjectProperties(null, mor,
                  new String[] { propertyName });

            Object propertyValue = null;
            if (objContent != null)
            {
                DynamicProperty[] dynamicProperty = objContent[0].propSet;
                if (dynamicProperty != null)
                {
                    Object dynamicPropertyVal = dynamicProperty[0].val;
                    String dynamicPropertyName = dynamicPropertyVal.GetType().FullName;
                    propertyValue = dynamicPropertyVal;

                }
            }
            return propertyValue;
        }

        public IReadOnlyCollection<string> GetVirtualDiskPaths(ManagedObjectReference virtualMachineRef)
        {
            var virtualDiskPaths = new List<string>();

            VirtualMachineConfigInfo virtualMachineConfig = (VirtualMachineConfigInfo)GetDynamicProperty(virtualMachineRef, "config");
            Verifiers.VerifyNotNull(virtualMachineConfig, "Failed to obtain virtual machine configuration information");

            VirtualDevice[] virtualDevices = virtualMachineConfig.hardware.device;
            foreach(VirtualDevice virtualDevice in virtualDevices)
            {
                if (virtualDevice is VirtualDisk)
                {
                    VirtualDeviceFileBackingInfo backingInfo = (VirtualDeviceFileBackingInfo) virtualDevice.backing;
                    virtualDiskPaths.Add(backingInfo.fileName);
                }
            }

            return virtualDiskPaths;
        }
    }
}
