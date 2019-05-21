using Foundation;
using StoreInfo.Plugin.Abstraction;

namespace StoreInfo.Plugin.iOS
{
    public class AppInfoProvider : IAppInfoProvider
    {
        public string PackageName
        {
            get { return NSBundle.MainBundle.BundleIdentifier; }
        }

        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
        public int GetBuild()
        {
            return int.Parse(NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString());
        }
    }
}