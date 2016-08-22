#include "stdafx.h"
#include "SafeVixOsInfo.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	SafeVixOsInfo::SafeVixOsInfo(const VixDiskSetHandle &diskSetHandle)
	{
		VixError vixError = VIX_ERROR_CODE(VixMntapi_GetOsInfo(diskSetHandle, &osInfo_));
		CHECK_AND_THROW(vixError);
	}
	
	SafeVixOsInfo::~SafeVixOsInfo()
	{
		VixMntapi_FreeOsInfo(osInfo_);
	}

	VixOsInfo* SafeVixOsInfo::operator->()
	{
		return osInfo_;
	}
}
}