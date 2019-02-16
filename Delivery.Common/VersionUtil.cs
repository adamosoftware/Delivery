using System;
using System.Diagnostics;

namespace Delivery.Common
{
	public static class VersionUtil
	{
		public static bool TryGetProductVersion(string fileName, out string version)
		{
			version = null;
			try
			{
				version = GetProductVersion(fileName).ToString();
				return true;
			}
			catch 
			{
				return false;
			}
		}

		public static Version GetProductVersion(string fileName)
		{
			return GetVersionInfo(fileName, (info) => new Version(info.ProductVersion));
		}

		public static Version GetVersionInfo(string fileName, Func<FileVersionInfo, Version> selector)
		{
			try
			{
				var fv = FileVersionInfo.GetVersionInfo(fileName);
				return selector.Invoke(fv);
			}
			catch (Exception exc)
			{
				throw new Exception($"Failed to get version info from {fileName}: {exc.Message}");
			}
		}
	}
}