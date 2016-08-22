#pragma once

#include "DiskVolumesManager.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace DiskVolumesExplorer::Service::Core::Configs;
using namespace DiskVolumesExplorer::Service::Core::Data;

namespace DiskVolumesExplorer
{
namespace Native
{
namespace Wrappers
{
	public ref class DiskVolumesManager
	{
	public:
		DiskVolumesManager(IVmWareConnectionConfig ^connectionConfig);
		~DiskVolumesManager();
		!DiskVolumesManager();

		::DiskData^ GetDiskData(IVmWareDiskConfig ^diskConfig);

	private:
		DiskVolumesExplorer::Native::DiskVolumesManager *nativeManager_;
	};
}
}
}
