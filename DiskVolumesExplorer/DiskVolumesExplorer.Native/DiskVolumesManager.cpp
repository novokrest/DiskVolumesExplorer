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

	void DiskVolumesManager::GetVolumes(const VmWareDiskConfig &diskConfig)
	{
		VixDiskLibConnectParams connectParams = { 0 };
		std::string vmxSpec;
		FillInConnectionParams(connectParams, vmxSpec, diskConfig);

		SafeVixDiskLibConnection connection(connectParams);
		std::string diskPath = "[" + diskConfig.Datastore + "] " + diskConfig.VirtualDiskFilePath;
		SafeVixDiskLibHandle diskHandle(connection, diskPath, VIXDISKLIB_FLAG_OPEN_READ_ONLY);

		VixDiskLibHandle diskHandles[1] = { diskHandle };
		SafeVixDiskSetHandle diskSetHandle(diskHandles, 1, VIXDISKLIB_FLAG_OPEN_READ_ONLY);

		SafeVixOsInfo osInfo(diskSetHandle);
		SafeVixVolumeHandles volumes(diskSetHandle);

		for (size_t i = 0, numberOfVolumes = volumes.Count(); i < numberOfVolumes; i++) {
			VixVolumeHandle volumeHandle = volumes[i];
			VixVolumeMounter volumeMounter(volumeHandle);
			SafeVixVolumeInfo volumeInfo(volumeMounter);

			//volumeInfo->
		}
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

	void DiskVolumesManager::Connect()
	{
		VixDiskLibConnectParams cnxParams = { 0 };

		cnxParams.vmxSpec = "WindowsVM/WindowsVM.vmx?dcPath=ha-datacenter&dsName=datastore1";
		cnxParams.serverName = "192.168.36.128";
		cnxParams.credType = VIXDISKLIB_CRED_UID;
		cnxParams.creds.uid.userName = "root";
		cnxParams.creds.uid.password = "1234567";
		cnxParams.thumbPrint = "d9:1d:22:40:c0:08:e8:cd:9f:ff:48:98:54:a8:a7:b5:93:70:28:8f";

		//VixError vixError = VixDiskLib_ConnectEx(&cnxParams, true, NULL,
		//										 NULL,
		//										 &connection);

		VixError vixError = VixDiskLib_Connect(&cnxParams, &connection_);
		CHECK_AND_THROW(vixError);
		connected_ = true;

		VixDiskLibHandle diskHandle;
		vixError = VixDiskLib_Open(connection_, "[datastore1] WindowsVM/WindowsVM.vmdk", VIXDISKLIB_FLAG_OPEN_READ_ONLY, &diskHandle);
		CHECK_AND_THROW(vixError);

		VixDiskLibHandle diskHandles[1] = {diskHandle};
		VixDiskSetHandle diskSetHandle;
		const char * diskNames[1] = { "[datastore1] WindowsVM/WindowsVM.vmdk" };
		//vixError = VixMntapi_OpenDisks(connection_, diskNames, 1, VIXDISKLIB_FLAG_OPEN_READ_ONLY, &diskSetHandle);
		vixError = VixMntapi_OpenDiskSet(diskHandles, 1, VIXDISKLIB_FLAG_OPEN_READ_ONLY, &diskSetHandle);
		CHECK_AND_THROW(vixError);

		//VixOsInfo *osInfo;
		//vixError = VixMntapi_GetOsInfo(diskSetHandle, &osInfo);
		//CHECK_AND_THROW(vixError);
		//VixMntapi_FreeOsInfo(osInfo);

		VixVolumeHandle *volumeHandles;
		size_t numberOfVolumes;
		vixError = VixMntapi_GetVolumeHandles(diskSetHandle, &numberOfVolumes, &volumeHandles);
		CHECK_AND_THROW(vixError);

		for (size_t i = 0; i < numberOfVolumes; i++) {
			VixVolumeHandle volumeHandle = volumeHandles[i];
			VixVolumeInfo *volumeInfo;
			vixError = VIX_ERROR_CODE(VixMntapi_MountVolume(volumeHandle, TRUE));
			CHECK_AND_THROW(vixError);
			vixError = VIX_ERROR_CODE(VixMntapi_GetVolumeInfo(volumeHandle, &volumeInfo));
			CHECK_AND_THROW(vixError);
			VixMntapi_FreeVolumeInfo(volumeInfo);
		}

		VixMntapi_FreeVolumeHandles(volumeHandles);
		vixError = VIX_ERROR_CODE(VixMntapi_CloseDiskSet(diskSetHandle));
		CHECK_AND_THROW(vixError);

		vixError = VixDiskLib_Close(diskHandle);
		CHECK_AND_THROW(vixError);
	}
}
}