namespace WinOpenGL_ShaderToy
{
	partial class controlEventScript
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(controlEventScript));
			this.panelEventDescription = new System.Windows.Forms.FlowLayoutPanel();
			this.lstEventType = new System.Windows.Forms.ComboBox();
			this.txtSource = new FastColoredTextBoxNS.FastColoredTextBox();
			this.panelEventDescription.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.txtSource)).BeginInit();
			this.SuspendLayout();
			// 
			// panelEventDescription
			// 
			this.panelEventDescription.Controls.Add(this.lstEventType);
			this.panelEventDescription.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEventDescription.Location = new System.Drawing.Point(0, 0);
			this.panelEventDescription.Name = "panelEventDescription";
			this.panelEventDescription.Size = new System.Drawing.Size(575, 27);
			this.panelEventDescription.TabIndex = 0;
			// 
			// lstEventType
			// 
			this.lstEventType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstEventType.FormattingEnabled = true;
			this.lstEventType.Location = new System.Drawing.Point(3, 3);
			this.lstEventType.Name = "lstEventType";
			this.lstEventType.Size = new System.Drawing.Size(146, 21);
			this.lstEventType.TabIndex = 0;
			// 
			// txtSource
			// 
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
			this.txtSource.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);";
			this.txtSource.AutoScrollMinSize = new System.Drawing.Size(25, 12);
			this.txtSource.BackBrush = null;
			this.txtSource.CharHeight = 12;
			this.txtSource.CharWidth = 7;
			this.txtSource.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtSource.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSource.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSource.IsReplaceMode = false;
			this.txtSource.Location = new System.Drawing.Point(0, 27);
			this.txtSource.Name = "txtSource";
			this.txtSource.Paddings = new System.Windows.Forms.Padding(0);
			this.txtSource.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.txtSource.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtSource.ServiceColors")));
			this.txtSource.Size = new System.Drawing.Size(575, 133);
			this.txtSource.TabIndex = 1;
			this.txtSource.Zoom = 100;
			// 
			// controlEventScript
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.txtSource);
			this.Controls.Add(this.panelEventDescription);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "controlEventScript";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 4, 4);
			this.Size = new System.Drawing.Size(579, 164);
			this.Load += new System.EventHandler(this.controlEventScript_Load);
			this.panelEventDescription.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.txtSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel panelEventDescription;
		private System.Windows.Forms.ComboBox lstEventType;
		private FastColoredTextBoxNS.FastColoredTextBox txtSource;
	}
}
