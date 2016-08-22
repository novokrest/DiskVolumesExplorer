#include "Stdafx.h"
#include "Converters.h"

using namespace msclr::interop;

namespace DiskVolumesExplorer
{
namespace Native
{
namespace Wrappers
{
	VmWareConnectionConfig Converters::ToNative(IVmWareConnectionConfig ^connectionConfig)
	{
		VmWareConnectionConfig nativeConnectionConfig = { 0 };

		nativeConnectionConfig.Server = marshal_as<std::string>(connectionConfig->Server);
		nativeConnectionConfig.User = marshal_as<std::string>(connectionConfig->User);
		nativeConnectionConfig.Password = marshal_as<std::string>(connectionConfig->Password);
		nativeConnectionConfig.ThumbPrint = marshal_as<std::string>(connectionConfig->ThumbPrint);

		return nativeConnectionConfig;
	}

	VmWareDiskConfig Converters::ToNative(IVmWareDiskConfig ^diskConfig)
	{

	}
}
}
}