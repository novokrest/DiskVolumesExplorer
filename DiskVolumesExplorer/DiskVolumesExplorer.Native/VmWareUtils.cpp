#include "Stdafx.h"
#include "VixDiskLibUtils.h"

namespace DiskVolumesExplorer
{
namespace Native
{
	/*
	*--------------------------------------------------------------------------
	*
	* LogFunc --
	*
	*      Callback for VixDiskLib Log messages.
	*
	* Results:
	*      None.
	*
	* Side effects:
	*      None.
	*
	*--------------------------------------------------------------------------
	*/

	void VmWareUtils::LogFunc(const char *fmt, va_list args)
	{
		printf("Log: ");
		vprintf(fmt, args);
	}


	/*
	*--------------------------------------------------------------------------
	*
	* WarnFunc --
	*
	*      Callback for VixDiskLib Warning messages.
	*
	* Results:
	*      None.
	*
	* Side effects:
	*      None.
	*
	*--------------------------------------------------------------------------
	*/

	void VmWareUtils::WarnFunc(const char *fmt, va_list args)
	{
		printf("Warning: ");
		vprintf(fmt, args);
	}


	/*
	*--------------------------------------------------------------------------
	*
	* PanicFunc --
	*
	*      Callback for VixDiskLib Panic messages.
	*
	* Results:
	*      None.
	*
	* Side effects:
	*      None.
	*
	*--------------------------------------------------------------------------
	*/

	void VmWareUtils::PanicFunc(const char *fmt, va_list args)
	{
		printf("Panic: ");
		vprintf(fmt, args);
		exit(10);
	}
}
}