using StoreInfo.Plugin.Abstraction;
using Xamarin.Forms;

namespace StoreInfo.Plugin.Droid
{
    public static class StoreInfo
    {
        public static void Init()
        {
            DependencyService.Register<IAppInfoProvider, AppInfoProvider>();
        }
    }
}