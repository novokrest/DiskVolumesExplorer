#include "Stdafx.h"
#include "Converters.h"

using namespace System;
using namespace System::Collections::Generic;
using namespace msclr::interop;
using namespace DiskVolumesExplorer::Service::Core::Configs;

namespace DiskVolumesExplorer
{
namespace Native
{
namespace Wrappers
{
	VmWareConnectionConfig ConfigConverter::ToNative(IVmWareConnectionConfig ^connectionConfig)
	{
		VmWareConnectionConfig nativeConnectionConfig = { 0 };

		nativeConnectionConfig.Server = marshal_as<std::string>(connectionConfig->Server);
		nativeConnectionConfig.User = marshal_as<std::string>(connectionConfig->User);
		nativeConnectionConfig.Password = marshal_as<std::string>(connectionConfig->Password);
		nativeConnectionConfig.ThumbPrint = marshal_as<std::string>(connectionConfig->ThumbPrint);

		return nativeConnectionConfig;
	}

	VmWareDiskConfig ConfigConverter::ToNative(IVmWareDiskConfig ^diskConfig)
	{
		VmWareDiskConfig nativeDiskConfig = { 0 };

		nativeDiskConfig.Datacenter = marshal_as<std::string>(diskConfig->Datacenter);
		nativeDiskConfig.Datastore = marshal_as<std::string>(diskConfig->Datastore);
		nativeDiskConfig.VirtualDiskFilePath = marshal_as<std::string>(diskConfig->VirtualDiskFilePath);
		nativeDiskConfig.VirtualMachineConfigFilePath = marshal_as<std::string>(diskConfig->VirtualMachineConfigFilePath);

		return nativeDiskConfig;
	}

	::DiskData^ DataConverter::FromNative(const DiskData &nativeDiskData)
	{
		::DiskData^ diskData = gcnew ::DiskData();

		diskData->Title = gcnew String(nativeDiskData.Title.c_str());
		diskData->Type = gcnew String(nativeDiskData.Type.c_str());
		diskData->Status = gcnew String(nativeDiskData.Status.c_str());
		diskData->SizeInBytes = nativeDiskData.SizeInBytes;

		List<::VolumeData^>^ volumes = gcnew List<::VolumeData^>();
		for each(const VolumeData& nativeVolumeData in nativeDiskData.Volumes)
		{
			::VolumeData^ volumeData = FromNative(nativeVolumeData);
			volumes->Add(volumeData);
		}

		diskData->Volumes = volumes->ToArray();

		return diskData;
	}
	
	::VolumeData^ DataConverter::FromNative(const VolumeData &nativeVolumeData)
	{
		::VolumeData^ volumeData = gcnew ::VolumeData();

		volumeData->Title = gcnew String(nativeVolumeData.Title.c_str());
		volumeData->FileSystem = gcnew String(nativeVolumeData.FileSystem.c_str());
		volumeData->Status = gcnew String(nativeVolumeData.Status.c_str());
		volumeData->CapacityInBytes = nativeVolumeData.CapacityInBytes;
		volumeData->FreeSpaceInBytes = nativeVolumeData.FreeSpaceInBytes;

		return volumeData;
	}
}
}
}