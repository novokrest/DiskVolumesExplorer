#pragma once

namespace DiskVolumesExplorer
{
namespace Native
{
	class SafeVixDiskLibConnection
	{
	public:
		SafeVixDiskLibConnection(const VixDiskLibConnectParams &connectParams);
		~SafeVixDiskLibConnection();

		operator VixDiskLibConnection& ();
		VixDiskLibConnection* operator&();

	private:
		VixDiskLibConnection connection_;
	};
}
}