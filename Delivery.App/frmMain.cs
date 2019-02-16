using Delivery.Library.Classes;
using Newtonsoft.Json;
using System;
using System.Windows.Forms;
using WinForms.Library;

namespace Delivery.App
{
	public partial class frmMain : Form
	{
		private DocumentManager<DeployScript> _docManager = null;

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

		private void ScriptOpened(object sender, EventArgs e)
		{
			Text = $"Delivery - {_docManager.Filename}";
		}

		private void OnSave(JsonSerializerSettings obj)
		{
			obj.TypeNameHandling = TypeNameHandling.Objects;
		}
	}
}