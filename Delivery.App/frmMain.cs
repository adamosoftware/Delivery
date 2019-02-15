using Delivery.Library.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Delivery.App
{
	public partial class frmMain : Form
	{
		private DeployManager _deployManager = null;

		public frmMain()
		{
			InitializeComponent();
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			try
			{
				OpenFileDialog dlg = new OpenFileDialog();
				dlg.Filter = "Json Files|*.json|All Files|*.*";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					_deployManager = DeployManager.Load(dlg.FileName);
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}
	}
}
