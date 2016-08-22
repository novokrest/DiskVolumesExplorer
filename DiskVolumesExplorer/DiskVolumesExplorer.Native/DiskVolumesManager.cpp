#include "stdafx.h"
#include "DiskVolumesManager.h"
#include "SafeVixDiskLibConnection.h"
#include "SafeVixDiskLibHandle.h"
#include "SafeVixDiskSetHandle.h"
#include "SafeVixOsInfo.h"
#include "SafeVixVolumeHandles.h"
#include "VixVolumeMounter.h"
#include "SafeVixVolumeInfo.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	DiskVolumesManager::DiskVolumesManager(const VmWareConnectionConfig &connectionConfig)
		: connectionConfig_(connectionConfig)
		, connected_(false)
	{

	}

	DiskVolumesManager::~DiskVolumesManager()
	{
		if (connected_) {
			VixDiskLib_Disconnect(connection_);
		}
	}

	DiskData DiskVolumesManager::GetDiskData(const VmWareDiskConfig &diskConfig)
	{
		VixDiskLibConnectParams connectParams = { 0 };
		std::string vmxSpec;
		FillInConnectionParams(connectParams, vmxSpec, diskConfig);

		SafeVixDiskLibConnection connection(connectParams);
		std::string diskPath = "[" + diskConfig.Datastore + "] " + diskConfig.VirtualDiskFilePath;
		SafeVixDiskLibHandle diskHandle(connection, diskPath, VIXDISKLIB_FLAG_OPEN_READ_ONLY);

		VixDiskLibHandle diskHandles[1] = { diskHandle };
		SafeVixDiskSetHandle diskSetHandle(diskHandles, 1, VIXDISKLIB_FLAG_OPEN_READ_ONLY);
		//const char * diskNames[1] = { "[datastore1] WindowsVM/WindowsVM.vmdk" };
		//vixError = VixMntapi_OpenDisks(connection_, diskNames, 1, VIXDISKLIB_FLAG_OPEN_READ_ONLY, &diskSetHandle);

		SafeVixOsInfo osInfo(diskSetHandle);
		SafeVixVolumeHandles volumes(diskSetHandle);

		for (size_t i = 0, numberOfVolumes = volumes.Count(); i < numberOfVolumes; i++) {
			VixVolumeHandle volumeHandle = volumes[i];
			VixVolumeMounter volumeMounter(volumeHandle);
			SafeVixVolumeInfo volumeInfo(volumeMounter);
		}

		return DiskData();
	}

	void DiskVolumesManager::FillInConnectionParams(VixDiskLibConnectParams &connectParams, std::string &vmxSpec, const VmWareDiskConfig &diskConfig)
	{
		vmxSpec = diskConfig.VirtualMachineConfigFilePath + "?dcPath=" + diskConfig.Datacenter + "&dsName=" + diskConfig.Datastore;
		connectParams.vmxSpec = const_cast<char*>(vmxSpec.c_str());
		
		connectParams.serverName = const_cast<char*>(connectionConfig_.Server.c_str());
		connectParams.creds.uid.userName = const_cast<char*>(connectionConfig_.User.c_str());;
		connectParams.creds.uid.password = const_cast<char*>(connectionConfig_.Password.c_str());;
		connectParams.credType = VIXDISKLIB_CRED_UID;
		connectParams.thumbPrint = const_cast<char*>(connectionConfig_.ThumbPrint.c_str());
	}
}
}