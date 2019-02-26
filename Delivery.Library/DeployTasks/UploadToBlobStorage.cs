using Delivery.Common;
using Delivery.Library.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Library.DeployTasks
{
	public class UploadToBlobStorage : IDeployTask
	{
		private static HttpClient _client = new HttpClient();

		public UploadToBlobStorage()
		{			
		}

		public string StatusMessage => $"Uploading {OutputUri} to {AccountName}";

		/// <summary>
		/// Small json file is uploaded to storage with this version number to tell clients what version of the app is available
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// Azure blob storage account name
		/// </summary>
		public string AccountName { get; set; }

		/// <summary>
		/// Azure blob storage account key
		/// </summary>
		public string AccountKey { get; set; }

		/// <summary>
		/// Container to upload to
		/// </summary>
		public string ContainerName { get; set; }

		public string InputUri { get; set; }		

		public string OutputUri
		{
			get { return BlobUtil.GetBlobUrl(AccountName, ContainerName, InputUri); }

			set => throw new NotImplementedException();
		}

		public string CredentialSource { get; set; }

		public bool HasDeployedVersionInfo => true;

		public void Authenticate(Dictionary<string, string> credentials)
		{
			AccountName = credentials["AccountName"];
			AccountKey = credentials["AccountKey"];
		}

		public async Task ExecuteAsync()
		{
			var account = new CloudStorageAccount(new StorageCredentials(AccountName, AccountKey), true);
			var client = account.CreateCloudBlobClient();
			var container = client.GetContainerReference(ContainerName);
			await container.CreateIfNotExistsAsync();

			string blobName = Path.GetFileName(InputUri);
			var blob = container.GetBlockBlobReference(blobName);
			// blob.Properties.ContentType = "application/exe"; do we need to set this explicitly?
			await blob.UploadFromFileAsync(InputUri);

			string checksum = GetFileHash(InputUri);
			long length = new FileInfo(InputUri).Length;
			
			string infoName = BlobUtil.GetProductInfoBlobName(InputUri);
			var infoBlob = container.GetBlockBlobReference(infoName);
			infoBlob.Properties.ContentType = "text/json";			
			var info = new CloudVersionInfo()
			{
				Version = Version,
				Checksum = checksum,
				Length = length
			};
			string json = JsonConvert.SerializeObject(info);
			await infoBlob.UploadTextAsync(json);
		}

		public async Task<Version> GetDeployedVersionAsync()
		{
			var info = await CloudVersionInfo.GetAsync(_client, BlobUtil.GetProductInfoUrl(AccountName, ContainerName, InputUri));
			return new Version(info.Version);
		}

		private string GetFileHash(string fileName)
		{
			// thanks to https://stackoverflow.com/a/10520086/2023653 and https://stackoverflow.com/a/34667459/2023653

			using (var md5 = MD5.Create())
			{
				using (var stream = File.OpenRead(fileName))
				{
					var hash = md5.ComputeHash(stream);
					return Encoding.Default.GetString(hash);
				}
			}
		}

		public override string ToString()
		{
			return $"{AccountName}.{ContainerName}";
		}
	}
}