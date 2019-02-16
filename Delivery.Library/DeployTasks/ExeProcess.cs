using Delivery.Library.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Delivery.Library.Classes
{
	public class ExeProcess : IDeployTask
	{
		/// <summary>
		/// Command line to execute to build installer output. In my case it would be:
		/// "C:\Program Files\Just Great Software\DeployMaster\DeployMaster.exe"
		/// </summary>
		public string ExeFile { get; set; }

		/// <summary>
		/// Arguments to pass to the ExeFile		
		/// </summary>
		public string Arguments { get; set; }

		/// <summary>
		/// Output of installer build, and the file that's uploaded to the download location available to users
		/// </summary>
		public string OutputUri { get; set; }

		public int? BuildSuccessCode { get; set; }

		public string Version { get; set; }

		public string CredentialSource { get; set; }

		public string InputUri { get; set; }

		public string StatusMessage => $"Executing {ExeFile}\r\n{Arguments}";

		public bool HasDeployedVersion => false;

		public async Task ExecuteAsync()
		{
			OnBeforeRun();

			ProcessStartInfo psi = new ProcessStartInfo(ExeFile);
			psi.Arguments = Arguments.Replace("{version}", Version);
			var process = Process.Start(psi);
			process.WaitForExit();

			OnAfterRun();

			if (BuildSuccessCode.HasValue)
			{
				int code = process.ExitCode;
				if (code != BuildSuccessCode.Value)
				{
					throw new Exception($"Process {ExeFile} with arguments {psi.Arguments} failed with code {code}.");
				}
			}

			await Task.CompletedTask;
		}

		/// <summary>
		/// Override this to make any dynamic changes before running process
		/// </summary>
		protected virtual void OnBeforeRun()
		{
			// do nothing by default
		}

		/// <summary>
		/// Override this to cleanup any temp resources that were created as a result of this task
		/// </summary>
		protected virtual void OnAfterRun()
		{
			// do nothing by default
		}

		public void Authenticate(Dictionary<string, string> credentials)
		{
			throw new NotImplementedException();
		}

		public Task<Version> GetDeployedVersionAsync()
		{
			throw new NotImplementedException();
		}
	}
}