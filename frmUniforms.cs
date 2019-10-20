using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using modProject;
namespace WinOpenGL_ShaderToy
{
	public partial class frmUniforms : DockContent
	{
		public frmUniforms()
		{
			InitializeComponent();
		}
		private void FrmUniforms_Load(object sender, EventArgs e)
		{
			DataGridViewComboBoxColumn rowCombo = ((DataGridViewComboBoxColumn)datagridUniforms.Columns["columnType"]);
			for (int itr = 0; itr < clsUniformSet.UniformBindDelegate.Keys.Count; itr++)
			{
				rowCombo.Items.Add(clsUniformSet.UniformBindDelegate.Keys.ElementAt(itr).ToString());
			}
		}

		private void DatagridUniforms_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			DataGridViewCell cellCurrent = datagridUniforms.CurrentCell;
			DataGridViewColumn columnCurrent = cellCurrent.OwningColumn;
			Panel panelEdit = datagridUniforms.EditingPanel;
			Control controlEdit = datagridUniforms.EditingControl;
		}
		private void DatagridUniforms_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			DataGridViewCell cellCurrent = datagridUniforms.CurrentCell;
			DataGridViewColumn columnCurrent = cellCurrent.OwningColumn;
			Panel panelEdit = datagridUniforms.EditingPanel;
			Control controlEdit = new PropertyGrid();
			panelEdit.Controls.Add(controlEdit);
		}
	}
}
