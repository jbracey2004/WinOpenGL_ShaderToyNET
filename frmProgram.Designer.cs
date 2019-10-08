namespace WinOpenGL_ShaderToy
{
	partial class frmProgram
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
			this.components = new System.ComponentModel.Container();
			ShaderToy_Components.controlProjectObject.clsDesigner clsDesigner1 = new ShaderToy_Components.controlProjectObject.clsDesigner();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.panelMain = new ShaderToy_Components.controlProjectObject();
			this.layoutMain = new System.Windows.Forms.TableLayoutPanel();
			this.panelLinkShaders = new System.Windows.Forms.GroupBox();
			this.panelLstLink = new System.Windows.Forms.Panel();
			this.lstLink = new System.Windows.Forms.PictureBox();
			this.menuShader = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.menuShader_Detach = new System.Windows.Forms.ToolStripMenuItem();
			this.lstShaders = new System.Windows.Forms.ListBox();
			this.panelAttributes = new System.Windows.Forms.GroupBox();
			this.panelDataInputLinks = new System.Windows.Forms.Panel();
			this.datagridInputLinks = new System.Windows.Forms.DataGridView();
			this.columnShaderInputName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnVertexDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.panelListsGeometry = new System.Windows.Forms.Panel();
			this.panelListShaderInputs = new System.Windows.Forms.Panel();
			this.lstShaderInputs = new System.Windows.Forms.ListBox();
			this.panelListGeometry = new System.Windows.Forms.Panel();
			this.lstGeometry = new System.Windows.Forms.ComboBox();
			this.lblGeometry = new System.Windows.Forms.Label();
			this.panelUniforms = new System.Windows.Forms.GroupBox();
			this.datagridUniformLinks = new System.Windows.Forms.DataGridView();
			this.columnShaderUniform = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnUniformExpression = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.lstShaderUniform = new System.Windows.Forms.ListBox();
			this.splitterLinkStatus = new System.Windows.Forms.Splitter();
			this.panelLinkStatus = new System.Windows.Forms.Panel();
			this.dataLinkStatus = new System.Windows.Forms.DataGridView();
			this.panelLinkStatusHead = new System.Windows.Forms.Panel();
			this.chkLinkWarnings = new System.Windows.Forms.CheckBox();
			this.chkLinkErrors = new System.Windows.Forms.CheckBox();
			this.lblLinkStatus = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.gadgetLink = new System.Windows.Forms.Panel();
			this.btnLink = new System.Windows.Forms.Button();
			this.chkAutoLink = new System.Windows.Forms.CheckBox();
			this.panelMain.Content.SuspendLayout();
			this.panelMain.Status.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.layoutMain.SuspendLayout();
			this.panelLinkShaders.SuspendLayout();
			this.panelLstLink.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.lstLink)).BeginInit();
			this.menuShader.SuspendLayout();
			this.panelAttributes.SuspendLayout();
			this.panelDataInputLinks.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.datagridInputLinks)).BeginInit();
			this.panelListsGeometry.SuspendLayout();
			this.panelListShaderInputs.SuspendLayout();
			this.panelListGeometry.SuspendLayout();
			this.panelUniforms.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.datagridUniformLinks)).BeginInit();
			this.panelLinkStatus.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataLinkStatus)).BeginInit();
			this.panelLinkStatusHead.SuspendLayout();
			this.gadgetLink.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			// 
			// panelMain.Content
			// 
			this.panelMain.Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Content.Controls.Add(this.layoutMain);
			this.panelMain.Content.Controls.Add(this.splitterLinkStatus);
			this.panelMain.Content.Controls.Add(this.panelLinkStatus);
			this.panelMain.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Content.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.panelMain.Content.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Content.Name = "Content";
			this.panelMain.Content.Size = new System.Drawing.Size(748, 544);
			this.panelMain.Content.TabIndex = 2;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Name = "panelMain";
			this.panelMain.ParentControl = this;
			clsDesigner1.Name = "ProjectObject";
			clsDesigner1.ParentControl = this.panelMain;
			this.panelMain.ProjectObject = clsDesigner1;
			this.panelMain.Size = new System.Drawing.Size(748, 574);
			// 
			// panelMain.Status
			// 
			this.panelMain.Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Status.Controls.Add(this.lblName);
			this.panelMain.Status.Controls.Add(this.gadgetLink);
			this.panelMain.Status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelMain.Status.Location = new System.Drawing.Point(0, 544);
			this.panelMain.Status.Name = "Status";
			this.panelMain.Status.Size = new System.Drawing.Size(748, 30);
			this.panelMain.Status.TabIndex = 3;
			this.panelMain.TabIndex = 3;
			// 
			// layoutMain
			// 
			this.layoutMain.ColumnCount = 1;
			this.layoutMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.layoutMain.Controls.Add(this.panelLinkShaders, 0, 0);
			this.layoutMain.Controls.Add(this.panelAttributes, 0, 1);
			this.layoutMain.Controls.Add(this.panelUniforms, 0, 2);
			this.layoutMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.layoutMain.Location = new System.Drawing.Point(0, 0);
			this.layoutMain.Name = "layoutMain";
			this.layoutMain.RowCount = 3;
			this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
			this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.layoutMain.Size = new System.Drawing.Size(746, 508);
			this.layoutMain.TabIndex = 0;
			// 
			// panelLinkShaders
			// 
			this.panelLinkShaders.Controls.Add(this.panelLstLink);
			this.panelLinkShaders.Controls.Add(this.lstShaders);
			this.panelLinkShaders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelLinkShaders.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelLinkShaders.Location = new System.Drawing.Point(3, 3);
			this.panelLinkShaders.Name = "panelLinkShaders";
			this.panelLinkShaders.Size = new System.Drawing.Size(740, 174);
			this.panelLinkShaders.TabIndex = 0;
			this.panelLinkShaders.TabStop = false;
			this.panelLinkShaders.Text = "Link Shaders";
			// 
			// panelLstLink
			// 
			this.panelLstLink.AllowDrop = true;
			this.panelLstLink.AutoScroll = true;
			this.panelLstLink.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelLstLink.Controls.Add(this.lstLink);
			this.panelLstLink.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelLstLink.Location = new System.Drawing.Point(268, 18);
			this.panelLstLink.Name = "panelLstLink";
			this.panelLstLink.Size = new System.Drawing.Size(469, 153);
			this.panelLstLink.TabIndex = 1;
			this.panelLstLink.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstLink_DragDrop);
			this.panelLstLink.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstLink_DragEnter);
			// 
			// lstLink
			// 
			this.lstLink.BackColor = System.Drawing.Color.Transparent;
			this.lstLink.ContextMenuStrip = this.menuShader;
			this.lstLink.Location = new System.Drawing.Point(10, 10);
			this.lstLink.Name = "lstLink";
			this.lstLink.Size = new System.Drawing.Size(150, 125);
			this.lstLink.TabIndex = 0;
			this.lstLink.TabStop = false;
			this.lstLink.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstLink_DragDrop);
			this.lstLink.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstLink_DragEnter);
			this.lstLink.Paint += new System.Windows.Forms.PaintEventHandler(this.LstLink_Paint);
			this.lstLink.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstLink_MouseDown);
			this.lstLink.MouseLeave += new System.EventHandler(this.LstLink_MouseLeave);
			this.lstLink.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LstLink_MouseMove);
			// 
			// menuShader
			// 
			this.menuShader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuShader_Detach});
			this.menuShader.Name = "menuShader";
			this.menuShader.Size = new System.Drawing.Size(151, 26);
			// 
			// menuShader_Detach
			// 
			this.menuShader_Detach.Name = "menuShader_Detach";
			this.menuShader_Detach.Size = new System.Drawing.Size(150, 22);
			this.menuShader_Detach.Text = "Detach Shader";
			this.menuShader_Detach.Click += new System.EventHandler(this.MenuShader_Detach_Click);
			// 
			// lstShaders
			// 
			this.lstShaders.AllowDrop = true;
			this.lstShaders.Dock = System.Windows.Forms.DockStyle.Left;
			this.lstShaders.FormattingEnabled = true;
			this.lstShaders.IntegralHeight = false;
			this.lstShaders.ItemHeight = 16;
			this.lstShaders.Location = new System.Drawing.Point(3, 18);
			this.lstShaders.Name = "lstShaders";
			this.lstShaders.Size = new System.Drawing.Size(265, 153);
			this.lstShaders.TabIndex = 0;
			this.lstShaders.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstShaders_DragDrop);
			this.lstShaders.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstShaders_DragEnter);
			this.lstShaders.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstShaders_MouseDown);
			// 
			// panelAttributes
			// 
			this.panelAttributes.Controls.Add(this.panelDataInputLinks);
			this.panelAttributes.Controls.Add(this.panelListsGeometry);
			this.panelAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelAttributes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelAttributes.Location = new System.Drawing.Point(3, 183);
			this.panelAttributes.Name = "panelAttributes";
			this.panelAttributes.Size = new System.Drawing.Size(740, 158);
			this.panelAttributes.TabIndex = 1;
			this.panelAttributes.TabStop = false;
			this.panelAttributes.Text = "Link Attributes";
			// 
			// panelDataInputLinks
			// 
			this.panelDataInputLinks.BackColor = System.Drawing.Color.Transparent;
			this.panelDataInputLinks.Controls.Add(this.datagridInputLinks);
			this.panelDataInputLinks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelDataInputLinks.Location = new System.Drawing.Point(268, 18);
			this.panelDataInputLinks.Name = "panelDataInputLinks";
			this.panelDataInputLinks.Size = new System.Drawing.Size(469, 137);
			this.panelDataInputLinks.TabIndex = 3;
			// 
			// datagridInputLinks
			// 
			this.datagridInputLinks.AllowUserToOrderColumns = true;
			this.datagridInputLinks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridInputLinks.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.datagridInputLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridInputLinks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnShaderInputName,
            this.columnVertexDescription});
			this.datagridInputLinks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridInputLinks.Location = new System.Drawing.Point(0, 0);
			this.datagridInputLinks.Name = "datagridInputLinks";
			this.datagridInputLinks.Size = new System.Drawing.Size(469, 137);
			this.datagridInputLinks.TabIndex = 2;
			this.datagridInputLinks.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DatagridInputLinks_CellEndEdit);
			this.datagridInputLinks.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DatagridLinks_EditingControlShowing);
			this.datagridInputLinks.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DatagridInputLinks_RowsAdded);
			this.datagridInputLinks.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DatagridInputLinks_RowsRemoved);
			// 
			// columnShaderInputName
			// 
			this.columnShaderInputName.HeaderText = "Shader Input";
			this.columnShaderInputName.Name = "columnShaderInputName";
			this.columnShaderInputName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.columnShaderInputName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// columnVertexDescription
			// 
			this.columnVertexDescription.HeaderText = "Vertex Description";
			this.columnVertexDescription.Name = "columnVertexDescription";
			this.columnVertexDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.columnVertexDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// panelListsGeometry
			// 
			this.panelListsGeometry.Controls.Add(this.panelListShaderInputs);
			this.panelListsGeometry.Controls.Add(this.panelListGeometry);
			this.panelListsGeometry.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelListsGeometry.Location = new System.Drawing.Point(3, 18);
			this.panelListsGeometry.Name = "panelListsGeometry";
			this.panelListsGeometry.Size = new System.Drawing.Size(265, 137);
			this.panelListsGeometry.TabIndex = 6;
			// 
			// panelListShaderInputs
			// 
			this.panelListShaderInputs.Controls.Add(this.lstShaderInputs);
			this.panelListShaderInputs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelListShaderInputs.Location = new System.Drawing.Point(0, 24);
			this.panelListShaderInputs.Name = "panelListShaderInputs";
			this.panelListShaderInputs.Size = new System.Drawing.Size(265, 113);
			this.panelListShaderInputs.TabIndex = 5;
			// 
			// lstShaderInputs
			// 
			this.lstShaderInputs.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstShaderInputs.FormattingEnabled = true;
			this.lstShaderInputs.IntegralHeight = false;
			this.lstShaderInputs.ItemHeight = 16;
			this.lstShaderInputs.Location = new System.Drawing.Point(0, 0);
			this.lstShaderInputs.Name = "lstShaderInputs";
			this.lstShaderInputs.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.lstShaderInputs.Size = new System.Drawing.Size(265, 113);
			this.lstShaderInputs.TabIndex = 0;
			// 
			// panelListGeometry
			// 
			this.panelListGeometry.BackColor = System.Drawing.Color.Transparent;
			this.panelListGeometry.Controls.Add(this.lstGeometry);
			this.panelListGeometry.Controls.Add(this.lblGeometry);
			this.panelListGeometry.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelListGeometry.Location = new System.Drawing.Point(0, 0);
			this.panelListGeometry.Name = "panelListGeometry";
			this.panelListGeometry.Size = new System.Drawing.Size(265, 24);
			this.panelListGeometry.TabIndex = 4;
			// 
			// lstGeometry
			// 
			this.lstGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstGeometry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstGeometry.FormattingEnabled = true;
			this.lstGeometry.Location = new System.Drawing.Point(67, 0);
			this.lstGeometry.Name = "lstGeometry";
			this.lstGeometry.Size = new System.Drawing.Size(198, 24);
			this.lstGeometry.TabIndex = 1;
			this.lstGeometry.SelectedIndexChanged += new System.EventHandler(this.LstGeometry_SelectedIndexChanged);
			// 
			// lblGeometry
			// 
			this.lblGeometry.AutoSize = true;
			this.lblGeometry.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblGeometry.ForeColor = System.Drawing.Color.Black;
			this.lblGeometry.Location = new System.Drawing.Point(0, 0);
			this.lblGeometry.Name = "lblGeometry";
			this.lblGeometry.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.lblGeometry.Size = new System.Drawing.Size(67, 22);
			this.lblGeometry.TabIndex = 0;
			this.lblGeometry.Text = "Geometry";
			// 
			// panelUniforms
			// 
			this.panelUniforms.Controls.Add(this.datagridUniformLinks);
			this.panelUniforms.Controls.Add(this.lstShaderUniform);
			this.panelUniforms.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelUniforms.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.panelUniforms.Location = new System.Drawing.Point(3, 347);
			this.panelUniforms.Name = "panelUniforms";
			this.panelUniforms.Size = new System.Drawing.Size(740, 158);
			this.panelUniforms.TabIndex = 2;
			this.panelUniforms.TabStop = false;
			this.panelUniforms.Text = "Link Uniforms";
			// 
			// datagridUniformLinks
			// 
			this.datagridUniformLinks.AllowUserToOrderColumns = true;
			this.datagridUniformLinks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.datagridUniformLinks.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.datagridUniformLinks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.datagridUniformLinks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnShaderUniform,
            this.columnUniformExpression});
			this.datagridUniformLinks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridUniformLinks.Location = new System.Drawing.Point(268, 18);
			this.datagridUniformLinks.Name = "datagridUniformLinks";
			this.datagridUniformLinks.Size = new System.Drawing.Size(469, 137);
			this.datagridUniformLinks.TabIndex = 3;
			this.datagridUniformLinks.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DatagridUniformLinks_CellEndEdit);
			this.datagridUniformLinks.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DatagridLinks_EditingControlShowing);
			this.datagridUniformLinks.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DatagridUniformLinks_RowsAdded);
			this.datagridUniformLinks.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DatagridUniformLinks_RowsRemoved);
			// 
			// columnShaderUniform
			// 
			this.columnShaderUniform.HeaderText = "Shader Uniform";
			this.columnShaderUniform.Name = "columnShaderUniform";
			this.columnShaderUniform.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.columnShaderUniform.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// columnUniformExpression
			// 
			this.columnUniformExpression.HeaderText = "Expression";
			this.columnUniformExpression.Name = "columnUniformExpression";
			this.columnUniformExpression.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.columnUniformExpression.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			// 
			// lstShaderUniform
			// 
			this.lstShaderUniform.Dock = System.Windows.Forms.DockStyle.Left;
			this.lstShaderUniform.FormattingEnabled = true;
			this.lstShaderUniform.IntegralHeight = false;
			this.lstShaderUniform.ItemHeight = 16;
			this.lstShaderUniform.Location = new System.Drawing.Point(3, 18);
			this.lstShaderUniform.Name = "lstShaderUniform";
			this.lstShaderUniform.SelectionMode = System.Windows.Forms.SelectionMode.None;
			this.lstShaderUniform.Size = new System.Drawing.Size(265, 137);
			this.lstShaderUniform.TabIndex = 1;
			// 
			// splitterLinkStatus
			// 
			this.splitterLinkStatus.BackColor = System.Drawing.SystemColors.ControlLight;
			this.splitterLinkStatus.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitterLinkStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterLinkStatus.Location = new System.Drawing.Point(0, 508);
			this.splitterLinkStatus.Name = "splitterLinkStatus";
			this.splitterLinkStatus.Size = new System.Drawing.Size(746, 10);
			this.splitterLinkStatus.TabIndex = 1;
			this.splitterLinkStatus.TabStop = false;
			// 
			// panelLinkStatus
			// 
			this.panelLinkStatus.Controls.Add(this.dataLinkStatus);
			this.panelLinkStatus.Controls.Add(this.panelLinkStatusHead);
			this.panelLinkStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelLinkStatus.Location = new System.Drawing.Point(0, 518);
			this.panelLinkStatus.Name = "panelLinkStatus";
			this.panelLinkStatus.Size = new System.Drawing.Size(746, 24);
			this.panelLinkStatus.TabIndex = 5;
			// 
			// dataLinkStatus
			// 
			this.dataLinkStatus.AllowUserToAddRows = false;
			this.dataLinkStatus.AllowUserToDeleteRows = false;
			this.dataLinkStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataLinkStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataLinkStatus.Location = new System.Drawing.Point(0, 23);
			this.dataLinkStatus.Name = "dataLinkStatus";
			this.dataLinkStatus.ReadOnly = true;
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.dataLinkStatus.RowsDefaultCellStyle = dataGridViewCellStyle1;
			this.dataLinkStatus.Size = new System.Drawing.Size(746, 1);
			this.dataLinkStatus.TabIndex = 0;
			// 
			// panelLinkStatusHead
			// 
			this.panelLinkStatusHead.Controls.Add(this.chkLinkWarnings);
			this.panelLinkStatusHead.Controls.Add(this.chkLinkErrors);
			this.panelLinkStatusHead.Controls.Add(this.lblLinkStatus);
			this.panelLinkStatusHead.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelLinkStatusHead.Location = new System.Drawing.Point(0, 0);
			this.panelLinkStatusHead.Name = "panelLinkStatusHead";
			this.panelLinkStatusHead.Size = new System.Drawing.Size(746, 23);
			this.panelLinkStatusHead.TabIndex = 1;
			// 
			// chkLinkWarnings
			// 
			this.chkLinkWarnings.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkLinkWarnings.AutoSize = true;
			this.chkLinkWarnings.BackColor = System.Drawing.Color.Transparent;
			this.chkLinkWarnings.Checked = true;
			this.chkLinkWarnings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLinkWarnings.Dock = System.Windows.Forms.DockStyle.Left;
			this.chkLinkWarnings.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkLinkWarnings.Location = new System.Drawing.Point(342, 0);
			this.chkLinkWarnings.Name = "chkLinkWarnings";
			this.chkLinkWarnings.Size = new System.Drawing.Size(74, 23);
			this.chkLinkWarnings.TabIndex = 4;
			this.chkLinkWarnings.Text = "Warnings: 0";
			this.chkLinkWarnings.UseVisualStyleBackColor = false;
			this.chkLinkWarnings.CheckedChanged += new System.EventHandler(this.ChkLinkWarnings_CheckedChanged);
			// 
			// chkLinkErrors
			// 
			this.chkLinkErrors.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkLinkErrors.AutoSize = true;
			this.chkLinkErrors.BackColor = System.Drawing.Color.Transparent;
			this.chkLinkErrors.Checked = true;
			this.chkLinkErrors.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkLinkErrors.Dock = System.Windows.Forms.DockStyle.Left;
			this.chkLinkErrors.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chkLinkErrors.Location = new System.Drawing.Point(286, 0);
			this.chkLinkErrors.Name = "chkLinkErrors";
			this.chkLinkErrors.Size = new System.Drawing.Size(56, 23);
			this.chkLinkErrors.TabIndex = 3;
			this.chkLinkErrors.Text = "Errors: 0";
			this.chkLinkErrors.UseVisualStyleBackColor = false;
			this.chkLinkErrors.CheckedChanged += new System.EventHandler(this.ChkLinkErrors_CheckedChanged);
			// 
			// lblLinkStatus
			// 
			this.lblLinkStatus.BackColor = System.Drawing.Color.Transparent;
			this.lblLinkStatus.Dock = System.Windows.Forms.DockStyle.Left;
			this.lblLinkStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLinkStatus.ForeColor = System.Drawing.Color.Green;
			this.lblLinkStatus.Location = new System.Drawing.Point(0, 0);
			this.lblLinkStatus.Name = "lblLinkStatus";
			this.lblLinkStatus.Size = new System.Drawing.Size(286, 23);
			this.lblLinkStatus.TabIndex = 0;
			this.lblLinkStatus.Text = "Link Status: Good";
			this.lblLinkStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.BackColor = System.Drawing.Color.Transparent;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(3, 6);
			this.lblName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(60, 16);
			this.lblName.TabIndex = 2;
			this.lblName.Text = "Program";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gadgetLink
			// 
			this.gadgetLink.Controls.Add(this.btnLink);
			this.gadgetLink.Controls.Add(this.chkAutoLink);
			this.gadgetLink.Location = new System.Drawing.Point(232, 3);
			this.gadgetLink.Name = "gadgetLink";
			this.gadgetLink.Size = new System.Drawing.Size(94, 21);
			this.gadgetLink.TabIndex = 7;
			// 
			// btnLink
			// 
			this.btnLink.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnLink.Enabled = false;
			this.btnLink.Location = new System.Drawing.Point(39, 0);
			this.btnLink.Margin = new System.Windows.Forms.Padding(0);
			this.btnLink.Name = "btnLink";
			this.btnLink.Size = new System.Drawing.Size(56, 21);
			this.btnLink.TabIndex = 5;
			this.btnLink.Text = "Link";
			this.btnLink.UseVisualStyleBackColor = true;
			this.btnLink.Click += new System.EventHandler(this.BtnLink_Click);
			// 
			// chkAutoLink
			// 
			this.chkAutoLink.Appearance = System.Windows.Forms.Appearance.Button;
			this.chkAutoLink.AutoSize = true;
			this.chkAutoLink.Checked = true;
			this.chkAutoLink.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkAutoLink.Dock = System.Windows.Forms.DockStyle.Left;
			this.chkAutoLink.Location = new System.Drawing.Point(0, 0);
			this.chkAutoLink.Margin = new System.Windows.Forms.Padding(0);
			this.chkAutoLink.Name = "chkAutoLink";
			this.chkAutoLink.Size = new System.Drawing.Size(39, 21);
			this.chkAutoLink.TabIndex = 4;
			this.chkAutoLink.Text = "Auto";
			this.chkAutoLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.chkAutoLink.UseVisualStyleBackColor = true;
			this.chkAutoLink.CheckedChanged += new System.EventHandler(this.ChkAutoLink_CheckedChanged);
			// 
			// frmProgram
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(748, 574);
			this.Controls.Add(this.panelMain);
			this.Name = "frmProgram";
			this.Text = "Designer_ProjectObject";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProgram_FormClosing);
			this.Load += new System.EventHandler(this.FrmProgram_Load);
			this.panelMain.Content.ResumeLayout(false);
			this.panelMain.Status.ResumeLayout(false);
			this.panelMain.Status.PerformLayout();
			this.panelMain.ResumeLayout(false);
			this.layoutMain.ResumeLayout(false);
			this.panelLinkShaders.ResumeLayout(false);
			this.panelLstLink.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.lstLink)).EndInit();
			this.menuShader.ResumeLayout(false);
			this.panelAttributes.ResumeLayout(false);
			this.panelDataInputLinks.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.datagridInputLinks)).EndInit();
			this.panelListsGeometry.ResumeLayout(false);
			this.panelListShaderInputs.ResumeLayout(false);
			this.panelListGeometry.ResumeLayout(false);
			this.panelListGeometry.PerformLayout();
			this.panelUniforms.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.datagridUniformLinks)).EndInit();
			this.panelLinkStatus.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataLinkStatus)).EndInit();
			this.panelLinkStatusHead.ResumeLayout(false);
			this.panelLinkStatusHead.PerformLayout();
			this.gadgetLink.ResumeLayout(false);
			this.gadgetLink.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label lblName;
		private ShaderToy_Components.controlProjectObject panelMain;
		private System.Windows.Forms.Splitter splitterLinkStatus;
		private System.Windows.Forms.TableLayoutPanel layoutMain;
		private System.Windows.Forms.Panel panelLinkStatus;
		private System.Windows.Forms.DataGridView dataLinkStatus;
		private System.Windows.Forms.Panel panelLinkStatusHead;
		private System.Windows.Forms.Label lblLinkStatus;
		private System.Windows.Forms.Panel gadgetLink;
		private System.Windows.Forms.Button btnLink;
		private System.Windows.Forms.CheckBox chkAutoLink;
		private System.Windows.Forms.GroupBox panelLinkShaders;
		private System.Windows.Forms.ListBox lstShaders;
		private System.Windows.Forms.Panel panelLstLink;
		private System.Windows.Forms.PictureBox lstLink;
		private System.Windows.Forms.ContextMenuStrip menuShader;
		private System.Windows.Forms.ToolStripMenuItem menuShader_Detach;
		private System.Windows.Forms.GroupBox panelAttributes;
		private System.Windows.Forms.ComboBox lstGeometry;
		private System.Windows.Forms.Label lblGeometry;
		private System.Windows.Forms.DataGridView datagridInputLinks;
		private System.Windows.Forms.Panel panelDataInputLinks;
		private System.Windows.Forms.Panel panelListsGeometry;
		private System.Windows.Forms.Panel panelListShaderInputs;
		private System.Windows.Forms.Panel panelListGeometry;
		private System.Windows.Forms.ListBox lstShaderInputs;
		private System.Windows.Forms.CheckBox chkLinkWarnings;
		private System.Windows.Forms.CheckBox chkLinkErrors;
		private System.Windows.Forms.GroupBox panelUniforms;
		private System.Windows.Forms.DataGridView datagridUniformLinks;
		private System.Windows.Forms.ListBox lstShaderUniform;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnShaderUniform;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnUniformExpression;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnShaderInputName;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnVertexDescription;
	}
}