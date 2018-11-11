using Delivery.Library.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Delivery.Library.DeployTasks
{
	public class UploadToBlobStorage : IDeployTask
	{
		public UploadToBlobStorage()
		{
			StatusMessage = "Uploading to blob storage...";
		}

		public string StatusMessage { get; set; }

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

		/// <summary>
		/// File to upload, usually the installer binary
		/// </summary>
		public string Filename { get; set; }

		public void Run()
		{
			var account = new CloudStorageAccount(new StorageCredentials(AccountName, AccountKey), true);
			var client = account.CreateCloudBlobClient();
			var container = client.GetContainerReference(ContainerName);
			container.CreateIfNotExists();

			string blobName = Path.GetFileName(Filename);
			var blob = container.GetBlockBlobReference(blobName);
			// blob.Properties.ContentType = "application/exe"; do we need to set this explicitly?
			blob.UploadFromFile(Filename);

			string infoBlobName = Path.GetFileNameWithoutExtension(Filename) + ".info";
			var infoBlob = container.GetBlockBlobReference(infoBlobName);
			var info = new { version = Version };
			string json = JsonConvert.SerializeObject(info);
			infoBlob.UploadText(json);
		}
	}
}