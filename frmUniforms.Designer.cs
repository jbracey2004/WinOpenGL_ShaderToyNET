namespace WinOpenGL_ShaderToy
{
	partial class frmUniforms
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
			this.datagridUniforms = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnUniformName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.datagridUniforms)).BeginInit();
			this.SuspendLayout();
			// 
			// datagridUniforms
			// 
			this.datagridUniforms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridUniforms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnUniformName,
            this.columnType});
			this.datagridUniforms.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.datagridUniforms.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridUniforms.Location = new System.Drawing.Point(0, 0);
			this.datagridUniforms.Margin = new System.Windows.Forms.Padding(4);
			this.datagridUniforms.Name = "datagridUniforms";
			this.datagridUniforms.Size = new System.Drawing.Size(616, 324);
			this.datagridUniforms.TabIndex = 0;
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.HeaderText = "Name";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.HeaderText = "Value";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			// 
			// columnUniformName
			// 
			this.columnUniformName.HeaderText = "Name";
			this.columnUniformName.Name = "columnUniformName";
			this.columnUniformName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			// 
			// columnType
			// 
			this.columnType.HeaderText = "Type";
			this.columnType.Name = "columnType";
			// 
			// frmUniforms
			// 
			this.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(616, 324);
			this.Controls.Add(this.datagridUniforms);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "frmUniforms";
			this.Text = "Uniforms";
			this.Load += new System.EventHandler(this.FrmUniforms_Load);
			((System.ComponentModel.ISupportInitialize)(this.datagridUniforms)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView datagridUniforms;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnUniformName;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnType;
	}
}