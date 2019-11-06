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
using modUniformDataView;
using static generalUtils;
using static WinOpenGL_ShaderToy.controlUniformData;
using static modProject.clsUniformSet;
using OpenTK;

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
			DataGridViewComboBoxColumn columnCombo = ((DataGridViewComboBoxColumn)datagridUniforms.Columns["columnType"]);
			clsUniformDataColumn columnValue = new clsUniformDataColumn
			{
				HeaderText = "Value",
				Name = "columnValue"
			};
			datagridUniforms.Columns.Add(columnValue);
			for (int itr = 0; itr < UniformBindDelegate.Keys.Count; itr++)
			{
				columnCombo.Items.Add(UniformBindDelegate.Keys.ElementAt(itr).ToString());
			}
		}

		private void datagridUniforms_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
			DataGridViewCell cell = datagridUniforms.Rows[e.RowIndex].Cells[e.ColumnIndex];
			if(cell.OwningColumn.Name == "columnType")
			{
				clsUniformDataCell cellValue = datagridUniforms.Rows[e.RowIndex].Cells["columnValue"] as clsUniformDataCell;
				
			}
		}
	}
}
