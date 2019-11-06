namespace WinOpenGL_ShaderToy
{
	partial class controlUniformData
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
			this.datagridData = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.datagridData)).BeginInit();
			this.SuspendLayout();
			// 
			// datagridData
			// 
			this.datagridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridData.ColumnHeadersVisible = false;
			this.datagridData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridData.Location = new System.Drawing.Point(0, 0);
			this.datagridData.Name = "datagridData";
			this.datagridData.ShowRowErrors = false;
			this.datagridData.Size = new System.Drawing.Size(553, 36);
			this.datagridData.TabIndex = 0;
			this.datagridData.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridData_CellValueChanged);
			// 
			// controlUniformData
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.datagridData);
			this.Name = "controlUniformData";
			this.Size = new System.Drawing.Size(553, 36);
			((System.ComponentModel.ISupportInitialize)(this.datagridData)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView datagridData;
	}
}
