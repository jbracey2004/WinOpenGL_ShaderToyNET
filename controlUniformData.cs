using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static generalUtils;
using static modProject.clsUniformSet;
using System.Text.RegularExpressions;
namespace WinOpenGL_ShaderToy
{
	public partial class controlUniformData : UserControl
	{
		public List<object[]> DataObject { get; set; } = new List<object[]>();
		private UniformType typeUniformData = UniformType.Int;
		public UniformType DataUniformType { get => typeUniformData; set { SetComponentDescriptionFromDataType(value);} }
		public int DataComponentCount
		{
			get => UniformType_ComponentCount(typeUniformData);
			set
			{
				List<int> dimsCurrent = new List<int>();
				foreach (Match regMatch in Regex.Matches(typeUniformData.ToString(), @"\d+"))
				{
					int intNum;
					string strNum = regMatch.Value;
					if (int.TryParse(strNum, out intNum)) dimsCurrent.Add(intNum);
				}
				if(dimsCurrent.Count == 0)
				{
					if(value > 1)
					{
						UniformType enumNew;
						string strNew = typeUniformData.ToString() + dimsCurrent[0].ToString();
						if (Enum.TryParse(strNew, out enumNew)) DataUniformType = enumNew;
					}
				} else if (dimsCurrent.Count == 1) {
					UniformType enumNew;
					string strNew = Regex.Replace(typeUniformData.ToString(), @"\d+", value.ToString());
					if (Enum.TryParse(strNew, out enumNew)) DataUniformType = enumNew;
				}
			}
		}
		public Type DataComponentType { get => UniformType_ComponentType[typeUniformData]; }
		public controlUniformData()
		{
			InitializeComponent();
			DataObject.Add(new object[] { 0 });
		}
		private void controlUniformData_Load(object sender, EventArgs e)
		{
			foreach (string itm in Enum.GetNames(typeof(UniformType)))
			{
				lstType.Items.Add(itm);
			}
			lstType.SelectedIndex = (int)typeUniformData;
		}
		private void SetComponentDescriptionFromDataType(UniformType Typ)
		{
			int intComponentCount = UniformType_ComponentCount(Typ);
			Type typComp = UniformType_ComponentType[Typ];
			for (int itr = 0; itr < DataObject.Count; itr++)
			{
				List<object> ary = new List<object>(DataObject[itr]);
				ResizeList(ref ary, intComponentCount, itmEmpty => 0);
				DataObject[itr] = ary.ToArray();
			}
			typeUniformData = Typ;
			bolTypeLocked = true;
			datagridData.Columns.Clear();
			datagridData.Rows.Clear();
			for(int itr = 0; itr < intComponentCount; itr++)
			{
				datagridData.Columns.Add($"column{itr}", itr.ToString());
			}
			datagridData.Rows.Add(DataObject.Count);
			for (int itr = 0; itr < DataObject.Count; itr++)
			{
				for(int itrComp = 0; itrComp < intComponentCount; itrComp++)
				{
					datagridData.Rows[itr].Cells[itrComp].Value = DataObject[itr][itrComp].ToString();
				}
			}
			if(lstType.Items.Count != 0)
			{
				lstType.SelectedIndex = (int)Typ;
			}
			bolTypeLocked = false;
			UpdateRowHeaders();
			UpdateAutoHeight();
		}
		private void datagridData_RowHeightChanged(object sender, DataGridViewRowEventArgs e)
		{
			UpdateRowHeaders();
			UpdateAutoHeight();
		}
		private void datagridData_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			UpdateRowHeaders();
			UpdateAutoHeight();
		}
		private void datagridData_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
		{
			UpdateRowHeaders();
			UpdateAutoHeight();
		}
		private bool bolTypeLocked = false;
		private void datagridData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			if (e.RowIndex >= DataObject.Count) { return; }
			DataGridViewCell cell = datagridData.Rows[e.RowIndex].Cells[e.ColumnIndex];
			double.TryParse(e.FormattedValue.ToString(), out double fValue);
			cell.Value = Convert.ChangeType(fValue, DataComponentType).ToString();
		}
		private void datagridData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (bolTypeLocked) return;
			if (e.ColumnIndex < 0) return;
			if (e.RowIndex >= DataObject.Count) { return; }
			DataGridViewCell cell = datagridData.Rows[e.RowIndex].Cells[e.ColumnIndex];
			string strValue = cell.Value as string;
			double.TryParse(strValue, out double fValue);
			DataObject[e.RowIndex][e.ColumnIndex] = Convert.ChangeType(fValue, DataComponentType);
		}
		private void datagridData_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			int intCompLen = DataComponentCount;
			object[] elemNew = new object[intCompLen];
			for (int itrComp = 0; itrComp < elemNew.Length; itrComp++)
			{
				elemNew[itrComp] = 0;
			}
			DataObject.Add(elemNew);
		}
		private void datagridData_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
		{
			DataObject.RemoveAt(e.Row.Index);
		}
		private void lstType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (bolTypeLocked) return;
			UniformType val;
			if(Enum.TryParse(lstType.SelectedItem.ToString(), out val))
			{
				DataUniformType = val;
			}
		}
		public static string ArrayToString(List<object[]> ary)
		{
			string strRet = "";
			for (int itr = 0; itr < ary.Count; itr++)
			{
				if(ary.Count > 1) strRet += "(";
				for (int itrComp = 0; itrComp < ary[itr].Length; itrComp++)
				{
					strRet += ary[itr][itrComp].ToString();
					if (itrComp < ary[itr].Length - 1) strRet += ", ";
				}
				if (ary.Count > 1) { strRet += ")"; if (itr < ary.Count - 1) strRet += " "; };
			}
			return strRet;
		}
		public int ContentHeight { get { int intRet = 0; for (int itr = 0; itr < datagridData.Rows.Count; itr++) { intRet+=datagridData.Rows[itr].Height; }; return intRet; } }
		public override string Text
		{
			get => $"<{typeUniformData}> " + ArrayToString(DataObject);
			set 
			{
				this.DataObject = StringToArray(value, out int intNewCompLen, out int intNewCompType);
				if (intNewCompType == -1) { DataComponentCount = intNewCompLen; } else { DataUniformType = (UniformType)intNewCompType; }
			}
		}
		public void UpdateAutoHeight()
		{
			Height = ContentHeight + 8;
		}
		private void UpdateRowHeaders()
		{
			for (int itr = 0; itr < datagridData.Rows.Count; itr++)
			{
				datagridData.Rows[itr].HeaderCell.Value = (!datagridData.Rows[itr].IsNewRow) ? itr.ToString() : "+";
			}
		}
	}
}
