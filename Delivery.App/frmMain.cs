using Delivery.Common;
using Delivery.Library.Classes;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows.Forms;
using WinForms.Library;

namespace Delivery.App
{
	public partial class frmMain : Form
	{
		private DocumentManager<DeployScript> _docManager = null;
		private static HttpClient _client = new HttpClient();

		public frmMain()
		{
			InitializeComponent();
		}

		private async void btnOpen_Click(object sender, EventArgs e)
		{
			await _docManager.PromptOpenAsync();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			_docManager = new DocumentManager<DeployScript>("delivery.json", "Delivery Settings|*.delivery.json", "Click OK to save changes.");
			_docManager.UpdateSerializerSettingsOnSave = OnSave;
			_docManager.FileOpened += ScriptOpened;
					
		}

		private async void ScriptOpened(object sender, EventArgs e)
		{
			Text = $"Delivery - {_docManager.Filename}";

			tbLocalVersionSource.Text = _docManager.Document.LocalVersionFile;
			lblLocalVersion.Text = (VersionUtil.TryGetProductVersion(_docManager.Document.LocalVersionFile, out string version)) ? version : "(unknown)";

			tbDeployedVersionInfoUrl.Text = _docManager.Document.DeployedVersionInfoUrl;
			if (!string.IsNullOrEmpty(tbDeployedVersionInfoUrl.Text))
			{
				lblDeployedVersion.Text = (await CloudVersionInfo.GetAsync(_client, _docManager.Document.DeployedVersionInfoUrl)).Version;
			}			
		}

		private void OnSave(JsonSerializerSettings obj)
		{
			obj.TypeNameHandling = TypeNameHandling.Objects;
		}

		private async void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			await _docManager?.FormClosingAsync(e);
		}
	}
}