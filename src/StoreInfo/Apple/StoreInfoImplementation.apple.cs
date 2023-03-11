using Foundation;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Plugin.StoreInfo
{
    /// <summary>
    /// <see cref="IStoreInfo"/> implementation for iOS.
    /// </summary>
    [Foundation.Preserve(AllMembers = true)]
    public class StoreInfoImplementation : IStoreInfo
    {
        string _bundleName => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleName").ToString();
        string _bundleIdentifier => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleIdentifier").ToString();
        string _bundleVersion => NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();

        /// <inheritdoc />
        public string InstalledVersionNumber
        {
            get => this._bundleVersion;
        }

        /// <inheritdoc />
        public async Task<bool> IsUsingLatestVersion()
        {
            var latestVersion = string.Empty;

            try
            {
                latestVersion = await GetLatestVersionNumber();

                return Version.Parse(latestVersion).CompareTo(Version.Parse(this._bundleVersion)) <= 0;
            }
            catch (Exception e)
            {
                throw new StoreInfoException($"Error comparing current app version number with latest. Bundle version={this._bundleVersion} and lastest version={latestVersion} .", e);
            }
        }

        /// <inheritdoc />
        public async Task<AppStoreInfo> GetAppInfo()
        {
            return await GetAppInfo(this._bundleIdentifier);
        }

        /// <inheritdoc />
        public async Task<AppStoreInfo> GetAppInfo(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentNullException(nameof(appName));
            }

            string appVersion = string.Empty;
            string appUrl = string.Format("https://itunes.apple.com/us/lookup?bundleId={0}", appName);

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows; U; WindowsNT 5.1; en-US; rv1.8.1.6) Gecko/20070725 Firefox/2.0.0.6");

                    var response = await client.GetAsync(appUrl);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new StoreInfoException($"Error connecting to the App Store. Url={appUrl}.");
                    }


                    string contentResponse = await response.Content.ReadAsStringAsync();

                    var lookupContractResponse = JsonSerializer.Deserialize<AppStoreLookupRoot>(contentResponse);

                    if (lookupContractResponse.ResultCount > 0)
                    {
                        var lookupResult = lookupContractResponse.LookupResults.FirstOrDefault();

                        if (lookupResult != null)
                        {
                            appVersion = lookupResult.Version;
                            var storeUrl = lookupResult.TrackViewUrl.OriginalString;

                            return new AppStoreInfo() { AppName = appName, StoreUrl = storeUrl, StoreVersion = appVersion };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new StoreInfoException($"Error parsing content from the App Store. Url={appUrl}.", e);
            }

            return new AppStoreInfo() { StoreVersion = appVersion, StoreUrl = appUrl, AppName = appName };
        }

        /// <inheritdoc />
        public async Task<string> GetLatestVersionNumber()
        {
            return await GetLatestVersionNumber(this._bundleIdentifier);
        }

        /// <inheritdoc />
        public async Task<string> GetLatestVersionNumber(string appName)
        {
            var appStoreInfo = await GetAppInfo(appName);
            return appStoreInfo.StoreVersion;
        }

        /// <inheritdoc />
        public string GetPackageName()
        {
            return this._bundleName;
        }

        /// <inheritdoc />
        public Task OpenAppInStore()
        {
            return OpenAppInStore(this._bundleName);
        }

        /// <inheritdoc />
        public Task OpenAppInStore(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentNullException(nameof(appName));
            }

            try
            {
                //appName = appName.MakeSafeForAppStoreShortLinkUrl();

#if __IOS__
                UIKit.UIApplication.SharedApplication.OpenUrl(new NSUrl($"http://appstore.com/{appName}"));
#elif __MACOS__
                AppKit.NSWorkspace.SharedWorkspace.OpenUrl(new NSUrl($"http://appstore.com/mac/{appName}"));
#endif

                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new StoreInfoException($"Unable to open {appName} in App Store.", e);
            }
        }
    }
}