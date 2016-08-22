#pragma once

namespace DiskVolumesExplorer
{
	namespace Native
	{
		class VixVolumeMounter
		{
		public:
			VixVolumeMounter(const VixVolumeHandle &volumeHandle);
			~VixVolumeMounter();

			VixVolumeHandle Volume() const;

		private:
			VixVolumeHandle volumeHandle_;
		};
	}
}