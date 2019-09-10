using modProject;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.RegularExpressions;
using WeifenLuo.WinFormsUI.Docking;
using static modProject.clsProjectObject;
using static generalUtils;
using static OpenTK.Platform.Utilities;
using System.ComponentModel;
using System.Reflection;

namespace WinOpenGL_ShaderToy
{
	public static class ProjectDef
	{
		public static DockContent NewFormFromObject(clsProjectObject obj)
		{
			switch(obj.ProjectObjType)
			{
				case ProjectObjectTypes.Geometry:
					return new frmGeometry((clsGeometry)obj);
				case ProjectObjectTypes.Shader:
					return new frmShader((clsShader)obj);
				case ProjectObjectTypes.Render:
					return new frmRender((clsRender)obj);
				case ProjectObjectTypes.Program:
					return new frmProgram((clsProgram)obj);
				default:
					return null;
			}
		}
		public static frmMain formMain;
		public static clsProject projectMain;
		public static DockPanel dockMainPanel;
		public static ContextHandle glHandle_Main;
		public static GraphicsMode glMode_Main;
		public static GraphicsContext glContext_Main;
		public static IWindowInfo infoWindow;
		public static void glInit(IntPtr handle)
		{
			glHandle_Main = new ContextHandle(handle);
			infoWindow = CreateWindowsWindowInfo(handle);
			glMode_Main = new GraphicsMode();
			glContext_Main = new GraphicsContext(glMode_Main, infoWindow, 4, 0, GraphicsContextFlags.ForwardCompatible);
			glContext_Main.LoadAll();
		}
	}
}
namespace modProject
{
	public class clsProject
	{
		public string Name { set; get; }
		public string Path { set; get; }
		public List<clsProjectObject> ProjectObjects { set; get; } = new List<clsProjectObject>() { };
	}
	public class clsShader : clsProjectObject
	{
		public ShaderType Type { set; get; } = ShaderType.VertexShader;
		public string Path { set; get; }
		public string Source { set; get; }
		public string[] Lines { get { return (Source+"\n").Split(); } }
		public int glID { private set; get; } = -1;
		public clsInfoString CompileInfo { private set; get; } = new clsInfoString();
		public clsShader(ShaderType typ) : base(ProjectObjectTypes.Shader)
		{
			Type = typ;
			glID = GL.CreateShader(Type);
			AddToCollection();
		}
		public void LoadSourceFromFile(string pathFile)
		{
			Source = System.IO.File.ReadAllText(pathFile);
		}
		public void SaveSourceToFile(string pathFile)
		{
			System.IO.File.WriteAllText(pathFile, Source);
		}
		public void Compile()
		{
			GL.ShaderSource(glID, Source+"\n");
			GL.CompileShader(glID);
			CompileInfo = new clsInfoString(GL.GetShaderInfoLog(glID));
		}
		public void Delete()
		{
			GL.DeleteShader(glID);
			glID = -1;
		}
		public override void Dispose()
		{
			Delete();
			base.Dispose();
		}
		public override string ToFullString(string name)
		{
			return Regex.Replace(Type.ToString(), @"Arb\z", "") + "_" + name;
		}
	}
	public class clsProgram : clsProjectObject
	{
		public List<clsShader> Shaders { set; get; } = new List<clsShader>() { };
		public int glID { private set; get; } = -1;
		public clsInfoString LinkInfo { private set; get; }
		public clsProgram() : base(ProjectObjectTypes.Program)
		{
			glID = GL.CreateProgram();
			AddToCollection();
		}
		public void Link()
		{
			foreach (clsShader objShader in Shaders)
			{
				GL.AttachShader(glID, objShader.glID);
			}
			GL.LinkProgram(glID);
			LinkInfo = new clsInfoString(GL.GetProgramInfoLog(glID));
			foreach (clsShader objShader in Shaders)
			{
				GL.DetachShader(glID, objShader.glID);
			}
		}
		public void Delete()
		{
			GL.DeleteProgram(glID);
			glID = -1;
		}
		public override void Dispose()
		{
			Delete();
			base.Dispose();
		}
	}
	public class clsGeometry : clsProjectObject
	{
		public class VertexCollectionConverter : ArrayConverter
		{
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection props = new PropertyDescriptorCollection(new PropertyDescriptor[] { });
				clsVertexCollection aryVrt = value as clsVertexCollection;
				if(aryVrt != null)
				{
					props.Add(new MemberPropertyDescriptor(typeof(int), typeof(int), "Count"));
					for(int vrtItr = 0; vrtItr < aryVrt.Count; vrtItr++)
					{
						props.Add(new ArrayPropertyDescriptor(typeof(clsVertex), typeof(clsVertex), vrtItr));
					}
				}
				return props;
			}
			private class MemberPropertyDescriptor : SimplePropertyDescriptor
			{
				private string strName = ""; 
				public MemberPropertyDescriptor(Type componentType, Type elementType, string Name) : base(componentType, Name, elementType, null)
				{
					strName = Name;
				}
				public override object GetValue(object instance)
				{
					object objRet = null;
					Type propTyp = instance.GetType().GetProperty(strName).PropertyType;
					PropertyInfo prop = instance.GetType().GetProperty(strName, propTyp);
					objRet = Convert.ChangeType(prop.GetValue(instance, null), propTyp);
					return objRet;
				}
				public override void SetValue(object instance, object value)
				{
					Type propTyp = instance.GetType().GetProperty(strName).PropertyType;
					PropertyInfo prop = instance.GetType().GetProperty(strName, propTyp);
					prop.SetValue(instance, Convert.ChangeType(value,propTyp), null);
					OnValueChanged(instance, EventArgs.Empty);
				}
			}
			private class ArrayPropertyDescriptor : SimplePropertyDescriptor
			{
				private int index;
				public ArrayPropertyDescriptor(Type arrayType, Type elementType, int index) : base(arrayType, "[" + index + "]", elementType, null)
				{
					this.index = index;
				}
				public override object GetValue(object instance)
				{
					clsVertexCollection ary = instance as clsVertexCollection;
					if (ary != null)
					{
						if (ary.Count > index)
						{
							return ary[index];
						}
					}
					return null;
				}
				public override void SetValue(object instance, object value)
				{
					clsVertexCollection ary = instance as clsVertexCollection;
					if (ary != null)
					{
						if (ary.Count > index)
						{
							ary[index] = (clsVertex)value;
							OnValueChanged(instance, EventArgs.Empty);
						}
					}
				}
			}
		}
		public class VertexConverter : ArrayConverter
		{
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection props = new PropertyDescriptorCollection(new PropertyDescriptor[] { });
				clsVertex aryVrt = value as clsVertex;
				if (aryVrt != null)
				{
					clsVertexProperty[] propVrt = aryVrt.Components;
					if(propVrt != null)
					{
						for (int vrtItr = 0; vrtItr < propVrt.Length; vrtItr++)
						{
							props.Add(new ArrayPropertyDescriptor(typeof(clsVertexProperty), typeof(clsVertexProperty), propVrt[vrtItr].ComponentName, vrtItr));
						}
					}
				}
				return props;
			}
			private class ArrayPropertyDescriptor : SimplePropertyDescriptor
			{
				private int index;
				public ArrayPropertyDescriptor(Type arrayType, Type elementType, string strName, int index) : base(arrayType, strName, elementType, null)
				{
					this.index = index;
				}
				public override object GetValue(object instance)
				{
					clsVertex ary = instance as clsVertex;
					if (ary != null)
					{
						if (ary.Components.Length > index)
						{
							return ary.Components[index];
						}
					}
					return null;
				}
				public override void SetValue(object instance, object value)
				{
					clsVertex ary = instance as clsVertex;
					if (ary != null)
					{
						if (ary.Components.Length > index)
						{
							ary.Components[index] = (clsVertexProperty)value;
							OnValueChanged(instance, EventArgs.Empty);
						}
					}
				}
			}
		}
		public class VertexPropertyConverter : ArrayConverter
		{
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptor[] props = null;
				clsVertexProperty vrt = value as clsVertexProperty;
				if (vrt != null)
				{
					Array valueArray = (Array)vrt.Elements;
					int length = valueArray.GetLength(0);
					props = new PropertyDescriptor[length];
					Type arrayType = vrt.ElementType;
					Type elementType = vrt.ElementType;
					for (int i = 0; i < length; i++)
					{
						props[i] = new ArrayPropertyDescriptor(arrayType, elementType, i, attributes);
					}
				}
				return new PropertyDescriptorCollection(props);
			}
			private class ArrayPropertyDescriptor : SimplePropertyDescriptor
			{
				private int index;
				public ArrayPropertyDescriptor(Type arrayType, Type elementType, int index, Attribute[] attr) : base(arrayType, "[" + index + "]", elementType, attr)
				{
					this.index = index;
				}
				public override object GetValue(object instance)
				{
					clsVertexProperty vrt = instance as clsVertexProperty;
					if (vrt != null)
					{
						return vrt[index];
					}
					return null;
				}
				public override void SetValue(object instance, object value)
				{
					clsVertexProperty vrt = instance as clsVertexProperty;
					if (vrt != null)
					{
						vrt[index] = value;
						OnValueChanged(instance, EventArgs.Empty);
					}
				}
			}
			public override bool CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType)
			{
				if (sourceType == typeof(string))
					return true;
				else
					return base.CanConvertFrom(context, sourceType);
			}
			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				if (destinationType == typeof(ICollection<object>))
					return true;
				else
					return base.CanConvertTo(context, destinationType);
			}
			public override object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
			{
				if (value.GetType() == typeof(string))
				{
					string strName = context.PropertyDescriptor.DisplayName;
					clsVertex vrt = context.Instance as clsVertex;
					if (vrt != null)
					{
						clsVertexDescriptionComponent comp = vrt.Desc.First(itm => itm.Name == strName);
						if(comp != null)
						{
							string[] vals = (value as string).Split(',');
							//return GetList(vals, comp.ElementType);
						}
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
			private object GetList(string[] vals, Type Typ)
			{
				List<object> tVals = new List<object>();
				foreach (string val in vals)
				{
					try
					{
						object tVal = Convert.ChangeType(val, Typ);
						tVals.Add(tVal);
					}
					catch
					{
						tVals.Add(Typ);
					}
				}
				return tVals.ToArray();
			}
			public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
			{
				if (destinationType == typeof(string))
				{
					clsVertexProperty vrt = value as clsVertexProperty;
					Type Typ = vrt.ElementType;
					if(vrt != null)
					{
						ICollection collection = vrt.Elements as ICollection;
						if (collection != null)
						{
							List<string> vals = new List<string>();
							foreach (object val in collection)
							{
								object objVal = Convert.ChangeType(val, Typ);
								vals.Add(objVal.ToString());
							}
							return string.Join(", ", vals.ToArray());
						}
					}
				}
				return base.ConvertTo(context, culture, value, destinationType);
			}
		}
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class clsVertexDescriptionComponent : IDisposable
		{
			public static Dictionary<VertexAttribPointerType, Type> VertexTypes = new Dictionary<VertexAttribPointerType, Type>()
			{
				{VertexAttribPointerType.Byte, typeof(sbyte)},
				{VertexAttribPointerType.UnsignedByte, typeof(byte)},
				{VertexAttribPointerType.Short, typeof(short)},
				{VertexAttribPointerType.UnsignedShort, typeof(ushort)},
				{VertexAttribPointerType.Int, typeof(int)},
				{VertexAttribPointerType.UnsignedInt, typeof(uint)},
				{VertexAttribPointerType.HalfFloat, typeof(Half)},
				{VertexAttribPointerType.Fixed, typeof(Single)},
				{VertexAttribPointerType.Float, typeof(float)},
				{VertexAttribPointerType.Double, typeof(double)}
			};
			public clsVertexDescriptionComponent(clsVertexDescription refDesc, string strName, int intElements, object InitialValue)
			{
				Desc = refDesc;
				Name = strName;
				ElementGLType = VertexAttribPointerType.Byte;
				ElementCount = intElements;
				InitialElementValue = Convert.ChangeType(InitialValue, ElementType);
			}
			public clsVertexDescriptionComponent(clsVertexDescription refDesc, VertexAttribPointerType glType, string strName, int intElements, object InitialValue) 
			{
				Desc = refDesc;
				ElementGLType = glType;
				Name = strName;
				ElementCount = intElements;
				InitialElementValue = Convert.ChangeType(InitialValue, ElementType);
			}
			[Browsable(false)]
			public clsVertexDescription Desc { private set; get; }
			public string Name { set; get; }
			public object InitialElementValue { set; get; }
			private VertexAttribPointerType glElementType;
			public VertexAttribPointerType ElementGLType { get { return glElementType; } set { glElementType = value; Desc.RaiseUpdated(); } }
			public Type ElementType { get { return VertexTypes[ElementGLType]; } }
			public int ElementSize { get { return Marshal.SizeOf(VertexTypes[ElementGLType]); } }
			private int intElementCount;
			public int ElementCount { set { intElementCount = value; Desc.RaiseUpdated(); } get { return intElementCount; } }
			public int ComponentSize { get { return ElementSize * ElementCount; } }
			public int Index
			{
				set
				{
					if (Desc == null) return;
					if (Index == value) return;
					Desc.SetComponentIndex(this, value);
					Desc.RaiseUpdated();
				}
				get { return Desc.IndexOf(this); }
			}
			public void Dispose()
			{
				Desc = null;
			}
			public override string ToString()
			{
				return $"{Name} {{<{ElementGLType}> Type: {ElementType}; Elements: {ElementCount} x {ElementSize}B; Size: {ComponentSize}B; }}";
			}
		}
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class clsVertexDescription : IDisposable, IList<clsVertexDescriptionComponent>
		{
			internal List<clsVertexDescriptionComponent> aryComponents = new List<clsVertexDescriptionComponent>();
			public delegate void delegateUpdated();
			public event delegateUpdated Updated;
			internal void RaiseUpdated()
			{
				delegateUpdated evnt = new delegateUpdated(Updated);
				evnt?.Invoke();
			}
			public clsVertexDescriptionComponent this[int index]
			{
				get
				{
					return aryComponents[index];
				}
				set
				{
					aryComponents[index] = value;
					value.Index = index;
					RaiseUpdated();
				}
			}
			public clsVertexDescriptionComponent Add(VertexAttribPointerType glType, string Name, int Components, object InitialValue)
			{
				clsVertexDescriptionComponent componentNew = new clsVertexDescriptionComponent(this, glType, Name, Components, InitialValue);
				aryComponents.Add(componentNew);
				RaiseUpdated();
				return componentNew;
			}
			public void Add(clsVertexDescriptionComponent item)
			{
				aryComponents.Add(item);
				RaiseUpdated();
			}
			public void Insert(int index, clsVertexDescriptionComponent item)
			{
				aryComponents.Insert(index, item);
				item.Index = index;
				RaiseUpdated();
			}
			public void RemoveAt(int index)
			{
				aryComponents.RemoveAt(index);
				RaiseUpdated();
			}
			public void Remove(string name)
			{
				aryComponents.RemoveAll(itm => itm.Name == name);
				RaiseUpdated();
			}
			public void Remove(clsVertexDescriptionComponent itm)
			{
				if (!aryComponents.Contains(itm)) return;
				itm.Dispose();
				aryComponents.Remove(itm);
				RaiseUpdated();
			}
			public void Clear()
			{
				aryComponents.Clear();
				RaiseUpdated();
			}
			public int Count { get { return aryComponents.Count; } }
			public int TotalVertexSize
			{
				get
				{
					int intRet = 0;
					for(int itr = 0; itr < aryComponents.Count; itr++)
					{
						intRet += aryComponents[itr].ComponentSize;
					}
					return intRet;
				}
			}

			[Browsable(false)]
			public bool IsReadOnly => false;

			protected internal int GetComponentOffset(clsVertexDescriptionComponent itm)
			{
				int intRet = 0;
				for(int itr = 0; itr < aryComponents.Count; itr++)
				{
					if(aryComponents[itr] == itm)
					{
						return intRet;
					}
					intRet += aryComponents[itr].ComponentSize;
				}
				return 0;
			}
			protected internal void SetComponentIndex(clsVertexDescriptionComponent itm, int index)
			{
				if(aryComponents.Contains(itm)) aryComponents.Remove(itm);
				aryComponents.Insert(Math.Min(index, aryComponents.Count), itm);
				RaiseUpdated();
			}
			public int IndexOf(clsVertexDescriptionComponent item)
			{
				return aryComponents.IndexOf(item);
			}
			public bool Contains(clsVertexDescriptionComponent item)
			{
				return aryComponents.Contains(item);
			}
			public void CopyTo(clsVertexDescriptionComponent[] array, int arrayIndex)
			{
				aryComponents.CopyTo(array, arrayIndex);
			}
			bool ICollection<clsVertexDescriptionComponent>.Remove(clsVertexDescriptionComponent item)
			{
				return aryComponents.Remove(item);
			}
			public IEnumerator<clsVertexDescriptionComponent> GetEnumerator()
			{
				return aryComponents.GetEnumerator();
			}
			IEnumerator IEnumerable.GetEnumerator()
			{
				return aryComponents.GetEnumerator();
			}
			public void Dispose()
			{
				foreach (clsVertexDescriptionComponent itm in aryComponents) itm.Dispose();
				aryComponents.Clear();
			}
			public override string ToString()
			{
				string strRet = "";
				foreach (clsVertexDescriptionComponent itm in aryComponents) strRet += itm.Name + "; ";
				return strRet;
			}
		}
		[TypeConverter(typeof(VertexPropertyConverter))]
		public class clsVertexProperty
		{
			[Browsable(false)]
			public clsVertex Vertex { private set; get; }
			[Browsable(false)]
			public int VertexDataIndex { get => ((Vertex != null && intComponentIndex >= 0) ? (Vertex.Index) : (-1)); }
			[Browsable(false)]
			public int VertexDataSize { get => ((Vertex != null && intComponentIndex >= 0) ? (Vertex.TotalSize) : (-1)); }
			[Browsable(false)]
			public int VertexDataOffset { get => ((Vertex != null && intComponentIndex >= 0) ? (Vertex.Index*Vertex.TotalSize) : (-1)); }
			[Browsable(false)]
			public string ComponentName { get => ((Vertex != null && intComponentIndex >= 0) ? (Vertex.Desc[intComponentIndex].Name) : ("")); }
			[Browsable(false)]
			public int ElementSize { get => ((Vertex != null && intComponentIndex >= 0) ? (Vertex.Desc[intComponentIndex].ElementSize) : (0)); }
			[Browsable(false)]
			public int ElementCount { get => ((Vertex != null && intComponentIndex >= 0) ? (Vertex.Desc[intComponentIndex].ElementCount) : (0)); }
			[Browsable(false)]
			public int ElementOffset { get => ((Vertex != null && intComponentIndex >= 0) ? (Vertex.Index*ElementCount) : (0)); }
			[Browsable(false)]
			public VertexAttribPointerType ElementGLType { get => ((Vertex != null && intComponentIndex >= 0) ? (Vertex.Desc[intComponentIndex].ElementGLType) : ((VertexAttribPointerType)0)); }
			[Browsable(false)]
			public Type ElementType { get => ((Vertex != null && intComponentIndex >= 0) ? (Vertex.Desc[intComponentIndex].ElementType) : (null)); }
			[Browsable(false)]
			public List<object> ComponentData { get => ((Vertex != null) ? (Vertex.Data[Vertex.Desc[intComponentIndex]]) : (null)); }
			private int intComponentIndex;
			public clsVertexProperty(clsVertex refVertex, int intComponent)
			{
				Vertex = refVertex;
				intComponentIndex = intComponent;
			}
			public object this[int index]
			{
				get
				{
					return Convert.ChangeType(ComponentData[Vertex.Index*ElementCount + index], ElementType);
				}
				set
				{
					ComponentData[Vertex.Index * ElementCount + index] = Convert.ChangeType(value, ElementType);
				}
			}
			public object[] Elements
			{
				get
				{
					object[] ret = new object[ElementCount];
					for (int itr = 0; itr < ElementCount; itr++)
					{
						ret[itr] = Convert.ChangeType(ComponentData[Vertex.Index*ElementCount + itr], ElementType);
					}
					return ret;
				}
				set
				{
					for (int itr = 0; itr < ElementCount; itr++)
					{
						if (itr < value.Length) ComponentData[Vertex.Index*ElementCount + itr] = Convert.ChangeType(value[itr], ElementType);
					}
				}
			}
			public override string ToString()
			{
				string strComponents = "";
				int dSt = ElementOffset; int dLen = ElementCount;
				for(int dItr = 0; dItr < dLen; dItr++)
				{
					strComponents += $"{ComponentData[dSt + dItr]}{((dItr < dLen-1)?(", "):(""))}";
				}
				return $"{ComponentName}: ({strComponents})";
			}
		}
		[TypeConverter(typeof(VertexConverter))]
		public class clsVertex
		{
			[Browsable(false)]
			public clsVertexCollection VertexCollection { private set; get; }
			[Browsable(false)]
			public int Index { get { return ((VertexCollection != null) ? (VertexCollection.aryVertices.IndexOf(this)) : (-1)); } }
			[Browsable(false)]
			public Dictionary<clsVertexDescriptionComponent, List<object>> Data { get { return ((VertexCollection != null) ? (VertexCollection.Data) : (null)); } }
			[Browsable(false)]
			public clsVertexDescription Desc { get { return ((VertexCollection != null) ? (VertexCollection.Desc) : (null)); } }
			[Browsable(false)]
			public int TotalSize { get { return ((Desc != null) ? (Desc.TotalVertexSize) : (0)); } }
			public clsVertex(clsVertexCollection refCollection)
			{
				VertexCollection = refCollection;
			}
			public clsVertexProperty[] Components
			{
				get
				{
					clsVertexProperty[] ret = new clsVertexProperty[Desc.Count];
					for (int itr = 0; itr < Desc.Count; itr++) ret[itr] = new clsVertexProperty(this, itr);
					return ret;
				}
			}
			public object[] this[int index]
			{
				get
				{
					clsVertexDescriptionComponent comp = Desc[index];
					int elemLen = comp.ElementCount;
					object[] ret = new Object[elemLen];
					Data[comp].CopyTo(Index * elemLen, ret, 0, elemLen);
					return ret;
				}
				set
				{
					clsVertexDescriptionComponent comp = Desc[index];
					int elemLen = comp.ElementCount;
					int elemSt = Index * elemLen;
					for(int elemItr = 0; elemItr < elemLen; elemItr++)
					{
						Data[comp][elemSt + elemItr] = value.GetValue(elemItr);
					}
				}
			}
			public object[] this[string ComponentName]
			{
				get
				{
					int intRet = Components.ToList().FindIndex(itm => itm.ComponentName == ComponentName);
					return (intRet >= 0) ? (this[intRet]) : (null);
				}
				set
				{
					int intComp = Components.ToList().FindIndex(itm => itm.ComponentName == ComponentName);
					if (intComp < 0) return;
					this[intComp] = value;
				}
			}
			public override string ToString()
			{
				string strRet = "";
				for(int itr = 0; itr < Components.Length; itr++)
				{
					strRet += Components[itr].ToString() + "; ";
				}
				return strRet;
			}
		}
		[TypeConverter(typeof(VertexCollectionConverter))]
		public class clsVertexCollection : IDisposable, IList<clsVertex>
		{
			[Browsable(false)]
			public Dictionary<clsVertexDescriptionComponent, List<object>> Data { private set; get; } = new Dictionary<clsVertexDescriptionComponent, List<object>>();
			internal List<clsVertex> aryVertices = new List<clsVertex>();
			public clsVertexCollection(clsVertexDescription refDesc)
			{	
				Desc = refDesc;
				Desc.Updated += desc_Updated;
			}
			private clsVertexDescription refDesc;
			[Browsable(false)]
			[NotifyParentProperty(true)]
			public clsVertexDescription Desc
			{
				get
				{
					return refDesc;
				}
				set
				{
					if (refDesc != null)
					{
						refDesc.Updated -= desc_Updated;
					}
					value.Updated += desc_Updated;
					refDesc = value;
					desc_Updated();
				}
			}
			private int intCount = 0;
			[NotifyParentProperty(true)]
			public int Count
			{
				get { return intCount; }
				set
				{
					foreach(KeyValuePair<clsVertexDescriptionComponent, List<object>> comp in Data)
					{
						List<object> itm = comp.Value;
						ResizeList<object>(ref itm, value*comp.Key.ElementCount, idx => comp.Key.InitialElementValue);
					}
					ResizeList(ref aryVertices, value, idx => new clsVertex(this));
					intCount = value;
				}
			}
			[Browsable(false)]
			public int TotalSize { get { return intCount * refDesc.TotalVertexSize; } }

			[Browsable(false)]
			public bool IsReadOnly => false;

			public clsVertex this[int index]
			{
				get
				{
					return aryVertices[index];
				}
				set
				{
					foreach (clsVertexDescriptionComponent comp in refDesc)
					{
						Array srcData = null;
						srcData = value[comp.Name];
						if (srcData != null)
						{
							WriteValue(index, comp, value[comp.Name]);
						}
					}
				}
			}
			public clsVertex[] Items
			{
				get { return aryVertices.ToArray(); }
			}
			public void Add(KeyValuePair<string, Array>[] value)
			{
				Count++;
				SetKeyValuePairs(intCount - 1, value);
			}
			public void Add(KeyValuePair<string, Array>[][] values)
			{
				int intCountPrev = intCount;
				Count += values.Length;
				for(int itr = 0; itr < intCount-intCountPrev; itr++)
				{
					SetKeyValuePairs(intCount + itr, values[itr]);
				}
			}
			private void SetKeyValuePairs(int index, KeyValuePair<string, Array>[] values)
			{
				for (int itrComp = 0; itrComp < values.Length; itrComp++)
				{
					SetKeyValuePair(index, values[itrComp]);
				}
			}
			private void SetKeyValuePair(int index, KeyValuePair<string, Array> value)
			{
				clsVertexDescriptionComponent comp = null;
				if(refDesc.Count > 0) comp = refDesc.First(itm => itm.Name == value.Key);
				if (comp == null)
				{
					VertexAttribPointerType glType = VertexAttribPointerType.Float;
					if (clsVertexDescriptionComponent.VertexTypes.ContainsValue(value.Value.GetValue(0).GetType()))
					{
						glType = clsVertexDescriptionComponent.VertexTypes.First(itm => itm.Value == value.Value.GetValue(0).GetType()).Key;
					}
					comp = refDesc.Add(glType, value.Key, value.Value.Length, (object)0);
				}
				WriteValue(index, comp, value.Value);
			}
			private void WriteValue(int index, clsVertexDescriptionComponent comp, Array value)
			{
				for (int intElem = 0; intElem < comp.ElementCount; intElem++)
				{
					Data[comp][index * comp.ElementCount + intElem] = value.GetValue(intElem);
				}
			}
			public void RemoveAt(int index)
			{
				aryVertices.RemoveAt(index);
				Count = aryVertices.Count;
			}
			public void Clear()
			{
				aryVertices.Clear();
				Count = 0;
			}
			private void desc_Updated()
			{
				KeyValuePair<clsVertexDescriptionComponent, List<object>>[] aryComp = Data.ToArray();
				for(int itr = 0; itr < aryComp.Length; itr++)
				{
					if(!refDesc.Contains(aryComp[itr].Key))
					{
						Data.Remove(aryComp[itr].Key);
					}
				}
				for(int itr = 0; itr < refDesc.Count; itr++)
				{
					if (!Data.ContainsKey(refDesc[itr]))
					{
						object[] aryElements = ArrayList.Repeat(refDesc[itr].InitialElementValue, refDesc[itr].ElementCount*intCount).ToArray();
						Data.Add(refDesc[itr], new List<object>((IEnumerable<object>)aryElements));
					} else
					{
						List<object> ary = Data[refDesc[itr]];
						ResizeList(ref ary, refDesc[itr].ElementCount * intCount, idx => refDesc[itr].InitialElementValue);
					}
				}
			}
			public void Dispose()
			{
				refDesc.Updated -= desc_Updated;
				Count = 0;
				Data.Clear();
				Data = null;
				refDesc = null;
			}
			public override string ToString()
			{
				return $"Count={intCount}";
			}

			public int IndexOf(clsVertex item)
			{
				return ((IList<clsVertex>)aryVertices).IndexOf(item);
			}

			public void Insert(int index, clsVertex item)
			{
				((IList<clsVertex>)aryVertices).Insert(index, item);
			}

			public void Add(clsVertex item)
			{
				((IList<clsVertex>)aryVertices).Add(item);
			}

			public bool Contains(clsVertex item)
			{
				return ((IList<clsVertex>)aryVertices).Contains(item);
			}

			public void CopyTo(clsVertex[] array, int arrayIndex)
			{
				((IList<clsVertex>)aryVertices).CopyTo(array, arrayIndex);
			}

			public bool Remove(clsVertex item)
			{
				return ((IList<clsVertex>)aryVertices).Remove(item);
			}

			public IEnumerator<clsVertex> GetEnumerator()
			{
				return ((IList<clsVertex>)aryVertices).GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IList<clsVertex>)aryVertices).GetEnumerator();
			}
		}
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class clsTriangleCollection : IDisposable, IList<uint[]>
		{
			private List<uint> aryIndices;
			public uint[] Indices { get => aryIndices.ToArray(); }
			public clsTriangleCollection(List<uint> refaryIndices)
			{
				aryIndices = refaryIndices;
			}
			public void Dispose()
			{
				aryIndices = null;
			}
			public int Count
			{
				set
				{ 
					ResizeList<uint>(ref aryIndices, value * 3, idx => 0);
				}
				get => (int)(aryIndices.Count/3);
			}

			[Browsable(false)]
			public bool IsReadOnly => false;

			public uint[] this[int index]
			{
				get
				{
					uint[] ret = new uint[3];
					aryIndices.CopyTo(index*3, ret, 0, 3);
					return ret;
				}
				set
				{
					for(int itr = 0; itr < 3; itr++)
					{
						aryIndices[index * 3 + itr] = value[itr]; 
					}
				}
			}
			public uint[][] Items
			{
				get
				{
					List<uint[]> ret = new List<uint[]>();
					for(int itr = 0; itr < Count; itr++)
					{
						ret.Add(this[itr]);
					}
					return ret.ToArray();
				}
			}

			public int IndexOf(uint[] item)
			{
				return Items.ToList().FindIndex(itm => (
					itm[0] == item[0] &&
					itm[1] == item[1] &&
					itm[2] == item[2]
				));
			}

			public void Insert(int index, uint[] item)
			{
				for(int itr = item.Length-1; itr >= 0; itr--)
				{
					aryIndices.Insert(index * 3, item[itr]);
				}
			}

			public void RemoveAt(int index)
			{
				for (int itr = 0; itr < 3; itr++) aryIndices.RemoveAt(index * 3);
			}

			public void Add(uint[] item)
			{
				aryIndices.AddRange(item);
			}

			public void Clear()
			{
				aryIndices.Clear();
			}

			public bool Contains(uint[] item)
			{
				return IndexOf(item) >= 0;
			}

			public void CopyTo(uint[][] array, int arrayIndex)
			{
				for(int itr = 0; itr < array.Length; itr++)
				{
					for (int itrN = 0; itrN < 3; itrN++)  aryIndices[arrayIndex * 3 + itr] = array[itr][itrN];
				}
			}

			public bool Remove(uint[] item)
			{
				int idx = IndexOf(item);
				if (idx < 0) return false;
				RemoveAt(idx);
				return true;
			}

			public IEnumerator<uint[]> GetEnumerator()
			{
				return (IEnumerator<uint[]>)Items.GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return Items.GetEnumerator();
			}

			public override string ToString()
			{
				return $"Count={Count}";
			}
		} 
		public clsVertexDescription VertexDescription { set; get; }
		public clsVertexCollection Vertices { set; get; }
		public clsTriangleCollection Triangles { set; get; }
		private List<uint> aryIndices = new List<uint>();
		public clsGeometry() : base(ProjectObjectTypes.Geometry)
		{
			VertexDescription = new clsVertexDescription();
			Vertices = new clsVertexCollection(VertexDescription);
			Triangles = new clsTriangleCollection(aryIndices);
			AddToCollection();
		}
		public override void Dispose()
		{
			VertexDescription.Dispose();
			VertexDescription = null;
			base.Dispose();
		}
	}
	public class clsInfoString
	{
		public class InfoLocation
		{
			public string FullString;
			public int Line { get { return MatchParse().Y; } }
			public int Column { get { return MatchParse().X; } }
			private Point MatchParse()
			{
				int retLine = -1;
				int retColumn = -1;
				MatchCollection matches = Regex.Matches(FullString, @"\b(?<Line>\d{1,})\:(?<Column>\d{1,})\b");
				if(matches.Count > 0)
				{
					string strLine = matches[0].Groups["Line"].Value;
					string strColumn = matches[0].Groups["Column"].Value;
					if(strLine!="") retLine = int.Parse(strLine);
					if(strColumn!="") retColumn = int.Parse(strColumn);
				}
				return new Point(retColumn, retLine);
			}
			public InfoLocation()
			{
				FullString = "";
			}
			public InfoLocation(string str)
			{
				FullString = str;
			}
			public override string ToString()
			{
				return $"Line: {Line}, Column: {Column}";
			}
		}
		public class InfoMessage
		{
			public string FullString;
			public static implicit operator string(InfoMessage itm) => itm.ToString();
			public static implicit operator InfoMessage(string str) => new InfoMessage(str);
			public static bool operator ==(string str, InfoMessage itm) => itm.ToString() == str;
			public static bool operator !=(string str, InfoMessage itm) => itm.ToString() != str;
			public InfoMessage()
			{
				FullString = "\n";
			}
			public InfoMessage(string str)
			{
				FullString = str;
			}
			public string Level
			{
				get
				{
					MatchCollection matches = Regex.Matches(FullString, @"\A[A-Z]+\b");
					return (matches.Count > 0) ? (matches[0].Value) : ("");
				}
			}
			public InfoLocation Location
			{
				get
				{
					MatchCollection matches = Regex.Matches(FullString, @"\b\d{1,}\:\d{1,}\b");
					return (matches.Count > 0) ? (new InfoLocation(matches[0].Value)) : (new InfoLocation());
				}
			}
			public string Message { get { return FullString.Replace(Level+":", "").Replace(Location.FullString+":", "").Trim(); } }
			public override bool Equals(object obj)
			{
				return base.Equals(obj);
			}
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}
			public override string ToString()
			{
				return FullString;
			}
		}
		public string Info { set; get; }
		public InfoMessage[] AllMessages
		{
			get
			{
				return Array.ConvertAll(Array.FindAll(Info.Split('\n'),str => str != ""), str => new InfoMessage(str));
			}
		}
		public InfoMessage[] WarningMessages
		{
			get
			{
				return Array.FindAll(AllMessages, str => str.FullString.StartsWith("WARNING"));
			}
		}
		public InfoMessage[] ErrorMessages
		{
			get
			{
				return Array.FindAll(AllMessages, str => str.FullString.StartsWith("ERROR"));
			}
		}
		public clsInfoString()
		{
			Info = "";
		}
		public clsInfoString(string strInfo)
		{
			Info = strInfo;
		}
		public override string ToString()
		{
			return Info;
		}
	}
	public class clsRender : clsProjectObject
	{
		public clsRender() : base(ProjectObjectTypes.Render)
		{
			AddToCollection();
		}
	}
}