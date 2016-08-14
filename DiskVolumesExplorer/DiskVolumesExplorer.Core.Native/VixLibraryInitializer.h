#pragma once

namespace DiskVolumesExplorer
{
namespace Core
{
namespace Native
{
	class VixLibraryInitializer
	{
	public:
		VixLibraryInitializer();
		~VixLibraryInitializer();
	private:
		static std::string const VmWareLibraryDir;
		static std::string const VmWareConfigFile;
	};
}
}
}