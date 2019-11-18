namespace WinOpenGL_ShaderToy
{
	partial class frmProgram
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
			ShaderToy_Components.controlProjectObject.clsDesigner clsDesigner1 = new ShaderToy_Components.controlProjectObject.clsDesigner();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panelMain = new ShaderToy_Components.controlProjectObject();
			this.datagridShaderLinks = new System.Windows.Forms.DataGridView();
			this.columnShader = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.splitterLinkStatus = new System.Windows.Forms.Splitter();
			this.panelLinkStatus = new System.Windows.Forms.Panel();
			this.dataLinkStatus = new System.Windows.Forms.DataGridView();
			this.panelLinkStatusHead = new System.Windows.Forms.Panel();
			this.chkLinkWarnings = new System.Windows.Forms.CheckBox();
			this.chkLinkErrors = new System.Windows.Forms.CheckBox();
			this.lblLinkStatus = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.gadgetLink = new System.Windows.Forms.Panel();
			this.btnLink = new System.Windows.Forms.Button();
			this.chkAutoLink = new System.Windows.Forms.CheckBox();
			this.panelMain.Content.SuspendLayout();
			this.panelMain.Status.SuspendLayout();
			this.panelMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.datagridShaderLinks)).BeginInit();
			this.panelLinkStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataLinkStatus)).BeginInit();
			this.panelLinkStatusHead.SuspendLayout();
			this.gadgetLink.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			// 
			// panelMain.Content
			// 
			this.panelMain.Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Content.Controls.Add(this.datagridShaderLinks);
			this.panelMain.Content.Controls.Add(this.splitterLinkStatus);
			this.panelMain.Content.Controls.Add(this.panelLinkStatus);
			this.panelMain.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Content.ForeColor = System.Drawing.SystemColors.ControlText;
			this.panelMain.Content.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Content.Name = "Content";
			this.panelMain.Content.Size = new System.Drawing.Size(423, 250);
			this.panelMain.Content.TabIndex = 2;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Name = "panelMain";
			this.panelMain.ParentControl = this;
			clsDesigner1.Name = "ProjectObject";
			clsDesigner1.ParentControl = this.panelMain;
			this.panelMain.ProjectObject = clsDesigner1;
			this.panelMain.Size = new System.Drawing.Size(423, 280);
			// 
			// panelMain.Status
			// 
			this.panelMain.Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Status.Controls.Add(this.lblName);
			this.panelMain.Status.Controls.Add(this.gadgetLink);
			this.panelMain.Status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelMain.Status.Location = new System.Drawing.Point(0, 250);
			this.panelMain.Status.Name = "Status";
			this.panelMain.Status.Size = new System.Drawing.Size(423, 30);
			this.panelMain.Status.TabIndex = 3;
			this.panelMain.TabIndex = 3;
			// 
			// datagridShaderLinks
			// 
			this.datagridShaderLinks.AllowUserToResizeColumns = false;
			this.datagridShaderLinks.AllowUserToResizeRows = false;
			this.datagridShaderLinks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridShaderLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridShaderLinks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnShader});
			this.datagridShaderLinks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridShaderLinks.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.datagridShaderLinks.Location = new System.Drawing.Point(0, 0);
			this.datagridShaderLinks.Name = "datagridShaderLinks";
			this.datagridShaderLinks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.datagridShaderLinks.ShowCellErrors = false;
			this.datagridShaderLinks.Size = new System.Drawing.Size(421, 214);
			this.datagridShaderLinks.TabIndex = 6;
			this.datagridShaderLinks.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridShaderLinks_CellValueChanged);
			this.datagridShaderLinks.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.datagridShaderLinks_DataError);
			this.datagridShaderLinks.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridShaderLinks_EditingControlShowing);
			this.datagridShaderLinks.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.datagridShaderLinks_RowsRemoved);
			this.datagridShaderLinks.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridShaderLinks_UserAddedRow);
			// 
			// columnShader
			// 
			this.columnShader.HeaderText = "Shader Link";
			this.columnShader.Name = "columnShader";
			// 
			// splitterLinkStatus
			// 
			this.splitterLinkStatus.BackColor = System.Drawing.SystemColors.ControlLight;
			this.splitterLinkStatus.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitterLinkStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterLinkStatus.Location = new System.Drawing.Point(0, 214);
			this.splitterLinkStatus.Name = "splitterLinkStatus";
			this.splitterLinkStatus.Size = new System.Drawing.Size(421, 10);
			this.splitterLinkStatus.TabIndex = 1;
			this.splitterLinkStatus.TabStop = false;
			// 
			// panelLinkStatus
			// 
			this.panelLinkStatus.Controls.Add(this.dataLinkStatus);
			this.panelLinkStatus.Controls.Add(this.panelLinkStatusHead);
			this.panelLinkStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelLinkStatus.Location = new System.Drawing.Point(0, 224);
			this.panelLinkStatus.Name = "panelLinkStatus";
			this.panelLinkStatus.Size = new System.Drawing.Size(421, 24);
			this.panelLinkStatus.TabIndex = 5;
			// 
			// dataLinkStatus
			// 
			this.dataLinkStatus.AllowUserToAddRows = false;
			this.dataLinkStatus.AllowUserToDeleteRows = false;
			this.dataLinkStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataLinkStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataLinkStatus.Location = new System.Drawing.Point(0, 23);
			this.dataLinkStatus.Name = "dataLinkStatus";
			this.dataLinkStatus.ReadOnly = true;
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.dataLinkStatus.RowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataLinkStatus.Size = new System.Drawing.Size(421, 1);
			this.dataLinkStatus.TabIndex = 0;
			// 
			// panelLinkStatusHead
			// 
			this.panelLinkStatusHead.Controls.Add(this.chkLinkWarnings);
			this.panelLinkStatusHead.Controls.Add(this.chkLinkErrors);
			this.panelLinkStatusHead.Controls.Add(this.lblLinkStatus);
			this.panelLinkStatusHead.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelLinkStatusHead.Location = new System.Drawing.Point(0, 0);
			this.panelLinkStatusHead.Name = "panelLinkStatusHead";
			this.panelLinkStatusHead.Size = new System.Drawing.Size(421, 23);
			this.panelLinkStatusHead.TabIndex = 1;
			// 
			// chkLinkWarnings
			// 
			this.chkLinkWarnings.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkLinkWarnings.AutoSize = true;
			this.chkLinkWarnings.BackColor = System.Drawing.Color.Transparent;
			this.chkLinkWarnings.Checked = true;
			this.chkLinkWarnings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLinkWarnings.Dock = System.Windows.Forms.DockStyle.Left;
			this.chkLinkWarnings.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkLinkWarnings.Location = new System.Drawing.Point(342, 0);
			this.chkLinkWarnings.Name = "chkLinkWarnings";
			this.chkLinkWarnings.Size = new System.Drawing.Size(74, 23);
			this.chkLinkWarnings.TabIndex = 4;
			this.chkLinkWarnings.Text = "Warnings: 0";
			this.chkLinkWarnings.UseVisualStyleBackColor = false;
			this.chkLinkWarnings.CheckedChanged += new System.EventHandler(this.ChkLinkWarnings_CheckedChanged);
			// 
			// chkLinkErrors
			// 
			this.chkLinkErrors.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkLinkErrors.AutoSize = true;
			this.chkLinkErrors.BackColor = System.Drawing.Color.Transparent;
			this.chkLinkErrors.Checked = true;
			this.chkLinkErrors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLinkErrors.Dock = System.Windows.Forms.DockStyle.Left;
			this.chkLinkErrors.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkLinkErrors.Location = new System.Drawing.Point(286, 0);
			this.chkLinkErrors.Name = "chkLinkErrors";
			this.chkLinkErrors.Size = new System.Drawing.Size(56, 23);
			this.chkLinkErrors.TabIndex = 3;
			this.chkLinkErrors.Text = "Errors: 0";
			this.chkLinkErrors.UseVisualStyleBackColor = false;
			this.chkLinkErrors.CheckedChanged += new System.EventHandler(this.ChkLinkErrors_CheckedChanged);
			// 
			// lblLinkStatus
			// 
			this.lblLinkStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblLinkStatus.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblLinkStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLinkStatus.ForeColor = System.Drawing.Color.Green;
			this.lblLinkStatus.Location = new System.Drawing.Point(0, 0);
			this.lblLinkStatus.Name = "lblLinkStatus";
			this.lblLinkStatus.Size = new System.Drawing.Size(286, 23);
			this.lblLinkStatus.TabIndex = 0;
			this.lblLinkStatus.Text = "Link Status: Good";
			this.lblLinkStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.BackColor = System.Drawing.Color.Transparent;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(3, 6);
			this.lblName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(60, 16);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "Program";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gadgetLink
			// 
			this.gadgetLink.Controls.Add(this.btnLink);
			this.gadgetLink.Controls.Add(this.chkAutoLink);
			this.gadgetLink.Location = new System.Drawing.Point(232, 3);
			this.gadgetLink.Name = "gadgetLink";
			this.gadgetLink.Size = new System.Drawing.Size(94, 21);
			this.gadgetLink.TabIndex = 7;
			// 
			// btnLink
			// 
			this.btnLink.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnLink.Enabled = false;
			this.btnLink.Location = new System.Drawing.Point(39, 0);
			this.btnLink.Margin = new System.Windows.Forms.Padding(0);
			this.btnLink.Name = "btnLink";
			this.btnLink.Size = new System.Drawing.Size(56, 21);
			this.btnLink.TabIndex = 5;
			this.btnLink.Text = "Link";
			this.btnLink.UseVisualStyleBackColor = true;
			this.btnLink.Click += new System.EventHandler(this.BtnLink_Click);
			// 
			// chkAutoLink
			// 
			this.chkAutoLink.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkAutoLink.AutoSize = true;
			this.chkAutoLink.Checked = true;
			this.chkAutoLink.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoLink.Dock = System.Windows.Forms.DockStyle.Left;
			this.chkAutoLink.Location = new System.Drawing.Point(0, 0);
			this.chkAutoLink.Margin = new System.Windows.Forms.Padding(0);
			this.chkAutoLink.Name = "chkAutoLink";
			this.chkAutoLink.Size = new System.Drawing.Size(39, 21);
			this.chkAutoLink.TabIndex = 4;
			this.chkAutoLink.Text = "Auto";
			this.chkAutoLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkAutoLink.UseVisualStyleBackColor = true;
			this.chkAutoLink.CheckedChanged += new System.EventHandler(this.ChkAutoLink_CheckedChanged);
			// 
			// frmProgram
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(423, 280);
			this.Controls.Add(this.panelMain);
			this.Name = "frmProgram";
			this.Text = "Designer_ProjectObject";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProgram_FormClosing);
			this.Load += new System.EventHandler(this.FrmProgram_Load);
			this.panelMain.Content.ResumeLayout(false);
			this.panelMain.Status.ResumeLayout(false);
			this.panelMain.Status.PerformLayout();
			this.panelMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.datagridShaderLinks)).EndInit();
			this.panelLinkStatus.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataLinkStatus)).EndInit();
			this.panelLinkStatusHead.ResumeLayout(false);
			this.panelLinkStatusHead.PerformLayout();
			this.gadgetLink.ResumeLayout(false);
			this.gadgetLink.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label lblName;
		private ShaderToy_Components.controlProjectObject panelMain;
		private System.Windows.Forms.Splitter splitterLinkStatus;
		private System.Windows.Forms.Panel panelLinkStatus;
		private System.Windows.Forms.DataGridView dataLinkStatus;
		private System.Windows.Forms.Panel panelLinkStatusHead;
		private System.Windows.Forms.Label lblLinkStatus;
		private System.Windows.Forms.Panel gadgetLink;
		private System.Windows.Forms.Button btnLink;
		private System.Windows.Forms.CheckBox chkAutoLink;
		private System.Windows.Forms.CheckBox chkLinkWarnings;
		private System.Windows.Forms.CheckBox chkLinkErrors;
		private System.Windows.Forms.DataGridView datagridShaderLinks;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnShader;
	}
}