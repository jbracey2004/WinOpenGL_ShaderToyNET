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
using System.Linq;
using OpenTK.Graphics.OpenGL;

namespace WinOpenGL_ShaderToy
{
	public partial class frmVertexDescription : DockContent
	{
		public frmVertexDescription(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public clsVertexDescription VertexDescription { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsVertexDescription; } }
		private bool bolDataGridReady = false;
		private void FrmProgram_Load(object sender, EventArgs e)
		{
			DataGridViewComboBoxColumn rowCombo = ((DataGridViewComboBoxColumn)datagridVertexDescriptions.Columns["columnElementType"]);
			for (int itr = 0; itr < clsGeometry.clsVertexDescriptionComponent.VertexTypes.Keys.Count; itr++)
			{
				rowCombo.Items.Add(clsGeometry.clsVertexDescriptionComponent.VertexTypes.Keys.ElementAt(itr).ToString());
			}
			for (int itr = 0; itr < VertexDescription.Count; itr++)
			{
				int idxrow = datagridVertexDescriptions.Rows.Add(
					VertexDescription[itr].Index,
					VertexDescription[itr].Name,
					VertexDescription[itr].ElementGLType.ToString(),
					VertexDescription[itr].ElementCount);
				DataGridViewRow row = datagridVertexDescriptions.Rows[idxrow];
				row.Tag = VertexDescription[itr];
			}
			bolDataGridReady = true;
			ProjectDef.AllForms.Add(this);
		}
		private void FrmProgram_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			panelMain.ProjectObject = null;
			ProjectDef.AllForms.Remove(this);
		}
		private void DatagridVertexDescriptions_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			if (!bolDataGridReady) return;
			bolDataGridReady = false;
			clsVertexDescriptionComponent compNew = VertexDescription.Add(VertexAttribPointerType.Byte, "", 1, (object)0);
			DataGridViewRow rowNew = datagridVertexDescriptions.Rows[e.RowIndex - 1];
			rowNew.Tag = compNew;
			rowNew.Cells["columnIndex"].Value = compNew.Index.ToString();
			rowNew.Cells["columnElementType"].Value = compNew.ElementGLType.ToString();
			rowNew.Cells["columnElementCount"].Value = compNew.ElementCount.ToString();
			bolDataGridReady = true;
		}
		private void DatagridVertexDescriptions_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			List<clsVertexDescriptionComponent> aryToDelete = new List<clsVertexDescriptionComponent>();
			foreach (clsVertexDescriptionComponent vrt in VertexDescription)
			{
				bool bolFound = false;
				for (int itr = 0; itr < datagridVertexDescriptions.Rows.Count; itr++)
				{
					DataGridViewRow row = datagridVertexDescriptions.Rows[itr];
					int vrtIdx = -1;
					string strIdx = (string)(row.Cells["columnIndex"].Value ?? " ");
					if (strIdx != null) int.TryParse(strIdx, out vrtIdx);
					if (vrt.Index == vrtIdx) { bolFound = true; break; }
				}
				if (!bolFound) aryToDelete.Add(vrt);
			}
			for (int itr = 0; itr < aryToDelete.Count; itr++) VertexDescription.Remove(aryToDelete[itr]);
		}
		private void DatagridVertexDescriptions_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (!bolDataGridReady) return;
			DataGridViewRow rowData = datagridVertexDescriptions.Rows[e.RowIndex];
			clsVertexDescriptionComponent vrt = rowData.Tag as clsVertexDescriptionComponent;
			if (vrt == null) return;
			vrt.Index = int.Parse(rowData.Cells["columnIndex"].Value.ToString());
			vrt.Name = (string)rowData.Cells["columnComponentName"].Value;
			vrt.ElementGLType = (VertexAttribPointerType)Enum.Parse(typeof(VertexAttribPointerType), rowData.Cells["columnElementType"].Value.ToString());
			vrt.ElementCount = int.Parse(rowData.Cells["columnElementCount"].Value.ToString());
			for (int itrRow = 0; itrRow < datagridVertexDescriptions.Rows.Count; itrRow++)
			{
				DataGridViewRow rowItr = datagridVertexDescriptions.Rows[itrRow];
				clsVertexDescriptionComponent vrtItr = rowItr.Tag as clsVertexDescriptionComponent;
				if (vrtItr != null) rowItr.Cells["columnIndex"].Value = vrtItr.Index.ToString();
			}
		}
		private void DatagridVertexDescriptions_DataError(object sender, DataGridViewDataErrorEventArgs e)
		{
			Console.WriteLine($"Vertex Description: DataError {{{e.Exception}}} - Row{e.RowIndex} Column{e.ColumnIndex}");
		}
	}
}
