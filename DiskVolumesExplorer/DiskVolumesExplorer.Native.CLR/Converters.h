#pragma once

#include "VmWareConnectionConfig.h"
#include "VmWareDiskConfig.h"
#include "DiskData.h"
#include "VolumeData.h"

using namespace DiskVolumesExplorer::Service::Core::Configs;
using namespace DiskVolumesExplorer::Service::Core::Data;

namespace DiskVolumesExplorer
{
namespace Native
{
namespace Wrappers
{
	ref class ConfigConverter
	{
	public:
		static VmWareConnectionConfig ToNative(IVmWareConnectionConfig ^connectionConfig);
		static VmWareDiskConfig ToNative(IVmWareDiskConfig ^diskConfig);
	};

	ref class DataConverter
	{
	public:
		static ::DiskData^ FromNative(const DiskData &diskData);
		static ::VolumeData^ FromNative(const VolumeData &volumeData);
	};
}
}
}