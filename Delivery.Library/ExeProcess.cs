using Delivery.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Delivery.Library
{
	public class ExeProcess : IBuildTask
	{
		/// <summary>
		/// Command line to execute to build installer output. In my case it would be:
		/// "C:\Program Files\Just Great Software\DeployMaster\DeployMaster.exe" "
		/// </summary>
		public string ExeFile { get; set; }

		/// <summary>
		/// Arguments to pass to the BuildExe. Reference the SourceDocument with "%source%" <see cref="SourceFileMacro"/>
		/// For example in my case it would be "%source%" /b
		/// </summary>
		public string Arguments { get; set; }

		/// <summary>
		/// Output of installer build, and the file that's uploaded to the download location available to users
		/// </summary>
		public string OutputFile { get; set; }

		public int? BuildSuccessCode { get; set; }

		public string Version { get; set; }

		public void Run()
		{
			OnBeforeBuild();

			ProcessStartInfo psi = new ProcessStartInfo(ExeFile);
			psi.Arguments = Arguments;
			var process = Process.Start(psi);
			process.WaitForExit();

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
		protected virtual void OnBeforeBuild()
		{
			// do nothing by default
		}
	}
}