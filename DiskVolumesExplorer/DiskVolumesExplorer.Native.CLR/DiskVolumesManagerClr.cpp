// This is the main DLL file.

#include "stdafx.h"
#include "DiskVolumesManagerClr.h"
#include "Converters.h"

namespace DiskVolumesExplorer
{
namespace Native
{
namespace Wrappers
{
	DiskVolumesManager::DiskVolumesManager(IVmWareConnectionConfig ^connectionConfig)
	{
		VmWareConnectionConfig nativeConnectionConfig = Converters::ToNative(connectionConfig);
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
}
}
}

