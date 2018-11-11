using Delivery.Library.Interfaces;
using RestSharp;
using System;

namespace Delivery.Library
{
	public class ApiCall : IDeployTask
	{
		protected RestClient _client = null;
		protected IRestResponse _response = null;

		public Method Method { get; set; }

		public string Version { get; set; }		

		public string BaseUrl { get; set; }

		/// <summary>
		/// API endpoint
		/// </summary>
		public string Resource { get; set; }

		public void Run()
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