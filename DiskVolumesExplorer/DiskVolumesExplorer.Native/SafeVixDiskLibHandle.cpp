#include "stdafx.h"
#include "SafeVixDiskLibHandle.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	SafeVixDiskLibHandle::SafeVixDiskLibHandle(const VixDiskLibConnection &connection, const std::string &path, uint32 flags)
	{
		VixError vixError = VIX_ERROR_CODE(VixDiskLib_Open(connection, path.c_str(), flags, &diskHandle_));
		CHECK_AND_THROW(vixError);
	}

	SafeVixDiskLibHandle::~SafeVixDiskLibHandle()
	{
		VixError vixError = VIX_ERROR_CODE(VixDiskLib_Close(diskHandle_));
		//CHECK_AND_THROW(vixError);
	}

	SafeVixDiskLibHandle::operator VixDiskLibHandle&()
	{
		return diskHandle_;
	}

	VixDiskLibHandle* SafeVixDiskLibHandle::operator&()
	{
		return &diskHandle_;
	}
}
}