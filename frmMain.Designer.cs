namespace WinOpenGL_ShaderToy
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
			this.components = new System.ComponentModel.Container();
			this.themeDock_Main = new WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme();
			this.dockMain = new WeifenLuo.WinFormsUI.Docking.DockPanel();
			this.menuWindows = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuProject = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStats = new System.Windows.Forms.ToolStripMenuItem();
			this.menusWindows = new System.Windows.Forms.ToolStripMenuItem();
			this.menusMain = new System.Windows.Forms.MenuStrip();
			this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
			this.menuFile_NewProject = new System.Windows.Forms.ToolStripMenuItem();
			this.menuFile_LoadProject = new System.Windows.Forms.ToolStripMenuItem();
			this.menuFile_SaveProject = new System.Windows.Forms.ToolStripMenuItem();
			this.dialogLoad = new System.Windows.Forms.OpenFileDialog();
			this.dialogSave = new System.Windows.Forms.SaveFileDialog();
			this.menuWindows.SuspendLayout();
			this.menusMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// dockMain
			// 
			this.dockMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.dockMain.DefaultFloatWindowSize = new System.Drawing.Size(400, 400);
			this.dockMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dockMain.DockBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
			this.dockMain.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
			this.dockMain.ForeColor = System.Drawing.SystemColors.Window;
			this.dockMain.Location = new System.Drawing.Point(0, 24);
			this.dockMain.Margin = new System.Windows.Forms.Padding(0);
			this.dockMain.Name = "dockMain";
			this.dockMain.Padding = new System.Windows.Forms.Padding(6);
			this.dockMain.ShowDocumentIcon = true;
			this.dockMain.Size = new System.Drawing.Size(1264, 737);
			this.dockMain.TabIndex = 10;
			this.dockMain.Theme = this.themeDock_Main;
			// 
			// menuWindows
			// 
			this.menuWindows.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProject,
            this.menuStats});
			this.menuWindows.Name = "menuWindows";
			this.menuWindows.OwnerItem = this.menusWindows;
			this.menuWindows.Size = new System.Drawing.Size(121, 48);
			this.menuWindows.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.MenuWindows_Closing);
			// 
			// menuProject
			// 
			this.menuProject.Checked = true;
			this.menuProject.CheckOnClick = true;
			this.menuProject.CheckState = System.Windows.Forms.CheckState.Checked;
			this.menuProject.Name = "menuProject";
			this.menuProject.Size = new System.Drawing.Size(120, 22);
			this.menuProject.Text = "Project";
			this.menuProject.CheckedChanged += new System.EventHandler(this.MenuProject_CheckedChanged);
			// 
			// menuStats
			// 
			this.menuStats.CheckOnClick = true;
			this.menuStats.Name = "menuStats";
			this.menuStats.Size = new System.Drawing.Size(120, 22);
			this.menuStats.Text = "Statistics";
			// 
			// menusWindows
			// 
			this.menusWindows.DropDown = this.menuWindows;
			this.menusWindows.Name = "menusWindows";
			this.menusWindows.Size = new System.Drawing.Size(68, 20);
			this.menusWindows.Text = "Windows";
			// 
			// menusMain
			// 
			this.menusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menusWindows});
			this.menusMain.Location = new System.Drawing.Point(0, 0);
			this.menusMain.Name = "menusMain";
			this.menusMain.Size = new System.Drawing.Size(1264, 24);
			this.menusMain.TabIndex = 13;
			this.menusMain.Text = "Main Menu";
			// 
			// menuFile
			// 
			this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile_NewProject,
            this.menuFile_LoadProject,
            this.menuFile_SaveProject});
			this.menuFile.Name = "menuFile";
			this.menuFile.Size = new System.Drawing.Size(37, 20);
			this.menuFile.Text = "File";
			// 
			// menuFile_NewProject
			// 
			this.menuFile_NewProject.Name = "menuFile_NewProject";
			this.menuFile_NewProject.Size = new System.Drawing.Size(140, 22);
			this.menuFile_NewProject.Text = "New Project";
			this.menuFile_NewProject.Click += new System.EventHandler(this.menuFile_NewProject_Click);
			// 
			// menuFile_LoadProject
			// 
			this.menuFile_LoadProject.Name = "menuFile_LoadProject";
			this.menuFile_LoadProject.Size = new System.Drawing.Size(140, 22);
			this.menuFile_LoadProject.Text = "Load Project";
			this.menuFile_LoadProject.Click += new System.EventHandler(this.menuFile_LoadProject_Click);
			// 
			// menuFile_SaveProject
			// 
			this.menuFile_SaveProject.Name = "menuFile_SaveProject";
			this.menuFile_SaveProject.Size = new System.Drawing.Size(140, 22);
			this.menuFile_SaveProject.Text = "Save Project";
			this.menuFile_SaveProject.Click += new System.EventHandler(this.menuFile_SaveProject_Click);
			// 
			// dialogLoad
			// 
			this.dialogLoad.FileName = "openFileDialog1";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(1264, 761);
			this.Controls.Add(this.dockMain);
			this.Controls.Add(this.menusMain);
			this.DoubleBuffered = true;
			this.ForeColor = System.Drawing.SystemColors.Control;
			this.MainMenuStrip = this.menusMain;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "OpenGL - ShaderToy";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
			this.Load += new System.EventHandler(this.FormMain_Load);
			this.menuWindows.ResumeLayout(false);
			this.menusMain.ResumeLayout(false);
			this.menusMain.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private WeifenLuo.WinFormsUI.Docking.VS2015DarkTheme themeDock_Main;
		private WeifenLuo.WinFormsUI.Docking.DockPanel dockMain;
		private System.Windows.Forms.ContextMenuStrip menuWindows;
		private System.Windows.Forms.ToolStripMenuItem menuProject;
		private System.Windows.Forms.ToolStripMenuItem menuStats;
		private System.Windows.Forms.ToolStripMenuItem menusWindows;
		private System.Windows.Forms.MenuStrip menusMain;
		private System.Windows.Forms.ToolStripMenuItem menuFile;
		private System.Windows.Forms.ToolStripMenuItem menuFile_LoadProject;
		private System.Windows.Forms.ToolStripMenuItem menuFile_SaveProject;
		private System.Windows.Forms.OpenFileDialog dialogLoad;
		private System.Windows.Forms.SaveFileDialog dialogSave;
		private System.Windows.Forms.ToolStripMenuItem menuFile_NewProject;
	}
}

