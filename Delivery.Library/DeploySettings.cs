﻿using Delivery.Library.DeployTasks;
using Delivery.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Delivery.Library
{
	/// <summary>
	/// Describes the settings required for deploying a solution
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

		/// <summary>
		/// Tasks in this deployment, executed in order as set in the array
		/// </summary>
		public IDeployTask[] Tasks { get; set; }

		public static Dictionary<string, Type> InstallerTypes
		{
			get
			{
				return new Dictionary<string, Type>()
				{
					{ "DeployMaster", typeof(BuildDeployMaster) }
				};
			}
		}
	}
}