using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using static clsHPTimer;
using modProject;

namespace ShaderToy_Components
{
	[System.ComponentModel.Designer(typeof(controlProjectObject.Designer))]
	public partial class controlProjectObject
	{
		public class Designer : ParentControlDesigner
		{
			private IDesignerHost designerHost;
			private controlProjectObject containerHost;
			private Panel containerCurrent;
			private Panel containerContent;
			private Panel containerStatus;
			public override void Initialize(System.ComponentModel.IComponent component)
			{
				base.Initialize(component);
				containerHost = (controlProjectObject)component;
				containerContent = containerHost.Content;
				containerStatus = containerHost.Status;
				containerCurrent = containerContent;
				EnableDesignMode(containerContent, "Content");
				EnableDesignMode(containerStatus, "Status");
				designerHost = (IDesignerHost)component.Site.GetService(typeof(IDesignerHost));
			}
			protected override bool AllowControlLasso
			{
				get
				{
					return true;
				}
			}
			protected override Control GetParentForComponent(IComponent component)
			{
				return containerContent;
			}
			public override int NumberOfInternalControlDesigners()
			{
				return 2;
			}
			public override ControlDesigner InternalControlDesigner(int internalControlIndex)
			{
				Control panelRet = null;
				switch (internalControlIndex)
				{
					case 0:
						{
							panelRet = containerContent;
							break;
						}

					case 1:
						{
							panelRet = containerStatus;
							break;
						}

					default:
						{
							panelRet = containerHost;
							break;
						}
				}
				return (ControlDesigner)designerHost.GetDesigner(panelRet);
			}
			protected override void Dispose(bool disposing)
			{
				designerHost = null;
				base.Dispose(disposing);
			}
		}
		public class clsDesigner : clsProjectObject
		{
			public clsDesigner() : base(ProjectObjectTypes.Designer)
			{
				Name = "ProjectObject";
			}
		}
		public static clsDesigner objPlaceHolder = new clsDesigner();
		public Control ParentControl { set; get; } = null;
		private clsProjectObject objProjectObject;
		public clsProjectObject ProjectObject
		{
			set
			{
				if (objProjectObject != null) objProjectObject.ParentControl = null;
				objProjectObject = value;
				if (objProjectObject != null)
				{
					txtName.Text = objProjectObject.Name;
					objProjectObject.ParentControl = this;
					UpdateTitle();
				}
			}
			get
			{
				return objProjectObject;
			}
		}
		public controlProjectObject()
		{
			InitializeComponent();
			ProjectObject = objPlaceHolder;
			ProjectObject.ParentControl = this;
		}
		public controlProjectObject(clsProjectObject refObj)
		{
			InitializeComponent();
			ProjectObject = refObj;
			ProjectObject.ParentControl = this;
		}
		private clsHPTimer timerUpdate;
		private void FrmProjectObject_HandleCreated(object sender, EventArgs e)
		{
			panelStatus.Controls.SetChildIndex(txtName, 1);
			if(ProjectObject != null)
			{
				txtName.Text = ProjectObject.Name;
			}
			UpdateTitle();
			timerUpdate = new clsHPTimer(this);
			timerUpdate.Interval = 1000.0;
			timerUpdate.SleepInterval = 250;
			timerUpdate.Performence.HistoryDuration = timerUpdate.Interval / 1000.0;
			timerUpdate.IntervalEnd += new HPIntervalEventHandler(timerUpdate_Tick);
			timerUpdate.Start();
		}
		private void FrmProjectObject_HandleDestroyed(object sender, EventArgs e)
		{
			if(timerUpdate != null)
			{
				timerUpdate.Stop();
				timerUpdate = null;
			}
		}
		private void FrmProjectObject_Disposed(object sender, EventArgs e)
		{
			if (ProjectObject != null) ProjectObject.ParentControl = null;
		}
		protected virtual void timerUpdate_Tick(object sender, HPIntervalEventArgs e)
		{
			if (ProjectObject != null)
			{
				txtName.Text = ProjectObject.Name;
				UpdateTitle();
			}
		}
		public virtual void UpdateTitle()
		{
			Text = ProjectObject.ToString();
			if(ParentControl != null)
			{
				ParentControl.Text = ProjectObject.ToString();
			}
		}
		private void TxtName_TextChanged(object sender, EventArgs e)
		{
			ProjectObject.Name = txtName.Text;
			UpdateTitle();
		}
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Panel Content { get { return panelContent; } }
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public Panel Status { get { return panelStatus; } }
		public override string ToString()
		{
			return $"Control<{Text}>";
		}
	}
}
