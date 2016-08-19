#pragma once

namespace DiskVolumesExplorer
{
namespace Native
{
	class VixDiskLibErrWrapper
	{
	public:
		explicit VixDiskLibErrWrapper(VixError errCode, const char* file, int line);
		VixDiskLibErrWrapper(const char* description, const char* file, int line);

		std::string Description() const;
		VixError ErrorCode() const;
		std::string File() const;
		int Line() const;

	private:
		VixError _errCode;
		std::string _desc;
		std::string _file;
		int _line;
	};
}
}