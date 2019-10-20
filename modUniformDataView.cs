using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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

		}
	}
	public class clsUniformDataEditor 
	{

	}
}
