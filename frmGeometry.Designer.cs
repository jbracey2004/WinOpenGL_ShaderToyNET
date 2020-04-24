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
			this.splitterLeft = new System.Windows.Forms.Splitter();
			this.splitterRight = new System.Windows.Forms.Splitter();
			this.panelLeft = new System.Windows.Forms.Panel();
			this.panelRight = new System.Windows.Forms.Panel();
			this.panelGeometry = new System.Windows.Forms.Panel();
			this.propsGeometry = new System.Windows.Forms.PropertyGrid();
			this.lblHeader_Geometry = new System.Windows.Forms.Label();
			this.lblName = new System.Windows.Forms.Label();
			this.lblVertexDesc = new System.Windows.Forms.Label();
			this.lstVertexDesc = new System.Windows.Forms.ComboBox();
			this.lblPositionAttr = new System.Windows.Forms.Label();
			this.lstPositionAttr = new System.Windows.Forms.ComboBox();
			this.panelMain.Content.SuspendLayout();
			this.panelMain.Status.SuspendLayout();
			this.panelMain.SuspendLayout();
			this.panelCollapse.SuspendLayout();
			this.panelRight.SuspendLayout();
			this.panelGeometry.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			// 
			// panelMain.Content
			// 
			this.panelMain.Content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Content.Controls.Add(this.panelCollapse);
			this.panelMain.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Content.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Content.Name = "Content";
			this.panelMain.Content.Size = new System.Drawing.Size(871, 479);
			this.panelMain.Content.TabIndex = 2;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Name = "panelMain";
			clsDesigner1.Name = "ProjectObject";
			clsDesigner1.ParentControl = this.panelMain;
			this.panelMain.ProjectObject = clsDesigner1;
			this.panelMain.Size = new System.Drawing.Size(871, 509);
			// 
			// panelMain.Status
			//
			this.panelMain.Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Status.Controls.Add(this.lblName);
			this.panelMain.Status.Controls.Add(this.lblVertexDesc);
			this.panelMain.Status.Controls.Add(this.lstVertexDesc);
			this.panelMain.Status.Controls.Add(this.lblPositionAttr);
			this.panelMain.Status.Controls.Add(this.lstPositionAttr);
			this.panelMain.Status.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelMain.Status.Location = new System.Drawing.Point(0, 479);
			this.panelMain.Status.Name = "Status";
			this.panelMain.Status.Size = new System.Drawing.Size(871, 30);
			this.panelMain.Status.TabIndex = 3;
			this.panelMain.TabIndex = 0;
			// 
			// panelCollapse
			// 
			this.panelCollapse.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panelCollapse.Controls.Add(this.splitterLeft);
			this.panelCollapse.Controls.Add(this.splitterRight);
			this.panelCollapse.Controls.Add(this.panelLeft);
			this.panelCollapse.Controls.Add(this.panelRight);
			this.panelCollapse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelCollapse.ForeColor = System.Drawing.SystemColors.Window;
			this.panelCollapse.Location = new System.Drawing.Point(0, 0);
			this.panelCollapse.Name = "panelCollapse";
			this.panelCollapse.Size = new System.Drawing.Size(869, 477);
			this.panelCollapse.TabIndex = 10;
			// 
			// splitterLeft
			// 
			this.splitterLeft.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitterLeft.Location = new System.Drawing.Point(0, 0);
			this.splitterLeft.Margin = new System.Windows.Forms.Padding(0);
			this.splitterLeft.MinExtra = 0;
			this.splitterLeft.MinSize = 0;
			this.splitterLeft.Name = "splitterLeft";
			this.splitterLeft.Size = new System.Drawing.Size(8, 477);
			this.splitterLeft.TabIndex = 12;
			this.splitterLeft.TabStop = false;
			// 
			// splitterRight
			// 
			this.splitterRight.BackColor = System.Drawing.SystemColors.ControlDark;
			this.splitterRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.splitterRight.Location = new System.Drawing.Point(612, 0);
			this.splitterRight.Margin = new System.Windows.Forms.Padding(0);
			this.splitterRight.MinExtra = 0;
			this.splitterRight.MinSize = 0;
			this.splitterRight.Name = "splitterRight";
			this.splitterRight.Size = new System.Drawing.Size(8, 477);
			this.splitterRight.TabIndex = 11;
			this.splitterRight.TabStop = false;
			// 
			// panelLeft
			// 
			this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelLeft.Location = new System.Drawing.Point(0, 0);
			this.panelLeft.Name = "panelLeft";
			this.panelLeft.Size = new System.Drawing.Size(0, 477);
			this.panelLeft.TabIndex = 0;
			// 
			// panelRight
			// 
			this.panelRight.Controls.Add(this.panelGeometry);
			this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelRight.Location = new System.Drawing.Point(620, 0);
			this.panelRight.Name = "panelRight";
			this.panelRight.Size = new System.Drawing.Size(249, 477);
			this.panelRight.TabIndex = 1;
			// 
			// panelGeometry
			// 
			this.panelGeometry.BackColor = System.Drawing.Color.Transparent;
			this.panelGeometry.Controls.Add(this.propsGeometry);
			this.panelGeometry.Controls.Add(this.lblHeader_Geometry);
			this.panelGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelGeometry.Location = new System.Drawing.Point(0, 0);
			this.panelGeometry.Name = "panelGeometry";
			this.panelGeometry.Size = new System.Drawing.Size(249, 477);
			this.panelGeometry.TabIndex = 1;
			// 
			// propsGeometry
			// 
			this.propsGeometry.BackColor = System.Drawing.SystemColors.ControlDark;
			this.propsGeometry.CommandsDisabledLinkColor = System.Drawing.Color.Black;
			this.propsGeometry.DisabledItemForeColor = System.Drawing.Color.Black;
			this.propsGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propsGeometry.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.propsGeometry.HelpBackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.propsGeometry.HelpBorderColor = System.Drawing.SystemColors.ControlLight;
			this.propsGeometry.HelpForeColor = System.Drawing.SystemColors.Window;
			this.propsGeometry.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.propsGeometry.Location = new System.Drawing.Point(0, 24);
			this.propsGeometry.Name = "propsGeometry";
			this.propsGeometry.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.propsGeometry.Size = new System.Drawing.Size(249, 453);
			this.propsGeometry.TabIndex = 0;
			this.propsGeometry.ToolbarVisible = false;
			this.propsGeometry.ViewBackColor = System.Drawing.SystemColors.ControlDark;
			this.propsGeometry.ViewBorderColor = System.Drawing.SystemColors.ControlLight;
			this.propsGeometry.ViewForeColor = System.Drawing.SystemColors.Window;
			this.propsGeometry.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropsGeometry_PropertyValueChanged);
			// 
			// lblHeader_Geometry
			// 
			this.lblHeader_Geometry.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.lblHeader_Geometry.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblHeader_Geometry.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblHeader_Geometry.Location = new System.Drawing.Point(0, 0);
			this.lblHeader_Geometry.Name = "lblHeader_Geometry";
			this.lblHeader_Geometry.Size = new System.Drawing.Size(249, 24);
			this.lblHeader_Geometry.TabIndex = 0;
			this.lblHeader_Geometry.Text = "Geometry";
			// 
			// lblName
			// 
			this.lblName.AutoSize = true;
			this.lblName.BackColor = System.Drawing.Color.Transparent;
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblName.ForeColor = System.Drawing.SystemColors.Window;
			this.lblName.Location = new System.Drawing.Point(3, 6);
			this.lblName.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(67, 16);
			this.lblName.TabIndex = 3;
			this.lblName.Text = "Geometry";
			this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblVertexDesc
			// 
			this.lblVertexDesc.BackColor = System.Drawing.Color.Transparent;
			this.lblVertexDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblVertexDesc.ForeColor = System.Drawing.SystemColors.Window;
			this.lblVertexDesc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblVertexDesc.Location = new System.Drawing.Point(239, 0);
			this.lblVertexDesc.Name = "lblVertexDesc";
			this.lblVertexDesc.Size = new System.Drawing.Size(119, 29);
			this.lblVertexDesc.TabIndex = 6;
			this.lblVertexDesc.Text = "Vertex Description";
			this.lblVertexDesc.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lstVertexDesc
			// 
			this.lstVertexDesc.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.lstVertexDesc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstVertexDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lstVertexDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstVertexDesc.ForeColor = System.Drawing.SystemColors.Window;
			this.lstVertexDesc.FormattingEnabled = true;
			this.lstVertexDesc.Location = new System.Drawing.Point(364, 3);
			this.lstVertexDesc.Name = "lstVertexDesc";
			this.lstVertexDesc.Size = new System.Drawing.Size(177, 24);
			this.lstVertexDesc.TabIndex = 7;
			this.lstVertexDesc.SelectedIndexChanged += new System.EventHandler(this.lstVertexDesc_SelectedIndexChanged);
			// 
			// lblPositionAttr
			// 
			this.lblPositionAttr.BackColor = System.Drawing.Color.Transparent;
			this.lblPositionAttr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblPositionAttr.ForeColor = System.Drawing.SystemColors.Window;
			this.lblPositionAttr.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblPositionAttr.Location = new System.Drawing.Point(547, 0);
			this.lblPositionAttr.Name = "lblPositionAttr";
			this.lblPositionAttr.Size = new System.Drawing.Size(109, 29);
			this.lblPositionAttr.TabIndex = 4;
			this.lblPositionAttr.Text = "Position Attribute";
			this.lblPositionAttr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lstPositionAttr
			// 
			this.lstPositionAttr.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.lstPositionAttr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstPositionAttr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.lstPositionAttr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstPositionAttr.ForeColor = System.Drawing.SystemColors.Window;
			this.lstPositionAttr.FormattingEnabled = true;
			this.lstPositionAttr.Location = new System.Drawing.Point(662, 3);
			this.lstPositionAttr.Name = "lstPositionAttr";
			this.lstPositionAttr.Size = new System.Drawing.Size(190, 24);
			this.lstPositionAttr.TabIndex = 5;
			this.lstPositionAttr.SelectedIndexChanged += new System.EventHandler(this.LstPositionAttr_SelectedIndexChanged);
			// 
			// frmGeometry
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(871, 509);
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
			this.panelGeometry.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private ShaderToy_Components.controlProjectObject panelMain;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Panel panelCollapse;
		private System.Windows.Forms.Splitter splitterLeft;
		private System.Windows.Forms.Splitter splitterRight;
		private System.Windows.Forms.Panel panelLeft;
		private System.Windows.Forms.Panel panelRight;
		private System.Windows.Forms.Panel panelGeometry;
		private System.Windows.Forms.PropertyGrid propsGeometry;
		private System.Windows.Forms.Label lblPositionAttr;
		private System.Windows.Forms.ComboBox lstPositionAttr;
		private System.Windows.Forms.Label lblVertexDesc;
		private System.Windows.Forms.ComboBox lstVertexDesc;
		private System.Windows.Forms.Label lblHeader_Geometry;
	}
}