using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreInfo.Plugin.Abstraction
{
	public interface IStoreInfo : IDisposable
	{
		string GetCurrentAppVersion();
        Task<AppStoreInfo> GetStoreAppVersion();
    }
}
