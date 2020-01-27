using System.Windows.Forms;

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
			this.txtSource = new FastColoredTextBoxNS.FastColoredTextBox();
			this.panelMain = new System.Windows.Forms.Panel();
			this.panelEventDescription = new System.Windows.Forms.FlowLayoutPanel();
			this.lstEventType = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.txtSource)).BeginInit();
			this.panelMain.SuspendLayout();
			this.panelEventDescription.SuspendLayout();
			this.SuspendLayout();
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
			this.txtSource.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:" +
    "]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
			this.txtSource.AutoScrollMinSize = new System.Drawing.Size(25, 12);
			this.txtSource.BackBrush = null;
			this.txtSource.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
			this.txtSource.CharHeight = 12;
			this.txtSource.CharWidth = 7;
			this.txtSource.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtSource.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.txtSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtSource.Font = new System.Drawing.Font("Courier New", 8.25F);
			this.txtSource.IsReplaceMode = false;
			this.txtSource.Language = FastColoredTextBoxNS.Language.CSharp;
			this.txtSource.LeftBracket = '(';
			this.txtSource.LeftBracket2 = '{';
			this.txtSource.Location = new System.Drawing.Point(0, 27);
			this.txtSource.Name = "txtSource";
			this.txtSource.Paddings = new System.Windows.Forms.Padding(0);
			this.txtSource.RightBracket = ')';
			this.txtSource.RightBracket2 = '}';
			this.txtSource.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.txtSource.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtSource.ServiceColors")));
			this.txtSource.Size = new System.Drawing.Size(575, 133);
			this.txtSource.TabIndex = 1;
			this.txtSource.Zoom = 100;
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.SystemColors.Control;
			this.panelMain.Controls.Add(this.txtSource);
			this.panelMain.Controls.Add(this.panelEventDescription);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(575, 160);
			this.panelMain.TabIndex = 2;
			// 
			// panelEventDescription
			// 
			this.panelEventDescription.Controls.Add(this.lstEventType);
			this.panelEventDescription.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEventDescription.Location = new System.Drawing.Point(0, 0);
			this.panelEventDescription.Name = "panelEventDescription";
			this.panelEventDescription.Size = new System.Drawing.Size(575, 27);
			this.panelEventDescription.TabIndex = 1;
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
			// controlEventScript
			// 
			//this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			//this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.panelMain);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "controlEventScript";
			this.Padding = new System.Windows.Forms.Padding(0, 0, 4, 4);
			this.Size = new System.Drawing.Size(579, 164);
			((System.ComponentModel.ISupportInitialize)(this.txtSource)).EndInit();
			this.panelMain.ResumeLayout(false);
			this.panelEventDescription.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private FastColoredTextBoxNS.FastColoredTextBox txtSource;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.FlowLayoutPanel panelEventDescription;
		private System.Windows.Forms.ComboBox lstEventType;
	}
}
