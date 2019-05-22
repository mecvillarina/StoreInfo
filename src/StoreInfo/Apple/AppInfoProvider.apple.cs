using Foundation;

namespace Plugin.StoreInfo
{
    [Foundation.Preserve(AllMembers = true)]
    internal class AppInfoProvider : IAppInfoProvider
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
