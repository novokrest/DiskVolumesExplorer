#pragma once

#include "DiskVolumesManager.h"

using namespace System;

namespace DiskVolumesExplorer
{
namespace Native
{
namespace Wrappers
{
	public ref class DiskVolumesManager
	{
		DiskVolumesManager();
		~DiskVolumesManager();
		!DiskVolumesManager();

		

	private:
		DiskVolumesExplorer::Native::DiskVolumesManager *nativeManager_;
	};
}
}
}
