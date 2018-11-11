using Delivery.Library.DeployTasks;
using Delivery.Library.Interfaces;
using JsonSettings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Delivery.Library.Classes
{
	/// <summary>
	/// Describes the settings required for deploying a solution
	/// </summary>
	public class DeployManager
	{
		/// <summary>
		/// File in the solution that defines the version, usually the main build output, for example
		/// "C:\Users\Adam\Source\Repos\SchemaSync.WinForms\WinFormsApp\bin\Release\WinFormsApp.exe"
		/// </summary>
		public string VersionReferenceFile { get; set; }

		/// <summary>
		/// Will likely refactor this into a more formal validation interface
		/// </summary>
		[Category("Testing")]
		public bool RequirePassingTests { get; set; }

		[Category("Testing")]
		public string TestProject { get; set; }

		/// <summary>
		/// Tasks in this deployment, executed in order as set in the array
		/// </summary>
		public IDeployTask[] Tasks { get; set; }

		public void Execute()
		{
			var versionInfo = FileVersionInfo.GetVersionInfo(VersionReferenceFile);
			string version = $"{versionInfo.ProductMajorPart}.{versionInfo.ProductMinorPart}.{versionInfo.ProductBuildPart}";
			Console.WriteLine($"Version {version}");

			foreach (var t in Tasks)
			{
				Console.WriteLine(t.StatusMessage);
				t.Version = version;
				if (!string.IsNullOrEmpty(t.CredentialSource)) AuthenticateTask(t, t.CredentialSource);				
				t.Execute();
			}
		}

		private static void AuthenticateTask(IDeployTask task, string credentialFile)
		{
			var creds = JsonFile.Load<TaskCredentials>(credentialFile);
			var dictionary = ParseCredentials(creds);
			task.Authenticate(dictionary);
		}

		private static Dictionary<string, string> ParseCredentials(TaskCredentials credentials)
		{
			return credentials.SecureString.Split(';').Select(s =>
			{
				string[] parts = s.Split(':');
				return new { Name = parts[0].Trim(), Value = parts[1].Trim() };
			}).ToDictionary(item => item.Name, item => item.Value);			
		}

		/// <summary>
		/// Not sure this will be needed
		/// </summary>
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