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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.datagridData = new System.Windows.Forms.DataGridView();
			this.lstType = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.datagridData)).BeginInit();
			this.SuspendLayout();
			// 
			// datagridData
			// 
			this.datagridData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridData.BackgroundColor = System.Drawing.SystemColors.Control;
			this.datagridData.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.datagridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridData.ColumnHeadersVisible = false;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridData.DefaultCellStyle = dataGridViewCellStyle1;
			this.datagridData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridData.Location = new System.Drawing.Point(99, 0);
			this.datagridData.Name = "datagridData";
			this.datagridData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
			this.datagridData.ShowRowErrors = false;
			this.datagridData.Size = new System.Drawing.Size(172, 33);
			this.datagridData.TabIndex = 0;
			this.datagridData.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.datagridData_CellValidating);
			this.datagridData.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridData_CellValueChanged);
			this.datagridData.RowHeightChanged += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridData_RowHeightChanged);
			this.datagridData.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.datagridData_RowsAdded);
			this.datagridData.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.datagridData_RowsRemoved);
			this.datagridData.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridData_UserAddedRow);
			this.datagridData.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridData_UserDeletedRow);
			// 
			// lstType
			// 
			this.lstType.Dock = System.Windows.Forms.DockStyle.Left;
			this.lstType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstType.FormattingEnabled = true;
			this.lstType.Location = new System.Drawing.Point(0, 0);
			this.lstType.Name = "lstType";
			this.lstType.Size = new System.Drawing.Size(99, 21);
			this.lstType.TabIndex = 1;
			this.lstType.SelectedIndexChanged += new System.EventHandler(this.lstType_SelectedIndexChanged);
			// 
			// controlUniformData
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.datagridData);
			this.Controls.Add(this.lstType);
			this.Name = "controlUniformData";
			this.Size = new System.Drawing.Size(271, 33);
			this.Load += new System.EventHandler(this.controlUniformData_Load);
			((System.ComponentModel.ISupportInitialize)(this.datagridData)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView datagridData;
		private System.Windows.Forms.ComboBox lstType;
	}
}
