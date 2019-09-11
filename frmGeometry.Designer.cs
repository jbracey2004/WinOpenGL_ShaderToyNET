namespace WinOpenGL_ShaderToy
{
	partial class frmGeometry
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
			this.panelCollapse = new System.Windows.Forms.Panel();
			this.glRender = new OpenTK.GLControl();
			this.splitterLeft = new System.Windows.Forms.Splitter();
			this.splitterRight = new System.Windows.Forms.Splitter();
			this.panelLeft = new System.Windows.Forms.Panel();
			this.panelRight = new System.Windows.Forms.Panel();
			this.groupGeometry = new System.Windows.Forms.GroupBox();
			this.panelGeometry = new System.Windows.Forms.Panel();
			this.propsGeometry = new System.Windows.Forms.PropertyGrid();
			this.splitterBottom = new System.Windows.Forms.Splitter();
			this.panelBottom = new System.Windows.Forms.Panel();
			this.groupVertexDefinition = new System.Windows.Forms.GroupBox();
			this.panelVertexDefinition = new System.Windows.Forms.Panel();
			this.datagridVertexDescriptions = new System.Windows.Forms.DataGridView();
			this.columnIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnComponentName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.columnElementType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.columnElementCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.splitterTop = new System.Windows.Forms.Splitter();
			this.panelTop = new System.Windows.Forms.Panel();
			this.lblName = new System.Windows.Forms.Label();
			this.panelMain.Content.SuspendLayout();
			this.panelMain.Status.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.panelCollapse.SuspendLayout();
			this.panelRight.SuspendLayout();
			this.groupGeometry.SuspendLayout();
			this.panelGeometry.SuspendLayout();
			this.panelBottom.SuspendLayout();
			this.groupVertexDefinition.SuspendLayout();
			this.panelVertexDefinition.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.datagridVertexDescriptions)).BeginInit();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			// 
			// panelMain.Content
			// 
			this.panelMain.Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Content.Controls.Add(this.panelCollapse);
			this.panelMain.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Content.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.panelMain.Content.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Content.Name = "Content";
			this.panelMain.Content.Size = new System.Drawing.Size(825, 457);
			this.panelMain.Content.TabIndex = 2;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Name = "panelMain";
			this.panelMain.ParentControl = this;
			clsDesigner1.Name = "ProjectObject";
			clsDesigner1.ParentControl = this.panelMain;
			this.panelMain.ProjectObject = clsDesigner1;
			this.panelMain.Size = new System.Drawing.Size(825, 487);
			// 
			// panelMain.Status
			// 
			this.panelMain.Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Status.Controls.Add(this.lblName);
			this.panelMain.Status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelMain.Status.Location = new System.Drawing.Point(0, 457);
			this.panelMain.Status.Name = "Status";
			this.panelMain.Status.Size = new System.Drawing.Size(825, 30);
			this.panelMain.Status.TabIndex = 3;
			this.panelMain.TabIndex = 0;
			// 
			// panelCollapse
			// 
			this.panelCollapse.BackColor = System.Drawing.SystemColors.Control;
			this.panelCollapse.Controls.Add(this.glRender);
			this.panelCollapse.Controls.Add(this.splitterLeft);
			this.panelCollapse.Controls.Add(this.splitterRight);
			this.panelCollapse.Controls.Add(this.panelLeft);
			this.panelCollapse.Controls.Add(this.panelRight);
			this.panelCollapse.Controls.Add(this.splitterBottom);
			this.panelCollapse.Controls.Add(this.panelBottom);
			this.panelCollapse.Controls.Add(this.splitterTop);
			this.panelCollapse.Controls.Add(this.panelTop);
			this.panelCollapse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelCollapse.Location = new System.Drawing.Point(0, 0);
			this.panelCollapse.Name = "panelCollapse";
			this.panelCollapse.Size = new System.Drawing.Size(823, 455);
			this.panelCollapse.TabIndex = 10;
			// 
			// glRender
			// 
			this.glRender.BackColor = System.Drawing.SystemColors.Control;
			this.glRender.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.glRender.Dock = System.Windows.Forms.DockStyle.Fill;
			this.glRender.ForeColor = System.Drawing.SystemColors.ControlText;
			this.glRender.Location = new System.Drawing.Point(8, 8);
			this.glRender.Name = "glRender";
			this.glRender.Size = new System.Drawing.Size(558, 342);
			this.glRender.TabIndex = 13;
			this.glRender.VSync = false;
			// 
			// splitterLeft
			// 
			this.splitterLeft.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitterLeft.Location = new System.Drawing.Point(0, 8);
			this.splitterLeft.Margin = new System.Windows.Forms.Padding(0);
			this.splitterLeft.MinExtra = 0;
			this.splitterLeft.MinSize = 0;
			this.splitterLeft.Name = "splitterLeft";
			this.splitterLeft.Size = new System.Drawing.Size(8, 342);
			this.splitterLeft.TabIndex = 12;
			this.splitterLeft.TabStop = false;
			// 
			// splitterRight
			// 
			this.splitterRight.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitterRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitterRight.Location = new System.Drawing.Point(566, 8);
			this.splitterRight.Margin = new System.Windows.Forms.Padding(0);
			this.splitterRight.MinExtra = 0;
			this.splitterRight.MinSize = 0;
			this.splitterRight.Name = "splitterRight";
			this.splitterRight.Size = new System.Drawing.Size(8, 342);
			this.splitterRight.TabIndex = 11;
			this.splitterRight.TabStop = false;
			// 
			// panelLeft
			// 
			this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelLeft.Location = new System.Drawing.Point(0, 8);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new System.Drawing.Size(0, 342);
			this.panelLeft.TabIndex = 0;
			// 
			// panelRight
			// 
			this.panelRight.Controls.Add(this.groupGeometry);
			this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelRight.Location = new System.Drawing.Point(574, 8);
			this.panelRight.Name = "panelRight";
			this.panelRight.Size = new System.Drawing.Size(249, 342);
			this.panelRight.TabIndex = 1;
			// 
			// groupGeometry
			// 
			this.groupGeometry.BackColor = System.Drawing.Color.Transparent;
			this.groupGeometry.Controls.Add(this.panelGeometry);
			this.groupGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupGeometry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupGeometry.Location = new System.Drawing.Point(0, 0);
			this.groupGeometry.Name = "groupGeometry";
			this.groupGeometry.Size = new System.Drawing.Size(249, 342);
			this.groupGeometry.TabIndex = 7;
			this.groupGeometry.TabStop = false;
			this.groupGeometry.Text = "Geometry";
			// 
			// panelGeometry
			// 
			this.panelGeometry.BackColor = System.Drawing.Color.Transparent;
			this.panelGeometry.Controls.Add(this.propsGeometry);
			this.panelGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelGeometry.Location = new System.Drawing.Point(3, 18);
			this.panelGeometry.Name = "panelGeometry";
			this.panelGeometry.Size = new System.Drawing.Size(243, 321);
			this.panelGeometry.TabIndex = 1;
			// 
			// propsGeometry
			// 
			this.propsGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propsGeometry.Location = new System.Drawing.Point(0, 0);
			this.propsGeometry.Name = "propsGeometry";
			this.propsGeometry.Size = new System.Drawing.Size(243, 321);
			this.propsGeometry.TabIndex = 0;
			this.propsGeometry.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropsGeometry_PropertyValueChanged);
			// 
			// splitterBottom
			// 
			this.splitterBottom.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitterBottom.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitterBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterBottom.Location = new System.Drawing.Point(0, 350);
			this.splitterBottom.Margin = new System.Windows.Forms.Padding(0);
			this.splitterBottom.MinExtra = 0;
			this.splitterBottom.MinSize = 0;
			this.splitterBottom.Name = "splitterBottom";
			this.splitterBottom.Size = new System.Drawing.Size(823, 8);
			this.splitterBottom.TabIndex = 10;
			this.splitterBottom.TabStop = false;
			// 
			// panelBottom
			// 
			this.panelBottom.Controls.Add(this.groupVertexDefinition);
			this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelBottom.Location = new System.Drawing.Point(0, 358);
			this.panelBottom.Name = "panelBottom";
			this.panelBottom.Size = new System.Drawing.Size(823, 97);
			this.panelBottom.TabIndex = 3;
			// 
			// groupVertexDefinition
			// 
			this.groupVertexDefinition.BackColor = System.Drawing.Color.Transparent;
			this.groupVertexDefinition.Controls.Add(this.panelVertexDefinition);
			this.groupVertexDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupVertexDefinition.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupVertexDefinition.ForeColor = System.Drawing.SystemColors.ControlText;
			this.groupVertexDefinition.Location = new System.Drawing.Point(0, 0);
			this.groupVertexDefinition.Name = "groupVertexDefinition";
			this.groupVertexDefinition.Size = new System.Drawing.Size(823, 97);
			this.groupVertexDefinition.TabIndex = 10;
			this.groupVertexDefinition.TabStop = false;
			this.groupVertexDefinition.Text = "Vertex Definition";
			// 
			// panelVertexDefinition
			// 
			this.panelVertexDefinition.AutoScroll = true;
			this.panelVertexDefinition.Controls.Add(this.datagridVertexDescriptions);
			this.panelVertexDefinition.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelVertexDefinition.Location = new System.Drawing.Point(3, 18);
			this.panelVertexDefinition.Name = "panelVertexDefinition";
			this.panelVertexDefinition.Size = new System.Drawing.Size(817, 76);
			this.panelVertexDefinition.TabIndex = 2;
			// 
			// datagridVertexDescriptions
			// 
			this.datagridVertexDescriptions.AllowUserToOrderColumns = true;
			this.datagridVertexDescriptions.AllowUserToResizeRows = false;
			this.datagridVertexDescriptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.datagridVertexDescriptions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.columnIndex,
            this.columnComponentName,
            this.columnElementType,
            this.columnElementCount});
			this.datagridVertexDescriptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.datagridVertexDescriptions.Location = new System.Drawing.Point(0, 0);
			this.datagridVertexDescriptions.Name = "datagridVertexDescriptions";
			this.datagridVertexDescriptions.Size = new System.Drawing.Size(817, 76);
			this.datagridVertexDescriptions.TabIndex = 0;
			this.datagridVertexDescriptions.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DatagridVertexDescriptions_CellEndEdit);
			this.datagridVertexDescriptions.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DatagridVertexDescriptions_DataError);
			this.datagridVertexDescriptions.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DatagridVertexDescriptions_RowsAdded);
			this.datagridVertexDescriptions.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.DatagridVertexDescriptions_RowsRemoved);
			// 
			// columnIndex
			// 
			this.columnIndex.HeaderText = "Index";
			this.columnIndex.Name = "columnIndex";
			this.columnIndex.Width = 65;
			// 
			// columnComponentName
			// 
			this.columnComponentName.HeaderText = "Component Name";
			this.columnComponentName.Name = "columnComponentName";
			this.columnComponentName.Width = 142;
			// 
			// columnElementType
			// 
			this.columnElementType.HeaderText = "Element Type";
			this.columnElementType.Name = "columnElementType";
			this.columnElementType.Width = 98;
			// 
			// columnElementCount
			// 
			this.columnElementCount.HeaderText = "Element Count";
			this.columnElementCount.Name = "columnElementCount";
			this.columnElementCount.Width = 119;
			// 
			// splitterTop
			// 
			this.splitterTop.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitterTop.Cursor = System.Windows.Forms.Cursors.HSplit;
			this.splitterTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitterTop.Location = new System.Drawing.Point(0, 0);
			this.splitterTop.Margin = new System.Windows.Forms.Padding(0);
			this.splitterTop.MinExtra = 0;
			this.splitterTop.MinSize = 0;
			this.splitterTop.Name = "splitterTop";
			this.splitterTop.Size = new System.Drawing.Size(823, 8);
			this.splitterTop.TabIndex = 9;
			this.splitterTop.TabStop = false;
			// 
			// panelTop
			// 
			this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTop.Location = new System.Drawing.Point(0, 0);
			this.panelTop.Name = "panelTop";
			this.panelTop.Size = new System.Drawing.Size(823, 0);
			this.panelTop.TabIndex = 2;
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.BackColor = System.Drawing.Color.Transparent;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.Location = new System.Drawing.Point(3, 6);
			this.lblName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(67, 16);
			this.lblName.TabIndex = 3;
			this.lblName.Text = "Geometry";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// frmGeometry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(825, 487);
			this.Controls.Add(this.panelMain);
			this.DoubleBuffered = true;
			this.Name = "frmGeometry";
			this.Text = "Designer_ProjectObject";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGeometry_FormClosing);
			this.Load += new System.EventHandler(this.FrmGeometry_Load);
			this.panelMain.Content.ResumeLayout(false);
			this.panelMain.Status.ResumeLayout(false);
			this.panelMain.Status.PerformLayout();
			this.panelMain.ResumeLayout(false);
			this.panelCollapse.ResumeLayout(false);
			this.panelRight.ResumeLayout(false);
			this.groupGeometry.ResumeLayout(false);
			this.panelGeometry.ResumeLayout(false);
			this.panelBottom.ResumeLayout(false);
			this.groupVertexDefinition.ResumeLayout(false);
			this.panelVertexDefinition.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.datagridVertexDescriptions)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private ShaderToy_Components.controlProjectObject panelMain;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Panel panelCollapse;
		private OpenTK.GLControl glRender;
		private System.Windows.Forms.Splitter splitterLeft;
		private System.Windows.Forms.Splitter splitterRight;
		private System.Windows.Forms.Splitter splitterBottom;
		private System.Windows.Forms.Splitter splitterTop;
		private System.Windows.Forms.Panel panelLeft;
		private System.Windows.Forms.Panel panelRight;
		private System.Windows.Forms.GroupBox groupGeometry;
		private System.Windows.Forms.Panel panelGeometry;
		private System.Windows.Forms.Panel panelBottom;
		private System.Windows.Forms.GroupBox groupVertexDefinition;
		private System.Windows.Forms.Panel panelVertexDefinition;
		private System.Windows.Forms.Panel panelTop;
		private System.Windows.Forms.DataGridView datagridVertexDescriptions;
		private System.Windows.Forms.PropertyGrid propsGeometry;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnIndex;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnComponentName;
		private System.Windows.Forms.DataGridViewComboBoxColumn columnElementType;
		private System.Windows.Forms.DataGridViewTextBoxColumn columnElementCount;
	}
}