using System.IO;
using System.Text;

namespace Delivery.Library.Installers
{
	public class DeployMaster : ExeProcess
	{
		public DeployMaster()
		{
			BuildSuccessCode = 0;
		}

		/// <summary>
		/// Filename that contains the installer content, for example
		/// "C:\Users\Adam\Source\Repos\SchemaSync.WinForms\installer.deploy"
		/// </summary>
		public string SourceFile { get; set; }

		public new string Arguments
		{
			get { return SourceFile + " /b /q"; }
			set { Arguments = value; }
		}

		protected override void OnBeforeBuild()
		{
			base.OnBeforeBuild();

			var lines = File.ReadAllLines(SourceFile);
			string versionedContent = ApplyVersion(lines, Version);

			string tempFile = Path.GetTempFileName();
			File.WriteAllText(tempFile, versionedContent);
			SourceFile = tempFile;
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
	}
}