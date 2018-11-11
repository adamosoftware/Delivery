using Delivery.Library.Classes;
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
		
		protected override void OnBeforeRun()
		{
			base.OnBeforeRun();

			var lines = File.ReadAllLines(InputUri);
			string versionedContent = ApplyVersion(lines, Version);			

			_tempFile = Path.Combine(Path.GetDirectoryName(InputUri), Path.GetFileNameWithoutExtension(InputUri) + ".tmp");
			File.WriteAllText(_tempFile, versionedContent);
			InputUri = _tempFile;
			Arguments = _tempFile + " /b /q";
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