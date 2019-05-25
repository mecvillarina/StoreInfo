using Acr.UserDialogs;
using Plugin.StoreInfo;
using Prism.Commands;
using Prism.Navigation;
using System.Threading.Tasks;

namespace StoreInfo.Sample.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            this.CheckUpdateCommand = new DelegateCommand(async () => await ExecuteCheckUpdateCommand());
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

        public DelegateCommand CheckUpdateCommand { get; private set; }

        private async Task ExecuteCheckUpdateCommand()
        {
            var isLatest = await CrossStoreInfo.Current.IsUsingLatestVersion();

            if (!isLatest)
            {
                var confirmResult = await UserDialogs.Instance.ConfirmAsync(message: "There's new update from store.", okText: "Update", cancelText: "Later");
                if (confirmResult)
                {
                    await CrossStoreInfo.Current.OpenAppInStore();
                }
            }
        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            this.PackageName = CrossStoreInfo.Current.GetAppPackageName();
            this.ManifestVersion = CrossStoreInfo.Current.GetCurrentVersion();
            var appStoreInfo = await CrossStoreInfo.Current.GetStoreAppVersionAsync();

            this.AppStoreVersion = appStoreInfo?.AppVersion;
        }
    }
}
