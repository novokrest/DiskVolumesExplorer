#include "stdafx.h"
#include "SafeVixVolumeInfo.h"
#include "VixVolumeMounter.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	SafeVixVolumeInfo::SafeVixVolumeInfo(const VixVolumeMounter &volumeMounter)
	{
		VixError vixError = VIX_ERROR_CODE(VixMntapi_GetVolumeInfo(volumeMounter.Volume(), &volumeInfo_));
		CHECK_AND_THROW(vixError);
	}

	SafeVixVolumeInfo::~SafeVixVolumeInfo()
	{
		VixMntapi_FreeVolumeInfo(volumeInfo_);
	}

	VixVolumeInfo* SafeVixVolumeInfo::operator->()
	{
		return volumeInfo_;
	}

	SafeVixVolumeInfo::operator VixVolumeInfo*()
	{
		return volumeInfo_;
	}

	VixVolumeInfo** SafeVixVolumeInfo::operator&()
	{
		return &volumeInfo_;
	}
}
}