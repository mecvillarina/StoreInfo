using Plugin.StoreInfo;
using Prism.Navigation;

namespace StoreInfo.Sample.ViewModels
{
	public class MainPageViewModel : ViewModelBase
	{
		public MainPageViewModel(INavigationService navigationService)
			: base(navigationService)
		{
			this.Title = "Main Page";
		}

		private string _packageName;
		public string PackageName
		{
			get => _packageName;
			set => SetProperty(ref _packageName, value);
		}

		private string _manifestVersion;
		public string ManifestVersion
		{
			get => _manifestVersion;
			set => SetProperty(ref _manifestVersion, value);
		}

		private string _appStoreVersion;
		public string AppStoreVersion
		{
			get => _appStoreVersion;
			set => SetProperty(ref _appStoreVersion, value);
		}

		public override async void OnNavigatedTo(INavigationParameters parameters)
		{
			base.OnNavigatedTo(parameters);

			PackageName = CrossStoreInfo.Current.GetAppPackageName();
			ManifestVersion = CrossStoreInfo.Current.GetCurrentVersion();
			var appStoreInfo = await CrossStoreInfo.Current.GetStoreAppVersionAsync();

			AppStoreVersion = appStoreInfo?.AppVersion;
		}
	}
}
