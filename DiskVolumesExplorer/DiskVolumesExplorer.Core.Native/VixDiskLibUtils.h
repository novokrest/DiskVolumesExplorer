#pragma once

#define VIXDISKLIB_VERSION_MAJOR 6
#define VIXDISKLIB_VERSION_MINOR 0
#define VIXDISKLIB_LIBDIR       "C:\\Program Files\\VmWare\\VDDK550"
#define VIXDISKLIB_CONFIG       "VmWareConfig.txt"


#define THROW_ERROR(vixError) \
   throw VixDiskLibErrWrapper((vixError), __FILE__, __LINE__)

#define CHECK_AND_THROW(vixError)                                    \
   do {                                                              \
      if (VIX_FAILED((vixError))) {                                  \
         throw VixDiskLibErrWrapper((vixError), __FILE__, __LINE__); \
      }                                                              \
   } while (0)

namespace DiskVolumesExplorer
{
namespace Core
{
namespace Native
{
	class VmWareUtils
	{
	public:
		static void LogFunc(const char *fmt, va_list args);
		static void WarnFunc(const char *fmt, va_list args);
		static void PanicFunc(const char *fmt, va_list args);
	};
}
}
}