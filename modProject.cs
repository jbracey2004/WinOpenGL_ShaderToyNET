using modProject;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using WeifenLuo.WinFormsUI.Docking;
using static modProject.clsProjectObject;
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
		public class clsVertexComponent : IDisposable
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
			public clsVertexComponent(clsVertexDescription refDesc, VertexAttribPointerType glType, string strName, string strAttrName, int intElements) 
			{
				Desc = refDesc;
				ElementGLType = glType;
				Name = strName;
				AttributeName = strAttrName;
				ElementCount = intElements;
			}
			public clsVertexDescription Desc { private set; get; }
			public string Name { set; get; }
			public VertexAttribPointerType ElementGLType { get; set; }
			public Type ElementType { get { return VertexTypes[ElementGLType]; } }
			public int ElementSize { get { return Marshal.SizeOf(VertexTypes[ElementGLType]); } }
			public int ElementCount { set; get; }
			public int ComponentSize { get { return ElementSize * ElementCount; } }
			public int ComponentOffset { get { return Desc.GetComponentOffset(this); } }
			public string AttributeName { set; get; }
			private int intIndex = 0;
			public int Index { set { Desc.SetComponentIndex(this, value); } get { return intIndex; } }
			public void Dispose()
			{
				Desc = null;
			}
			public override string ToString()
			{
				return $"{Name} {{<{ElementGLType}> {((AttributeName != null)?($"Attr: {AttributeName}; "):"")}Type: {ElementType}; Elements: {ElementCount} x {ElementSize}B; Size: {ComponentSize}B; Offset: {ComponentOffset}B; }}";
			}
		}
		public class clsVertexDescription : IDisposable
		{
			private List<clsVertexComponent> aryComponents = new List<clsVertexComponent>();
			public delegate void delegateUpdated();
			public event delegateUpdated Updated;
			public clsVertexComponent this[int index]
			{
				get
				{
					return aryComponents[index];
				}
				set
				{
					aryComponents[index] = value;
					Sort();
				}
			}
			public void AddComponent(VertexAttribPointerType glType, string Name, string AttributeName, int Components)
			{
				clsVertexComponent componentNew = new clsVertexComponent(this, glType, Name, AttributeName, Components);
				componentNew.Index = aryComponents.Count;
				aryComponents.Add(componentNew);
			}
			public void RemoveComponent(clsVertexComponent itm)
			{
				if (!aryComponents.Contains(itm)) return;
				itm.Dispose();
				aryComponents.Remove(itm);
				Sort();
			}
			public int ComponentCount { get { return aryComponents.Count; } }
			public int TotalVertexSize
			{
				get
				{
					int intRet = 0;
					for(int itr = 0; itr < aryComponents.Count; itr++)
					{
						intRet += aryComponents[itr].ComponentSize;
					}
					return 0;
				}
			}
			protected void Sort()
			{
				aryComponents.Sort((itmA, itmB) => (itmB.Index - itmA.Index));
			}
			protected internal int GetComponentOffset(clsVertexComponent itm)
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
			protected internal void SetComponentIndex(clsVertexComponent itm, int index)
			{
				if (!aryComponents.Contains(itm)) return;
				itm.Index = index;
				Sort();
				for (int itr = 0; itr < aryComponents.Count; itr++)
				{
					aryComponents[itr].Index = itr;
				}
			}
			public void Dispose()
			{
				foreach (clsVertexComponent itm in aryComponents) itm.Dispose();
				aryComponents.Clear();
			}
			public override string ToString()
			{
				string strRet = "";
				foreach (clsVertexComponent itm in aryComponents) strRet += itm.Name + "; ";
				return strRet;
			}
		}
		public class clsVertexCollection : IDisposable
		{
			private IntPtr addrData;
			public clsVertexCollection(clsVertexDescription refDesc)
			{
				addrData = Marshal.AllocHGlobal(0);
				Desc = refDesc;
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

				}
			}
			public Array this[int index]
			{
				get
				{
					List<KeyValuePair<string, Array>> objRet = new List<KeyValuePair<string, Array>>();
					for(int compItr = 0; compItr < refDesc.ComponentCount; compItr++)
					{
						List<object> ary = new List<object>();
						for(int elemItr = 0; elemItr < refDesc[compItr].ElementCount; elemItr++)
						{
							ary.Add(Marshal.PtrToStructure(addrData + GetByteLocation(refDesc, index, compItr, elemItr), refDesc[compItr].ElementType));
						}
						objRet.Add(new KeyValuePair<string, Array>(refDesc[compItr].Name, ary.ToArray()));
					}
					return objRet.ToArray();
				}
				set
				{

				}
			}
			public static int GetByteLocation(clsVertexDescription desc, int VertexIdx, int VertexComponentIdx, int VertexComponentElementIdx)
			{
				return VertexIdx * desc.TotalVertexSize + 
					desc[VertexComponentIdx].ComponentOffset + 
					desc[VertexComponentIdx].ElementSize*VertexComponentElementIdx;
			}
			private void desc_Updated()
			{

			}
			public void Dispose()
			{
				Marshal.FreeHGlobal(addrData);
				refDesc.Updated -= desc_Updated;
				refDesc = null;
			}
		}
		public clsVertexDescription VertexDescription { set; get; }
		public clsVertexCollection Vertices { set; get; }
		public clsGeometry() : base(ProjectObjectTypes.Geometry)
		{
			VertexDescription = new clsVertexDescription();
			Vertices = new clsVertexCollection(VertexDescription);
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
			public int Line { get { return MatchParse().Item1; } }
			public int Column { get { return MatchParse().Item2; } }
			private (int, int) MatchParse()
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
				return (retLine, retColumn);
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