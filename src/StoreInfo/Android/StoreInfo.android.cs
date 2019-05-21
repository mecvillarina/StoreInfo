using Xamarin.Forms;

namespace Plugin.StoreInfo
{
    public static class StoreInfo 
    {
        public static void Init()
        {
            DependencyService.Register<IAppInfoProvider, AppInfoProvider>();
        }
    }
}
