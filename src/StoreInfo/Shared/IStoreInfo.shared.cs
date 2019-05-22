using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.StoreInfo
{
    public interface IStoreInfo
    {
        string GetAppPackageName();
		string GetCurrentVersion();
        Task<AppStoreInfo> GetStoreAppVersionAsync();
    }
}
