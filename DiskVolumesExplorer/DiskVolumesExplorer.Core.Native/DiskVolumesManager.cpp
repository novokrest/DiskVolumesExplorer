#include "Stdafx.h"
#include "DiskVolumesManager.h"

namespace DiskVolumesExplorer
{
namespace Core
{
namespace Native
{
	DiskVolumesManager::DiskVolumesManager()
		: vixDiskLibInitializer_(NULL)
	{
		vixDiskLibInitializer_ = new VixDiskLibInitializer();
		try 
		{
			vixMntApiInitializer_ = new VixMntApiInitializer();
		}
		catch(...)
		{
			delete vixDiskLibInitializer_;
			throw;
		}		
	}

	DiskVolumesManager::~DiskVolumesManager()
	{
		this->!DiskVolumesManager();
	}

	DiskVolumesManager::!DiskVolumesManager()
	{
		try
		{
			delete vixMntApiInitializer_;
		}
		finally
		{
			delete vixDiskLibInitializer_;
		}
	}

	void DiskVolumesManager::Connect()
	{
		VixDiskLibConnection connection = {0};
		VixDiskLibConnectParams cnxParams = {0};

		cnxParams.vmxSpec = "UbuntuVM/UbuntuVM.vmx?dcPath=ha-datacenter&dsName=datastore1";
		cnxParams.serverName = "192.168.36.128";
		cnxParams.credType = VIXDISKLIB_CRED_UID;
		cnxParams.creds.uid.userName = "root";
		cnxParams.creds.uid.password = "1234567";
		cnxParams.thumbPrint = "b1:6f:61:f3:9e:48:b7:94:e1:ca:83:b4:6d:5a:fd:8a:96:5c:2b:99";

		//VixError vixError = VixDiskLib_ConnectEx(&cnxParams, true, NULL,
		//										 NULL,
		//										 &connection);

		VixError vixError = VixDiskLib_Connect(&cnxParams, &connection);
		CHECK_AND_THROW(vixError);

		VixDiskLib_Disconnect(connection);
	}
}
}
}