using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Plugin.StoreInfo
{
    /// <summary>
    /// StoreInfo plugin
    /// </summary>
    public interface IStoreInfo
    {
        /// <summary>
        /// Gets the version number of the current app's installed version.
        /// </summary>
        /// <value>The current app's installed version number.</value>
        string InstalledVersionNumber { get; }

        /// <summary>
        /// Check if the current app is using the latest version.
        /// </summary>
        /// <returns>Return True if the app is using the latest version.</returns>
        Task<bool> IsUsingLatestVersion();

        /// <summary>
        /// Gets the information of the current app's latest version available in the public store.
        /// </summary>
        /// <returns>The current app's latest information.</returns>
        Task<AppStoreInfo> GetAppInfo();

        /// <summary>
        /// Gets the information of an app's latest version available in the public store.
        /// </summary>
        /// <returns>The specified app's latest information.</returns>
        /// <param name="appName">Name of the app to get.</param>
        Task<AppStoreInfo> GetAppInfo(string appName);

        /// <summary>
        /// Gets the information of the current app's latest version available in the public store.
        /// </summary>
        /// <returns>The current app's latest version number.</returns>
        Task<string> GetLatestVersionNumber();

        /// <summary>
        /// Gets the version number of an app's latest version available in the public store.
        /// </summary>
        /// <returns>The specified app's latest version number</returns>
        /// <param name="appName">Name of the app to get.</param>
        Task<string> GetLatestVersionNumber(string appName);

        /// <summary>
        /// Gets the package name/bundle id of the current app.
        /// </summary>
        /// <returns>The current app's package name/bundle id.</returns>
        string GetPackageName();

        /// <summary>
        /// Opens the current app in the public store.
        /// </summary>
        Task OpenAppInStore();

        /// <summary>
        /// Opens an app in the public store.
        /// </summary>
        /// <param name="appName">Name of the app to open.</param>
        Task OpenAppInStore(string appName);

    }
}
