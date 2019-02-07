using System.IO;

namespace Delivery.Common
{
	public static class BlobUtil
	{
		public static string GetBlobUrl(string accountName, string containerName, string fileName)
		{
			string baseName = Path.GetFileName(fileName);
			return $"https://{accountName}.blob.core.windows.net/{containerName}/{baseName}";
		}

		public static string GetProductInfoBlobName(string fileName)
		{
			return Path.GetFileNameWithoutExtension(fileName) + ".info";
		}

		public static string GetProductInfoUrl(string accountName, string containerName, string fileName)
		{			
			return GetBlobUrl(accountName, containerName, GetProductInfoBlobName(fileName));
		}		
	}
}