using HtmlAgilityPack;
using Newtonsoft.Json;
using StoreInfo.Plugin.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        public override async Task<AppStoreInfo> GetStoreAppVersion()
        {
            var version = await GetStoreAppVersion(_appInfoProvider.PackageName);
            var appVersion = version.Item1;
            var storeUrl = version.Item2;

            if (!string.IsNullOrEmpty(appVersion))
            {
                return new AppStoreInfo() { AppVersion = appVersion, StoreUrl = storeUrl };
            }

            return null;
        }

        private async Task<Tuple<string, string>> GetStoreAppVersion(string packageName)
        {
            packageName = packageName ?? string.Empty;
            if (string.IsNullOrEmpty(packageName.Trim()))
            {
                return new Tuple<string, string>(string.Empty, string.Empty);
            }

            if (Device.RuntimePlatform == Device.Android)
            {
                return await GetPlayStoreAppVersion(packageName);
            }
            else if (Device.RuntimePlatform == Device.iOS)
            {
                return await GetAppStoreAppVersion(packageName);
            }

            return new Tuple<string, string>(string.Empty, string.Empty);
        }

        private async Task<Tuple<string, string>> GetPlayStoreAppVersion(string packageName)
        {
#if DEBUG
            packageName = "com.mecodes.chronos.droid";
#endif
            string appVersion = string.Empty;
            string appUrlString = string.Format("https://play.google.com/store/apps/details?id={0}", packageName);

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows; U; WindowsNT 5.1; en-US; rv1.8.1.6) Gecko/20070725 Firefox/2.0.0.6");


                    var response = await client.GetAsync(appUrlString);

                    if (response.IsSuccessStatusCode)
                    {
                        string contentResponse = await response.Content.ReadAsStringAsync();

                        var htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(contentResponse);
                        var rootNode = htmlDoc.DocumentNode;

                        if (rootNode != null)
                        {
                            var currentVersionNode = rootNode.Descendants().FirstOrDefault(x => x.Attributes.Contains("class") && x.Attributes["class"].Value.Contains("hAyfc") && x.InnerText.Contains("Current Version"));

                            if (currentVersionNode != null && !string.IsNullOrEmpty(currentVersionNode.InnerText))
                            {
                                appVersion = currentVersionNode.InnerText.Replace("Current Version", "").Trim();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return new Tuple<string, string>(appVersion, appUrlString);
        }

        private async Task<Tuple<string, string>> GetAppStoreAppVersion(string packageName)
        {

#if DEBUG
            packageName = "com.mecodes.chronos.droid";
#endif
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows; U; WindowsNT 5.1; en-US; rv1.8.1.6) Gecko/20070725 Firefox/2.0.0.6");

                    string appUrlString = string.Format("https://itunes.apple.com/us/lookup?bundleId={0}", packageName);

                    var response = await client.GetAsync(appUrlString);

                    if (response.IsSuccessStatusCode)
                    {
                        string contentResponse = await response.Content.ReadAsStringAsync();

                        var lookupContractResponse = JsonConvert.DeserializeObject<AppStoreLookupRoot>(contentResponse);

                        if (lookupContractResponse.ResultCount > 0)
                        {
                            var lookupResult = lookupContractResponse.LookupResults.FirstOrDefault();

                            if (lookupResult != null)
                            {
                                var appVersion = lookupResult.Version;
                                var storeUrl = lookupResult.TrackViewUrl.OriginalString;
                                return new Tuple<string, string>(appVersion, storeUrl);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return new Tuple<string, string>(string.Empty, string.Empty);
        }

    }
}
