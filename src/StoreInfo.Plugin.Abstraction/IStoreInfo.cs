using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreInfo.Plugin.Abstraction
{
	public interface IStoreInfo
	{
		bool IsLatest(string appVersion);
		string GetCurrentAppVersion();
		Task<AppStoreInfo> GetStoreAppVersion(string platform);
	}
}
