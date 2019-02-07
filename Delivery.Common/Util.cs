using System.IO;

namespace Delivery.Common
{
	public static class Util
	{
		public static string GetBlobUrl(string accountName, string containerName, string fileName)
		{
			string baseName = Path.GetFileName(fileName);
			return $"https://{accountName}.blob.core.windows.net/{containerName}/{baseName}";
		}
	}
}