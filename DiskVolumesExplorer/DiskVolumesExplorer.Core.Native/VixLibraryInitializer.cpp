#include "Stdafx.h"
#include "VixLibraryInitializer.h"

namespace DiskVolumesExplorer
{
namespace Core
{
namespace Native
{
	std::string const VixLibraryInitializer::VmWareLibraryDir = "C:\\Program Files\\VmWare\\VDDK550";
	std::string const VixLibraryInitializer::VmWareConfigFile = "VmWareConfig.txt";

	VixLibraryInitializer::VixLibraryInitializer()
	{
		VixError vixError = VixDiskLib_InitEx(VIXDISKLIB_VERSION_MAJOR,
											  VIXDISKLIB_VERSION_MINOR,
											  &VmWareUtils::LogFunc, &VmWareUtils::WarnFunc, &VmWareUtils::PanicFunc,
											  VmWareLibraryDir.c_str(),
											  VmWareConfigFile.c_str());
		CHECK_AND_THROW(vixError);
	}

	VixLibraryInitializer::~VixLibraryInitializer()
	{
		VixDiskLib_Exit();
	}
}
}
}