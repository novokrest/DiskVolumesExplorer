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
		VixError vixError = VixMntapi_Init(VIXMNTAPI_VERSION_MAJOR,
										   VIXMNTAPI_VERSION_MINOR,
			                               &VmWareUtils::LogFunc, &VmWareUtils::WarnFunc, &VmWareUtils::PanicFunc,
			                               VIXDISKLIB_LIBDIR,
			                               NULL/*VIXDISKLIB_CONFIG*/);
		CHECK_AND_THROW(vixError);
	}

	VixMntApiInitializer::~VixMntApiInitializer()
	{
		VixMntapi_Exit();
	}
}
}
}