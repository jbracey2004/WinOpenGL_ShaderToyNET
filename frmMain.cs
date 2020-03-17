using modProject;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml.Serialization;
using WeifenLuo.WinFormsUI.Docking;
using static modProject.modXml;
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
			dockMainPanel = dockMain;
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

		private void menuFile_NewProject_Click(object sender, EventArgs e)
		{
			projectMain.Dispose();
			windowProject.UpdateProjectTree();
		}
		private void menuFile_SaveProject_Click(object sender, EventArgs e)
		{
			dialogSave.ShowDialog();
			Stream file = dialogSave.OpenFile();
			XmlSerializer writeBin = new XmlSerializer(typeof(Xml_Project), ProjectXmlTypes);
			writeBin.Serialize(file, projectMain.Xml);
			file.Close();
			file.Dispose();
		}
		private void menuFile_LoadProject_Click(object sender, EventArgs e)
		{
			dialogLoad.ShowDialog();
			Stream file = dialogLoad.OpenFile();
			XmlSerializer readBin = new XmlSerializer(typeof(Xml_Project), ProjectXmlTypes);
			Xml_Project XmlProject = (Xml_Project)readBin.Deserialize(file);
			file.Close();
			file.Dispose();
			projectMain.Dispose();
			projectMain = new clsProject();
			XmlProject.InitObject(ref projectMain);
			windowProject.Project = projectMain;
			windowProject.UpdateProjectTree();
		}
	}
}
