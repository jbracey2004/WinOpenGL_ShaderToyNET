using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static modProject.clsUniformSet;
using static WinOpenGL_ShaderToy.controlUniformData.clsComponentDelegateSet;
using OpenTK;
using System.Reflection;

namespace WinOpenGL_ShaderToy
{
	public partial class controlUniformData : UserControl
	{
		public static Dictionary<UniformType, clsComponentSet> UniformDelegateSet = new Dictionary<UniformType, clsComponentSet>()
		{
			{UniformType.Int, new clsComponentSet(null, new string[]{"int"},new ComponentGet[]{itm=>(int)itm}, new ComponentSet[]{(itm,val)=> { itm = (int)val; } }) },
			{UniformType.Float,  new clsComponentSet(null, new string[]{"float"},new ComponentGet[]{itm=>(float)itm}, new ComponentSet[]{(itm,val)=> { itm = (float)val; } }) },
			{UniformType.Double, new clsComponentSet(null, new string[]{"double"},new ComponentGet[]{itm=>(double)itm}, new ComponentSet[]{(itm,val)=> { itm = (double)val; } }) },
			{UniformType.Int2, new clsComponentSet(null, new string[]{"int x", "int y"}, clsComponentSet.MakeComponentDelegateArray<int>(2)) },
			{UniformType.Float2,  new clsComponentSet(null, new string[]{"float x", "float y"}, clsComponentSet.MakeComponentDelegateArray<float>(2)) },
			{UniformType.Double2, new clsComponentSet(null, new string[]{"double x", "double y"}, clsComponentSet.MakeComponentDelegateArray<double>(2)) },
			{UniformType.Int3, new clsComponentSet(null, new string[]{"int x", "int y", "int z"}, clsComponentSet.MakeComponentDelegateArray<int>(3)) },
			{UniformType.Float3, new clsComponentSet(null, new string[]{"float x", "float y", "float z"}, clsComponentSet.MakeComponentDelegateArray<float>(3)) },
			{UniformType.Double3, new clsComponentSet(null, new string[]{"double x", "double y", "double z"}, clsComponentSet.MakeComponentDelegateArray<double>(3)) },
			{UniformType.Int4, new clsComponentSet(null, new string[]{"int x", "int y", "int z", "int w"}, clsComponentSet.MakeComponentDelegateArray<int>(4)) },
			{UniformType.Float4,  new clsComponentSet(null, new string[]{"float x", "float y", "float z", "float w"}, clsComponentSet.MakeComponentDelegateArray<float>(4)) },
			{UniformType.Double4, new clsComponentSet(null, new string[]{"double x", "double y", "double z", "double w"}, clsComponentSet.MakeComponentDelegateArray<double>(4)) },
			{UniformType.Matrix2, new clsComponentSet(null, "Matrix", "OpenTK", 2, 2) },
			{UniformType.Matrix2x3, new clsComponentSet(null, "Matrix", "OpenTK", 2, 3)},
			{UniformType.Matrix2x4, new clsComponentSet(null, "Matrix", "OpenTK", 2, 4)},
			{UniformType.Matrix3, new clsComponentSet(null, "Matrix", "OpenTK", 3, 3)},
			{UniformType.Matrix3x2, new clsComponentSet(null, "Matrix", "OpenTK", 3, 2)},
			{UniformType.Matrix3x4, new clsComponentSet(null, "Matrix", "OpenTK", 3, 4)},
			{UniformType.Matrix4, new clsComponentSet(null, "Matrix", "OpenTK", 4, 4)},
			{UniformType.Matrix4x3, new clsComponentSet(null, "Matrix", "OpenTK", 4, 3)},
			{UniformType.Matrix4x2, new clsComponentSet(null, "Matrix", "OpenTK", 4, 2)}
		};
		public class clsComponentDelegateSet
		{
			public delegate void ComponentSet(object Obj, object value);
			public delegate object ComponentGet(object Obj);
			public ComponentGet ComponentGetter { get; set; }
			public ComponentSet ComponentSetter { get; set; }
			public clsComponentDelegateSet(ComponentGet delegateGet, ComponentSet delegateSet)
			{
				ComponentGetter = delegateGet;
				ComponentSetter = delegateSet;
			}
			public object GetValue(object obj) => ComponentGetter(obj);
			public void SetValue(object obj, object value) => ComponentSetter(obj, value);
		}
		public class clsComponentSet
		{
			public object Data { get; set; } = null;
			public Dictionary<string, clsComponentDelegateSet> ComponentDelegates { get; set; } = new Dictionary<string, clsComponentDelegateSet>();
			public clsComponentSet() { }
			public clsComponentSet(object value, KeyValuePair<string, clsComponentDelegateSet>[] aryComponentDelegates)
			{
				Data = value;
				foreach(KeyValuePair<string, clsComponentDelegateSet> comp in aryComponentDelegates)
					ComponentDelegates.Add(comp.Key, comp.Value);
			}
			public clsComponentSet(object value, string[] names, ComponentGet[] delegatesGet, ComponentSet[] delegatesSet)
			{
				if (names.Length != delegatesGet.Length) throw new ArgumentException("Array Lengths must be equal");
				if (names.Length != delegatesSet.Length) throw new ArgumentException("Array Lengths must be equal");
				Data = value;
				for (int itr = 0; itr < names.Length; itr++)
					ComponentDelegates.Add(names[itr], new clsComponentDelegateSet(delegatesGet[itr], delegatesSet[itr]));
			}
			public clsComponentSet(object value, string[] names, clsComponentDelegateSet[] delegatesGetSet)
			{
				if (names.Length != delegatesGetSet.Length) throw new ArgumentException("Array Lengths must be equal");
				Data = value;
				for (int itr = 0; itr < names.Length; itr++)
					ComponentDelegates.Add(names[itr], delegatesGetSet[itr]);
			}
			public clsComponentSet(object value, string strTypeName, string strAssemblyName, int elemRows, int elemColumns)
			{
				Data = value;
				string strSuffix = elemRows.ToString() + ((elemRows != elemColumns) ? ("x" + elemColumns.ToString()) : (""));
				Type Typ = Type.GetType($"{strAssemblyName}.{strTypeName}{strSuffix}, {strAssemblyName}");
				for (int itrRow = 1; itrRow <= elemRows; itrRow++) {
					for (int itrColumn = 1; itrColumn <= elemColumns; itrColumn++) {
						string strProp = $"M{itrRow}{itrColumn}";
						PropertyInfo prop = Typ.GetProperty(strProp);
						ComponentDelegates.Add($"[{itrRow},{itrColumn}]", new clsComponentDelegateSet(itm=>(float)prop.GetValue(itm, null), (itm, val) => prop.SetValue(itm, (float)Convert.ChangeType(val,typeof(float)), null) ));
					}
				}
			}
			public static clsComponentDelegateSet[] MakeComponentDelegateArray<Typ>(int elemCount)
			{
				clsComponentDelegateSet[] aryRet = new clsComponentDelegateSet[elemCount];
				for (int itr = 0; itr < elemCount; itr++)
					aryRet[itr] = new clsComponentDelegateSet((itm)=>((Typ[])itm)[itr], (itm, val)=>{ ((Typ[])itm)[0] = (Typ)val; });
				return aryRet;
			}
			public object this[string comp]
			{
				get
				{
					if (Data == null) return null;
					if (!ComponentDelegates.ContainsKey(comp)) return null;
					object ret;
					try
					{
						ret = ComponentDelegates[comp].GetValue(Data);
					}
					catch
					{
						ret = null;
					}
					return ret;
				}
				set
				{
					if (Data == null) return;
					if (!ComponentDelegates.ContainsKey(comp)) return;
					try
					{
						ComponentDelegates[comp].SetValue(Data, value);
					}
					catch
					{

					}
				}
			}
			public object this[int index]
			{
				get
				{
					if (Data == null) return null;
					if (index < 0 || index > ComponentDelegates.Count-1) return null;
					return this[ComponentDelegates.Keys.ToArray()[index]];
				}
				set
				{
					if (Data == null) return;
					if (index < 0 || index > ComponentDelegates.Count - 1) return;
					this[ComponentDelegates.Keys.ToArray()[index]] = value;
				}
			}
			public object[] ToArray()
			{
				object[] aryRet = new object[ComponentDelegates.Count];
				for (int i = 0; i < aryRet.Length; i++) aryRet[i] = this[i];
				return aryRet;
			}
		}
		public clsComponentSet DataObject { get; private set; } = new clsComponentSet();
		public controlUniformData()
		{
			InitializeComponent();
		}
		private void controlUniformData_Load(object sender, EventArgs e)
		{

		}
		public void SetComponentDescriptionFromDataType(UniformType Typ)
		{
			DataObject.ComponentDelegates = UniformDelegateSet[Typ].ComponentDelegates;
			datagridData.Columns.Clear();
			datagridData.Rows.Clear();
			for(int itr = 0; itr < DataObject.ComponentDelegates.Count; itr++)
			{
				datagridData.Columns.Add($"column{itr}", DataObject.ComponentDelegates.Keys.ToArray()[itr]);
			}
			datagridData.Rows.Add();
			for (int itr = 0; itr < DataObject.ComponentDelegates.Count; itr++)
			{
				datagridData.Rows[0].Cells[itr].Value = DataObject[itr];
			}
		}
	}
}
