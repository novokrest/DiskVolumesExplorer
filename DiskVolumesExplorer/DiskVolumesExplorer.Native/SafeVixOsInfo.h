#pragma once

namespace DiskVolumesExplorer
{
namespace Native
{
	class SafeVixOsInfo
	{
	public:
		SafeVixOsInfo(const VixDiskSetHandle &diskSetHandle);
		~SafeVixOsInfo();

		VixOsInfo* operator->();

	private:
		VixOsInfo *osInfo_;
	};
}
}