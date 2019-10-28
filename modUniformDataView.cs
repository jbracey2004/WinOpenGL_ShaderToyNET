using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinOpenGL_ShaderToy;
using static modProject.clsUniformSet;

namespace modUniformDataView
{
	public class clsUniformDataColumn : DataGridViewColumn
	{
		public clsUniformDataColumn() : base(new clsUniformDataCell()) {}
		public override DataGridViewCell CellTemplate
		{
			get => base.CellTemplate;
			set
			{
				if (value != null &&
					!value.GetType().IsAssignableFrom(typeof(clsUniformDataCell)))
				{
					throw new InvalidCastException("Must be a clsUniformDataCell");
				}
				base.CellTemplate = value;
			}
		}
	}
	public class clsUniformDataCell : DataGridViewTextBoxCell
	{
		public clsUniformDataCell() : base()
		{

		}
		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			clsUniformDataEditor ctl = DataGridView.EditingControl as clsUniformDataEditor;
			if (DataGridView.Columns.Contains("columnType"))
			{
				object objTyp = DataGridView.Rows[rowIndex].Cells["columnType"].Value;
				if(objTyp != null)
				{
					UniformType Typ = (UniformType)Enum.Parse(typeof(UniformType), objTyp.ToString());
					ctl.SetComponentDescriptionFromDataType(Typ);
				}
			}
			if (this.Value == null)
			{
				ctl.DataObject.Data = this.DefaultNewRowValue;
			}
			else
			{
				ctl.DataObject.Data = this.Value;
			}
		}

		public override Type EditType
		{
			get
			{
				return typeof(clsUniformDataEditor);
			}
		}

		public override Type ValueType
		{
			get
			{
				return typeof(object);
			}
		}

		public override object DefaultNewRowValue
		{
			get
			{
				return 0;
			}
		}
	}
	public class clsUniformDataEditor : controlUniformData, IDataGridViewEditingControl
	{
		public DataGridView EditingControlDataGridView { get; set; }
		public object EditingControlFormattedValue { get; set; }
		public int EditingControlRowIndex { get; set; }
		public bool EditingControlValueChanged { get; set; }

		public Cursor EditingPanelCursor => Cursor.Current;

		public bool RepositionEditingControlOnValueChange => true;

		public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			
		}

		public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
		{
			return dataGridViewWantsInputKey;
		}

		public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return this.DataObject.Data;
		}

		public void PrepareEditingControlForEdit(bool selectAll)
		{
			
		}
	}
}
