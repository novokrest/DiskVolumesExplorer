#pragma once

#include "VixDiskLibInitializer.h"
#include "VixMntApiInitializer.h"

namespace DiskVolumesExplorer
{
namespace Core
{
namespace Native
{
	public ref class DiskVolumesManager
	{
	public:
		DiskVolumesManager();
		~DiskVolumesManager();
		!DiskVolumesManager();

		void Connect();

	private:
		VixDiskLibInitializer *vixDiskLibInitializer_;
		VixMntApiInitializer *vixMntApiInitializer_;
	};
}
}
}