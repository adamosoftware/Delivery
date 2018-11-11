using Delivery.Library.Installers;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Delivery.Library
{
	/// <summary>
	/// Describes the settings required for publishing a solution
	/// </summary>
	public class PublishSettings
	{
		/// <summary>
		/// File in the solution that defines the version, for example
		/// "C:\Users\Adam\Source\Repos\SchemaSync.WinForms\WinFormsApp\bin\Release\WinFormsApp.exe"
		/// </summary>
		public string AppVersionFile { get; set; }

		[Category("Testing")]
		public bool RequirePassingTests { get; set; }

		[Category("Testing")]
		public string TestProject { get; set; }

		public string InstallerType { get; set; }

		public Installer Installer { get; set; }		

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