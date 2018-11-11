﻿using Delivery.Library.Interfaces;
using System;
using System.Diagnostics;

namespace Delivery.Library
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
		public string OutputFile { get; set; }

		public int? BuildSuccessCode { get; set; }

		public string Version { get; set; }

		public string StatusMessage { get; set; }

		public void Run()
		{
			OnBeforeRun();

			ProcessStartInfo psi = new ProcessStartInfo(ExeFile);
			psi.Arguments = Arguments;
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
	}
}