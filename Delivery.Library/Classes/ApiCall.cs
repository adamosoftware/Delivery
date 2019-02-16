using Delivery.Library.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Delivery.Library.Classes
{
	public class ApiCall : IDeployTask
	{
		protected RestClient _client = null;
		protected IRestResponse _response = null;

		public Method Method { get; set; }

		public string Version { get; set; }		

		public string BaseUrl { get; set; }

		public string StatusMessage { get; set; }

		public string ProductName { get; set; }

		/// <summary>
		/// API endpoint
		/// </summary>
		public string Resource { get; set; }

		public string InputUri { get; set; }

		public string OutputUri { get; set; }

		public string CredentialSource => throw new NotImplementedException();

		public bool HasDeployedVersion => false;

		public void Authenticate(Dictionary<string, string> credentials)
		{
			throw new NotImplementedException();
		}

		public async Task ExecuteAsync()
		{
			_client = new RestClient(BaseUrl);
			var request = new RestRequest(Resource, Method);
			await RunInnerAsync(request);

		}

		public Task<Version> GetDeployedVersionAsync()
		{
			throw new NotImplementedException();
		}

		protected virtual async Task RunInnerAsync(RestRequest request)
		{
			_response = await _client.ExecuteGetTaskAsync(request);
		}
	}
}