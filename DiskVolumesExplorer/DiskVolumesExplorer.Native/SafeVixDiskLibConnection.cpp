#include "stdafx.h"
#include "SafeVixDiskLibConnection.h"


namespace DiskVolumesExplorer
{
namespace Native
{
	SafeVixDiskLibConnection::SafeVixDiskLibConnection(const VixDiskLibConnectParams &connectParams)
		: connection_(nullptr)
	{
		VixError vixError = VIX_ERROR_CODE(VixDiskLib_Connect(&connectParams, &connection_));
		CHECK_AND_THROW(vixError);
	}

	SafeVixDiskLibConnection::~SafeVixDiskLibConnection()
	{
		VixError vixError =	VIX_ERROR_CODE(VixDiskLib_Disconnect(connection_));
		//CHECK_AND_THROW(vixError);
	}

	SafeVixDiskLibConnection::operator VixDiskLibConnection& ()
	{
		return connection_;
	}

	VixDiskLibConnection* SafeVixDiskLibConnection::operator&()
	{
		return &connection_;
	}
}
}