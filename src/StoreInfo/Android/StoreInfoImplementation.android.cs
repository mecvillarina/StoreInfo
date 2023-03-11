using Android.App;
using Android.Content;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Net = Android.Net;

namespace Plugin.StoreInfo
{
    /// <summary>
    /// <see cref="IStoreInfo"/> implementation for iOS.
    /// </summary>
    [Android.Runtime.Preserve(AllMembers = true)]
    public class StoreInfoImplementation : IStoreInfo
    {
        readonly string _packageName = Application.Context.PackageName;
        readonly string _versionName = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0).VersionName;

        /// <inheritdoc />
        public string InstalledVersionNumber
        {
            get => _versionName;
        }

        /// <inheritdoc />
        public async Task<bool> IsUsingLatestVersion()
        {
            var latestVersion = string.Empty;

            try
            {
                latestVersion = await GetLatestVersionNumber();

                return Version.Parse(latestVersion).CompareTo(Version.Parse(_versionName)) <= 0;
            }
            catch (Exception e)
            {
                throw new StoreInfoException($"Error comparing current app version number with latest. Version name={_versionName} and lastest version={latestVersion} .", e);
            }
        }

        /// <inheritdoc />
        public async Task<AppStoreInfo> GetAppInfo()
        {
            return await GetAppInfo(_packageName);
        }

        /// <inheritdoc />
        public async Task<AppStoreInfo> GetAppInfo(string appName)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentNullException(nameof(appName));
            }

            string appVersion = string.Empty;
            string appUrl = string.Format("https://play.google.com/store/apps/details?id={0}", appName);

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows; U; WindowsNT 5.1; en-US; rv1.8.1.6) Gecko/20070725 Firefox/2.0.0.6");

                    var response = await client.GetAsync(appUrl, HttpCompletionOption.ResponseContentRead);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new StoreInfoException($"Error connecting to the Play Store. Url={appUrl}.");
                    }

                    string contentResponse = await response.Content.ReadAsStringAsync();

                    var rx = new Regex(@"""\d+\.\d+\.\d", RegexOptions.Compiled);
                    MatchCollection matches = rx.Matches(contentResponse);

                    if (matches.Any())
                    {
                        appVersion = matches[0].Value.Replace("\"", string.Empty);
                        return new AppStoreInfo() { StoreVersion = appVersion, AppName = appName, StoreUrl = appUrl };
                    }
                }
            }
            catch (Exception e)
            {
                throw new StoreInfoException($"Error parsing content from the Play Store. Url={appUrl}.", e);
            }

            return new AppStoreInfo() { StoreVersion = appVersion, StoreUrl = appUrl, AppName = appName };
        }

        /// <inheritdoc />
        public async Task<string> GetLatestVersionNumber()
        {
            return await GetLatestVersionNumber(_packageName);
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
            return _packageName;
        }

        /// <inheritdoc />
        public Task OpenAppInStore()
        {
            return OpenAppInStore(_packageName);
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
                var intent = new Intent(Intent.ActionView, Net.Uri.Parse($"market://details?id={appName}"));
                intent.SetPackage("com.android.vending");
                intent.SetFlags(ActivityFlags.NewTask);
                Application.Context.StartActivity(intent);
            }
            catch (ActivityNotFoundException)
            {
                var intent = new Intent(Intent.ActionView, Net.Uri.Parse($"https://play.google.com/store/apps/details?id={appName}"));
                Application.Context.StartActivity(intent);
            }

            return Task.FromResult(true);
        }


    }
}
