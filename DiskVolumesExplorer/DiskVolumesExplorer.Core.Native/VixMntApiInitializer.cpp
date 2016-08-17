#include "Stdafx.h"
#include "VixMntApiInitializer.h"

namespace DiskVolumesExplorer
{
namespace Core
{
namespace Native
{
	VixMntApiInitializer::VixMntApiInitializer()
	{
		VixError vixError = VixMntapi_Init(VIXDISKLIB_VERSION_MAJOR,
										   VIXDISKLIB_VERSION_MINOR,
			                               &VmWareUtils::LogFunc, &VmWareUtils::WarnFunc, &VmWareUtils::PanicFunc,
			                               VIXDISKLIB_LIBDIR,
			                               VIXDISKLIB_CONFIG);
		CHECK_AND_THROW(vixError);
	}

	VixMntApiInitializer::~VixMntApiInitializer()
	{
		VixMntapi_Exit();
	}
}
}
}