using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Delivery.Common
{	
	public static class HttpExtensions
	{
		public static async Task<T> GetAsync<T>(this HttpClient client, string url, Func<HttpResponseMessage, Task<T>> getResult)
		{
			// help from https://blogs.msdn.microsoft.com/henrikn/2012/02/17/httpclient-downloading-to-a-local-file/
			var response = await client.GetAsync(url);
			response.EnsureSuccessStatusCode();
			await response.Content.LoadIntoBufferAsync();
			return await getResult.Invoke(response);
		}

		public static async Task<string> DownloadFileAsync(this HttpClient client, string url, string localFile, bool overwrite = false)
		{
			return await client.GetAsync(url,
				async (r) =>
				{
					await r.Content.DownloadFileContentAsync(localFile, overwrite);
					return localFile;
				});
		}

		/// <summary>
		/// Downloads a file from HttpContent
		/// thanks to https://blogs.msdn.microsoft.com/henrikn/2012/02/17/httpclient-downloading-to-a-local-file/
		/// </summary>		
		public static Task DownloadFileContentAsync(this HttpContent content, string localFile, bool overwrite)
		{			
			if (!overwrite && File.Exists(localFile))
			{
				throw new InvalidOperationException($"File {localFile} already exists.");
			}

			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(localFile, FileMode.Create, FileAccess.Write, FileShare.None);
				return content.CopyToAsync(fileStream).ContinueWith(
					(copyTask) =>
					{
						fileStream.Close();
					});
			}
			catch
			{
				if (fileStream != null) fileStream.Close();
				throw;
			}
		}
	}
}