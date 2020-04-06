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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panelSelectData = new System.Windows.Forms.FlowLayoutPanel();
			this.lblGeometry = new System.Windows.Forms.Label();
			this.lstGeometry = new System.Windows.Forms.ComboBox();
			this.lblProgram = new System.Windows.Forms.Label();
			this.lstProgram = new System.Windows.Forms.ComboBox();
			this.panelEvents = new System.Windows.Forms.Panel();
			this.datagridEvents = new System.Windows.Forms.DataGridView();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.panelEventsConsole = new System.Windows.Forms.Panel();
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
			this.panelEvents.Controls.Add(this.splitter1);
			this.panelEvents.Controls.Add(this.panelEventsConsole);
			this.panelEvents.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEvents.Location = new System.Drawing.Point(0, 446);
			this.panelEvents.Name = "panelEvents";
			this.panelEvents.Size = new System.Drawing.Size(643, 126);
			this.panelEvents.TabIndex = 8;
			// 
			// datagridEvents
			// 
			this.datagridEvents.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridEvents.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle25.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle25.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle25.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle25.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle25.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridEvents.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle25;
			this.datagridEvents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle26.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle26.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle26.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle26.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle26.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridEvents.DefaultCellStyle = dataGridViewCellStyle26;
			this.datagridEvents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridEvents.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridEvents.Location = new System.Drawing.Point(0, 0);
			this.datagridEvents.Name = "datagridEvents";
			dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle27.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle27.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridEvents.RowHeadersDefaultCellStyle = dataGridViewCellStyle27;
			this.datagridEvents.Size = new System.Drawing.Size(643, 96);
			this.datagridEvents.TabIndex = 8;
			// 
			// splitter1
			// 
			this.splitter1.BackColor = System.Drawing.SystemColors.Control;
			this.splitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 96);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(643, 8);
			this.splitter1.TabIndex = 10;
			this.splitter1.TabStop = false;
			// 
			// panelEventsConsole
			// 
			this.panelEventsConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.panelEventsConsole.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelEventsConsole.Location = new System.Drawing.Point(0, 104);
			this.panelEventsConsole.Name = "panelEventsConsole";
			this.panelEventsConsole.Size = new System.Drawing.Size(643, 22);
			this.panelEventsConsole.TabIndex = 11;
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
			dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle28.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle28.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsRouting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle28;
			this.datagridUniformsRouting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridUniformsRouting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProgramUniform,
            this.columnVarName});
			dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle29.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle29.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsRouting.DefaultCellStyle = dataGridViewCellStyle29;
			this.datagridUniformsRouting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridUniformsRouting.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridUniformsRouting.Location = new System.Drawing.Point(0, 0);
			this.datagridUniformsRouting.Name = "datagridUniformsRouting";
			dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle30.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridUniformsRouting.RowHeadersDefaultCellStyle = dataGridViewCellStyle30;
			this.datagridUniformsRouting.Size = new System.Drawing.Size(643, 130);
			this.datagridUniformsRouting.TabIndex = 4;
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
			dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle31.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle31.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsValues.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
			this.datagridUniformsValues.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridUniformsValues.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnVariableName});
			dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle32.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle32.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridUniformsValues.DefaultCellStyle = dataGridViewCellStyle32;
			this.datagridUniformsValues.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridUniformsValues.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridUniformsValues.Location = new System.Drawing.Point(0, 0);
			this.datagridUniformsValues.Name = "datagridUniformsValues";
			dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle33.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle33.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridUniformsValues.RowHeadersDefaultCellStyle = dataGridViewCellStyle33;
			this.datagridUniformsValues.Size = new System.Drawing.Size(643, 140);
			this.datagridUniformsValues.TabIndex = 6;
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
			dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle34.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle34.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle34.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle34.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridGeometryRouting.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle34;
			this.datagridGeometryRouting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridGeometryRouting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnProgramAttr,
            this.columnVertDesc});
			dataGridViewCellStyle35.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle35.BackColor = System.Drawing.SystemColors.ControlDark;
			dataGridViewCellStyle35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle35.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle35.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle35.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.datagridGeometryRouting.DefaultCellStyle = dataGridViewCellStyle35;
			this.datagridGeometryRouting.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridGeometryRouting.GridColor = System.Drawing.SystemColors.WindowText;
			this.datagridGeometryRouting.Location = new System.Drawing.Point(0, 0);
			this.datagridGeometryRouting.Name = "datagridGeometryRouting";
			dataGridViewCellStyle36.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle36.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			dataGridViewCellStyle36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			dataGridViewCellStyle36.ForeColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle36.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle36.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.datagridGeometryRouting.RowHeadersDefaultCellStyle = dataGridViewCellStyle36;
			this.datagridGeometryRouting.Size = new System.Drawing.Size(643, 146);
			this.datagridGeometryRouting.TabIndex = 2;
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
			// frmRenderConfigure
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.ClientSize = new System.Drawing.Size(643, 574);
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
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panelEventsConsole;
	}
}