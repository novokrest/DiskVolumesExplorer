#pragma once

#include "VmWareConnectionConfig.h"
#include "VmWareDiskConfig.h"

using namespace DiskVolumesExplorer::Core::Configs;

namespace DiskVolumesExplorer
{
namespace Native
{
namespace Wrappers
{
	ref class Converters
	{
	public:
		static VmWareConnectionConfig ToNative(IVmWareConnectionConfig ^connectionConfig);
		static VmWareDiskConfig ToNative(IVmWareDiskConfig ^diskConfig);
	};
}
}
}