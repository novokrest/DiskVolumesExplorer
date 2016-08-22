#include "stdafx.h"
#include "SafeVixVolumeHandles.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	SafeVixVolumeHandles::SafeVixVolumeHandles(const VixDiskSetHandle &diskSetHandle)
	{
		VixError vixError = VIX_ERROR_CODE(VixMntapi_GetVolumeHandles(diskSetHandle, &numberOfVolumes_, &volumeHandles_));
		CHECK_AND_THROW(vixError);
	}

	SafeVixVolumeHandles::~SafeVixVolumeHandles()
	{
		VixMntapi_FreeVolumeHandles(volumeHandles_);
	}

	VixVolumeHandle SafeVixVolumeHandles::operator[](int index) const
	{
		return volumeHandles_[index];
	}

	size_t SafeVixVolumeHandles::Count() const
	{
		return numberOfVolumes_;
	}
}
}