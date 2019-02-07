using System;
using System.Threading.Tasks;

namespace Delivery.Client
{
	public class InstallManager
	{
		public InstallManager(string storageAccount, string containerName, string productName)
		{
			StorageAccount = storageAccount;
			ContainerName = containerName;
			ProductName = productName;
		}

		public string StorageAccount { get; }
		public string ContainerName { get; }
		public string ProductName { get; set; }

		public async Task<bool> IsNewVersionAvailable()
		{
			throw new NotImplementedException();
		}
	}
}