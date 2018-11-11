﻿using Delivery.Library;
using Delivery.Library.Classes;
using Delivery.Library.DeployTasks;
using Delivery.Library.Interfaces;
using JsonSettings;

namespace Sample
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			//var creds = JsonFile.Load<TaskCredentials>(@"C:\Users\Adam\SkyDrive\Documents\AOSoftwareBlobStorage.json");

			DeployManager dm = GetSqlModelMergeDeployment();
			dm.Execute();
		}

		private static DeployManager GetSqlModelMergeDeployment()
		{
			return new DeployManager()
			{
				VersionReferenceFile = @"C:\Users\Adam\Source\Repos\SchemaSync.WinForms\WinFormsApp\bin\Release\WinFormsApp.exe",
				Tasks = new IDeployTask[]
				{
					new BuildDeployMaster()
					{
						ExeFile = @"C:\Program Files\Just Great Software\DeployMaster\DeployMaster.exe",
						InputUri = @"C:\Users\Adam\Source\Repos\SchemaSync.WinForms\installer.deploy"
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
						InputUri = @"C:\Users\Adam\Source\Repos\SchemaSync.WinForms\SqlModelMergeSetup.exe",
						CredentialSource = @"C:\Users\Adam\SkyDrive\Documents\AOSoftwareBlobStorage.json",
						ContainerName = "install"
					}
				}
			};
		}
	}
}