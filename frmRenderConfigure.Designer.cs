using ShaderToy_Components;
namespace WinOpenGL_ShaderToy
{
	partial class frmRenderConfigure
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panelSelectData = new System.Windows.Forms.FlowLayoutPanel();
			this.lblGeometry = new System.Windows.Forms.Label();
			this.lstGeometry = new System.Windows.Forms.ComboBox();
			this.lblProgram = new System.Windows.Forms.Label();
			this.lstProgram = new System.Windows.Forms.ComboBox();
			this.datagridGeometryRouting = new System.Windows.Forms.DataGridView();
			this.datagridUniformsRouting = new System.Windows.Forms.DataGridView();
			this.datagridUniformsValues = new System.Windows.Forms.DataGridView();
			this.columnVariableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnProgramAttr = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.columnVertDesc = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.columnProgramUniform = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.columnVarName = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.datagridEvents = new System.Windows.Forms.DataGridView();
			this.panelSelectData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.datagridGeometryRouting)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.datagridUniformsRouting)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.datagridUniformsValues)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.datagridEvents)).BeginInit();
			this.SuspendLayout();
			// 
			// panelSelectData
			// 
			this.panelSelectData.AutoSize = true;
			this.panelSelectData.BackColor = System.Drawing.SystemColors.ControlDark;
			this.panelSelectData.Controls.Add(this.lblGeometry);
			this.panelSelectData.Controls.Add(this.lstGeometry);
			this.panelSelectData.Controls.Add(this.lblProgram);
			this.panelSelectData.Controls.Add(this.lstProgram);
			this.panelSelectData.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelSelectData.Location = new System.Drawing.Point(0, 0);
			this.panelSelectData.Name = "panelSelectData";
			this.panelSelectData.Size = new System.Drawing.Size(643, 30);
			this.panelSelectData.TabIndex = 0;
			// 
			// lblGeometry
			// 
			this.lblGeometry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblGeometry.ForeColor = System.Drawing.SystemColors.Window;
			this.lblGeometry.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblGeometry.Location = new System.Drawing.Point(3, 0);
			this.lblGeometry.Name = "lblGeometry";
			this.lblGeometry.Size = new System.Drawing.Size(69, 29);
			this.lblGeometry.TabIndex = 8;
			this.lblGeometry.Text = "Geometry";
			this.lblGeometry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lstGeometry
			// 
			this.lstGeometry.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.lstGeometry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstGeometry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lstGeometry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstGeometry.ForeColor = System.Drawing.SystemColors.Window;
			this.lstGeometry.FormattingEnabled = true;
			this.lstGeometry.Location = new System.Drawing.Point(78, 3);
			this.lstGeometry.Name = "lstGeometry";
			this.lstGeometry.Size = new System.Drawing.Size(185, 24);
			this.lstGeometry.TabIndex = 9;
			this.lstGeometry.SelectedIndexChanged += new System.EventHandler(this.lstGeometry_SelectedIndexChanged);
			// 
			// lblProgram
			// 
			this.lblProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblProgram.ForeColor = System.Drawing.SystemColors.Window;
			this.lblProgram.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblProgram.Location = new System.Drawing.Point(269, 0);
			this.lblProgram.Name = "lblProgram";
			this.lblProgram.Size = new System.Drawing.Size(69, 29);
			this.lblProgram.TabIndex = 10;
			this.lblProgram.Text = "Program";
			this.lblProgram.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lstProgram
			// 
			this.lstProgram.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.lstProgram.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstProgram.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lstProgram.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstProgram.ForeColor = System.Drawing.SystemColors.Window;
			this.lstProgram.FormattingEnabled = true;
			this.lstProgram.Location = new System.Drawing.Point(344, 3);
			this.lstProgram.Name = "lstProgram";
			this.lstProgram.Size = new System.Drawing.Size(200, 24);
			this.lstProgram.TabIndex = 11;
			this.lstProgram.SelectedIndexChanged += new System.EventHandler(this.lstProgram_SelectedIndexChanged);
			// 
			// datagridGeometryRouting
			// 
			this.datagridGeometryRouting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridGeometryRouting.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridGeometryRouting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
			this.datagridGeometryRouting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridGeometryRouting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProgramAttr,
            this.columnVertDesc});
			dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridGeometryRouting.DefaultCellStyle = dataGridViewCellStyle14;
			this.datagridGeometryRouting.Dock = System.Windows.Forms.DockStyle.Top;
			this.datagridGeometryRouting.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridGeometryRouting.Location = new System.Drawing.Point(0, 30);
			this.datagridGeometryRouting.Name = "datagridGeometryRouting";
			dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridGeometryRouting.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
			this.datagridGeometryRouting.Size = new System.Drawing.Size(643, 123);
			this.datagridGeometryRouting.TabIndex = 1;
			this.datagridGeometryRouting.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridGeometryRouting_CellValueChanged);
			this.datagridGeometryRouting.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.datagridGeometryRouting_DataError);
			this.datagridGeometryRouting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridGeometryRouting_EditingControlShowing);
			this.datagridGeometryRouting.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridGeometryRouting_UserAddedRow);
			this.datagridGeometryRouting.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.datagridGeometryRouting_UserDeletingRow);
			// 
			// datagridUniformsRouting
			// 
			this.datagridUniformsRouting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridUniformsRouting.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsRouting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle16;
			this.datagridUniformsRouting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridUniformsRouting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProgramUniform,
            this.columnVarName});
			dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle17.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle17.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsRouting.DefaultCellStyle = dataGridViewCellStyle17;
			this.datagridUniformsRouting.Dock = System.Windows.Forms.DockStyle.Top;
			this.datagridUniformsRouting.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridUniformsRouting.Location = new System.Drawing.Point(0, 294);
			this.datagridUniformsRouting.Name = "datagridUniformsRouting";
			dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle18.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle18.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridUniformsRouting.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
			this.datagridUniformsRouting.Size = new System.Drawing.Size(643, 157);
			this.datagridUniformsRouting.TabIndex = 3;
			this.datagridUniformsRouting.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridUniformsRouting_CellValueChanged);
			this.datagridUniformsRouting.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.datagridUniformsRouting_DataError);
			this.datagridUniformsRouting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridUniformsRouting_EditingControlShowing);
			this.datagridUniformsRouting.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridUniformsRouting_UserAddedRow);
			this.datagridUniformsRouting.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.datagridUniformsRouting_UserDeletingRow);
			// 
			// datagridUniformsValues
			// 
			this.datagridUniformsValues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridUniformsValues.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsValues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
			this.datagridUniformsValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridUniformsValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnVariableName});
			dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle20.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle20.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsValues.DefaultCellStyle = dataGridViewCellStyle20;
			this.datagridUniformsValues.Dock = System.Windows.Forms.DockStyle.Top;
			this.datagridUniformsValues.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridUniformsValues.Location = new System.Drawing.Point(0, 153);
			this.datagridUniformsValues.Name = "datagridUniformsValues";
			dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle21.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle21.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridUniformsValues.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
			this.datagridUniformsValues.Size = new System.Drawing.Size(643, 141);
			this.datagridUniformsValues.TabIndex = 5;
			this.datagridUniformsValues.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridUniformsValues_CellEndEdit);
			this.datagridUniformsValues.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridUniformsValues_UserAddedRow);
			this.datagridUniformsValues.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.datagridUniformsValues_UserDeletingRow);
			this.datagridUniformsValues.KeyUp += new System.Windows.Forms.KeyEventHandler(this.datagridUniformsValues_KeyUp);
			// 
			// columnVariableName
			// 
			this.columnVariableName.HeaderText = "Variable Name";
			this.columnVariableName.Name = "columnVariableName";
			// 
			// columnProgramAttr
			// 
			this.columnProgramAttr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.columnProgramAttr.HeaderText = "Program Attribute";
			this.columnProgramAttr.Name = "columnProgramAttr";
			// 
			// columnVertDesc
			// 
			this.columnVertDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.columnVertDesc.HeaderText = "Vertex Description";
			this.columnVertDesc.Name = "columnVertDesc";
			// 
			// columnProgramUniform
			// 
			this.columnProgramUniform.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.columnProgramUniform.HeaderText = "Program Uniform";
			this.columnProgramUniform.Name = "columnProgramUniform";
			this.columnProgramUniform.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.columnProgramUniform.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// columnVarName
			// 
			this.columnVarName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.columnVarName.HeaderText = "Variable Name";
			this.columnVarName.Name = "columnVarName";
			this.columnVarName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.columnVarName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// datagridEvents
			// 
			this.datagridEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridEvents.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridEvents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
			this.datagridEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridEvents.DefaultCellStyle = dataGridViewCellStyle23;
			this.datagridEvents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridEvents.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridEvents.Location = new System.Drawing.Point(0, 451);
			this.datagridEvents.Name = "datagridEvents";
			dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle24.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle24.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle24.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle24.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridEvents.RowHeadersDefaultCellStyle = dataGridViewCellStyle24;
			this.datagridEvents.Size = new System.Drawing.Size(643, 123);
			this.datagridEvents.TabIndex = 7;
			this.datagridEvents.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridEvents_CellValueChanged);
			this.datagridEvents.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridEvents_EditingControlShowing);
			this.datagridEvents.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridEvents_UserAddedRow);
			this.datagridEvents.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.datagridEvents_UserDeletingRow);
			// 
			// frmRenderConfigure
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(643, 574);
			this.Controls.Add(this.datagridEvents);
			this.Controls.Add(this.datagridUniformsRouting);
			this.Controls.Add(this.datagridUniformsValues);
			this.Controls.Add(this.datagridGeometryRouting);
			this.Controls.Add(this.panelSelectData);
			this.ForeColor = System.Drawing.SystemColors.Control;
			this.Name = "frmRenderConfigure";
			this.Text = "Render Configuation";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmSRenderConfigure_FormClosing);
			this.Load += new System.EventHandler(this.frmRenderConfigure_Load);
			this.panelSelectData.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.datagridGeometryRouting)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.datagridUniformsRouting)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.datagridUniformsValues)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.datagridEvents)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel panelSelectData;
		private System.Windows.Forms.Label lblGeometry;
		private System.Windows.Forms.ComboBox lstGeometry;
		private System.Windows.Forms.Label lblProgram;
		private System.Windows.Forms.ComboBox lstProgram;
		private System.Windows.Forms.DataGridView datagridGeometryRouting;
		private System.Windows.Forms.DataGridView datagridUniformsRouting;
		private System.Windows.Forms.DataGridView datagridUniformsValues;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnVariableName;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnProgramAttr;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnVertDesc;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnProgramUniform;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnVarName;
		private System.Windows.Forms.DataGridView datagridEvents;
	}
}