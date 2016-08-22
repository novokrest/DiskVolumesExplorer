#pragma once

#include "DiskVolumesManager.h"

using namespace System;
using namespace DiskVolumesExplorer::Core::Configs;

namespace DiskVolumesExplorer
{
namespace Native
{
namespace Wrappers
{
	public ref class DiskVolumesManager
	{
		DiskVolumesManager(IVmWareConnectionConfig ^connectionConfig);
		~DiskVolumesManager();
		!DiskVolumesManager();

		void GetDisks();

	private:
		DiskVolumesExplorer::Native::DiskVolumesManager *nativeManager_;
	};
}
}
}
