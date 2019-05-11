using Delivery.Common;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Delivery.Client
{
	public class InstallManager
	{
		private static HttpClient _client = new HttpClient();

		public InstallManager(string storageAccount, string containerName, string installerExeName, string productName)
		{
			StorageAccount = storageAccount;
			ContainerName = containerName;
			InstallerExeName = installerExeName;
			ProductName = productName;
            LocalExe = Process.GetCurrentProcess().MainModule.FileName; // thanks to https://stackoverflow.com/a/5497123/2023653
            LocalVersion = VersionUtil.GetProductVersion(LocalExe);
		}

		public string StorageAccount { get; }
		public string ContainerName { get; }
		public string InstallerExeName { get; }
		public string ProductName { get; }

		private string LocalExe { get; }
		private Version LocalVersion { get; }

		public CloudVersionInfo NewVersionInfo { get; private set; }

		/// <summary>
		/// Checks for new version of app, prompts the user to download and install
		/// </summary>
		/// <param name="promptUser">Use this to display a message to the user and return true if the user accepts the download</param>
		/// <param name="exitApp">Use this to exit the application (i.e. Application.Quit in winform apps)</param>
		public async Task AutoInstallAsync(Func<bool> promptUser, Action exitApp)
		{
			if (await IsNewVersionAvailableAsync())
			{
				if (promptUser.Invoke())
				{
					string installerExe = await DownloadInstallerAsync();
					ProcessStartInfo psi = new ProcessStartInfo(installerExe);
					Process.Start(psi);
					exitApp.Invoke();
				}
			}
		}

		public async Task<bool> IsNewVersionAvailableAsync()
		{
			NewVersionInfo = null;

			try
			{
				var cloudInfo = await CloudVersionInfo.GetAsync(_client, BlobUtil.GetProductInfoUrl(StorageAccount, ContainerName, ProductName));
				if (cloudInfo.GetVersion() > LocalVersion)
				{
					NewVersionInfo = cloudInfo;
					return true;
				}
			}
			catch
			{
				// do nothing
			}

			return false;
		}

		public async Task<string> DownloadInstallerAsync()
		{
			return await _client.DownloadFileAsync(
				BlobUtil.GetBlobUrl(StorageAccount, ContainerName, InstallerExeName),
				Path.Combine(Path.GetTempPath(), InstallerExeName), true);
		}
	}
}