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
			this.columnUniformName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.columnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.datagridUniforms)).BeginInit();
			this.SuspendLayout();
			// 
			// datagridUniforms
			// 
			this.datagridUniforms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridUniforms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnUniformName,
            this.columnType,
            this.columnValue});
			this.datagridUniforms.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.datagridUniforms.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridUniforms.Location = new System.Drawing.Point(0, 0);
			this.datagridUniforms.Margin = new System.Windows.Forms.Padding(4);
			this.datagridUniforms.Name = "datagridUniforms";
			this.datagridUniforms.Size = new System.Drawing.Size(616, 324);
			this.datagridUniforms.TabIndex = 0;
			this.datagridUniforms.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DatagridUniforms_CellBeginEdit);
			this.datagridUniforms.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DatagridUniforms_EditingControlShowing);
			// 
			// columnUniformName
			// 
			this.columnUniformName.HeaderText = "Name";
			this.columnUniformName.Name = "columnUniformName";
			// 
			// columnType
			// 
			this.columnType.HeaderText = "Type";
			this.columnType.Name = "columnType";
			// 
			// columnValue
			// 
			this.columnValue.HeaderText = "Value";
			this.columnValue.Name = "columnValue";
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
		private System.Windows.Forms.DataGridViewTextBoxColumn columnUniformName;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnType;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnValue;
	}
}