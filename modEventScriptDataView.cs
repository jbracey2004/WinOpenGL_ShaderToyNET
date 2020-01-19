using System;
using modProject;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinOpenGL_ShaderToy;
using static WinOpenGL_ShaderToy.controlEventScript;
using static modProject.clsEventScript;
using System.Drawing;
using WeifenLuo.WinFormsUI.Docking;

namespace modEventScriptView
{
	public class clsEventScriptColumn : DataGridViewColumn
	{
		public clsEventScriptColumn() : base(new clsEventScriptCell()) {}
		public override DataGridViewCell CellTemplate
		{
			get => base.CellTemplate;
			set
			{
				if (value != null &&
					!value.GetType().IsAssignableFrom(typeof(clsEventScriptCell)))
				{
					throw new InvalidCastException("Must be a clsEventScriptCell");
				}
				base.CellTemplate = value;
			}
		}
	}
	public class clsEventScriptCell : DataGridViewTextBoxCell
	{
		public clsEventScriptCell() : base()
		{
			Value = "";
		}
		public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
		{
			base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
			clsEventScriptEditor ctl = DataGridView.EditingControl as clsEventScriptEditor;
			ctl.Text = (Value.ToString() != null)?(Value as string):"OnLoad() {}";
		}
		public override void DetachEditingControl()
		{
			DataGridView dataGridView = this.DataGridView;
			if (dataGridView == null || dataGridView.EditingControl == null)
				throw new InvalidOperationException("Cell is detached or its grid has no editing control.");
			clsEventScriptEditor ctl = DataGridView.EditingControl as clsEventScriptEditor;
			if (ctl != null)
			{
				Value = ctl.Text;
				ctl.Parent = ctl.EditingPanel;
				ctl.EditingPanel = null;
			}
			base.DetachEditingControl();
		}
		public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds, Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded, bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
		{
			clsEventScriptEditor ctl = DataGridView.EditingControl as clsEventScriptEditor;
			if (ctl == null) return;
			if (ctl.Parent != DataGridView.EditingPanel) return;
			ctl.Parent = ctl.ParentForm;
			Rectangle ctlSize = new Rectangle(new Point(cellBounds.Location.X, cellBounds.Location.Y), new Size(Math.Max(cellBounds.Width, 400), Math.Max(cellBounds.Height, ctl.Height)));
			base.PositionEditingControl(setLocation, setSize, ctlSize, ctlSize, cellStyle, singleVerticalBorderAdded, singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);
			ctl.Location = ctl.Parent.PointToClient(DataGridView.PointToScreen(cellBounds.Location));
			ctl.EditingPanel = DataGridView.EditingPanel;
			ctl.BringToFront();
		}
		public override Type EditType
		{
			get
			{
				return typeof(clsEventScriptEditor);
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
				return "";
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
	}
	public class clsEventScriptEditor : controlEventScript, IDataGridViewEditingControl
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
		protected override void OnSizeChanged(EventArgs e)
		{
			if (EditingPanel != null) EditingPanel.SetBounds(Left, Top, Width, Height);
			base.OnSizeChanged(e);
		}
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			if(EditingControlDataGridView.IsCurrentCellInEditMode)
			{
				EditingControlValueChanged = true;
				this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
				EditingControlDataGridView.EndEdit();
			}
		}
	}
}
