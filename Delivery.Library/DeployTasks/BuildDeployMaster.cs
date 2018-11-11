using System.IO;
using System.Text;

namespace Delivery.Library.DeployTasks
{
	/// <summary>
	/// Deploy task for Just Great Software's DeployMaster product
	/// https://www.deploymaster.com/index.html
	/// https://www.deploymaster.com/manual.html#commandline
	/// To use with this library, you must use token %version% in the Version field within your deploy script
	/// </summary>
	public class BuildDeployMaster : ExeProcess
	{
		private string _tempFile = null;

		public BuildDeployMaster()
		{
			BuildSuccessCode = 0;
			StatusMessage = "Building DeployMaster installer...";
		}

		/// <summary>
		/// Filename that contains the installer script, for example
		/// "C:\Users\Adam\Source\Repos\SchemaSync.WinForms\installer.deploy"
		/// </summary>
		public string SourceFile { get; set; }

		public new string Arguments
		{
			get { return SourceFile + " /b /q"; }
			set { Arguments = value; }
		}

		protected override void OnBeforeRun()
		{
			base.OnBeforeRun();

			var lines = File.ReadAllLines(SourceFile);
			string versionedContent = ApplyVersion(lines, Version);

			_tempFile = Path.Combine(Path.GetDirectoryName(SourceFile), Path.GetFileNameWithoutExtension(SourceFile) + ".tmp");
			File.WriteAllText(_tempFile, versionedContent);
			SourceFile = _tempFile;
		}

		private string ApplyVersion(string[] lines, string version)
		{
			const string VersionToken = "%version%";

			StringBuilder result = new StringBuilder();
			foreach (string line in lines)
			{
				string newline = line.Replace(VersionToken, version);
				result.AppendLine(newline);
			}
			return result.ToString();
		}

		protected override void OnAfterRun()
		{
			base.OnAfterRun();
			File.Delete(_tempFile);
		}
	}
}