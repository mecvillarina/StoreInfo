
using Android.App;
using Android.Content.PM;

namespace StoreInfo.Plugin.Droid
{
	public class AppInfoProvider : IAppInfoProvider
	{
		public string PackageName
		{
			get { return Application.Context.PackageName; }
		}

		public string GetVersion()
		{
			var context = global::Android.App.Application.Context;

			PackageManager manager = context.PackageManager;
			PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

			return info.VersionName;
		}

		public int GetBuild()
		{
			var context = global::Android.App.Application.Context;
			PackageManager manager = context.PackageManager;
			PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

			return info.VersionCode;
		}
	}
}