namespace WinOpenGL_ShaderToy
{
	partial class frmShaderIOLinks
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
			ShaderToy_Components.controlProjectObject.clsDesigner clsDesigner2 = new ShaderToy_Components.controlProjectObject.clsDesigner();
			this.panelMain = new ShaderToy_Components.controlProjectObject();
			this.lblName = new System.Windows.Forms.Label();
			this.panelMain.Status.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			// 
			// panelMain.Content
			// 
			this.panelMain.Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Content.ForeColor = System.Drawing.SystemColors.ControlText;
			this.panelMain.Content.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Content.Name = "Content";
			this.panelMain.Content.Size = new System.Drawing.Size(800, 420);
			this.panelMain.Content.TabIndex = 2;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Name = "panelMain";
			this.panelMain.ParentControl = null;
			clsDesigner2.Name = "ProjectObject";
			clsDesigner2.ParentControl = null;
			this.panelMain.ProjectObject = clsDesigner2;
			this.panelMain.Size = new System.Drawing.Size(800, 450);
			// 
			// panelMain.Status
			// 
			this.panelMain.Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Status.Controls.Add(this.lblName);
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
			this.lblName.Size = new System.Drawing.Size(102, 16);
			this.lblName.TabIndex = 4;
			this.lblName.Text = "Shader IO Links";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// frmShaderIOLinks
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.panelMain);
			this.Name = "frmShaderIOLinks";
			this.Text = "frmShaderIOLinks";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmShaderIOLinks_FormClosing);
			this.Load += new System.EventHandler(this.frmShaderIOLinks_Load);
			this.panelMain.Status.ResumeLayout(false);
			this.panelMain.Status.PerformLayout();
			this.panelMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ShaderToy_Components.controlProjectObject panelMain;
		private System.Windows.Forms.Label lblName;
	}
}