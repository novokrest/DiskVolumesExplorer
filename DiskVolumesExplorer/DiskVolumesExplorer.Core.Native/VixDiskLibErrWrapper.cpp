#include "Stdafx.h"
#include "VixDiskLibErrWrapper.h"

namespace DiskVolumesExplorer
{
namespace Core
{
namespace Native
{
	VixDiskLibErrWrapper::VixDiskLibErrWrapper(VixError errCode, const char* file, int line)
		:
		_errCode(errCode),
		_file(file),
		_line(line)
	{
		char* msg = VixDiskLib_GetErrorText(errCode, NULL);
		_desc = msg;
		VixDiskLib_FreeErrorText(msg);
	}

	VixDiskLibErrWrapper::VixDiskLibErrWrapper(const char* description, const char* file, int line)
		:
		_errCode(VIX_E_FAIL),
		_desc(description),
		_file(file),
		_line(line)
	{
	}

	std::string VixDiskLibErrWrapper::Description() const { return _desc; }
	VixError VixDiskLibErrWrapper::ErrorCode() const { return _errCode; }
	std::string VixDiskLibErrWrapper::File() const { return _file; }
	int VixDiskLibErrWrapper::Line() const { return _line; }
}
}
}