using Acr.UserDialogs;
using Plugin.StoreInfo;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Net.Http;
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
            try
            {
                UserDialogs.Instance.ShowLoading("Checking for new version");
                var isLatest = await CrossStoreInfo.Current.IsUsingLatestVersion();

                if (!isLatest)
                {
                    UserDialogs.Instance.HideLoading();
                    var confirmResult = await UserDialogs.Instance.ConfirmAsync(message: "There's new update from store.", okText: "Update Now", cancelText: "Later");
                    if (confirmResult)
                    {
                        await CrossStoreInfo.Current.OpenAppInStore();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync(title: "Error", message: "Please check internet connectivity.");
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync(title: "Error", message: ex.Message);
            }

        }

        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            this.PackageName = CrossStoreInfo.Current.GetPackageName();
            this.ManifestVersion = CrossStoreInfo.Current.InstalledVersionNumber;

            try
            {
                UserDialogs.Instance.ShowLoading("Getting app's information");

                var appStoreInfo = await CrossStoreInfo.Current.GetAppInfo();
                this.AppStoreVersion = appStoreInfo?.StoreVersion;
            }
            catch (HttpRequestException ex)
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync(title: "Error", message: "Please check internet connectivity.");
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                await UserDialogs.Instance.AlertAsync(title: "Error", message: ex.Message);
            }
        }
    }
}
