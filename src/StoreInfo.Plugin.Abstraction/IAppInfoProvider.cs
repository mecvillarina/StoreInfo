using System;
using System.Collections.Generic;
using System.Text;

namespace StoreInfo.Plugin.Abstraction
{
	public interface IAppInfoProvider
	{
		string PackageName { get; }
		string GetVersion();
		int GetBuild();
	}
}
