// This is the main DLL file.

#include "stdafx.h"
#include "DiskVolumesManagerClr.h"
#include "Converters.h"

using namespace DiskVolumesExplorer::Service::Core::Data;

namespace DiskVolumesExplorer
{
namespace Native
{
namespace Wrappers
{
	DiskVolumesManager::DiskVolumesManager(IVmWareConnectionConfig ^connectionConfig)
	{
		VmWareConnectionConfig nativeConnectionConfig = ConfigConverter::ToNative(connectionConfig);
		nativeManager_ = new DiskVolumesExplorer::Native::DiskVolumesManager(nativeConnectionConfig);
	}

	DiskVolumesManager::~DiskVolumesManager()
	{
		this->!DiskVolumesManager();
	}

	DiskVolumesManager::!DiskVolumesManager()
	{
		delete nativeManager_;
	}

	::DiskData^ DiskVolumesManager::GetDiskData(IVmWareDiskConfig ^diskConfig)
	{
		VmWareDiskConfig nativeDiskConfig = ConfigConverter::ToNative(diskConfig);
		DiskData nativeDiskData = nativeManager_->GetDiskData(nativeDiskConfig);

		::DiskData^ diskData = DataConverter::FromNative(nativeDiskData);
		return diskData;
	}
}
}
}

