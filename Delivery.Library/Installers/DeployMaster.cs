using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Library.Installers
{
	public class DeployMaster : Installer
	{
		private string[] _lines;

		private const int _versionLine = 6; // line number in script where the main version number is

		public DeployMaster()
		{
			BuildSuccessCode = 0;
		}

		protected override bool InsertVersion => true;

		protected override string ApplyVersion(string installerContent, string version)
		{
			Arguments = $"\"{SourceFile}\" /b /q";

			throw new NotImplementedException();
		}
	}
}
