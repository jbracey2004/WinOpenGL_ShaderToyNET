using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinOpenGL_ShaderToy;
using static WinOpenGL_ShaderToy.controlUniformData;
using static modProject.clsUniformSet;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

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
			Value = "0";
		}
		public UniformType DataUniformType => this.DataUniformType;
		public List<object[]> DataObject
		{
			get
			{
				return StringToArray(Value as string, out int dummyCount, out int dummyType);
			}
			set
			{
				this.Value = ArrayToString(value);
			}
		}
		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			clsUniformDataEditor ctl = DataGridView.EditingControl as clsUniformDataEditor;
			ctl.EditingPanel = ctl.Parent;
			ctl.Parent = DataGridView.FindForm();
			ctl.BringToFront();
			if (this.Value == null)
			{
				ctl.Text = this.DefaultNewRowValue as string;
			}
			else
			{
				ctl.Text = this.Value as string;
			}
		}
		public override void DetachEditingControl()
		{
			DataGridView dataGridView = this.DataGridView;
			if (dataGridView == null || dataGridView.EditingControl == null)
				throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
			clsUniformDataEditor ctl = DataGridView.EditingControl as clsUniformDataEditor;
			if (ctl != null)
			{
				Value = ctl.Text;
				ctl.Parent = ctl.EditingPanel;
			}
			base.DetachEditingControl();
		}
		public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			clsUniformDataEditor ctl = DataGridView.EditingControl as clsUniformDataEditor;
			if (ctl == null) return;
			Rectangle ctlSize = new Rectangle(new Point(cellBounds.Location.X, cellBounds.Location.Y), new Size(cellBounds.Width, cellBounds.Height));
			base.PositionEditingControl(setLocation, setSize, ctlSize, ctlSize, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
			Point pos = cellBounds.Location;
			pos.Offset(DataGridView.Location);
			ctl.Location = pos;
			ctl.UpdateAutoHeight();
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
				return typeof(string);
			}
		}
		public override object DefaultNewRowValue
		{
			get
			{
				return "0";
			}
		}
		public override Type FormattedValueType => base.FormattedValueType;
		public override object ParseFormattedValue(object formattedValue, DataGridViewCellStyle cellStyle, TypeConverter formattedValueTypeConverter, TypeConverter valueTypeConverter)
		{
			//return base.ParseFormattedValue(formattedValue, cellStyle, formattedValueTypeConverter, valueTypeConverter);
			//string strRet = "";
			//if (formattedValue.GetType().IsArray) { strRet = ArrayToString(formattedValue); } else { strRet = formattedValue.ToString(); }
			//return strRet
			return formattedValue;
		}
		protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
		{
			return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
		}
	}
	public class clsUniformDataEditor : controlUniformData, IDataGridViewEditingControl
	{
		public DataGridView EditingControlDataGridView { get; set; }
		public Control EditingPanel { get; set; }
		public object EditingControlFormattedValue
		{
			get
			{
				return this.Text as string;
			}
			set
			{
				this.Text = value as string;
			}
		}
		public int EditingControlRowIndex { get; set; }
		public bool EditingControlValueChanged { get; set; }

		public Cursor EditingPanelCursor => Cursor.Current;

		public bool RepositionEditingControlOnValueChange => false;

		public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
		{
			
		}

		public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
		{
			return dataGridViewWantsInputKey;
		}

		public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
		{
			return this.Text;
		}

		protected override void OnValidating(CancelEventArgs e)
		{
			base.OnValidating(e);
			if (!e.Cancel)
			{
				EditingControlValueChanged = true;
				this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
			}
		}
		public void PrepareEditingControlForEdit(bool selectAll)
		{
			
		}
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			if(EditingControlDataGridView.IsCurrentCellInEditMode)
			{
				EditingControlDataGridView.EndEdit();
			}
		}
	}
}
