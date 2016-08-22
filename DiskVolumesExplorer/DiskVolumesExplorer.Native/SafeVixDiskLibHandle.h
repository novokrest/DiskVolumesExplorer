#pragma once

namespace DiskVolumesExplorer
{
namespace Native
{
	class SafeVixDiskLibHandle
	{
	public:
		SafeVixDiskLibHandle(const VixDiskLibConnection &connection, const std::string &path, uint32 flags);
		~SafeVixDiskLibHandle();

		operator VixDiskLibHandle&();
		VixDiskLibHandle* operator&();

	private:
		VixDiskLibHandle diskHandle_;
	};
}
}