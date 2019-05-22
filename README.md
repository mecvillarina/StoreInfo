##  Store Info for Xamarin.Forms [![PayPal donate button](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=4KHTXCBWYXTNG "Donate to this project using Paypal")

Store Info for Xamarin Forms has a mechanism to provide to your app the current Play Store and App Store version and store link based on your app package name/bundle id. 

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

<!-- You have the option to use your DI/IOC container

```C#
    container.Register<IStoreInfo, 
    global::Xamarin.Forms.Forms.Init();
    Plugin.StoreInfo.StoreInfo.Init();
    ...
``` -->

<!-- ###Usage

On your XF PCL/Core Project. You can get your package name and current local version

```C#

    string packageName = CrossStoreInfo.Current.GetAppPackageName();
    string 
    Plugin.StoreInfo.StoreInfo.Init();

``` -->