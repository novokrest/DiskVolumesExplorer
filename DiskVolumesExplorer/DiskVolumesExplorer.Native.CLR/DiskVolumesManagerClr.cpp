// This is the main DLL file.

#include "stdafx.h"
#include "DiskVolumesManagerClr.h"

namespace DiskVolumesExplorer
{
namespace Native
{
namespace Wrappers
{
	DiskVolumesManager::DiskVolumesManager()
		: nativeManager_(new DiskVolumesExplorer::Native::DiskVolumesManager())
	{

	}

	DiskVolumesManager::~DiskVolumesManager()
	{
		this->!DiskVolumesManager();
	}

	DiskVolumesManager::!DiskVolumesManager()
	{
		delete nativeManager_;
	}
}
}
}

