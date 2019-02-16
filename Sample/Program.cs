using Delivery.Library.Classes;
using Delivery.Library.DeployTasks;
using Delivery.Library.Interfaces;
using DevSecrets.Library;
using System;

namespace Sample
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			try
			{
				DeployScript dm = GetSqlModelMergeDeployment();
				//dm.Save(@"c:\users\adam\desktop\SMM.delivery.json");
				//dm.ExecuteAsync().Wait();
			}
			catch (Exception exc)
			{
				Console.WriteLine(exc.Message);
				Console.ReadLine();
			}
		}

		private static DeployScript GetSqlModelMergeDeployment()
		{
			var secrets = DevSecretsDictionary.Load("Delivery.sln");

			return new DeployScript()
			{
				VersionReferenceFile = @"C:\Users\Adam\Source\Repos\SchemaSync.WinForms\App.PS\bin\Release\App.PS.exe",
				Tasks = new IDeployTask[]
				{
					new ExeProcess()
					{
						ExeFile = @"C:\Program Files\Just Great Software\DeployMaster\DeployMaster.exe",
						Arguments = @"C:\Users\Adam\Source\Repos\SchemaSync.WinForms\installerPS.deploy /ver {version} /b /q"
					}/*,
					new CreateGitHubRelease()
					{
						Owner = "adamosoftware",
						Repository = "SchemaSync.WinForms"
					},
					new CreateGitHubRelease()
					{
						Owner = "adamosoftware",
						Repository = "SchemaSync"
					}*/,
					new UploadToBlobStorage()
					{						
						InputUri = @"C:\Users\Adam\Source\Repos\SchemaSync.WinForms\SqlModelMergePS.exe",
						AccountName = secrets.Contents["name"],
						AccountKey = secrets.Contents["key"],
						ContainerName = "install"
					}
				}
			};
		}
	}
}