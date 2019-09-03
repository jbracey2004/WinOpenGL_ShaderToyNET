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
using static modProject.modExtern;
using static OpenTK.Platform.Utilities;

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
	public static class modExtern
	{
		[DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
		public static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);
	}
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
			private int intIndex = 0;
			public int Index { set { Desc.SetComponentIndex(this, value); Desc.RaiseUpdated(); } get { return intIndex; } }
			public void Dispose()
			{
				Desc = null;
			}
			public override string ToString()
			{
				return $"{Name} {{<{ElementGLType}> Type: {ElementType}; Elements: {ElementCount} x {ElementSize}B; Size: {ComponentSize}B; }}";
			}
		}
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
					Sort();
					RaiseUpdated();
				}
			}
			public void Add(VertexAttribPointerType glType, string Name, int Components, object InitialValue)
			{
				clsVertexDescriptionComponent componentNew = new clsVertexDescriptionComponent(this, glType, Name, Components, InitialValue);
				componentNew.Index = aryComponents.Count;
				aryComponents.Add(componentNew);
				RaiseUpdated();
			}
			public void Add(clsVertexDescriptionComponent item)
			{
				aryComponents.Add(item);
				Sort();
				RaiseUpdated();
			}
			public void Insert(int index, clsVertexDescriptionComponent item)
			{
				aryComponents.Insert(index, item);
				item.Index = index;
				Sort();
				RaiseUpdated();
			}
			public void RemoveAt(int index)
			{
				aryComponents.RemoveAt(index);
				Sort();
				RaiseUpdated();
			}
			public void Remove(clsVertexDescriptionComponent itm)
			{
				if (!aryComponents.Contains(itm)) return;
				itm.Dispose();
				aryComponents.Remove(itm);
				Sort();
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

			public bool IsReadOnly => ((IList<clsVertexDescriptionComponent>)aryComponents).IsReadOnly;

			protected void Sort()
			{
				aryComponents.Sort((itmA, itmB) => (itmB.Index - itmA.Index));
			}
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
				if (!aryComponents.Contains(itm)) return;
				itm.Index = index;
				Sort();
				for (int itr = 0; itr < aryComponents.Count; itr++)
				{
					aryComponents[itr].Index = itr;
				}
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
		public class clsVertex
		{
			public clsVertexCollection VertexCollection { private set; get; }
			public int Index { get { return ((VertexCollection != null) ? (VertexCollection.aryVertices.IndexOf(this)) : (-1)); } }
			public Dictionary<clsVertexDescriptionComponent, List<object>> Data { get { return ((VertexCollection != null) ? (VertexCollection.Data) : (null)); } }
			public clsVertexDescription Desc { get { return ((VertexCollection != null) ? (VertexCollection.Desc) : (null)); } }
			public int TotalSize { get { return ((Desc != null) ? (Desc.TotalVertexSize) : (0)); } }
			public clsVertex(clsVertexCollection refCollection)
			{
				VertexCollection = refCollection;
			}
			public Dictionary<string, Array> Components
			{
				get
				{
					Dictionary<clsVertexDescriptionComponent, List<object>> data = Data;
					clsVertexDescription desc = Desc;
					int index = Index;
					if (data == null) return null;
					if (desc == null) return null;
					if (index < 0) return null;
					Dictionary<string, Array> ret = new Dictionary<string, Array>();
					for (int compItr = 0; compItr < desc.Count; compItr++)
					{
						object[] itm = new object[desc[compItr].ElementCount];
						Data[desc[compItr]].CopyTo(index * desc[compItr].ElementCount, itm, 0, desc[compItr].ElementCount);
						if (itm != null)
						{
							ret.Add(desc[compItr].Name, itm.ToArray());
						}
					}
					return ret;
				}
			}
			public Array this[int index]
			{
				get
				{
					string[] componentnames = new string[Components.Keys.Count];
					Components.Keys.CopyTo(componentnames, 0);
					return Components[componentnames[index]];
				}
				set
				{
					string[] componentnames = new string[Components.Keys.Count];
					Components.Keys.CopyTo(componentnames, 0);
					value.CopyTo(Components[componentnames[index]], 0);
					if (Data != null && Desc != null)
					{
						clsVertexDescriptionComponent comp = Desc[index];
						if (comp != null)
						{
							for (int itr = 0; itr < comp.ElementCount; itr++)
							{
								Data[comp][Index * comp.ElementCount + itr] = value.GetValue(itr);
							}
						}
					}
				}
			}
			public Array this[string ComponentName]
			{
				get
				{
					if(Components.ContainsKey(ComponentName)) return Components[ComponentName];
					return null;
				}
				set
				{
					value.CopyTo(Components[ComponentName], 0);
					if (Data != null && Desc != null)
					{
						clsVertexDescriptionComponent comp = Desc.First(itm => itm.Name == ComponentName);
						if(comp != null)
						{
							for (int itr = 0; itr < comp.ElementCount; itr++)
							{
								Data[comp][Index * comp.ElementCount + itr] = value.GetValue(itr);
							}
						}
					}
				}
			}
			public override string ToString()
			{
				string strRet = "";
				foreach (KeyValuePair<string, Array> itm in Components)
				{
					strRet += $"{itm.Key}: ";
					strRet += "{";
					for(int itr = 0; itr < itm.Value.Length; itr++)
					{
						strRet += itm.Value.GetValue(itr) + $"{((itr < itm.Value.Length - 1)?(", "):(""))}";
					}
					strRet += "};";
				}
				return strRet;
			}
		}
		public class clsVertexCollection : IDisposable, IEnumerable<clsVertex>
		{
			internal Dictionary<clsVertexDescriptionComponent, List<object>> Data = new Dictionary<clsVertexDescriptionComponent, List<object>>();
			internal List<clsVertex> aryVertices = new List<clsVertex>();
			public clsVertexCollection(clsVertexDescription refDesc)
			{	
				Desc = refDesc;
				Desc.Updated += desc_Updated;
			}
			private clsVertexDescription refDesc;
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
			public static void ResizeList<Typ>(ref List<Typ> ary, int newsize, Func<int, Typ> funcLamda)
			{
				int oldsize = ary.Count;
				if (newsize > oldsize)
				{
					for (int itr = 0; itr < newsize - oldsize; itr++) ary.Add(funcLamda(oldsize + itr));
				}
				else if (newsize < oldsize)
				{
					ary.RemoveRange(oldsize, newsize - oldsize);
				}
			}
			public int TotalSize { get { return intCount * refDesc.TotalVertexSize; } }

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
							for (int intElem = 0; intElem < comp.ElementCount; intElem++)
							{
								Data[comp][index * comp.ElementCount + intElem] = value[comp.Name].GetValue(intElem);
							}
						}
					}
				}
			}
			public clsVertex[] Items
			{
				get { return aryVertices.ToArray(); }
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

			IEnumerator<clsVertex> IEnumerable<clsVertex>.GetEnumerator()
			{
				return this.GetEnumerator();
			}
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}
			public IEnumerator<clsVertex> GetEnumerator()
			{
				return aryVertices.GetEnumerator();
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
		}
		public clsVertexDescription VertexDescription { set; get; }
		public clsVertexCollection Vertices { set; get; }
		public clsGeometry() : base(ProjectObjectTypes.Geometry)
		{
			VertexDescription = new clsVertexDescription();
			Vertices = new clsVertexCollection(VertexDescription);
			VertexDescription.Add(VertexAttribPointerType.Float, "Position", 2, 0);
			VertexDescription.Add(VertexAttribPointerType.UnsignedByte, "Color", 3, 255);
			Vertices.Count = 4;
			Vertices[0]["Position"] = new float[] { 0f, 0f };
			Vertices[1]["Position"] = new float[] { 1f, 0f };
			Vertices[2]["Position"] = new float[] { 1f, 1f };
			Vertices[3]["Position"] = new float[] { 0f, 1f };
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