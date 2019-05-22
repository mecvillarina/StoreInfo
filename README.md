##  Store Info for Xamarin.Forms [![PayPal donate button](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4KHTXCBWYXTNG "Donate to this project using Paypal")

Store Info for Xamarin Forms has a mechanism to extract the current information of your app in store (playstore/appstore) using the package name/bundle id.  Install this in your PCL and platform specific projects.

Nuget: https://www.nuget.org/packages/Plugin.StoreInfo/
Sample: https://github.com/mecvillarina/StoreInfo/tree/master/sample

## Simple Example:

### Init

On your XF Android project. Add the following to your MainActivity.cs

```C#
protected override void OnCreate(Bundle savedInstanceState)
{
    ...
    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
    Plugin.StoreInfo.StoreInfo.Init();
    ...
}
```

On your XF iOS project. Add the following to your AppDelegate.cs

```C#
public override bool FinishedLaunching(UIApplication app, NSDictionary options)
{
    ...
    global::Xamarin.Forms.Forms.Init();
    Plugin.StoreInfo.StoreInfo.Init();
    ...
}
```

You have the option to use your DI/IOC container

```C#
...

containerRegistry.RegisterInstance<IStoreInfo>(CrossStoreInfo.Current);

...
```

###Usage

On your XF PCL/Core Project. You can get your package name and current local version

```C#

string packageName = CrossStoreInfo.Current.GetAppPackageName(); //Return package name / bundle id
string version = CrossStoreInfo.Current.GetCurrentVersion();    //Return manifest version
var appStoreInfo = await CrossStoreInfo.Current.GetStoreAppVersionAsync();  //Return store app version and link

```

## License
The Apache License 2.0 see [License file](LICENSE)
