#pragma once

#include "VixVolumeMounter.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	class SafeVixVolumeInfo
	{
	public:
		SafeVixVolumeInfo(const VixVolumeMounter &volumeMounter);
		~SafeVixVolumeInfo();

		operator VixVolumeInfo*();

		VixVolumeInfo* operator->();
		VixVolumeInfo** operator&();

	private:
		VixVolumeInfo *volumeInfo_;
	};
}
}