using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Library.Interfaces
{
	public interface IDeployTask
	{
		/// <summary>
		/// Version of the application as defined by the version of <see cref="DeploySettings.VersionReferenceFile"/>
		/// </summary>
		string Version { get; }

		/// <summary>
		/// Executes the deployment task
		/// </summary>
		void Run();
	}
}
