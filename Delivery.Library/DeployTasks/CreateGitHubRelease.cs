using Delivery.Library.Classes;
using Newtonsoft.Json;
using RestSharp;
using System.Threading.Tasks;

namespace Delivery.Library.DeployTasks
{
	/// <summary>
	/// Creates a GitHub release for a given repository as part of a deployment
	/// https://developer.github.com/v3/repos/releases/#create-a-release
	/// </summary>
	public class CreateGitHubRelease : ApiCall
	{
		private string _owner;
		private string _repo;

		public CreateGitHubRelease()
		{
			BaseUrl = "https://api.github.com";			
			Method = Method.POST;
			StatusMessage = "Creating GitHub release...";
		}

		public string Owner
		{
			get { return _owner; }
			set
			{
				_owner = value;
				UpdateResource();
			}
		}

		public string Repository
		{
			get { return _repo; }
			set
			{
				_repo = value;
				UpdateResource();
			}
		}

		private void UpdateResource()
		{
			Resource = $"/repos/{_owner}/{_repo}/releases";
		}

		protected override async Task RunInnerAsync(RestRequest request)
		{
			var post = new { tag_name = "v" + Version };
			request.AddJsonBody(post);

			await base.RunInnerAsync(request);

			// look at _response to determine success			

			// attach installer binary to release
			// https://developer.github.com/v3/repos/releases/#upload-a-release-asset
		}
	}
}