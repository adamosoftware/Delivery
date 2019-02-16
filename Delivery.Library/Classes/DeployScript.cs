using Delivery.Library.Interfaces;
using JsonSettings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Delivery.Library.Classes
{
	/// <summary>
	/// Describes the settings required for deploying a solution
	/// </summary>
	public class DeployScript
	{
		/// <summary>
		/// File in the solution that defines the version, usually the main build output, for example
		/// "C:\Users\Adam\Source\Repos\SchemaSync.WinForms\WinFormsApp\bin\Release\WinFormsApp.exe"
		/// </summary>
		public string VersionReferenceFile { get; set; }

		/// <summary>
		/// Will likely refactor this into a more formal validation interface
		/// </summary>
		[Category("Testing")]
		public bool RequirePassingTests { get; set; }

		[Category("Testing")]
		public string TestProject { get; set; }

		/// <summary>
		/// Tasks in this deployment, executed in order as set in the array
		/// </summary>
		[JsonConverter(typeof(DeployTaskArrayConverter))]
		public IDeployTask[] Tasks { get; set; }

		public async Task ExecuteAsync()
		{
			var versionInfo = FileVersionInfo.GetVersionInfo(VersionReferenceFile);
			string version = versionInfo.ProductVersion.ToString();
			Console.WriteLine($"Version {version}");

			foreach (var t in Tasks)
			{
				Console.WriteLine(t.StatusMessage);
				t.Version = version;
				if (!string.IsNullOrEmpty(t.CredentialSource)) AuthenticateTask(t, t.CredentialSource);
				await t.ExecuteAsync();
			}
		}

		private static void AuthenticateTask(IDeployTask task, string credentialFile)
		{
			credentialFile = OneDrive.ResolvePath(credentialFile);

			var creds = JsonFile.Load<TaskCredentials>(credentialFile);
			var dictionary = ParseCredentials(creds);
			task.Authenticate(dictionary);
		}

		private static Dictionary<string, string> ParseCredentials(TaskCredentials credentials)
		{
			string content = credentials.SecureString;
			return content.Split(';').Select(s =>
			{
				string[] parts = s.Split(':');
				return new { Name = parts[0].Trim(), Value = parts[1].Trim() };
			}).ToDictionary(item => item.Name, item => item.Value);
		}
	}

	public class DeployTaskArrayConverter : JsonConverter
	{
		// this answer was helpful: https://stackoverflow.com/a/38708827/2023653

		public override bool CanConvert(Type objectType) => true;		

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			List<IDeployTask> results = new List<IDeployTask>();

			JToken token = JToken.Load(reader);
			foreach (var child in token.Children())
			{		
				string typeName = child["$type"].ToString();
				Type type = Type.GetType(typeName);
				var item = child.ToObject(type);
				results.Add(item as IDeployTask);
			}

			return results.ToArray();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}



}