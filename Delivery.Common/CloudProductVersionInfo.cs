using System;

namespace Delivery.Common
{
	public class CloudProductVersionInfo
	{
		public string Version { get; set; }
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
		public string Checksum { get; set; }
		public long Length { get; set; }

		public Version GetVersion()
		{
			return new Version(Version);
		}
	}
}