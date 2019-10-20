using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace WinOpenGL_ShaderToy
{
	public partial class controlUniformData : UserControl
	{
		public class clsComponentDelegateSet
		{
			public delegate void ComponentSet(object Obj, object value);
			public delegate object ComponentGet(object Obj);
			public ComponentGet ComponentGetter { get; set; }
			public ComponentSet ComponentSetter { get; set; }
			public clsComponentDelegateSet(string name, ComponentGet delegateGet, ComponentSet delegateSet)
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
	}
}
