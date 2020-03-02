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
			this.groupGeometry = new System.Windows.Forms.GroupBox();
			this.panelGeometry = new System.Windows.Forms.Panel();
			this.propsGeometry = new System.Windows.Forms.PropertyGrid();
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
			this.groupGeometry.SuspendLayout();
			this.panelGeometry.SuspendLayout();
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
			this.panelMain.Content.Size = new System.Drawing.Size(871, 479);
			this.panelMain.Content.TabIndex = 2;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 0);
			this.panelMain.Margin = new System.Windows.Forms.Padding(0);
			this.panelMain.Name = "panelMain";
			this.panelMain.ParentControl = this;
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
			this.panelCollapse.BackColor = System.Drawing.SystemColors.Control;
			this.panelCollapse.Controls.Add(this.splitterLeft);
			this.panelCollapse.Controls.Add(this.splitterRight);
			this.panelCollapse.Controls.Add(this.panelLeft);
			this.panelCollapse.Controls.Add(this.panelRight);
			this.panelCollapse.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelCollapse.ForeColor = System.Drawing.SystemColors.ControlText;
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
			this.panelRight.Controls.Add(this.groupGeometry);
			this.panelRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelRight.Location = new System.Drawing.Point(620, 0);
			this.panelRight.Name = "panelRight";
			this.panelRight.Size = new System.Drawing.Size(249, 477);
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
			this.groupGeometry.Size = new System.Drawing.Size(249, 477);
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
			this.panelGeometry.Size = new System.Drawing.Size(243, 456);
			this.panelGeometry.TabIndex = 1;
			// 
			// propsGeometry
			// 
			this.propsGeometry.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propsGeometry.Location = new System.Drawing.Point(0, 0);
			this.propsGeometry.Name = "propsGeometry";
			this.propsGeometry.Size = new System.Drawing.Size(243, 456);
			this.propsGeometry.TabIndex = 0;
			this.propsGeometry.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropsGeometry_PropertyValueChanged);
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
			// lblVertexDesc
			// 
			this.lblVertexDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			this.lstVertexDesc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstVertexDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lstVertexDesc.FormattingEnabled = true;
			this.lstVertexDesc.Location = new System.Drawing.Point(364, 3);
			this.lstVertexDesc.Name = "lstVertexDesc";
			this.lstVertexDesc.Size = new System.Drawing.Size(177, 24);
			this.lstVertexDesc.TabIndex = 7;
			this.lstVertexDesc.SelectedIndexChanged += new System.EventHandler(this.lstVertexDesc_SelectedIndexChanged);
			// 
			// lblPositionAttr
			// 
			this.lblPositionAttr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			this.lstPositionAttr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.lstPositionAttr.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
			this.groupGeometry.ResumeLayout(false);
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
		private System.Windows.Forms.GroupBox groupGeometry;
		private System.Windows.Forms.Panel panelGeometry;
		private System.Windows.Forms.PropertyGrid propsGeometry;
		private System.Windows.Forms.Label lblPositionAttr;
		private System.Windows.Forms.ComboBox lstPositionAttr;
		private System.Windows.Forms.Label lblVertexDesc;
		private System.Windows.Forms.ComboBox lstVertexDesc;
	}
}