using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreInfo.Plugin.Abstraction
{
	public abstract class BaseStoreInfo : IStoreInfo
	{
		public abstract string GetCurrentAppVersion();
		public abstract Task<AppStoreInfo> GetStoreAppVersion(string platform);
		public abstract bool IsLatest(string appVersion);
	}
}
