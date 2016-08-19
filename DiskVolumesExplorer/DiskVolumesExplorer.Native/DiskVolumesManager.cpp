#include "stdafx.h"
#include "DiskVolumesManager.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	DiskVolumesManager::~DiskVolumesManager()
	{
		if (connected_) {
			VixDiskLib_Disconnect(connection_);
		}
	}

	DiskVolumesManager::DiskVolumesManager()
		: connected_(false)
	{

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