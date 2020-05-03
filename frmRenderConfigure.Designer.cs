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
			this.panelEvents = new System.Windows.Forms.Panel();
			this.datagridEvents = new System.Windows.Forms.DataGridView();
			this.panelUniformsRouting = new System.Windows.Forms.Panel();
			this.datagridUniformsRouting = new System.Windows.Forms.DataGridView();
			this.columnProgramUniform = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.columnVarName = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.panelUniformsValues = new System.Windows.Forms.Panel();
			this.datagridUniformsValues = new System.Windows.Forms.DataGridView();
			this.columnVariableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panelGeometryRouting = new System.Windows.Forms.Panel();
			this.datagridGeometryRouting = new System.Windows.Forms.DataGridView();
			this.columnProgramAttr = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.columnVertDesc = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.panelEventsConsole = new System.Windows.Forms.Panel();
			this.panelSelectData.SuspendLayout();
			this.panelEvents.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.datagridEvents)).BeginInit();
			this.panelUniformsRouting.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.datagridUniformsRouting)).BeginInit();
			this.panelUniformsValues.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.datagridUniformsValues)).BeginInit();
			this.panelGeometryRouting.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.datagridGeometryRouting)).BeginInit();
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
			// panelEvents
			// 
			this.panelEvents.Controls.Add(this.datagridEvents);
			this.panelEvents.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEvents.Location = new System.Drawing.Point(0, 446);
			this.panelEvents.Name = "panelEvents";
			this.panelEvents.Size = new System.Drawing.Size(643, 103);
			this.panelEvents.TabIndex = 8;
			// 
			// datagridEvents
			// 
			this.datagridEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridEvents.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridEvents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.datagridEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridEvents.DefaultCellStyle = dataGridViewCellStyle2;
			this.datagridEvents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridEvents.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridEvents.Location = new System.Drawing.Point(0, 0);
			this.datagridEvents.Name = "datagridEvents";
			dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridEvents.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
			this.datagridEvents.Size = new System.Drawing.Size(643, 103);
			this.datagridEvents.TabIndex = 8;
			this.datagridEvents.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridEvents_CellValueChanged);
			this.datagridEvents.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridEvents_EditingControlShowing);
			this.datagridEvents.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridEvents_UserAddedRow);
			this.datagridEvents.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.datagridEvents_UserDeletingRow);
			// 
			// panelUniformsRouting
			// 
			this.panelUniformsRouting.Controls.Add(this.datagridUniformsRouting);
			this.panelUniformsRouting.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelUniformsRouting.Location = new System.Drawing.Point(0, 30);
			this.panelUniformsRouting.Name = "panelUniformsRouting";
			this.panelUniformsRouting.Size = new System.Drawing.Size(643, 130);
			this.panelUniformsRouting.TabIndex = 9;
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
			this.datagridUniformsRouting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridUniformsRouting.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridUniformsRouting.Location = new System.Drawing.Point(0, 0);
			this.datagridUniformsRouting.Name = "datagridUniformsRouting";
			dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridUniformsRouting.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
			this.datagridUniformsRouting.Size = new System.Drawing.Size(643, 130);
			this.datagridUniformsRouting.TabIndex = 4;
			this.datagridUniformsRouting.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridUniformsRouting_CellValueChanged);
			this.datagridUniformsRouting.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.datagridUniformsRouting_DataError);
			this.datagridUniformsRouting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridUniformsRouting_EditingControlShowing);
			this.datagridUniformsRouting.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridUniformsRouting_UserAddedRow);
			this.datagridUniformsRouting.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.datagridUniformsRouting_UserDeletingRow);
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
			// panelUniformsValues
			// 
			this.panelUniformsValues.Controls.Add(this.datagridUniformsValues);
			this.panelUniformsValues.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelUniformsValues.Location = new System.Drawing.Point(0, 160);
			this.panelUniformsValues.Name = "panelUniformsValues";
			this.panelUniformsValues.Size = new System.Drawing.Size(643, 140);
			this.panelUniformsValues.TabIndex = 10;
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
			this.datagridUniformsValues.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridUniformsValues.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridUniformsValues.Location = new System.Drawing.Point(0, 0);
			this.datagridUniformsValues.Name = "datagridUniformsValues";
			dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridUniformsValues.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
			this.datagridUniformsValues.Size = new System.Drawing.Size(643, 140);
			this.datagridUniformsValues.TabIndex = 6;
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
			// panelGeometryRouting
			// 
			this.panelGeometryRouting.Controls.Add(this.datagridGeometryRouting);
			this.panelGeometryRouting.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelGeometryRouting.Location = new System.Drawing.Point(0, 300);
			this.panelGeometryRouting.Name = "panelGeometryRouting";
			this.panelGeometryRouting.Size = new System.Drawing.Size(643, 146);
			this.panelGeometryRouting.TabIndex = 11;
			// 
			// datagridGeometryRouting
			// 
			this.datagridGeometryRouting.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridGeometryRouting.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridGeometryRouting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
			this.datagridGeometryRouting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridGeometryRouting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProgramAttr,
            this.columnVertDesc});
			dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridGeometryRouting.DefaultCellStyle = dataGridViewCellStyle11;
			this.datagridGeometryRouting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridGeometryRouting.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridGeometryRouting.Location = new System.Drawing.Point(0, 0);
			this.datagridGeometryRouting.Name = "datagridGeometryRouting";
			dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridGeometryRouting.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
			this.datagridGeometryRouting.Size = new System.Drawing.Size(643, 146);
			this.datagridGeometryRouting.TabIndex = 2;
			this.datagridGeometryRouting.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.datagridGeometryRouting_CellValueChanged);
			this.datagridGeometryRouting.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.datagridGeometryRouting_DataError);
			this.datagridGeometryRouting.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.datagridGeometryRouting_EditingControlShowing);
			this.datagridGeometryRouting.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.datagridGeometryRouting_UserAddedRow);
			this.datagridGeometryRouting.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.datagridGeometryRouting_UserDeletingRow);
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
			// panelEventsConsole
			// 
			this.panelEventsConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panelEventsConsole.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEventsConsole.Location = new System.Drawing.Point(0, 549);
			this.panelEventsConsole.Name = "panelEventsConsole";
			this.panelEventsConsole.Size = new System.Drawing.Size(643, 88);
			this.panelEventsConsole.TabIndex = 12;
			// 
			// frmRenderConfigure
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(643, 645);
			this.Controls.Add(this.panelEventsConsole);
			this.Controls.Add(this.panelEvents);
			this.Controls.Add(this.panelGeometryRouting);
			this.Controls.Add(this.panelUniformsValues);
			this.Controls.Add(this.panelUniformsRouting);
			this.Controls.Add(this.panelSelectData);
			this.ForeColor = System.Drawing.SystemColors.Control;
			this.Name = "frmRenderConfigure";
			this.Text = "Render Configuation";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRenderConfigure_FormClosing);
			this.Load += new System.EventHandler(this.frmRenderConfigure_Load);
			this.panelSelectData.ResumeLayout(false);
			this.panelEvents.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.datagridEvents)).EndInit();
			this.panelUniformsRouting.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.datagridUniformsRouting)).EndInit();
			this.panelUniformsValues.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.datagridUniformsValues)).EndInit();
			this.panelGeometryRouting.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.datagridGeometryRouting)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.FlowLayoutPanel panelSelectData;
		private System.Windows.Forms.Label lblGeometry;
		private System.Windows.Forms.ComboBox lstGeometry;
		private System.Windows.Forms.Label lblProgram;
		private System.Windows.Forms.ComboBox lstProgram;
		private System.Windows.Forms.Panel panelEvents;
		private System.Windows.Forms.DataGridView datagridEvents;
		private System.Windows.Forms.Panel panelUniformsRouting;
		private System.Windows.Forms.DataGridView datagridUniformsRouting;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnProgramUniform;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnVarName;
		private System.Windows.Forms.Panel panelUniformsValues;
		private System.Windows.Forms.DataGridView datagridUniformsValues;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnVariableName;
		private System.Windows.Forms.Panel panelGeometryRouting;
		private System.Windows.Forms.DataGridView datagridGeometryRouting;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnProgramAttr;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnVertDesc;
		private System.Windows.Forms.Panel panelEventsConsole;
	}
}