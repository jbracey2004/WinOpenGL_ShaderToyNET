using modProject;
using System;
using WeifenLuo.WinFormsUI.Docking;
using static modProject.clsProjectObject;
using static WinOpenGL_ShaderToy.ProjectDef;
using static clsHPTimer;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static modProject.clsGeometry;
using System.Collections.Generic;

namespace WinOpenGL_ShaderToy
{
	public partial class frmProgram : DockContent
	{
		public frmProgram(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public clsProgram Program { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsProgram; } }
		private clsHPTimer timerAutoLink;
		private void FrmProgram_Load(object sender, EventArgs e)
		{
			UpdateLists();
			LinkShaders();
			panelMain.Timer_IntervalUpdate += timerUpdateLists_EndInterval;
			timerAutoLink = new clsHPTimer(this);
			timerAutoLink.Interval = 4000.0;
			timerAutoLink.SleepInterval = 4000;
			timerAutoLink.IntervalEnd += timerAutoLink_EndInterval;
			timerAutoLink.Start();
			ProjectDef.AllForms.Add(this);
		}
		private void FrmProgram_FormClosing(object sender, FormClosingEventArgs e)
		{
			DockPanel = null;
			timerAutoLink.Stop();
			timerAutoLink.Dispose();
			timerAutoLink = null;
			panelMain.ProjectObject = null;
			ProjectDef.AllForms.Remove(this);
		}
		private void timerUpdateLists_EndInterval(object sender, HPIntervalEventArgs e)
		{
			UpdateLists();
		}
		private void UpdateLists()
		{
			UpdateShaderList();
		}
		private void timerAutoLink_EndInterval(object sender, HPIntervalEventArgs e)
		{
			if(chkAutoLink.Checked)
			{
				LinkShaders();
			}
		}
		private void UpdateShaderList()
		{
			if(Program != null)
			{
				List<clsProjectObject> lst = projectMain.ProjectObjects.FindAll(itm => itm as clsShader != null);
				Program.Shaders.RemoveAll(itm => !lst.Contains(itm));
				if(Program.Shaders.Count != datagridShaderLinks.Rows.Count-1)
				{
					List<DataGridViewRow> aryRowsToRemove = new List<DataGridViewRow>();
					List<clsProjectObject> aryObjsToAdd = new List<clsProjectObject>(Program.Shaders);
					foreach (DataGridViewRow rowItr in datagridShaderLinks.Rows)
					{
						if (!rowItr.IsNewRow && !rowItr.Cells["columnShader"].IsInEditMode)
						{
							object obj = rowItr.Cells["columnShader"].EditedFormattedValue;
							if (obj != null)
							{
								string str = obj.ToString();
								clsProjectObject shd = projectMain.ProjectObjects.Find(itm => itm.ToString() == str);
								if (lst.FindIndex(itm => itm.ToString() == str) < 0) aryRowsToRemove.Add(rowItr);
								if (Program.Shaders.FindIndex(itm => itm == shd) >= 0) aryObjsToAdd.Remove(shd);
							}
						}
					}
					foreach (DataGridViewRow rowItr in aryRowsToRemove) datagridShaderLinks.Rows.Remove(rowItr);
					foreach (clsProjectObject shdItr in aryObjsToAdd) datagridShaderLinks.Rows.Add(shdItr.ToString());
				}
				DataGridViewColumn columnData = datagridShaderLinks.Columns["columnShader"];
				DataGridViewComboBoxColumn columnDropDowns = columnData as DataGridViewComboBoxColumn;
				columnDropDowns.Items.Clear();
				foreach(clsShader shaderItm in lst)
				{
					columnDropDowns.Items.Add(shaderItm.ToString());
				}
			}
		}
		private void LinkShaders()
		{
			if (Program != null)
			{
				lblLinkStatus.ForeColor = Color.Blue;
				lblLinkStatus.Text = "Link Status: Linking...";
				Program.Link();
				UpdateStatus();
			}
		}
		private void ChkAutoLink_CheckedChanged(object sender, EventArgs e)
		{
			btnLink.Enabled = !chkAutoLink.Checked;
		}
		private void BtnLink_Click(object sender, EventArgs e)
		{
			LinkShaders();
		}
		private void UpdateStatus()
		{
			if (Program != null)
			{
				if (Program.LinkInfo.ErrorMessages.Length > 0)
				{
					lblLinkStatus.ForeColor = Color.Red;
					lblLinkStatus.Text = "Link Status: Link Failed.";
				}
				else if (Program.LinkInfo.WarningMessages.Length > 0)
				{
					lblLinkStatus.ForeColor = Color.RoyalBlue;
					lblLinkStatus.Text = "Link Status: Linked* See Messages.";
				}
				else if (Program.LinkInfo.AllMessages.Length > 0)
				{
					lblLinkStatus.ForeColor = Color.RoyalBlue;
					lblLinkStatus.Text = "Link Status: Linked* See Messages.";
				}
				else
				{
					lblLinkStatus.ForeColor = Color.Lime;
					lblLinkStatus.Text = "Link Status: Linked. Good.";
				}
				chkLinkErrors.Text = $"Errors: {Program.LinkInfo.ErrorMessages.Length}";
				chkLinkWarnings.Text = $"Warnings: {Program.LinkInfo.WarningMessages.Length}";
				dataLinkStatus.DataSource = Array.FindAll(Program.LinkInfo.AllMessages, itm =>
				{
					if (itm.Level == "ERROR" && !chkLinkErrors.Checked) return false;
					if (itm.Level == "WARNING" && !chkLinkWarnings.Checked) return false;
					return true;
				});
				dataLinkStatus.Columns["Message"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			}
		}
		private void ChkLinkErrors_CheckedChanged(object sender, EventArgs e)
		{
			UpdateStatus();
		}
		private void ChkLinkWarnings_CheckedChanged(object sender, EventArgs e)
		{
			UpdateStatus();
		}
		private void datagridShaderLinks_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			if (Program != null)
			{
				DataGridViewComboBoxCell cell = datagridShaderLinks.Rows[e.Row.Index-1].Cells["columnShader"] as DataGridViewComboBoxCell;
				clsShader shd = projectMain.ProjectObjects.Find(itm => (cell.EditedFormattedValue!=null)?(itm.ToString() == (string)cell.EditedFormattedValue):false) as clsShader;
				Program.Shaders.Add(shd);
				UpdateShaderList();
			}
		}
		private void datagridShaderLinks_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			if (Program != null)
			{
				Program.Shaders.RemoveRange(e.RowIndex, e.RowCount);
				UpdateShaderList();
			}
		}
		private void datagridShaderLinks_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			DataGridViewComboBoxEditingControl control = e.Control as DataGridViewComboBoxEditingControl;
			DataGridViewCell cell = datagridShaderLinks.CurrentCell;
			string strValue = "";
			if(cell.Value != null) strValue = cell.Value.ToString();
			List<object> aryObjsToRemove = new List<object>();
			if(control != null)
			{
				foreach (object itmItr in control.Items)
				{
					if(strValue != itmItr.ToString())
					{
						clsShader shd = Program.Shaders.Find(itm => itm.ToString() == itmItr.ToString()) as clsShader;
						if (shd != null) aryObjsToRemove.Add(itmItr);
					}
				}
				foreach (object itm in aryObjsToRemove) control.Items.Remove(itm);
			}
		}
		private void datagridShaderLinks_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			e.Cancel = true;
		}

		private void datagridShaderLinks_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (Program != null)
			{
				DataGridViewComboBoxCell cell = datagridShaderLinks.Rows[e.RowIndex].Cells[e.ColumnIndex] as DataGridViewComboBoxCell;
				clsShader shd = projectMain.ProjectObjects.Find(itm => (cell.EditedFormattedValue != null) ? (itm.ToString() == (string)cell.EditedFormattedValue) : false) as clsShader;
				Program.Shaders[e.RowIndex] = shd;
			}
		}

		private void datagridShaderLinks_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{

		}
	}
}
