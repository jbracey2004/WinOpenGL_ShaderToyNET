namespace WinOpenGL_ShaderToy
{
	partial class frmVertexDescription
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
			this.datagridVertexDescriptions = new System.Windows.Forms.DataGridView();
			this.columnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnComponentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnElementType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.columnElementCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lblName = new System.Windows.Forms.Label();
			this.panelMain.Content.SuspendLayout();
			this.panelMain.Status.SuspendLayout();
			this.panelMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.datagridVertexDescriptions)).BeginInit();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			// 
			// panelMain.Content
			// 
			this.panelMain.Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Content.Controls.Add(this.datagridVertexDescriptions);
			this.panelMain.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Content.ForeColor = System.Drawing.SystemColors.ControlText;
			this.panelMain.Content.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Content.Name = "Content";
			this.panelMain.Content.Size = new System.Drawing.Size(519, 260);
			this.panelMain.Content.TabIndex = 2;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Name = "panelMain";
			clsDesigner1.Name = "ProjectObject";
			this.panelMain.ProjectObject = clsDesigner1;
			this.panelMain.Size = new System.Drawing.Size(519, 290);
			// 
			// panelMain.Status
			// 
			this.panelMain.Status.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panelMain.Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Status.Controls.Add(this.lblName);
			this.panelMain.Status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelMain.Status.ForeColor = System.Drawing.SystemColors.Window;
			this.panelMain.Status.Location = new System.Drawing.Point(0, 260);
			this.panelMain.Status.Name = "Status";
			this.panelMain.Status.Size = new System.Drawing.Size(519, 30);
			this.panelMain.Status.TabIndex = 3;
			this.panelMain.TabIndex = 0;
			// 
			// datagridVertexDescriptions
			// 
			this.datagridVertexDescriptions.AllowUserToOrderColumns = true;
			this.datagridVertexDescriptions.AllowUserToResizeRows = false;
			this.datagridVertexDescriptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.datagridVertexDescriptions.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			this.datagridVertexDescriptions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnIndex,
            this.columnComponentName,
            this.columnElementType,
            this.columnElementCount});
			this.datagridVertexDescriptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridVertexDescriptions.Location = new System.Drawing.Point(0, 0);
			this.datagridVertexDescriptions.Name = "datagridVertexDescriptions";
			this.datagridVertexDescriptions.Size = new System.Drawing.Size(517, 258);
			this.datagridVertexDescriptions.TabIndex = 1;
			this.datagridVertexDescriptions.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DatagridVertexDescriptions_CellEndEdit);
			this.datagridVertexDescriptions.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DatagridVertexDescriptions_DataError);
			this.datagridVertexDescriptions.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DatagridVertexDescriptions_RowsAdded);
			this.datagridVertexDescriptions.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DatagridVertexDescriptions_RowsRemoved);
			// 
			// columnIndex
			// 
			this.columnIndex.HeaderText = "Index";
			this.columnIndex.Name = "columnIndex";
			this.columnIndex.Width = 58;
			// 
			// columnComponentName
			// 
			this.columnComponentName.HeaderText = "Component Name";
			this.columnComponentName.Name = "columnComponentName";
			this.columnComponentName.Width = 117;
			// 
			// columnElementType
			// 
			this.columnElementType.HeaderText = "Element Type";
			this.columnElementType.Name = "columnElementType";
			this.columnElementType.Width = 78;
			// 
			// columnElementCount
			// 
			this.columnElementCount.HeaderText = "Element Count";
			this.columnElementCount.Name = "columnElementCount";
			this.columnElementCount.Width = 101;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.BackColor = System.Drawing.Color.Transparent;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(3, 6);
			this.lblName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(117, 16);
			this.lblName.TabIndex = 4;
			this.lblName.Text = "Vertex Description";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// frmVertexDescription
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(519, 290);
			this.Controls.Add(this.panelMain);
			this.Name = "frmVertexDescription";
			this.Text = "Designer_ProjectObject";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProgram_FormClosing);
			this.Load += new System.EventHandler(this.FrmProgram_Load);
			this.panelMain.Content.ResumeLayout(false);
			this.panelMain.Status.ResumeLayout(false);
			this.panelMain.Status.PerformLayout();
			this.panelMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.datagridVertexDescriptions)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private ShaderToy_Components.controlProjectObject panelMain;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.DataGridView datagridVertexDescriptions;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnIndex;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnComponentName;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnElementType;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnElementCount;
	}
}