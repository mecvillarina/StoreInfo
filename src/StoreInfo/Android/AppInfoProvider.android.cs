using Android.App;
using Android.Content.PM;

namespace Plugin.StoreInfo
{
    [Android.Runtime.Preserve(AllMembers = true)]
    internal class AppInfoProvider : IAppInfoProvider
    {
        public string PackageName
        {
            get { return Application.Context.PackageName; }
        }

        public string GetVersion()
        {
            var context = global::Android.App.Application.Context;

            var manager = context.PackageManager;
            var info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        public int GetBuild()
        {
            var context = global::Android.App.Application.Context;
            var manager = context.PackageManager;
            var info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionCode;
        }
    }
}
