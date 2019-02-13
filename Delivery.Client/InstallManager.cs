using Delivery.Common;
using Newtonsoft.Json;
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
			LocalExe = Assembly.GetExecutingAssembly().Location;
			LocalVersion = GetLocalProductVersion(LocalExe);
		}

		public string StorageAccount { get; }
		public string ContainerName { get; }
		public string InstallerExeName { get; }
		public string ProductName { get; }				

		private string LocalExe { get; }
		private Version LocalVersion { get; }

		public CloudProductVersionInfo NewVersionInfo { get; private set; }

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

		public async Task<CloudProductVersionInfo> GetCloudVersionInfoAsync()
		{
			return await DownloadInnerAsync(
				BlobUtil.GetProductInfoUrl(StorageAccount, ContainerName, ProductName),
				async (r) =>
				{
					string json = await r.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<CloudProductVersionInfo>(json);
				});
		}

		public async Task<bool> IsNewVersionAvailableAsync()
		{
			NewVersionInfo = null;

			try
			{				
				var cloudInfo = await GetCloudVersionInfoAsync();				
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
			string localFile = Path.Combine(Path.GetTempPath(), InstallerExeName);			

			return await DownloadInnerAsync(
				BlobUtil.GetBlobUrl(StorageAccount, ContainerName, InstallerExeName),
				async (r) =>
				{
					await r.Content.DownloadFileAsync(localFile, overwrite: true);
					return localFile;
				});			
		}

		private async Task<T> DownloadInnerAsync<T>(string url, Func<HttpResponseMessage, Task<T>> getResult)
		{
			// help from https://blogs.msdn.microsoft.com/henrikn/2012/02/17/httpclient-downloading-to-a-local-file/
			var response = await _client.GetAsync(url);
			response.EnsureSuccessStatusCode();
			await response.Content.LoadIntoBufferAsync();
			return await getResult.Invoke(response);
		}

		private static Version GetLocalProductVersion(string fileName)
		{
			try
			{
				var fv = FileVersionInfo.GetVersionInfo(fileName);
				return new Version(fv.ProductVersion);
			}
			catch (Exception exc)
			{
				throw new Exception($"Failed to get version info from {fileName}: {exc.Message}");
			}
		}
	}
}