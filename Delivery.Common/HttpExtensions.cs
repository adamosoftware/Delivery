using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Delivery.Common
{	
	public static class HttpExtensions
	{
		/// <summary>
		/// Downloads a file from HttpContent
		/// thanks to https://blogs.msdn.microsoft.com/henrikn/2012/02/17/httpclient-downloading-to-a-local-file/
		/// </summary>		
		public static Task DownloadFileAsync(this HttpContent content, string localFile, bool overwrite)
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