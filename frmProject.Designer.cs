namespace WinOpenGL_ShaderToy
{
	partial class frmProject
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Project");
			this.menuNew = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuNew_Geometry = new System.Windows.Forms.ToolStripMenuItem();
			this.menuNew_Shader = new System.Windows.Forms.ToolStripMenuItem();
			this.menuNew_Program = new System.Windows.Forms.ToolStripMenuItem();
			this.menuNew_Render = new System.Windows.Forms.ToolStripMenuItem();
			this.treeMain = new System.Windows.Forms.TreeView();
			this.menuProjectObject = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuProjectObject_Delete = new System.Windows.Forms.ToolStripMenuItem();
			this.menuNew_VertexDescription = new System.Windows.Forms.ToolStripMenuItem();
			this.menuNew.SuspendLayout();
			this.menuProjectObject.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuNew
			// 
			this.menuNew.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNew_VertexDescription,
            this.menuNew_Geometry,
            this.menuNew_Shader,
            this.menuNew_Program,
            this.menuNew_Render});
			this.menuNew.Name = "menuNew";
			this.menuNew.Size = new System.Drawing.Size(197, 136);
			// 
			// menuNew_Geometry
			// 
			this.menuNew_Geometry.Name = "menuNew_Geometry";
			this.menuNew_Geometry.Size = new System.Drawing.Size(196, 22);
			this.menuNew_Geometry.Text = "New Geometry";
			this.menuNew_Geometry.Click += new System.EventHandler(this.MenuNew_Geometry_Click);
			// 
			// menuNew_Shader
			// 
			this.menuNew_Shader.Name = "menuNew_Shader";
			this.menuNew_Shader.Size = new System.Drawing.Size(196, 22);
			this.menuNew_Shader.Text = "New Shader";
			// 
			// menuNew_Program
			// 
			this.menuNew_Program.Name = "menuNew_Program";
			this.menuNew_Program.Size = new System.Drawing.Size(196, 22);
			this.menuNew_Program.Text = "New Program";
			this.menuNew_Program.Click += new System.EventHandler(this.MenuNew_Program_Click);
			// 
			// menuNew_Render
			// 
			this.menuNew_Render.Name = "menuNew_Render";
			this.menuNew_Render.Size = new System.Drawing.Size(196, 22);
			this.menuNew_Render.Text = "New Render";
			this.menuNew_Render.Click += new System.EventHandler(this.MenuNew_Render_Click);
			// 
			// treeMain
			// 
			this.treeMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.treeMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.treeMain.ForeColor = System.Drawing.Color.White;
			this.treeMain.Indent = 18;
			this.treeMain.LabelEdit = true;
			this.treeMain.LineColor = System.Drawing.Color.DarkGray;
			this.treeMain.Location = new System.Drawing.Point(0, 0);
			this.treeMain.Name = "treeMain";
			treeNode1.ContextMenuStrip = this.menuNew;
			treeNode1.Name = "nodeProject";
			treeNode1.Text = "Project";
			this.treeMain.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
			this.treeMain.Size = new System.Drawing.Size(476, 450);
			this.treeMain.TabIndex = 0;
			this.treeMain.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeMain_BeforeLabelEdit);
			this.treeMain.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.TreeMain_AfterLabelEdit);
			this.treeMain.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeMain_BeforeSelect);
			this.treeMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeMain_AfterSelect);
			this.treeMain.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TreeMain_MouseDoubleClick);
			this.treeMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeMain_MouseDown);
			this.treeMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TreeMain_MouseMove);
			// 
			// menuProjectObject
			// 
			this.menuProjectObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuProjectObject_Delete});
			this.menuProjectObject.Name = "menuProjectObject";
			this.menuProjectObject.Size = new System.Drawing.Size(146, 26);
			// 
			// menuProjectObject_Delete
			// 
			this.menuProjectObject_Delete.Name = "menuProjectObject_Delete";
			this.menuProjectObject_Delete.Size = new System.Drawing.Size(145, 22);
			this.menuProjectObject_Delete.Text = "Delete Object";
			this.menuProjectObject_Delete.Click += new System.EventHandler(this.MenuProjectObject_Delete_Click);
			// 
			// menuNew_VertexDescription
			// 
			this.menuNew_VertexDescription.Name = "menuNew_VertexDescription";
			this.menuNew_VertexDescription.Size = new System.Drawing.Size(196, 22);
			this.menuNew_VertexDescription.Text = "New Vertex Description";
			this.menuNew_VertexDescription.Click += new System.EventHandler(this.menuNew_VertexDescription_Click);
			// 
			// frmProject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(476, 450);
			this.Controls.Add(this.treeMain);
			this.HideOnClose = true;
			this.Name = "frmProject";
			this.Text = "Project";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProject_FormClosing);
			this.Load += new System.EventHandler(this.FrmProjects_Load);
			this.menuNew.ResumeLayout(false);
			this.menuProjectObject.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeMain;
		private System.Windows.Forms.ContextMenuStrip menuNew;
		private System.Windows.Forms.ToolStripMenuItem menuNew_Shader;
		private System.Windows.Forms.ToolStripMenuItem menuNew_Program;
		private System.Windows.Forms.ToolStripMenuItem menuNew_Render;
		private System.Windows.Forms.ContextMenuStrip menuProjectObject;
		private System.Windows.Forms.ToolStripMenuItem menuProjectObject_Delete;
		private System.Windows.Forms.ToolStripMenuItem menuNew_Geometry;
		private System.Windows.Forms.ToolStripMenuItem menuNew_VertexDescription;
	}
}