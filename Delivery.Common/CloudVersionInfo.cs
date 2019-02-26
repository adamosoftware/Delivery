using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Delivery.Common
{
	public class CloudVersionInfo
	{
		public string Version { get; set; }
		public DateTime Timestamp { get; set; } = DateTime.UtcNow;
		public string Checksum { get; set; }
		public long Length { get; set; }
		public string ReadmeUrl { get; set; }

		public Version GetVersion()
		{
			return new Version(Version);
		}

		public static async Task<CloudVersionInfo> GetAsync(HttpClient client, string url)
		{
			return await client.GetAsync(url,
				async (r) =>
				{
					string json = await r.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<CloudVersionInfo>(json);
				});
		}
	}
}