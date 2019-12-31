using System;
namespace WinOpenGL_ShaderToy
{
	partial class frmShader
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
			this.components = new System.ComponentModel.Container();
			ShaderToy_Components.controlProjectObject.clsDesigner clsDesigner1 = new ShaderToy_Components.controlProjectObject.clsDesigner();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShader));
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panelMain = new ShaderToy_Components.controlProjectObject();
			this.panelSourceEdit = new System.Windows.Forms.Panel();
			this.txtSource = new FastColoredTextBoxNS.FastColoredTextBox();
			this.txtmapSource = new FastColoredTextBoxNS.DocumentMap();
			this.splitterCompileStatus = new System.Windows.Forms.Splitter();
			this.panelCompileStatus = new System.Windows.Forms.Panel();
			this.dataCompileStatus = new System.Windows.Forms.DataGridView();
			this.panelCompileStatusHead = new System.Windows.Forms.Panel();
			this.chkCompileWarnings = new System.Windows.Forms.CheckBox();
			this.chkCompileErrors = new System.Windows.Forms.CheckBox();
			this.lblCompileStatus = new System.Windows.Forms.Label();
			this.lstShaderType = new System.Windows.Forms.ComboBox();
			this.btnLoad = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.gadgetCompile = new System.Windows.Forms.Panel();
			this.btnCompile = new System.Windows.Forms.Button();
			this.chkAutoCompile = new System.Windows.Forms.CheckBox();
			this.dialogSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.dialogOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.panelMain.Content.SuspendLayout();
			this.panelMain.Status.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.panelSourceEdit.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSource)).BeginInit();
			this.panelCompileStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataCompileStatus)).BeginInit();
			this.panelCompileStatusHead.SuspendLayout();
			this.gadgetCompile.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			// 
			// panelMain.Content
			// 
			this.panelMain.Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Content.Controls.Add(this.panelSourceEdit);
			this.panelMain.Content.Controls.Add(this.splitterCompileStatus);
			this.panelMain.Content.Controls.Add(this.panelCompileStatus);
			this.panelMain.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Content.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.panelMain.Content.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Content.Name = "Content";
			this.panelMain.Content.Size = new System.Drawing.Size(800, 419);
			this.panelMain.Content.TabIndex = 2;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Name = "panelMain";
			this.panelMain.ParentControl = this;
			clsDesigner1.Name = "ProjectObject";
			clsDesigner1.ParentControl = this.panelMain;
			this.panelMain.ProjectObject = clsDesigner1;
			this.panelMain.Size = new System.Drawing.Size(800, 449);
			// 
			// panelMain.Status
			// 
			this.panelMain.Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Status.Controls.Add(this.lstShaderType);
			this.panelMain.Status.Controls.Add(this.btnLoad);
			this.panelMain.Status.Controls.Add(this.btnSave);
			this.panelMain.Status.Controls.Add(this.gadgetCompile);
			this.panelMain.Status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelMain.Status.Location = new System.Drawing.Point(0, 419);
			this.panelMain.Status.Name = "Status";
			this.panelMain.Status.Size = new System.Drawing.Size(800, 30);
			this.panelMain.Status.TabIndex = 3;
			this.panelMain.TabIndex = 4;
			// 
			// panelSourceEdit
			// 
			this.panelSourceEdit.Controls.Add(this.txtSource);
			this.panelSourceEdit.Controls.Add(this.txtmapSource);
			this.panelSourceEdit.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelSourceEdit.Location = new System.Drawing.Point(0, 0);
			this.panelSourceEdit.Name = "panelSourceEdit";
			this.panelSourceEdit.Size = new System.Drawing.Size(798, 381);
			this.panelSourceEdit.TabIndex = 2;
			// 
			// txtSource
			// 
			this.txtSource.AllowSeveralTextStyleDrawing = true;
			this.txtSource.AutoCompleteBrackets = true;
			this.txtSource.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
			this.txtSource.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:" +
    "]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
			this.txtSource.AutoScrollMinSize = new System.Drawing.Size(27, 14);
			this.txtSource.BackBrush = null;
			this.txtSource.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
			this.txtSource.CharHeight = 14;
			this.txtSource.CharWidth = 8;
			this.txtSource.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtSource.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSource.IsReplaceMode = false;
			this.txtSource.Language = FastColoredTextBoxNS.Language.CSharp;
			this.txtSource.LeftBracket = '(';
			this.txtSource.LeftBracket2 = '{';
			this.txtSource.Location = new System.Drawing.Point(0, 0);
			this.txtSource.Name = "txtSource";
			this.txtSource.Paddings = new System.Windows.Forms.Padding(0);
			this.txtSource.RightBracket = ')';
			this.txtSource.RightBracket2 = '}';
			this.txtSource.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.txtSource.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtSource.ServiceColors")));
			this.txtSource.ShowCaretWhenInactive = true;
			this.txtSource.ShowFoldingLines = true;
			this.txtSource.Size = new System.Drawing.Size(713, 381);
			this.txtSource.TabIndex = 0;
			this.txtSource.TextAreaBorder = FastColoredTextBoxNS.TextAreaBorderType.Shadow;
			this.txtSource.Zoom = 100;
			this.txtSource.TextChanged += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.TxtSource_TextChanged);
			// 
			// txtmapSource
			// 
			this.txtmapSource.Dock = System.Windows.Forms.DockStyle.Right;
			this.txtmapSource.ForeColor = System.Drawing.Color.Maroon;
			this.txtmapSource.Location = new System.Drawing.Point(713, 0);
			this.txtmapSource.Name = "txtmapSource";
			this.txtmapSource.Size = new System.Drawing.Size(85, 381);
			this.txtmapSource.TabIndex = 1;
			this.txtmapSource.Target = this.txtSource;
			// 
			// splitterCompileStatus
			// 
			this.splitterCompileStatus.BackColor = System.Drawing.SystemColors.ControlLight;
			this.splitterCompileStatus.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitterCompileStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterCompileStatus.Location = new System.Drawing.Point(0, 381);
			this.splitterCompileStatus.Margin = new System.Windows.Forms.Padding(0);
			this.splitterCompileStatus.MinExtra = 0;
			this.splitterCompileStatus.MinSize = 18;
			this.splitterCompileStatus.Name = "splitterCompileStatus";
			this.splitterCompileStatus.Size = new System.Drawing.Size(798, 10);
			this.splitterCompileStatus.TabIndex = 2;
			this.splitterCompileStatus.TabStop = false;
			// 
			// panelCompileStatus
			// 
			this.panelCompileStatus.Controls.Add(this.dataCompileStatus);
			this.panelCompileStatus.Controls.Add(this.panelCompileStatusHead);
			this.panelCompileStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelCompileStatus.Location = new System.Drawing.Point(0, 391);
			this.panelCompileStatus.Name = "panelCompileStatus";
			this.panelCompileStatus.Size = new System.Drawing.Size(798, 26);
			this.panelCompileStatus.TabIndex = 3;
			// 
			// dataCompileStatus
			// 
			this.dataCompileStatus.AllowUserToAddRows = false;
			this.dataCompileStatus.AllowUserToDeleteRows = false;
			this.dataCompileStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataCompileStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataCompileStatus.Location = new System.Drawing.Point(0, 23);
			this.dataCompileStatus.Name = "dataCompileStatus";
			this.dataCompileStatus.ReadOnly = true;
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.dataCompileStatus.RowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataCompileStatus.Size = new System.Drawing.Size(798, 3);
			this.dataCompileStatus.TabIndex = 0;
			// 
			// panelCompileStatusHead
			// 
			this.panelCompileStatusHead.Controls.Add(this.chkCompileWarnings);
			this.panelCompileStatusHead.Controls.Add(this.chkCompileErrors);
			this.panelCompileStatusHead.Controls.Add(this.lblCompileStatus);
			this.panelCompileStatusHead.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelCompileStatusHead.Location = new System.Drawing.Point(0, 0);
			this.panelCompileStatusHead.Name = "panelCompileStatusHead";
			this.panelCompileStatusHead.Size = new System.Drawing.Size(798, 23);
			this.panelCompileStatusHead.TabIndex = 1;
			// 
			// chkCompileWarnings
			// 
			this.chkCompileWarnings.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkCompileWarnings.AutoSize = true;
			this.chkCompileWarnings.BackColor = System.Drawing.Color.Transparent;
			this.chkCompileWarnings.Checked = true;
			this.chkCompileWarnings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCompileWarnings.Dock = System.Windows.Forms.DockStyle.Left;
			this.chkCompileWarnings.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkCompileWarnings.Location = new System.Drawing.Point(342, 0);
			this.chkCompileWarnings.Name = "chkCompileWarnings";
			this.chkCompileWarnings.Size = new System.Drawing.Size(74, 23);
			this.chkCompileWarnings.TabIndex = 2;
			this.chkCompileWarnings.Text = "Warnings: 0";
			this.chkCompileWarnings.UseVisualStyleBackColor = false;
			this.chkCompileWarnings.CheckedChanged += new System.EventHandler(this.ChkCompileWarnings_CheckedChanged);
			// 
			// chkCompileErrors
			// 
			this.chkCompileErrors.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkCompileErrors.AutoSize = true;
			this.chkCompileErrors.BackColor = System.Drawing.Color.Transparent;
			this.chkCompileErrors.Checked = true;
			this.chkCompileErrors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkCompileErrors.Dock = System.Windows.Forms.DockStyle.Left;
			this.chkCompileErrors.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkCompileErrors.Location = new System.Drawing.Point(286, 0);
			this.chkCompileErrors.Name = "chkCompileErrors";
			this.chkCompileErrors.Size = new System.Drawing.Size(56, 23);
			this.chkCompileErrors.TabIndex = 1;
			this.chkCompileErrors.Text = "Errors: 0";
			this.chkCompileErrors.UseVisualStyleBackColor = false;
			this.chkCompileErrors.CheckedChanged += new System.EventHandler(this.ChkCompileErrors_CheckedChanged);
			// 
			// lblCompileStatus
			// 
			this.lblCompileStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblCompileStatus.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblCompileStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblCompileStatus.ForeColor = System.Drawing.Color.Green;
			this.lblCompileStatus.Location = new System.Drawing.Point(0, 0);
			this.lblCompileStatus.Name = "lblCompileStatus";
			this.lblCompileStatus.Size = new System.Drawing.Size(286, 23);
			this.lblCompileStatus.TabIndex = 0;
			this.lblCompileStatus.Text = "Compile Status: Good";
			this.lblCompileStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lstShaderType
			// 
			this.lstShaderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstShaderType.FormattingEnabled = true;
			this.lstShaderType.Location = new System.Drawing.Point(3, 3);
			this.lstShaderType.Name = "lstShaderType";
			this.lstShaderType.Size = new System.Drawing.Size(138, 21);
			this.lstShaderType.TabIndex = 1;
			this.lstShaderType.SelectedIndexChanged += new System.EventHandler(this.lstShaderType_SelectedIndexChanged);
			// 
			// btnLoad
			// 
			this.btnLoad.Location = new System.Drawing.Point(310, 3);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Size = new System.Drawing.Size(80, 21);
			this.btnLoad.TabIndex = 3;
			this.btnLoad.Text = "Load Source";
			this.btnLoad.UseVisualStyleBackColor = true;
			this.btnLoad.Click += new System.EventHandler(this.BtnLoad_Click);
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(396, 3);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(85, 21);
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save Source";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);
			// 
			// gadgetCompile
			// 
			this.gadgetCompile.Controls.Add(this.btnCompile);
			this.gadgetCompile.Controls.Add(this.chkAutoCompile);
			this.gadgetCompile.Location = new System.Drawing.Point(487, 3);
			this.gadgetCompile.Name = "gadgetCompile";
			this.gadgetCompile.Size = new System.Drawing.Size(94, 21);
			this.gadgetCompile.TabIndex = 6;
			// 
			// btnCompile
			// 
			this.btnCompile.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnCompile.Enabled = false;
			this.btnCompile.Location = new System.Drawing.Point(39, 0);
			this.btnCompile.Margin = new System.Windows.Forms.Padding(0);
			this.btnCompile.Name = "btnCompile";
			this.btnCompile.Size = new System.Drawing.Size(56, 21);
			this.btnCompile.TabIndex = 5;
			this.btnCompile.Text = "Compile";
			this.btnCompile.UseVisualStyleBackColor = true;
			this.btnCompile.Click += new System.EventHandler(this.BtnCompile_Click);
			// 
			// chkAutoCompile
			// 
			this.chkAutoCompile.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkAutoCompile.AutoSize = true;
			this.chkAutoCompile.Checked = true;
			this.chkAutoCompile.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoCompile.Dock = System.Windows.Forms.DockStyle.Left;
			this.chkAutoCompile.Location = new System.Drawing.Point(0, 0);
			this.chkAutoCompile.Margin = new System.Windows.Forms.Padding(0);
			this.chkAutoCompile.Name = "chkAutoCompile";
			this.chkAutoCompile.Size = new System.Drawing.Size(39, 21);
			this.chkAutoCompile.TabIndex = 4;
			this.chkAutoCompile.Text = "Auto";
			this.chkAutoCompile.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkAutoCompile.UseVisualStyleBackColor = true;
			this.chkAutoCompile.CheckedChanged += new System.EventHandler(this.ChkAutoCompile_CheckedChanged);
			// 
			// dialogOpenFile
			// 
			this.dialogOpenFile.FileName = "openFileDialog1";
			// 
			// frmShader
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(800, 449);
			this.Controls.Add(this.panelMain);
			this.Name = "frmShader";
			this.Text = "Designer_ProjectObject";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmShader_FormClosing);
			this.Load += new System.EventHandler(this.FrmShader_Load);
			this.panelMain.Content.ResumeLayout(false);
			this.panelMain.Status.ResumeLayout(false);
			this.panelMain.Status.PerformLayout();
			this.panelMain.ResumeLayout(false);
			this.panelSourceEdit.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtSource)).EndInit();
			this.panelCompileStatus.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataCompileStatus)).EndInit();
			this.panelCompileStatusHead.ResumeLayout(false);
			this.panelCompileStatusHead.PerformLayout();
			this.gadgetCompile.ResumeLayout(false);
			this.gadgetCompile.PerformLayout();
			this.ResumeLayout(false);

		}
		#endregion
		private FastColoredTextBoxNS.DocumentMap txtmapSource;
		private FastColoredTextBoxNS.FastColoredTextBox txtSource;
		private System.Windows.Forms.SaveFileDialog dialogSaveFile;
		private System.Windows.Forms.OpenFileDialog dialogOpenFile;
		private System.Windows.Forms.ComboBox lstShaderType;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnLoad;
		private ShaderToy_Components.controlProjectObject panelMain;
		private System.Windows.Forms.Splitter splitterCompileStatus;
		private System.Windows.Forms.Panel panelCompileStatus;
		private System.Windows.Forms.Panel panelSourceEdit;
		private System.Windows.Forms.Panel gadgetCompile;
		private System.Windows.Forms.Button btnCompile;
		private System.Windows.Forms.CheckBox chkAutoCompile;
		private System.Windows.Forms.DataGridView dataCompileStatus;
		private System.Windows.Forms.Panel panelCompileStatusHead;
		private System.Windows.Forms.Label lblCompileStatus;
		private System.Windows.Forms.CheckBox chkCompileErrors;
		private System.Windows.Forms.CheckBox chkCompileWarnings;
	}
}