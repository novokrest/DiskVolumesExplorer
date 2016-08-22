#include "stdafx.h"
#include "SafeVixDiskSetHandle.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	SafeVixDiskSetHandle::SafeVixDiskSetHandle(VixDiskLibHandle diskHandles[], size_t numberOfDisks, uint32 openMode)
	{
		VixError vixError = VIX_ERROR_CODE(VixMntapi_OpenDiskSet(diskHandles, 1, VIXDISKLIB_FLAG_OPEN_READ_ONLY, &diskSetHandle_));
		CHECK_AND_THROW(vixError);
	}

	SafeVixDiskSetHandle::~SafeVixDiskSetHandle()
	{
		VixError vixError = VIX_ERROR_CODE(VixMntapi_CloseDiskSet(diskSetHandle_));
		//CHECK_AND_THROW(vixError);
	}

	SafeVixDiskSetHandle::operator VixDiskSetHandle&()
	{
		return diskSetHandle_;
	}

	VixDiskSetHandle* SafeVixDiskSetHandle::operator&()
	{
		return &diskSetHandle_;
	}
}
}