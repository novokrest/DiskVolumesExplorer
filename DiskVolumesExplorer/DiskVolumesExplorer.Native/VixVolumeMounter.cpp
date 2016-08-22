#include "stdafx.h"
#include "VixVolumeMounter.h"

namespace DiskVolumesExplorer
{
	namespace Native
	{
		VixVolumeMounter::VixVolumeMounter(const VixVolumeHandle &volumeHandle)
			: volumeHandle_(volumeHandle)
		{
			VixError vixError = VIX_ERROR_CODE(VixMntapi_MountVolume(volumeHandle_, TRUE));
			CHECK_AND_THROW(vixError);
		}

		VixVolumeMounter::~VixVolumeMounter()
		{
			VixError vixError = VIX_ERROR_CODE(VixMntapi_DismountVolume(volumeHandle_, TRUE));
			//CHECK_AND_THROW(vixError);
		}

		VixVolumeHandle VixVolumeMounter::Volume() const
		{
			return volumeHandle_;
		}
	}
}