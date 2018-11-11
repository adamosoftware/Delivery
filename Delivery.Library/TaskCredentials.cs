using JsonSettings;
using System.Security.Cryptography;

namespace Delivery.Library
{
	public class TaskCredentials
	{
		[JsonProtect(DataProtectionScope.CurrentUser)]
		public string SecureString { get; set; }
	}
}