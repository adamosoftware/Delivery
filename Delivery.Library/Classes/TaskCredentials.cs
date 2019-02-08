using JsonSettings;
using System.Security.Cryptography;

namespace Delivery.Library.Classes
{
	public class TaskCredentials
	{
		[JsonProtect(DataProtectionScope.LocalMachine)]
		public string SecureString { get; set; }
	}
}