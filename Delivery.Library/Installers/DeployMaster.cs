using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delivery.Library.Installers
{
	public class DeployMaster : Installer
	{
		protected override bool InjectVersion => true;

		public DeployMaster()
		{
			BuildSuccessCode = 0;
		}

		protected override string ResolveArgs(string arguments)
		{
			string result = base.ResolveArgs(arguments);

			arguments += " /b /q";

			return result;
		}

		protected override string OnInjectVersion(string installerContent, string version)
		{
			return base.OnInjectVersion(installerContent, version);
		}
	}
}
