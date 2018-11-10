using System;
using System.Diagnostics;
using System.IO;

namespace Delivery.Library
{
	public class Installer
	{
		public const string SourceDocMacro = "%source%";

		/// <summary>
		/// Command line to execute to build installer output. In my case it would be:
		/// "C:\Program Files\Just Great Software\DeployMaster\DeployMaster.exe" "
		/// </summary>
		public string BuildExe { get; set; }

		/// <summary>
		/// Filename that contains the installer content, for example
		/// "C:\Users\Adam\Source\Repos\SchemaSync.WinForms\installer.deploy"
		/// </summary>
		public string SourceDocument { get; set; }

		/// <summary>
		/// Arguments to pass to the BuildExe. Reference the SourceDocument with "%source%" <see cref="SourceDocMacro"/>
		/// For example in my case it would be "%source%" /b
		/// </summary>
		public string Arguments { get; set; } = SourceDocMacro;

		/// <summary>
		/// Output of installer build, and the file that's uploaded to the download location available to users
		/// </summary>
		public string OutputFile { get; set; }

		public int? BuildSuccessCode { get; set; }

		public void Build(string version)
		{
			if (InjectVersion)
			{
				string updatedDoc = null;
				using (var reader = File.OpenText(SourceDocument))
				{
					string content = reader.ReadToEnd();
					updatedDoc = OnInjectVersion(content, version);					
				}

				File.WriteAllText(SourceDocument, updatedDoc);
			}

			ProcessStartInfo psi = new ProcessStartInfo(BuildExe);
			psi.Arguments = ResolveArgs(Arguments);
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
			result = result.Replace(SourceDocMacro, SourceDocument);
			return result;
		}

		/// <summary>
		/// Set this to true in derived classes to call <see cref="OnInjectVersion(string, string)"/> during builds
		/// </summary>
		protected virtual bool InjectVersion { get; }

		/// <summary>
		/// Override this to inject the version number into the SourceDocument
		/// </summary>
		protected virtual string OnInjectVersion(string installerContent, string version)
		{
			return installerContent;
		}
	}
}