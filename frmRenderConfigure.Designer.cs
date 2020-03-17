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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panelSelectData = new System.Windows.Forms.FlowLayoutPanel();
			this.lblGeometry = new System.Windows.Forms.Label();
			this.lstGeometry = new System.Windows.Forms.ComboBox();
			this.lblProgram = new System.Windows.Forms.Label();
			this.lstProgram = new System.Windows.Forms.ComboBox();
			this.datagridGeometryRouting = new System.Windows.Forms.DataGridView();
			this.columnProgramAttr = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.columnVertDesc = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.datagridUniformsRouting = new System.Windows.Forms.DataGridView();
			this.columnProgramUniform = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.columnVarName = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.splitter2 = new System.Windows.Forms.Splitter();
			this.datagridUniformsValues = new System.Windows.Forms.DataGridView();
			this.columnVariableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.splitter3 = new System.Windows.Forms.Splitter();
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
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridGeometryRouting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.datagridGeometryRouting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridGeometryRouting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProgramAttr,
            this.columnVertDesc});
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridGeometryRouting.DefaultCellStyle = dataGridViewCellStyle2;
			this.datagridGeometryRouting.Dock = System.Windows.Forms.DockStyle.Top;
			this.datagridGeometryRouting.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridGeometryRouting.Location = new System.Drawing.Point(0, 30);
			this.datagridGeometryRouting.Name = "datagridGeometryRouting";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridGeometryRouting.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.datagridGeometryRouting.Size = new System.Drawing.Size(643, 91);
			this.datagridGeometryRouting.TabIndex = 1;
			this.datagridGeometryRouting.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridGeometryRouting_CellValueChanged);
			this.datagridGeometryRouting.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.datagridGeometryRouting_DataError);
			this.datagridGeometryRouting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridGeometryRouting_EditingControlShowing);
			this.datagridGeometryRouting.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridGeometryRouting_UserAddedRow);
			this.datagridGeometryRouting.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.datagridGeometryRouting_UserDeletingRow);
			// 
			// columnProgramAttr
			// 
			this.columnProgramAttr.HeaderText = "Program Attribute";
			this.columnProgramAttr.Name = "columnProgramAttr";
			// 
			// columnVertDesc
			// 
			this.columnVertDesc.HeaderText = "Vertex Description";
			this.columnVertDesc.Name = "columnVertDesc";
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 121);
			this.splitter1.Margin = new System.Windows.Forms.Padding(0);
			this.splitter1.MinExtra = 0;
			this.splitter1.MinSize = 0;
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(643, 6);
			this.splitter1.TabIndex = 2;
			this.splitter1.TabStop = false;
			// 
			// datagridUniformsRouting
			// 
			this.datagridUniformsRouting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridUniformsRouting.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsRouting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
			this.datagridUniformsRouting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridUniformsRouting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProgramUniform,
            this.columnVarName});
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsRouting.DefaultCellStyle = dataGridViewCellStyle5;
			this.datagridUniformsRouting.Dock = System.Windows.Forms.DockStyle.Top;
			this.datagridUniformsRouting.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridUniformsRouting.Location = new System.Drawing.Point(0, 274);
			this.datagridUniformsRouting.Name = "datagridUniformsRouting";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridUniformsRouting.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.datagridUniformsRouting.Size = new System.Drawing.Size(643, 157);
			this.datagridUniformsRouting.TabIndex = 3;
			this.datagridUniformsRouting.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridUniformsRouting_CellValueChanged);
			this.datagridUniformsRouting.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.datagridUniformsRouting_DataError);
			this.datagridUniformsRouting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridUniformsRouting_EditingControlShowing);
			this.datagridUniformsRouting.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridUniformsRouting_UserAddedRow);
			this.datagridUniformsRouting.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.datagridUniformsRouting_UserDeletingRow);
			// 
			// columnProgramUniform
			// 
			this.columnProgramUniform.HeaderText = "Program Uniform";
			this.columnProgramUniform.Name = "columnProgramUniform";
			this.columnProgramUniform.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.columnProgramUniform.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// columnVarName
			// 
			this.columnVarName.HeaderText = "Variable Name";
			this.columnVarName.Name = "columnVarName";
			this.columnVarName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.columnVarName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// splitter2
			// 
			this.splitter2.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter2.Location = new System.Drawing.Point(0, 268);
			this.splitter2.Margin = new System.Windows.Forms.Padding(0);
			this.splitter2.MinExtra = 0;
			this.splitter2.MinSize = 0;
			this.splitter2.Name = "splitter2";
			this.splitter2.Size = new System.Drawing.Size(643, 6);
			this.splitter2.TabIndex = 4;
			this.splitter2.TabStop = false;
			// 
			// datagridUniformsValues
			// 
			this.datagridUniformsValues.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridUniformsValues.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsValues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			this.datagridUniformsValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridUniformsValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnVariableName});
			dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsValues.DefaultCellStyle = dataGridViewCellStyle8;
			this.datagridUniformsValues.Dock = System.Windows.Forms.DockStyle.Top;
			this.datagridUniformsValues.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridUniformsValues.Location = new System.Drawing.Point(0, 127);
			this.datagridUniformsValues.Name = "datagridUniformsValues";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridUniformsValues.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.datagridUniformsValues.Size = new System.Drawing.Size(643, 141);
			this.datagridUniformsValues.TabIndex = 5;
			this.datagridUniformsValues.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridUniformsValues_CellValueChanged);
			this.datagridUniformsValues.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridUniformsValues_UserAddedRow);
			this.datagridUniformsValues.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.datagridUniformsValues_UserDeletingRow);
			this.datagridUniformsValues.KeyUp += new System.Windows.Forms.KeyEventHandler(this.datagridUniformsValues_KeyUp);
			// 
			// columnVariableName
			// 
			this.columnVariableName.HeaderText = "Variable Name";
			this.columnVariableName.Name = "columnVariableName";
			// 
			// splitter3
			// 
			this.splitter3.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter3.Location = new System.Drawing.Point(0, 431);
			this.splitter3.Margin = new System.Windows.Forms.Padding(0);
			this.splitter3.MinExtra = 0;
			this.splitter3.MinSize = 0;
			this.splitter3.Name = "splitter3";
			this.splitter3.Size = new System.Drawing.Size(643, 6);
			this.splitter3.TabIndex = 6;
			this.splitter3.TabStop = false;
			// 
			// datagridEvents
			// 
			this.datagridEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridEvents.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridEvents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.datagridEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridEvents.DefaultCellStyle = dataGridViewCellStyle11;
			this.datagridEvents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridEvents.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridEvents.Location = new System.Drawing.Point(0, 437);
			this.datagridEvents.Name = "datagridEvents";
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridEvents.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
			this.datagridEvents.Size = new System.Drawing.Size(643, 137);
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
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(643, 574);
			this.Controls.Add(this.datagridEvents);
			this.Controls.Add(this.splitter3);
			this.Controls.Add(this.datagridUniformsRouting);
			this.Controls.Add(this.splitter2);
			this.Controls.Add(this.datagridUniformsValues);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.datagridGeometryRouting);
			this.Controls.Add(this.panelSelectData);
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
		private System.Windows.Forms.DataGridViewComboBoxColumn columnProgramAttr;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnVertDesc;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.DataGridView datagridUniformsRouting;
		private System.Windows.Forms.Splitter splitter2;
		private System.Windows.Forms.DataGridView datagridUniformsValues;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnVariableName;
		private System.Windows.Forms.Splitter splitter3;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnProgramUniform;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnVarName;
		private System.Windows.Forms.DataGridView datagridEvents;
	}
}