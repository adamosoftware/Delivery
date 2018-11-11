using System.Collections.Generic;

namespace Delivery.Library.Interfaces
{
	public interface IDeployTask
	{
		/// <summary>
		/// Version of the application as defined by the version of <see cref="DeployManager.VersionReferenceFile"/>
		/// </summary>
		string Version { get; set; }

		/// <summary>
		/// Executes the deployment task. Failures should throw exceptions
		/// </summary>
		void Execute();

		/// <summary>
		/// File that acts as input to the task
		/// </summary>
		string InputUri { get; set; }

		/// <summary>
		/// Output of the task (usually handed off to next task)
		/// </summary>
		string OutputUri { get; set; }

		/// <summary>
		/// Message to display in UI while task is executing
		/// </summary>
		string StatusMessage { get; }

		/// <summary>
		/// Local file with credentials for this task (use AoJsonSettings DPAPI protected properties, for example)
		/// </summary>
		string CredentialSource { get; }

		/// <summary>
		/// Applies the credentials from CredentialSource to the task
		/// </summary>
		void Authenticate(Dictionary<string, string> credentials);
	}
}