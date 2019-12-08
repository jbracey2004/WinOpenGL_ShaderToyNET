using modProject;
using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static WinOpenGL_ShaderToy.ProjectDef;

namespace WinOpenGL_ShaderToy
{
	public partial class frmMain : Form
	{
		public frmMain()
		{
			InitializeComponent();
		}
		private DockContent windowProject;
		private void FormMain_Load(object sender, EventArgs e)
		{
			frmShaderIOLinks frm = new frmShaderIOLinks();
			frm.Show();
			formMain = this;
			glInit(Handle);
			dockMainPanel = this.dockMain;
			projectMain = new clsProject();
			windowProject = new frmProject(projectMain);
			if (menuProject.Checked)
			{
				windowProject.Show(dockMain, DockState.DockRight);
			}
		}
		private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			Array ary = projectMain.ProjectObjects.ToArray();
			foreach(clsProjectObject obj in ary)
			{
				if (obj.ParentControl != null)
				{
					if (obj.ParentControl.ParentForm != null)
					{
						obj.ParentControl.ParentForm.Close();
					}
					else
					{
						obj.ParentControl.Dispose();
					}
				}
				obj.Dispose();
			}
			projectMain.ProjectObjects.Clear();
			windowProject.HideOnClose = false;
			windowProject.Close();
			windowProject = null;
		}
		private void MenuWindows_Closing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			if(e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
			{
				e.Cancel = true;
			}
		}
		private void MenuProject_CheckedChanged(object sender, EventArgs e)
		{
			if (menuProject.Checked)
			{
				windowProject.Show(dockMain);
			} else
			{
				windowProject.Hide();
			}
		}
	}
}
