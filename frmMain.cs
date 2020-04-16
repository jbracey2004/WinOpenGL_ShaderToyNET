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
			DefaultScriptInit();
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
			CloseAllForms();
			projectMain.Dispose();
			projectMain = null;
			if(windowProject != null)
			{
				windowProject.HideOnClose = false;
				windowProject.Close();
				windowProject.Dispose();
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
			CloseAllForms();
			projectMain.Dispose();
			projectMain = new clsProject();
			windowProject.Project = projectMain;
			windowProject.UpdateProjectTree();
		}
		private void menuFile_SaveProject_Click(object sender, EventArgs e)
		{
			dialogSave.FileName = "";
			DialogResult resultDialog = dialogSave.ShowDialog(this);
			if (resultDialog == DialogResult.Cancel || resultDialog == DialogResult.Abort) return;
			Stream file = dialogSave.OpenFile();
			XmlSerializer writeBin = new XmlSerializer(typeof(Xml_Project), ProjectXmlTypes);
			writeBin.Serialize(file, projectMain.Xml);
			file.Close();
			file.Dispose();
		}
		private void menuFile_LoadProject_Click(object sender, EventArgs e)
		{
			dialogLoad.FileName = "";
			DialogResult resultDialog = dialogLoad.ShowDialog(this);
			if (resultDialog == DialogResult.Cancel || resultDialog == DialogResult.Abort) return;
			if (!File.Exists(dialogLoad.FileName)) return;
			Stream file = dialogLoad.OpenFile();
			XmlSerializer readBin = new XmlSerializer(typeof(Xml_Project), ProjectXmlTypes);
			Xml_Project XmlProject = (Xml_Project)readBin.Deserialize(file);
			file.Close();
			file.Dispose();
			CloseAllForms();
			projectMain.Dispose();
			projectMain = new clsProject();
			XmlProject.InitObject(ref projectMain);
			windowProject.Project = projectMain;
			windowProject.UpdateProjectTree();
		}
		private void CloseAllForms()
		{
			DockContent[] ary = ProjectDef.AllForms.ToArray();
			foreach(var frm in ary)
			{
				frm.Close();
				frm.Dispose();
			}
		}
	}
}
