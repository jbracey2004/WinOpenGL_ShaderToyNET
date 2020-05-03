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
			this.panelMain = new ShaderToy_Components.controlProjectObject();
			this.lblName = new System.Windows.Forms.Label();
			this.btnConfigure = new System.Windows.Forms.Button();
			this.panelFrameCount = new System.Windows.Forms.FlowLayoutPanel();
			this.lblFrameCount = new System.Windows.Forms.Label();
			this.lblFrameNumber = new System.Windows.Forms.Label();
			this.lblFrameTimeStamp = new System.Windows.Forms.Label();
			this.toolStripFPS = new System.Windows.Forms.ToolStrip();
			this.btnInterval = new System.Windows.Forms.ToolStripDropDownButton();
			this.txtInterval = new System.Windows.Forms.ToolStripTextBox();
			this.btnFPS = new System.Windows.Forms.ToolStripDropDownButton();
			this.txtFPS = new System.Windows.Forms.ToolStripTextBox();
			this.lblRenderDuration = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblRenderFreq = new System.Windows.Forms.ToolStripStatusLabel();
			this.lblRenderName = new System.Windows.Forms.ToolStripStatusLabel();
			this.panelMain.Status.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.panelFrameCount.SuspendLayout();
			this.toolStripFPS.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			// 
			// panelMain.Content
			// 
			this.panelMain.Content.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panelMain.Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Content.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Content.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Content.Name = "Content";
			this.panelMain.Content.Size = new System.Drawing.Size(800, 420);
			this.panelMain.Content.TabIndex = 2;
			this.panelMain.Content.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Content_Paint);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.ForeColor = System.Drawing.SystemColors.Window;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Name = "panelMain";
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
			this.panelMain.Status.Controls.Add(this.panelFrameCount);
			this.panelMain.Status.Controls.Add(this.toolStripFPS);
			this.panelMain.Status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelMain.Status.Location = new System.Drawing.Point(0, 420);
			this.panelMain.Status.Name = "Status";
			this.panelMain.Status.Size = new System.Drawing.Size(800, 30);
			this.panelMain.Status.TabIndex = 3;
			this.panelMain.TabIndex = 0;
			this.panelMain.Timer_IntervalUpdate = null;
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
			// btnConfigure
			// 
			this.btnConfigure.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.btnConfigure.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnConfigure.Location = new System.Drawing.Point(237, 3);
			this.btnConfigure.Name = "btnConfigure";
			this.btnConfigure.Size = new System.Drawing.Size(69, 23);
			this.btnConfigure.TabIndex = 4;
			this.btnConfigure.Text = "Configure";
			this.btnConfigure.UseVisualStyleBackColor = false;
			this.btnConfigure.Click += new System.EventHandler(this.btnConfigure_Click);
			// 
			// panelFrameCount
			// 
			this.panelFrameCount.AutoSize = true;
			this.panelFrameCount.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.panelFrameCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelFrameCount.Controls.Add(this.lblFrameCount);
			this.panelFrameCount.Controls.Add(this.lblFrameNumber);
			this.panelFrameCount.Controls.Add(this.lblFrameTimeStamp);
			this.panelFrameCount.Location = new System.Drawing.Point(312, 3);
			this.panelFrameCount.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.panelFrameCount.Name = "panelFrameCount";
			this.panelFrameCount.Size = new System.Drawing.Size(132, 20);
			this.panelFrameCount.TabIndex = 9;
			// 
			// lblFrameCount
			// 
			this.lblFrameCount.AutoSize = true;
			this.lblFrameCount.BackColor = System.Drawing.Color.Transparent;
			this.lblFrameCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblFrameCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFrameCount.Location = new System.Drawing.Point(0, 0);
			this.lblFrameCount.Margin = new System.Windows.Forms.Padding(0);
			this.lblFrameCount.Name = "lblFrameCount";
			this.lblFrameCount.Size = new System.Drawing.Size(47, 16);
			this.lblFrameCount.TabIndex = 6;
			this.lblFrameCount.Text = "Frame";
			this.lblFrameCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblFrameCount.DoubleClick += new System.EventHandler(this.lblFrameCount_DoubleClick);
			this.lblFrameCount.MouseEnter += new System.EventHandler(this.FrameCount_MouseEnter);
			this.lblFrameCount.MouseLeave += new System.EventHandler(this.FrameCount_MouseLeave);
			// 
			// lblFrameNumber
			// 
			this.lblFrameNumber.AutoSize = true;
			this.lblFrameNumber.BackColor = System.Drawing.Color.Transparent;
			this.lblFrameNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblFrameNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFrameNumber.Location = new System.Drawing.Point(50, 0);
			this.lblFrameNumber.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.lblFrameNumber.Name = "lblFrameNumber";
			this.lblFrameNumber.Size = new System.Drawing.Size(24, 15);
			this.lblFrameNumber.TabIndex = 8;
			this.lblFrameNumber.Text = "0.0";
			this.lblFrameNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblFrameNumber.DoubleClick += new System.EventHandler(this.lblFrameNumber_DoubleClick);
			this.lblFrameNumber.MouseEnter += new System.EventHandler(this.FrameCount_MouseEnter);
			this.lblFrameNumber.MouseLeave += new System.EventHandler(this.FrameCount_MouseLeave);
			// 
			// lblFrameTimeStamp
			// 
			this.lblFrameTimeStamp.AutoSize = true;
			this.lblFrameTimeStamp.BackColor = System.Drawing.Color.Transparent;
			this.lblFrameTimeStamp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblFrameTimeStamp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblFrameTimeStamp.Location = new System.Drawing.Point(77, 0);
			this.lblFrameTimeStamp.Name = "lblFrameTimeStamp";
			this.lblFrameTimeStamp.Size = new System.Drawing.Size(48, 15);
			this.lblFrameTimeStamp.TabIndex = 7;
			this.lblFrameTimeStamp.Text = "0.00000";
			this.lblFrameTimeStamp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblFrameTimeStamp.DoubleClick += new System.EventHandler(this.lblFrameTimeStamp_DoubleClick);
			this.lblFrameTimeStamp.MouseEnter += new System.EventHandler(this.FrameCount_MouseEnter);
			this.lblFrameTimeStamp.MouseLeave += new System.EventHandler(this.FrameCount_MouseLeave);
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
			this.toolStripFPS.Location = new System.Drawing.Point(447, 0);
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
			this.btnInterval.BackColor = System.Drawing.Color.Transparent;
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
			this.txtInterval.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.txtInterval.Name = "txtInterval";
			this.txtInterval.Size = new System.Drawing.Size(100, 23);
			this.txtInterval.TextChanged += new System.EventHandler(this.txtInterval_Change);
			// 
			// btnFPS
			// 
			this.btnFPS.BackColor = System.Drawing.Color.Transparent;
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
			this.txtFPS.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.txtFPS.Name = "txtFPS";
			this.txtFPS.Size = new System.Drawing.Size(100, 23);
			this.txtFPS.TextChanged += new System.EventHandler(this.txtFPS_Change);
			// 
			// lblRenderDuration
			// 
			this.lblRenderDuration.BackColor = System.Drawing.Color.Transparent;
			this.lblRenderDuration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.lblRenderDuration.Name = "lblRenderDuration";
			this.lblRenderDuration.Size = new System.Drawing.Size(23, 15);
			this.lblRenderDuration.Text = "ms";
			// 
			// lblRenderFreq
			// 
			this.lblRenderFreq.BackColor = System.Drawing.Color.Transparent;
			this.lblRenderFreq.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.lblRenderFreq.Name = "lblRenderFreq";
			this.lblRenderFreq.Size = new System.Drawing.Size(21, 15);
			this.lblRenderFreq.Text = "Hz";
			// 
			// lblRenderName
			// 
			this.lblRenderName.Name = "lblRenderName";
			this.lblRenderName.Size = new System.Drawing.Size(44, 20);
			this.lblRenderName.Text = "Render";
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
			this.panelFrameCount.ResumeLayout(false);
			this.panelFrameCount.PerformLayout();
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
		private System.Windows.Forms.Label lblFrameCount;
		private System.Windows.Forms.Label lblFrameTimeStamp;
		private System.Windows.Forms.Label lblFrameNumber;
		private System.Windows.Forms.FlowLayoutPanel panelFrameCount;
	}
}