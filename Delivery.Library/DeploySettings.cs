using Delivery.Library.Installers;
using Delivery.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Delivery.Library
{
	/// <summary>
	/// Describes the settings required for publishing a solution
	/// </summary>
	public class DeploySettings
	{
		/// <summary>
		/// File in the solution that defines the version, for example
		/// "C:\Users\Adam\Source\Repos\SchemaSync.WinForms\WinFormsApp\bin\Release\WinFormsApp.exe"
		/// </summary>
		public string VersionReferenceFile { get; set; }

		[Category("Testing")]
		public bool RequirePassingTests { get; set; }

		[Category("Testing")]
		public string TestProject { get; set; }

		public IDeployTask[] Tasks { get; set; }

		public static Dictionary<string, Type> InstallerTypes
		{
			get
			{
				return new Dictionary<string, Type>()
				{
					{ "DeployMaster", typeof(DeployMaster) }
				};
			}
		}
	}
}