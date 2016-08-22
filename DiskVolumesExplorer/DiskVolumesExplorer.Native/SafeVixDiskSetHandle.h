#pragma once

namespace DiskVolumesExplorer
{
namespace Native
{
	class SafeVixDiskSetHandle
	{
	public:
		SafeVixDiskSetHandle(VixDiskLibHandle diskHandles[], size_t numberOfDisks, uint32 openMode);
		~SafeVixDiskSetHandle();

		operator VixDiskSetHandle&();
		VixDiskSetHandle* operator&();

	private:
		VixDiskSetHandle diskSetHandle_;
	};
}
}