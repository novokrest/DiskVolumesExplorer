#pragma once

namespace DiskVolumesExplorer
{
namespace Native
{
	struct VolumeData
	{
		std::string Title;
		std::string FileSystem;
		std::string Status;

		ULONG64 CapacityInBytes;
		ULONG64 FreeSpaceInBytes;
	};

	typedef std::vector<VolumeData> VolumeVec;
}
}