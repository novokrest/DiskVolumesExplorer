#pragma once

namespace DiskVolumesExplorer
{
namespace Native
{
	struct VmWareDiskConfig
	{
		std::string Datacenter;
		std::string Datastore;
		std::string VirtualMachineConfigFilePath;
		std::string VirtualDiskFilePath;
	};
}
}