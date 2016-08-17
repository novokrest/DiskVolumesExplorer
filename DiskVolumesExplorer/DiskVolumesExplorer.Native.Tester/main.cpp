// DiskVolumesExplorer.Native.Tester.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "DiskVolumesManager.h"

using namespace DiskVolumesExplorer::Core::Native;

int main()
{
	DiskVolumesManager diskVolumesManager;

	diskVolumesManager.Connect();

    return 0;
}

