using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StoreInfo.Plugin.Abstraction
{
	public abstract class BaseStoreInfo : IStoreInfo, IDisposable
	{
		public abstract string GetCurrentAppVersion();
		public abstract Task<AppStoreInfo> GetStoreAppVersion(string platform);
		public abstract bool IsLatest(string appVersion);

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		~BaseStoreInfo()
		{
			Dispose(false);
		}

		private bool disposed = false;
		/// <summary>
		/// Dispose method
		/// </summary>
		/// <param name="disposing"></param>
		public virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					//dispose only
				}

				disposed = true;
			}
		}
	}
}
