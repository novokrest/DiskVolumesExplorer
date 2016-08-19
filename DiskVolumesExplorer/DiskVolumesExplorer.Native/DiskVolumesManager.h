#pragma once

#include "VixDiskLibInitializer.h"
#include "VixMntApiInitializer.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	class DiskVolumesManager
	{
	public:
		DiskVolumesManager();
		~DiskVolumesManager();

		void Connect();

	private:
		VixDiskLibInitializer vixDiskLibInitializer_;
		VixMntApiInitializer vixMntApiInitializer_;
		
		VixDiskLibConnection connection_;
		bool connected_;
	};
}
}