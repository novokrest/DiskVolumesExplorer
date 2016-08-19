#include "Stdafx.h"
#include "VixDiskLibInitializer.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	VixDiskLibInitializer::VixDiskLibInitializer()
	{
		VixError vixError = VixDiskLib_InitEx(VIXDISKLIB_VERSION_MAJOR,
											  VIXDISKLIB_VERSION_MINOR,
											  &VmWareUtils::LogFunc, &VmWareUtils::WarnFunc, &VmWareUtils::PanicFunc,
											  VIXDISKLIB_LIBDIR,
											  VIXDISKLIB_CONFIG);
		CHECK_AND_THROW(vixError);
	}

	VixDiskLibInitializer::~VixDiskLibInitializer()
	{
		VixDiskLib_Exit();
	}
}
}