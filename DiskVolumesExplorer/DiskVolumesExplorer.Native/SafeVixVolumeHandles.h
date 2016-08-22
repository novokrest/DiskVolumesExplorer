#pragma once

namespace DiskVolumesExplorer
{
namespace Native
{
	class SafeVixVolumeHandles
	{
	public:
		SafeVixVolumeHandles(const VixDiskSetHandle &diskSetHandle);
		~SafeVixVolumeHandles();

		VixVolumeHandle operator[](int index) const;
		size_t Count() const;

	private:
		VixVolumeHandle *volumeHandles_;
		size_t numberOfVolumes_;
	};
}
}