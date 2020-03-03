using modProject;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
		private frmProject windowProject;
		private void FormMain_Load(object sender, EventArgs e)
		{
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
			projectMain.Dispose();
			projectMain = null;
			if(windowProject != null)
			{
				windowProject.HideOnClose = false;
				windowProject.Close();
				windowProject = null;
			}
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

		private void menuFile_SaveProject_Click(object sender, EventArgs e)
		{
			dialogSave.ShowDialog();
			Stream file = dialogSave.OpenFile();
			BinaryFormatter writeBin = new BinaryFormatter();
			writeBin.Serialize(file, projectMain);
			file.Close();
			file.Dispose();
		}
		private void menuFile_LoadProject_Click(object sender, EventArgs e)
		{
			dialogLoad.ShowDialog();
			Stream file = dialogLoad.OpenFile();
			BinaryFormatter readBin = new BinaryFormatter();
			projectMain.Dispose();
			projectMain = null;
			projectMain = (clsProject)readBin.Deserialize(file);
			file.Close();
			file.Dispose();
			windowProject.Project = projectMain;
			windowProject.UpdateProjectTree();
		}
	}
}
