using Delivery.Common;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;

namespace Delivery.Client
{
	public class InstallManager
	{		
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

		public async Task<CloudProductVersionInfo> GetCloudVersionInfoAsync()
		{
			return await DownloadInnerAsync(
				Util.GetProductInfoUrl(StorageAccount, ContainerName, ProductName),
				async (r) =>
				{
					string json = await r.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<CloudProductVersionInfo>(json);
				});
		}

		public async Task<bool> IsNewVersionAvailableAsync()
		{
			var cloudInfo = await GetCloudVersionInfoAsync();
			return (cloudInfo.GetVersion() > LocalVersion);			
		}

		public async Task DownloadAsync()
		{
			
			using (var client = new HttpClient())
			{
				var url = Util.GetBlobUrl(StorageAccount, ContainerName, InstallerExeName);
				
			}
		}

		private async Task<T> DownloadInnerAsync<T>(string url, Func<HttpResponseMessage, Task<T>> getResult)
		{
			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(url);
				response.EnsureSuccessStatusCode();
				return await getResult.Invoke(response);
			}
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