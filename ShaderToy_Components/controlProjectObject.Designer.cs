using System.Windows.Forms;
namespace ShaderToy_Components
{
	partial class controlProjectObject : UserControl
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
			this.panelContent = new System.Windows.Forms.Panel();
			this.panelStatus = new System.Windows.Forms.FlowLayoutPanel();
			this.txtName = new System.Windows.Forms.TextBox();
			this.panelStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelContent
			// 
			this.panelContent.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.panelContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelContent.ForeColor = System.Drawing.SystemColors.Window;
			this.panelContent.Location = new System.Drawing.Point(0, 0);
			this.panelContent.Name = "panelContent";
			this.panelContent.Size = new System.Drawing.Size(739, 480);
			this.panelContent.TabIndex = 2;
			// 
			// panelStatus
			// 
			this.panelStatus.AutoSize = true;
			this.panelStatus.BackColor = System.Drawing.Color.Gray;
			this.panelStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelStatus.Controls.Add(this.txtName);
			this.panelStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelStatus.ForeColor = System.Drawing.SystemColors.Window;
			this.panelStatus.Location = new System.Drawing.Point(0, 480);
			this.panelStatus.Name = "panelStatus";
			this.panelStatus.Size = new System.Drawing.Size(739, 30);
			this.panelStatus.TabIndex = 3;
			// 
			// txtName
			// 
			this.txtName.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtName.ForeColor = System.Drawing.SystemColors.Window;
			this.txtName.Location = new System.Drawing.Point(3, 3);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(157, 22);
			this.txtName.TabIndex = 1;
			this.txtName.TextChanged += new System.EventHandler(this.TxtName_TextChanged);
			// 
			// controlProjectObject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.Controls.Add(this.panelContent);
			this.Controls.Add(this.panelStatus);
			this.ForeColor = System.Drawing.SystemColors.Window;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "controlProjectObject";
			this.Size = new System.Drawing.Size(739, 510);
			this.HandleCreated += new System.EventHandler(this.FrmProjectObject_HandleCreated);
			this.HandleDestroyed += new System.EventHandler(this.FrmProjectObject_HandleDestroyed);
			this.Disposed += new System.EventHandler(this.FrmProjectObject_Disposed);
			this.panelStatus.ResumeLayout(false);
			this.panelStatus.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panelContent;
		private System.Windows.Forms.FlowLayoutPanel panelStatus;
		public System.Windows.Forms.TextBox txtName;
	}
}
