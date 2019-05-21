using StoreInfo.Plugin.Abstraction;
using System;

namespace StoreInfo.Plugin
{
	public class CrossStoreInfo
	{
		static Lazy<IStoreInfo> implementation = new Lazy<IStoreInfo>(() => CreateStoreInfo(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

		static IStoreInfo CreateStoreInfo()
		{
#if PORTABLE
            return null;
#else
			return new StoreInfoImplementation();
#endif
		}

		public static IStoreInfo Current
		{
			get
			{
				var ret = implementation.Value;
				if (ret == null)
				{
					throw NotImplementedInReferenceAssembly();
				}
				return ret;
			}
		}

		internal static Exception NotImplementedInReferenceAssembly() =>
			new NotImplementedException("This functionality is not implemented in the portable version of this assembly.  You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");

		public static void Dispose()
		{
			if (implementation?.IsValueCreated ?? false)
			{
				implementation.Value.Dispose();

				implementation = new Lazy<IStoreInfo>(() => CreateStoreInfo(), System.Threading.LazyThreadSafetyMode.PublicationOnly);
			}
		}
	}
}
