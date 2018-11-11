using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Delivery.Library
{
	public class Installer
	{
		public const string SourceFileMacro = "%sourceFile%";

		/// <summary>
		/// Command line to execute to build installer output. In my case it would be:
		/// "C:\Program Files\Just Great Software\DeployMaster\DeployMaster.exe" "
		/// </summary>
		public string BuildExe { get; set; }

		/// <summary>
		/// Filename that contains the installer content, for example
		/// "C:\Users\Adam\Source\Repos\SchemaSync.WinForms\installer.deploy"
		/// </summary>
		public string SourceFile { get; set; }

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

		protected virtual bool InsertVersion { get; }

		public void Build(string version)
		{			
			if (InsertVersion)
			{				
				var lines = File.ReadAllLines(SourceFile);
				string updatedContent = ApplyVersion(lines, version);
				
				string tempFile = Path.GetTempFileName();
				File.WriteAllText(tempFile, updatedContent);
				SourceFile = tempFile;				
			}

			ProcessStartInfo psi = new ProcessStartInfo(BuildExe);
			psi.Arguments = Arguments;
			var process = Process.Start(psi);
			process.WaitForExit();

			if (BuildSuccessCode.HasValue)
			{
				int code = process.ExitCode;
				if (code != BuildSuccessCode.Value)
				{
					throw new Exception($"Process {BuildExe} with arguments {psi.Arguments} failed with code {code}.");
				}
			}
		}

		protected virtual string ResolveArgs(string arguments)
		{
			string result = arguments;
			result = result.Replace(SourceFileMacro, SourceFile);
			return result;
		}		

		/// <summary>
		/// Override this to inject the version number into the SourceDocument
		/// </summary>
		protected virtual string ApplyVersion(string[] lines, string version)
		{
			return string.Join("\r\n", lines);
		}		
	}
}