using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.StoreInfo
{
    internal interface IAppInfoProvider
    {
        string PackageName { get; }
        string GetVersion();
        int GetBuild();
    }
}
