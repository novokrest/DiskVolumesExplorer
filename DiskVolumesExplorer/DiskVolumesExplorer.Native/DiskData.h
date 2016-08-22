#pragma once

#include "VolumeData.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	struct DiskData
	{
		std::string Title;
		std::string Type;
		ULONG64 SizeInBytes;
		std::string Status;
		VolumeVec Volumes;
	};
}
}