namespace Delivery.Library.Interfaces
{
	public interface IDeployTask
	{
		/// <summary>
		/// Version of the application as defined by the version of <see cref="DeploySettings.VersionReferenceFile"/>
		/// </summary>
		string Version { get; }

		/// <summary>
		/// Executes the deployment task. Failures should throw exceptions
		/// </summary>
		void Run();
	}
}