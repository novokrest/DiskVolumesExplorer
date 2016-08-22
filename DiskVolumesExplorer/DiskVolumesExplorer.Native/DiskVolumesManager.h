#pragma once

#include "VixDiskLibInitializer.h"
#include "VixMntApiInitializer.h"
#include "VmWareConnectionConfig.h"
#include "VmWareDiskConfig.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	class DiskVolumesManager
	{
	public:
		DiskVolumesManager(const VmWareConnectionConfig &connectionConfig);
		~DiskVolumesManager();

		void Connect();

		void GetVolumes(const VmWareDiskConfig &diskConfig);

	private:
		VixDiskLibInitializer vixDiskLibInitializer_;
		VixMntApiInitializer vixMntApiInitializer_;

		VmWareConnectionConfig connectionConfig_;
		VixDiskLibConnection connection_;
		bool connected_;

		void FillInConnectionParams(VixDiskLibConnectParams &connectParams, std::string &vmxSpec, const VmWareDiskConfig &diskConfig);
	};
}
}