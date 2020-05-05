namespace WinOpenGL_ShaderToy
{
	partial class controlConsole
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(controlConsole));
			this.Output = new FastColoredTextBoxNS.FastColoredTextBox();
			this.Input = new FastColoredTextBoxNS.FastColoredTextBox();
			((System.ComponentModel.ISupportInitialize)(this.Output)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Input)).BeginInit();
			this.SuspendLayout();
			// 
			// Output
			// 
			this.Output.AllowSeveralTextStyleDrawing = true;
			this.Output.AutoCompleteBrackets = true;
			this.Output.AutoCompleteBracketsList = new char[] {
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
			this.Output.AutoIndent = false;
			this.Output.AutoIndentChars = false;
			this.Output.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:" +
    "]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
			this.Output.AutoIndentExistingLines = false;
			this.Output.AutoScrollMinSize = new System.Drawing.Size(0, 14);
			this.Output.BackBrush = null;
			this.Output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.Output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Output.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
			this.Output.CharHeight = 14;
			this.Output.CharWidth = 8;
			this.Output.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.Output.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.Output.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Output.Font = new System.Drawing.Font("Courier New", 9.75F);
			this.Output.IsReplaceMode = false;
			this.Output.Language = FastColoredTextBoxNS.Language.CSharp;
			this.Output.LeftBracket = '(';
			this.Output.LeftBracket2 = '{';
			this.Output.Location = new System.Drawing.Point(0, 0);
			this.Output.MinimumSize = new System.Drawing.Size(0, 24);
			this.Output.Name = "Output";
			this.Output.Paddings = new System.Windows.Forms.Padding(0);
			this.Output.RightBracket = ')';
			this.Output.RightBracket2 = '}';
			this.Output.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.Output.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("Output.ServiceColors")));
			this.Output.ShowCaretWhenInactive = true;
			this.Output.ShowFoldingLines = true;
			this.Output.ShowLineNumbers = false;
			this.Output.Size = new System.Drawing.Size(439, 161);
			this.Output.TabIndex = 0;
			this.Output.TextAreaBorder = FastColoredTextBoxNS.TextAreaBorderType.Shadow;
			this.Output.WordWrap = true;
			this.Output.Zoom = 100;
			this.Output.SelectionChanged += new System.EventHandler(this.Output_SelectionChanged);
			this.Output.KeyPressing += new System.Windows.Forms.KeyPressEventHandler(this.Output_KeyPressing);
			// 
			// Input
			// 
			this.Input.AllowSeveralTextStyleDrawing = true;
			this.Input.AutoCompleteBracketsList = new char[] {
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
			this.Input.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:" +
    "]*(?<range>:)\\s*(?<range>[^;]+);\r\n";
			this.Input.AutoScrollMinSize = new System.Drawing.Size(0, 14);
			this.Input.BackBrush = null;
			this.Input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
			this.Input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Input.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
			this.Input.CharHeight = 14;
			this.Input.CharWidth = 8;
			this.Input.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.Input.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
			this.Input.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Input.IsReplaceMode = false;
			this.Input.Language = FastColoredTextBoxNS.Language.CSharp;
			this.Input.LeftBracket = '(';
			this.Input.LeftBracket2 = '{';
			this.Input.Location = new System.Drawing.Point(0, 161);
			this.Input.MinimumSize = new System.Drawing.Size(0, 24);
			this.Input.Name = "Input";
			this.Input.Paddings = new System.Windows.Forms.Padding(0);
			this.Input.RightBracket = ')';
			this.Input.RightBracket2 = '}';
			this.Input.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
			this.Input.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("Input.ServiceColors")));
			this.Input.ShowCaretWhenInactive = true;
			this.Input.ShowLineNumbers = false;
			this.Input.Size = new System.Drawing.Size(439, 24);
			this.Input.TabIndex = 1;
			this.Input.TextAreaBorder = FastColoredTextBoxNS.TextAreaBorderType.Single;
			this.Input.WordWrap = true;
			this.Input.Zoom = 100;
			this.Input.TextChanging += new System.EventHandler<FastColoredTextBoxNS.TextChangingEventArgs>(this.Input_TextChanging);
			this.Input.SelectionChanged += new System.EventHandler(this.Input_SelectionChanged);
			this.Input.SizeChanged += new System.EventHandler(this.Input_SizeChanged);
			this.Input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Input_KeyDown);
			// 
			// controlConsole
			// 
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.Output);
			this.Controls.Add(this.Input);
			this.ForeColor = System.Drawing.SystemColors.Control;
			this.Name = "controlConsole";
			this.Size = new System.Drawing.Size(439, 185);
			this.Load += new System.EventHandler(this.controlConsole_Load);
			((System.ComponentModel.ISupportInitialize)(this.Output)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.Input)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public FastColoredTextBoxNS.FastColoredTextBox Output;
		public FastColoredTextBoxNS.FastColoredTextBox Input;
	}
}
