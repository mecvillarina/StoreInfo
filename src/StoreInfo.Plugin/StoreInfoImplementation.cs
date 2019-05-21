using StoreInfo.Plugin.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StoreInfo.Plugin
{
	public class StoreInfoImplementation : BaseStoreInfo
	{
		private readonly IAppInfoProvider _appInfoProvider;
		public StoreInfoImplementation()
		{
			_appInfoProvider = DependencyService.Get<IAppInfoProvider>();
		}

		public override string GetCurrentAppVersion()
		{
			return null;
		}

		public override Task<AppStoreInfo> GetStoreAppVersion(string platform)
		{
			return null;
		}

		public override bool IsLatest(string appVersion)
		{
			return false;
		}
	}
}
