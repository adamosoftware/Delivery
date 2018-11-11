using Delivery.Library.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;

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

		/// <summary>
		/// API endpoint
		/// </summary>
		public string Resource { get; set; }

		public string InputUri { get; set; }

		public string OutputUri { get; set; }

		public string CredentialSource => throw new NotImplementedException();

		public void Authenticate(Dictionary<string, string> credentials)
		{
			throw new NotImplementedException();
		}

		public void Execute()
		{
			_client = new RestClient(BaseUrl);
			var request = new RestRequest(Resource, Method);
			RunInner(request);
		}

		protected virtual void RunInner(RestRequest request)
		{
			_response = _client.Execute(request);
		}
	}
}