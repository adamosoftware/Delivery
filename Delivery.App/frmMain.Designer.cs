namespace Delivery.App
{
	partial class frmMain
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnOpen = new System.Windows.Forms.ToolStripButton();
			this.btnExecute = new System.Windows.Forms.ToolStripButton();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblLocalVersion = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tbLocalVersionSource = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lblDeployedVersion = new System.Windows.Forms.Label();
			this.tbDeployedVersionInfoUrl = new System.Windows.Forms.TextBox();
			this.toolStrip1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpen,
            this.btnExecute});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(703, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// btnOpen
			// 
			this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnOpen.Image = ((System.Drawing.Image)(resources.GetObject("btnOpen.Image")));
			this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(49, 22);
			this.btnOpen.Text = "Open...";
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// btnExecute
			// 
			this.btnExecute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnExecute.Image = ((System.Drawing.Image)(resources.GetObject("btnExecute.Image")));
			this.btnExecute.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnExecute.Name = "btnExecute";
			this.btnExecute.Size = new System.Drawing.Size(51, 22);
			this.btnExecute.Text = "Execute";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(63, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(85, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Local Source:";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tbDeployedVersionInfoUrl);
			this.panel1.Controls.Add(this.lblDeployedVersion);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.lblLocalVersion);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.tbLocalVersionSource);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 25);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(703, 129);
			this.panel1.TabIndex = 2;
			// 
			// lblLocalVersion
			// 
			this.lblLocalVersion.AutoSize = true;
			this.lblLocalVersion.Location = new System.Drawing.Point(596, 35);
			this.lblLocalVersion.Name = "lblLocalVersion";
			this.lblLocalVersion.Size = new System.Drawing.Size(41, 13);
			this.lblLocalVersion.TabIndex = 4;
			this.lblLocalVersion.Text = "label3";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(596, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Version:";
			// 
			// tbLocalVersionSource
			// 
			this.tbLocalVersionSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tbLocalVersionSource.Location = new System.Drawing.Point(154, 32);
			this.tbLocalVersionSource.Name = "tbLocalVersionSource";
			this.tbLocalVersionSource.Size = new System.Drawing.Size(436, 21);
			this.tbLocalVersionSource.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(36, 62);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Deployed Version:";
			// 
			// lblDeployedVersion
			// 
			this.lblDeployedVersion.AutoSize = true;
			this.lblDeployedVersion.Location = new System.Drawing.Point(596, 62);
			this.lblDeployedVersion.Name = "lblDeployedVersion";
			this.lblDeployedVersion.Size = new System.Drawing.Size(41, 13);
			this.lblDeployedVersion.TabIndex = 7;
			this.lblDeployedVersion.Text = "label4";
			// 
			// tbDeployedVersionInfoUrl
			// 
			this.tbDeployedVersionInfoUrl.Location = new System.Drawing.Point(154, 59);
			this.tbDeployedVersionInfoUrl.Name = "tbDeployedVersionInfoUrl";
			this.tbDeployedVersionInfoUrl.Size = new System.Drawing.Size(436, 21);
			this.tbDeployedVersionInfoUrl.TabIndex = 8;
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(703, 286);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.toolStrip1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Delivery";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnOpen;
		private System.Windows.Forms.ToolStripButton btnExecute;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox tbLocalVersionSource;
		private System.Windows.Forms.Label lblLocalVersion;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblDeployedVersion;
		private System.Windows.Forms.TextBox tbDeployedVersionInfoUrl;
	}
}

