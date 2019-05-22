##  Store Info for Xamarin.Forms [![PayPal donate button](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4KHTXCBWYXTNG "Donate to this project using Paypal")

Store Info for Xamarin Forms has a mechanism to extract the current information of your app in store (playstore/appstore) using the package name/bundle id.  Install this in your PCL and platform specific projects.

Nuget: https://www.nuget.org/packages/Plugin.StoreInfo/

Sample: https://github.com/mecvillarina/StoreInfo/tree/master/sample

## Usage:

You have the option to use your DI/IOC container

```C#
containerRegistry.RegisterInstance<IStoreInfo>(CrossStoreInfo.Current);
```

### Get installed version number

Gets the version number of the current app's installed version.

```C#
string versionNumber = await CrossStoreInfo.Current.InstalledVersionNumber;
```

### Get latest app information

Gets the information of the current app's latest version available in the public store.

```C#
var appStoreInfo = await CrossStoreInfo.Current.GetAppInfo();
```

Gets the information of an app's latest version available in the public store.

```C#
var appStoreInfo = await CrossStoreInfo.Current.GetAppInfo(appName);
```

- `appName` should be the app's **bundle identifier** (`CFBundleIdentifier`) on iOS and the app's **package name** on Android.

### Get latest version number

Get the version number of the current running app's latest version available in the public store:

```csharp
string latestVersionNumber = await CrossStoreInfo.Current.GetLatestVersionNumber();
```

Get the version number of any app's latest version available in the public store:

```csharp
string latestVersionNumber = await CrossStoreInfo.Current.GetLatestVersionNumber("appName");
```

- `appName` should be the app's **bundle identifier** (`CFBundleIdentifier`) on iOS and the app's **package name** on Android.

### Open app in public store

Open the current running app in the public store:

```csharp
await CrossStoreInfo.Current.OpenAppInStore();
```

Open any app in the public store:

```csharp
await CrossStoreInfo.Current.OpenAppInStore("appName");
```

- `appName` should be the app's **bundle identifier** (`CFBundleIdentifier`) on iOS and the app's **package name** on Android.

## License
The Apache License 2.0 see [License file](LICENSE)
