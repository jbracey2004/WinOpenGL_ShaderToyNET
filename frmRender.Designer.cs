using modProject;
namespace WinOpenGL_ShaderToy
{
	partial class frmRender
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
			this.lblRenderName = new System.Windows.Forms.ToolStripStatusLabel();
			this.panelMain = new ShaderToy_Components.controlProjectObject();
			this.lblName = new System.Windows.Forms.Label();
			this.toolStripFPS = new System.Windows.Forms.ToolStrip();
			this.btnInterval = new System.Windows.Forms.ToolStripDropDownButton();
			this.txtInterval = new System.Windows.Forms.ToolStripTextBox();
			this.btnFPS = new System.Windows.Forms.ToolStripDropDownButton();
			this.txtFPS = new System.Windows.Forms.ToolStripTextBox();
			this.lblRenderDuration = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblRenderFreq = new System.Windows.Forms.ToolStripStatusLabel();
			this.btnConfigure = new System.Windows.Forms.Button();
			this.panelMain.Status.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.toolStripFPS.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblRenderName
			// 
			this.lblRenderName.Name = "lblRenderName";
			this.lblRenderName.Size = new System.Drawing.Size(44, 20);
			this.lblRenderName.Text = "Render";
			// 
			// panelMain
			// 
			// 
			// panelMain.Content
			// 
			this.panelMain.Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Content.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.panelMain.Content.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Content.Name = "Content";
			this.panelMain.Content.Size = new System.Drawing.Size(800, 420);
			this.panelMain.Content.TabIndex = 2;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Name = "panelMain";
			this.panelMain.ParentControl = this;
			clsDesigner1.Name = "ProjectObject";
			clsDesigner1.ParentControl = this.panelMain;
			this.panelMain.ProjectObject = clsDesigner1;
			this.panelMain.Size = new System.Drawing.Size(800, 450);
			// 
			// panelMain.Status
			// 
			this.panelMain.Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Status.Controls.Add(this.lblName);
			this.panelMain.Status.Controls.Add(this.btnConfigure);
			this.panelMain.Status.Controls.Add(this.toolStripFPS);
			this.panelMain.Status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelMain.Status.Location = new System.Drawing.Point(0, 420);
			this.panelMain.Status.Name = "Status";
			this.panelMain.Status.Size = new System.Drawing.Size(800, 30);
			this.panelMain.Status.TabIndex = 3;
			this.panelMain.TabIndex = 0;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.BackColor = System.Drawing.Color.Transparent;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(3, 6);
			this.lblName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(65, 16);
			this.lblName.TabIndex = 3;
			this.lblName.Text = "Renderer";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// toolStripFPS
			// 
			this.toolStripFPS.BackColor = System.Drawing.Color.Transparent;
			this.toolStripFPS.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStripFPS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInterval,
            this.btnFPS,
            this.lblRenderDuration,
            this.lblRenderFreq});
			this.toolStripFPS.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.toolStripFPS.Location = new System.Drawing.Point(309, 0);
			this.toolStripFPS.Name = "toolStripFPS";
			this.toolStripFPS.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
			this.toolStripFPS.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStripFPS.Size = new System.Drawing.Size(119, 27);
			this.toolStripFPS.Stretch = true;
			this.toolStripFPS.TabIndex = 5;
			this.toolStripFPS.Text = "toolStripFPS";
			// 
			// btnInterval
			// 
			this.btnInterval.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnInterval.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtInterval});
			this.btnInterval.Name = "btnInterval";
			this.btnInterval.Size = new System.Drawing.Size(36, 19);
			this.btnInterval.Text = "ms";
			this.btnInterval.DropDownClosed += new System.EventHandler(this.BtnInterval_DropDownClosed);
			// 
			// txtInterval
			// 
			this.txtInterval.Name = "txtInterval";
			this.txtInterval.Size = new System.Drawing.Size(100, 23);
			this.txtInterval.Text = "";
			this.txtInterval.TextChanged += new System.EventHandler(this.txtInterval_Change);
			// 
			// btnFPS
			// 
			this.btnFPS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnFPS.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtFPS});
			this.btnFPS.Name = "btnFPS";
			this.btnFPS.Size = new System.Drawing.Size(39, 19);
			this.btnFPS.Text = "FPS";
			this.btnFPS.DropDownClosed += new System.EventHandler(this.BtnFPS_DropDownClosed);
			// 
			// txtFPS
			// 
			this.txtFPS.Name = "txtFPS";
			this.txtFPS.Size = new System.Drawing.Size(100, 23);
			this.txtFPS.Text = "";
			this.txtFPS.TextChanged += new System.EventHandler(this.txtFPS_Change);
			// 
			// lblRenderDuration
			// 
			this.lblRenderDuration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.lblRenderDuration.Name = "lblRenderDuration";
			this.lblRenderDuration.Size = new System.Drawing.Size(23, 15);
			this.lblRenderDuration.Text = "ms";
			// 
			// lblRenderFreq
			// 
			this.lblRenderFreq.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.lblRenderFreq.Name = "lblRenderFreq";
			this.lblRenderFreq.Size = new System.Drawing.Size(21, 15);
			this.lblRenderFreq.Text = "Hz";
			// 
			// btnConfigure
			// 
			this.btnConfigure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnConfigure.Location = new System.Drawing.Point(237, 3);
			this.btnConfigure.Name = "btnConfigure";
			this.btnConfigure.Size = new System.Drawing.Size(69, 23);
			this.btnConfigure.TabIndex = 4;
			this.btnConfigure.Text = "Configure";
			this.btnConfigure.UseVisualStyleBackColor = true;
			this.btnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
			// 
			// frmRender
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panelMain);
			this.DoubleBuffered = true;
			this.Name = "frmRender";
			this.Text = "Designer_ProjectObject";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGLMain_FormClosing);
			this.Load += new System.EventHandler(this.FrmGLMain_Load);
			this.panelMain.Status.ResumeLayout(false);
			this.panelMain.Status.PerformLayout();
			this.panelMain.ResumeLayout(false);
			this.toolStripFPS.ResumeLayout(false);
			this.toolStripFPS.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.ToolStripStatusLabel lblRenderName;
		private System.Windows.Forms.ToolStripDropDownButton btnInterval;
		private System.Windows.Forms.ToolStripDropDownButton btnFPS;
		private System.Windows.Forms.ToolStripStatusLabel lblRenderDuration;
		private System.Windows.Forms.ToolStripStatusLabel lblRenderFreq;
		private System.Windows.Forms.ToolStripTextBox txtInterval;
		private System.Windows.Forms.ToolStripTextBox txtFPS;
		private ShaderToy_Components.controlProjectObject panelMain;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnConfigure;
		private System.Windows.Forms.ToolStrip toolStripFPS;
	}
}