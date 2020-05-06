using modProject;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using WeifenLuo.WinFormsUI.Docking;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Diagnostics;
using static modProject.clsProjectObject;
using static WinOpenGL_ShaderToy.ProjectDef;
using static generalUtils;
using static OpenTK.Platform.Utilities;
using System.ComponentModel;
using System.Reflection;
using static modProject.clsGeometry;
using System.Windows.Forms;
using static modProject.clsUniformSet;
using Microsoft.CodeAnalysis;
using System.Collections.Immutable;
using WinOpenGL_ShaderToy;
using static modProject.clsEventScript;
using static modCommon.modWndProcInterop.InputInterface;
using System.Xml.Serialization;
using static modProject.modXml;
using System.Xml;
using System.Text;
using static modCommon.modWndProcInterop;
using System.Threading.Tasks;
using modCommon;
using System.Web.Configuration;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WinOpenGL_ShaderToy
{
	public static class ProjectDef
	{
		public static List<DockContent> AllForms = new List<DockContent>();
		public static DockContent NewFormFromObject(clsProjectObject obj)
		{
			switch(obj.ProjectObjType)
			{
				case ProjectObjectTypes.VertexDescription:
					return new frmVertexDescription((clsVertexDescription)obj);
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
		public static clsProjectObject NewProjectObject(ProjectObjectTypes typ)
		{
			switch (typ)
			{
				case ProjectObjectTypes.VertexDescription:
					return new clsVertexDescription();
				case ProjectObjectTypes.Geometry:
					return new clsGeometry();
				case ProjectObjectTypes.Shader:
					return new clsShader(ShaderType.VertexShader);
				case ProjectObjectTypes.Render:
					return new clsRender();
				case ProjectObjectTypes.Program:
					return new clsProgram();
				default:
					return null;
			}
		}
		public static ScriptOptions GenericScriptOptions;
		public static Script MainScript;
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
		public static void DefaultScriptInit()
		{
			Assembly[] aryAssm = new Assembly[]
			{
				Assembly.GetExecutingAssembly()
			};
			string[] aryImports = new string[] 
			{
				"System",
				"System.Drawing",
				"modCommon",
				"modProject.clsEventScript.clsEventScriptContext_Functions",
				"modCommon.modWndProcInterop", "modCommon.modWndProcInterop.InputInterface"
			};
			GenericScriptOptions = ScriptOptions.Default
				.AddReferences(aryAssm)
				.AddImports(aryImports);
			MainScript = CSharpScript.Create("", GenericScriptOptions, typeof(clsEventScriptContext));
			MainScript.Compile();
		}
		public static string ExceptionFullString(Exception err)
		{
			string strErr = ""; Exception errInner = err;
			while (errInner != null)
			{
				strErr += errInner.Message + "; ";
				errInner = errInner.InnerException;
			}
			return strErr;
		}
		public static string PreProcess(string strCode, clsEventScriptContext context)
		{
			return Regex.Replace(strCode, @"Obj\((?<objectDef>.*?)\)", match =>
			{
				if (!match.Success) return "";
				if (match.Groups["objectDef"] == null) return "";
				if (!match.Groups["objectDef"].Success) return "";
				string strObj = match.Groups["objectDef"].Value;
				object obj = null;
				try
				{
					obj = CSharpScript.EvaluateAsync(strObj, GenericScriptOptions, context).Result;
				}
				catch(Exception err)
				{
					Console.WriteLine(ExceptionFullString(err));
				}
				if (obj == null) return "";
				Type typ = obj.GetType();
				return $"({strObj} as {typ.Name})";
			});
		}
	}
}
namespace modProject
{
	public static class modXml
	{
		public static Type[] ProjectXmlTypes = new Type[]
		{
			typeof(Xml_Render),
			typeof(Xml_Geometry),
			typeof(Xml_VertexDescription),
			typeof(Xml_VertexDescriptionComponent),
			typeof(Xml_Shader),
			typeof(Xml_Program)
		};
		public static string GetXmlString(object obj)
		{
			if (obj == null) return "";
			StringBuilder TextStream = new StringBuilder();
			XmlSerializer parse = new XmlSerializer(obj.GetType(), ProjectXmlTypes);
			XmlWriter writer = XmlWriter.Create(TextStream);
			parse.Serialize(writer, obj);
			string strRet = TextStream.ToString();
			writer.Close();
			writer.Dispose();
			TextStream.Clear();
			return strRet;
		}
		public class Xml_Project
		{
			public List<Xml_ProjectObject> ProjectObjects;
			public Xml_Project() { }
			public Xml_Project(clsProject obj)
			{
				ProjectObjects = obj.ProjectObjects.ConvertAll(itm => itm.Xml);
			}
			public void InitObject(ref clsProject obj)
			{
				List<Xml_KeyValuePair<Xml_ProjectObject, clsProjectObject>> lstTags = new List<Xml_KeyValuePair<Xml_ProjectObject, clsProjectObject>>();
				foreach (var itm in ProjectObjects)
				{
					clsProjectObject objNew = NewProjectObject(itm.ObjectType);
					itm.InitObject(ref objNew);
					obj.ProjectObjects.Add(objNew);
					lstTags.Add(new Xml_KeyValuePair<Xml_ProjectObject, clsProjectObject>(itm, objNew));
				}
				lstTags.Sort((a, b) => Math.Sign(a.Value.ProjectObjType - b.Value.ProjectObjType));
				foreach (var itrTag in lstTags)
				{
					itrTag.Key.UpdateObject(ref itrTag.Value);
				}
				lstTags.Clear();
			}
			public override string ToString()
			{
				return GetXmlString(this);
			}
		}
		public class Xml_Render : Xml_ProjectObject
		{
			public double RenderInterval;
			public string Geometry;
			public string Program;
			public List<Xml_KeyValuePair<string, int>> GeometryShaderLinks;
			public List<Xml_KeyValuePair<string, string>> UniformShaderLinks;
			public List<Xml_KeyValuePair<string, string>> Uniforms;
			public List<string> EventScripts;
			public Xml_Render() : base() { }
			public Xml_Render(clsRender obj) : base(obj)
			{
				if (obj == null) return;
				RenderInterval = obj.RenderInterval;
				Geometry = obj.Geometry?.ToString();
				Program = obj.Program?.ToString();
				GeometryShaderLinks = new List<Xml_KeyValuePair<string, int>>();
				foreach (var itm in obj.GeometryShaderLinks)
				{
					GeometryShaderLinks.Add(new Xml_KeyValuePair<string, int>(itm.Key, itm.Value.Index));
				}
				Uniforms = new List<Xml_KeyValuePair<string, string>>();
				foreach (var itm in obj.Uniforms)
				{
					Uniforms.Add(new Xml_KeyValuePair<string, string>(itm.Key, itm.Value.ToString()));
				}
				UniformShaderLinks = new List<Xml_KeyValuePair<string, string>>();
				foreach (var itm in obj.UniformShaderLinks)
				{
					UniformShaderLinks.Add(new Xml_KeyValuePair<string, string>(itm.Key, itm.Value));
				}
				EventScripts = new List<string>();
				foreach (var itm in obj.EventScripts)
				{
					EventScripts.Add(itm.ToString());
				}
			}
			public override void UpdateObject(ref clsProjectObject obj)
			{
				clsRender rend = obj as clsRender;
				if (rend == null) return;
				rend.RenderInterval = RenderInterval;
				rend.Geometry = clsProjectObject.All.FirstOrDefault(itm => (itm.ToString() == Geometry)) as clsGeometry;
				rend.Program = clsProjectObject.All.FirstOrDefault(itm => (itm.ToString() == Program)) as clsProgram;
				if(rend.Geometry != null)
				{
					clsVertexDescription Desc = rend.Geometry.VertexDescription;
					if(Desc != null)
					{
						foreach (var itr in GeometryShaderLinks)
						{
							clsVertexDescriptionComponent compDesc = Desc.FirstOrDefault(itm => (itm.Index == itr.Value));
							if (compDesc == null) continue;
							rend.GeometryShaderLinks.Add(new KeyValuePair<string, clsVertexDescriptionComponent>(itr.Key, compDesc));
						}
					}
					rend.Geometry.glUpdateBuffers();
				}
				foreach(var itr in Uniforms)
				{
					rend.Uniforms.Add(new KeyValuePair<string, clsUniformSet>(itr.Key, new clsUniformSet(itr.Value)));
				}
				foreach(var itr in UniformShaderLinks)
				{
					rend.UniformShaderLinks.Add(new KeyValuePair<string, string>(itr.Key, itr.Value));
				}
				foreach(var itr in EventScripts)
				{
					clsEventScript script = EventScript_FromString(itr);
					script.Subject = rend;
					script.Compile();
					rend.EventScripts.Add(script);
				}
				rend.LinkShaderUniforms();
			}
			public override string ToString()
			{
				return GetXmlString(this);
			}
		}
		public class Xml_Geometry : Xml_ProjectObject
		{
			public string VertexDescription;
			public List<Xml_KeyValuePair<string, List<object>>> VertexData;
			public List<uint> TriangleData;
			public int VertexCount;
			public int TriangleCount;
			public Xml_Geometry() : base() { }
			public Xml_Geometry(clsGeometry obj) : base(obj)
			{
				VertexDescription = obj.VertexDescription.ToString();
				VertexData = new List<Xml_KeyValuePair<string, List<object>>>();
				foreach (var itm in obj.Vertices.Data)
				{
					VertexData.Add(new Xml_KeyValuePair<string, List<object>>(itm.Key.Name, itm.Value));
				}
				TriangleData = new List<uint>(obj.Triangles.Indices);
				VertexCount = obj.Vertices.Count;
				TriangleCount = obj.Triangles.Count;
			}
			public override void UpdateObject(ref clsProjectObject obj)
			{
				clsGeometry geom = obj as clsGeometry;
				if (geom == null) return;
				clsVertexDescription desc = clsProjectObject.All.First(itm => (itm.ToString() == VertexDescription)) as clsVertexDescription;
				if (desc == null) return;
				geom.VertexDescription = desc;
				geom.Vertices = new clsVertexCollection(geom);
				geom.Triangles = new clsTriangleCollection(geom);
				geom.Vertices.Count = VertexCount;
				geom.Triangles.Count = TriangleCount;
				foreach(var itrVrt in VertexData)
				{
					clsVertexDescriptionComponent compDesc = desc.First(itm => (itm.Name == itrVrt.Key));
					if (compDesc == null) continue;
					geom.Vertices.Data[compDesc] = itrVrt.Value;
				}
				for (int itr = 0; itr < TriangleCount; itr++)
				{
					geom.Triangles[itr].Items = new uint[]
					{
						TriangleData[0 + itr*3],
						TriangleData[1 + itr*3],
						TriangleData[2 + itr*3]
					};
				}
				geom.glUpdateBuffers();
			}
			public override string ToString()
			{
				return GetXmlString(this);
			}
		}
		public class Xml_VertexDescription : Xml_ProjectObject
		{
			public List<Xml_VertexDescriptionComponent> Components;
			public Xml_VertexDescription() : base() { }
			public Xml_VertexDescription(clsVertexDescription obj) : base(obj)
			{
				Components = new List<Xml_VertexDescriptionComponent>();
				foreach (var itm in obj)
				{
					Components.Add(new Xml_VertexDescriptionComponent()
					{
						Name = itm.Name,
						Index = itm.Index,
						ElementGLType = itm.ElementGLType,
						ElementCount = itm.ElementCount
					});
				}
			}
			public override void InitObject(ref clsProjectObject obj)
			{
				base.InitObject(ref obj);
				clsVertexDescription desc = obj as clsVertexDescription;
				if (desc == null) return;
				foreach (var itrDesc in Components)
				{
					desc.Add(new clsVertexDescriptionComponent(desc, itrDesc.ElementGLType, itrDesc.Name, itrDesc.ElementCount, 0));
				}
			}
			public override string ToString()
			{
				return GetXmlString(this);
			}
		}
		public class Xml_VertexDescriptionComponent
		{
			public string Name;
			public int Index;
			public VertexAttribPointerType ElementGLType;
			public int ElementCount;
			public Xml_VertexDescriptionComponent() { }
			public override string ToString()
			{
				return GetXmlString(this);
			}
		}
		public class Xml_Shader : Xml_ProjectObject
		{
			public ShaderType ShaderType;
			public string Source;
			public Xml_Shader() : base() { }
			public Xml_Shader(clsShader obj) : base(obj)
			{
				ShaderType = obj.Type;
				Source = obj.Source;
			}
			public override void InitObject(ref clsProjectObject obj)
			{
				clsShader shd = obj as clsShader;
				if(shd != null) shd.Type = ShaderType;
				base.InitObject(ref obj);
			}
			public override void UpdateObject(ref clsProjectObject obj)
			{
				clsShader shd = obj as clsShader;
				if (shd == null) return;
				shd.Source = Source;
				shd.Compile();
			}
			public override string ToString()
			{
				return GetXmlString(this);
			}
		}
		public class Xml_Program : Xml_ProjectObject
		{
			public List<string> Shaders;
			public Xml_Program() : base() { }
			public Xml_Program(clsProgram obj) : base(obj)
			{
				Shaders = new List<string>();
				foreach(var itm in obj.Shaders)
				{
					Shaders.Add(itm.ToString());
				}
			}
			public override void UpdateObject(ref clsProjectObject obj)
			{
				clsProgram prog = obj as clsProgram;
				if (prog == null) return;
				foreach(var strItm in Shaders)
				{
					clsShader refShader = clsProjectObject.All.First(itm => (itm.ToString() == strItm)) as clsShader;
					if (refShader != null) prog.Shaders.Add(refShader);
				}
				prog.Link();
			}
			public override string ToString()
			{
				return GetXmlString(this);
			}
		}
		public class Xml_KeyValuePair<TKey, TValue>
		{
			public TKey Key;
			public TValue Value;
			public Xml_KeyValuePair() { }
			public Xml_KeyValuePair(TKey objKey, TValue objValue)
			{
				Key = objKey;
				Value = objValue;
			}
			public override string ToString()
			{
				return GetXmlString(this);
			}
		}
	}
	public class clsProject : IDisposable
	{
		public string Name { set; get; }
		public string Path { set; get; }
		public List<clsProjectObject> ProjectObjects { set; get; } = new List<clsProjectObject>() { };
		private bool bolDisposed = false;
		protected virtual void Dispose(bool bolDisposing)
		{
			if(!bolDisposed)
			{
				if(bolDisposing)
				{
					foreach (var obj in ProjectObjects)
					{
						obj.Dispose();
					}
					ProjectObjects.Clear();
					ProjectObjects = null;
				}
			}
			bolDisposed = true;
		}
		public void Dispose()
		{
			Dispose(true);
		}

		[Browsable(false)]
		public Xml_Project Xml
		{
			get => new Xml_Project(this);
		}
	}
	public class clsKeyCollection<TKey, TValue>
	{
		public IList<KeyValuePair<TKey, TValue>> Collection { get; set; }
		public clsKeyCollection(IList<KeyValuePair<TKey, TValue>> collection) 
		{
			Collection = collection;
		}
		public TValue this[int index]
		{
			get => Collection[index].Value;
			set
			{
				KeyValuePair<TKey, TValue> keypairOld = Collection[index];
				KeyValuePair<TKey, TValue> keypairNew = new KeyValuePair<TKey, TValue>(keypairOld.Key, value);
				Collection[index] = keypairNew;
			}
		}
		public TValue this[TKey key]
		{
			get => Collection.FirstOrDefault(itm => itm.Key.Equals(key)).Value;
			set
			{
				KeyValuePair<TKey, TValue> keypairOld = Collection.First(itm => itm.Key.Equals(key));
				KeyValuePair<TKey, TValue> keypairNew = new KeyValuePair<TKey, TValue>(keypairOld.Key, value);
				int index = Collection.IndexOf(keypairOld);
				Collection[index] = keypairNew;
			}
		}
	}
	public class clsUniformSet : IEnumerable
	{
		public enum UniformType
		{
			Int, Float, Double,
			Int2, Float2, Double2,
			Int3, Float3, Double3,
			Int4, Float4, Double4,
			Float2x2, Float2x3, Float2x4,
			Float3x3, Float3x2, Float3x4,
			Float4x4, Float4x3, Float4x2,
			Double2x2, Double2x3, Double2x4,
			Double3x3, Double3x2, Double3x4,
			Double4x4, Double4x3, Double4x2
		}
		public static int UniformType_ComponentCount(UniformType typ)
		{
			int intRet = 1;
			foreach (Match regMatch in Regex.Matches(typ.ToString(), @"\d+"))
			{
				int intNum;
				string strNum = regMatch.Value;
				if (int.TryParse(strNum, out intNum)) intRet *= intNum;
			}
			return intRet;
		}
		public delegate void delegateUniform(int glUniformLocation, int Count, object[] dat);
		public static Dictionary<UniformType, delegateUniform> UniformBindDelegate = new Dictionary<UniformType, delegateUniform>()
		{
			{UniformType.Int, (glUniform,  intCount,intDat) => {GL.Uniform1(glUniform, intCount, ObjectArrayAsType<int>(intDat)); } },
			{UniformType.Float,  (glUniform, intCount, floatDat) => {GL.Uniform1(glUniform, intCount, ObjectArrayAsType<float>(floatDat)); } },
			{UniformType.Double, (glUniform, intCount, doubleDat) => {GL.Uniform1(glUniform, intCount, ObjectArrayAsType<double>(doubleDat)); } },
			{UniformType.Int2, (glUniform, intCount, intDat) => {GL.Uniform2(glUniform, intCount, ObjectArrayAsType<int>(intDat)); } },
			{UniformType.Float2,  (glUniform, intCount, floatDat) => {GL.Uniform2(glUniform, intCount, ObjectArrayAsType<float>(floatDat)); } },
			{UniformType.Double2, (glUniform, intCount, doubleDat) => {GL.Uniform2(glUniform, intCount, ObjectArrayAsType<double>(doubleDat)); } },
			{UniformType.Int3, (glUniform, intCount, intDat) => {GL.Uniform3(glUniform, intCount, ObjectArrayAsType<int>(intDat)); } },
			{UniformType.Float3,  (glUniform, intCount, floatDat) => {GL.Uniform3(glUniform, intCount, ObjectArrayAsType<float>(floatDat)); } },
			{UniformType.Double3, (glUniform, intCount, doubleDat) => {GL.Uniform3(glUniform, intCount, ObjectArrayAsType<double>(doubleDat)); } },
			{UniformType.Int4, (glUniform, intCount, intDat) => {GL.Uniform4(glUniform, intCount, ObjectArrayAsType<int>(intDat)); } },
			{UniformType.Float4,  (glUniform, intCount, floatDat) => {GL.Uniform4(glUniform, intCount, ObjectArrayAsType<float>(floatDat)); } },
			{UniformType.Double4, (glUniform, intCount, doubleDat) => {GL.Uniform4(glUniform, intCount, ObjectArrayAsType<double>(doubleDat)); } },
			{UniformType.Float2x2, (glUniform, intCount, matxDat) => {GL.UniformMatrix2(glUniform, intCount, false, ObjectArrayAsType<float>(matxDat) ); } },
			{UniformType.Float2x3, (glUniform, intCount, matxDat) => {GL.UniformMatrix2x3(glUniform, intCount, false, ObjectArrayAsType<float>(matxDat) ); }},
			{UniformType.Float2x4, (glUniform, intCount, matxDat) => {GL.UniformMatrix2x4(glUniform, intCount, false, ObjectArrayAsType<float>(matxDat) ); }},
			{UniformType.Float3x3, (glUniform, intCount, matxDat) => {GL.UniformMatrix3(glUniform, intCount, false, ObjectArrayAsType<float>(matxDat) ); }},
			{UniformType.Float3x2, (glUniform, intCount, matxDat) => {GL.UniformMatrix3x2(glUniform, intCount, false, ObjectArrayAsType<float>(matxDat) ); }},
			{UniformType.Float3x4, (glUniform, intCount, matxDat) => {GL.UniformMatrix3x4(glUniform, intCount, false, ObjectArrayAsType<float>(matxDat) ); }},
			{UniformType.Float4x4, (glUniform, intCount, matxDat) => {GL.UniformMatrix4(glUniform, intCount, false, ObjectArrayAsType<float>(matxDat) ); }},
			{UniformType.Float4x3, (glUniform, intCount, matxDat) => {GL.UniformMatrix4x3(glUniform, intCount, false, ObjectArrayAsType<float>(matxDat) ); }},
			{UniformType.Float4x2, (glUniform, intCount, matxDat) => {GL.UniformMatrix4x2(glUniform, intCount, false, ObjectArrayAsType<float>(matxDat) ); }},
			{UniformType.Double2x2, (glUniform, intCount, matxDat) => {GL.UniformMatrix2(glUniform, intCount, false, ObjectArrayAsType<double>(matxDat)  ); } },
			{UniformType.Double2x3, (glUniform, intCount, matxDat) => {GL.UniformMatrix2x3(glUniform, intCount, false, ObjectArrayAsType<double>(matxDat) ); }},
			{UniformType.Double2x4, (glUniform, intCount, matxDat) => {GL.UniformMatrix2x4(glUniform, intCount, false, ObjectArrayAsType<double>(matxDat) ); }},
			{UniformType.Double3x3, (glUniform, intCount, matxDat) => {GL.UniformMatrix3(glUniform, intCount, false, ObjectArrayAsType<double>(matxDat) ); }},
			{UniformType.Double3x2, (glUniform, intCount, matxDat) => {GL.UniformMatrix3x2(glUniform, intCount, false, ObjectArrayAsType<double>(matxDat) ); }},
			{UniformType.Double3x4, (glUniform, intCount, matxDat) => {GL.UniformMatrix3x4(glUniform, intCount, false, ObjectArrayAsType<double>(matxDat) ); }},
			{UniformType.Double4x4, (glUniform, intCount, matxDat) => {GL.UniformMatrix4(glUniform, intCount, false, ObjectArrayAsType<double>(matxDat) ); }},
			{UniformType.Double4x3, (glUniform, intCount, matxDat) => {GL.UniformMatrix4x3(glUniform, intCount, false, ObjectArrayAsType<double>(matxDat) ); }},
			{UniformType.Double4x2, (glUniform, intCount, matxDat) => {GL.UniformMatrix4x2(glUniform, intCount, false, ObjectArrayAsType<double>(matxDat) ); }}
		};
		public static Dictionary<UniformType, object[]> UniformType_InitialValues = new Dictionary<UniformType, object[]>()
		{
			{UniformType.Int, new object[] { (int)0 } },
			{UniformType.Float,  new object[] { (float)0 } },
			{UniformType.Double, new object[] { (double)0 } },
			{UniformType.Int2, new object[] {(int)0, (int)0} },
			{UniformType.Float2,  new object[] {(float)0, (float)0} },
			{UniformType.Double2, new object[] {(double)0, (double)0} },
			{UniformType.Int3, new object[] {(int)0, (int)0, (int)0} },
			{UniformType.Float3,  new object[] {(float)0, (float)0, (float)0} },
			{UniformType.Double3, new object[] {(double)0, (double)0, (double)0} },
			{UniformType.Int4, new object[] {(int)0, (int)0, (int)0, (int)0} },
			{UniformType.Float4,  new object[] {(float)0, (float)0, (float)0, (float)0} },
			{UniformType.Double4, new object[] {(double)0, (double)0, (double)0, (double)0} },
			{UniformType.Float2x2, new object[] {(float)1, (float)0, (float)0, (float)1} },
			{UniformType.Float2x3, new object[] {(float)1, (float)0, (float)0, (float)1, (float)0, (float)0} },
			{UniformType.Float2x4, new object[] {(float)1, (float)0, (float)0, (float)1, (float)0, (float)0, (float)0, (float)0} },
			{UniformType.Float3x3, new object[] {(float)1, (float)0, (float)0, (float)0, (float)1, (float)0, (float)0, (float)0, (float)1} },
			{UniformType.Float3x2, new object[] {(float)1, (float)0, (float)0, (float)0, (float)1, (float)0} },
			{UniformType.Float3x4, new object[] {(float)1, (float)0, (float)0, (float)0, (float)1, (float)0, (float)0, (float)0, (float)1, (float)0, (float)0, (float)0} },
			{UniformType.Float4x4, new object[] {(float)1, (float)0, (float)0, (float)0, (float)0, (float)1, (float)0, (float)0, (float)0, (float)0, (float)1, (float)0, (float)0, (float)0, (float)0, (float)1} },
			{UniformType.Float4x3, new object[] {(float)1, (float)0, (float)0, (float)0, (float)0, (float)1, (float)0, (float)0, (float)0, (float)0, (float)1, (float)0} },
			{UniformType.Float4x2, new object[] {(float)1, (float)0, (float)0, (float)0, (float)0, (float)1, (float)0, (float)0} },
			{UniformType.Double2x2, new object[] {(double)1, (double)0, (double)0, (double)1} },
			{UniformType.Double2x3, new object[] {(double)1, (double)0, (double)0, (double)1, (double)0, (double)0} },
			{UniformType.Double2x4, new object[] {(double)1, (double)0, (double)0, (double)1, (double)0, (double)0, (double)0, (double)0} },
			{UniformType.Double3x3, new object[] {(double)1, (double)0, (double)0, (double)0, (double)1, (double)0, (double)0, (double)0, (double)1} },
			{UniformType.Double3x2, new object[] {(double)1, (double)0, (double)0, (double)0, (double)1, (double)0} },
			{UniformType.Double3x4, new object[] {(double)1, (double)0, (double)0, (double)0, (double)1, (double)0, (double)0, (double)0, (double)1, (double)0, (double)0, (double)0} },
			{UniformType.Double4x4, new object[] {(double)1, (double)0, (double)0, (double)0, (double)0, (double)1, (double)0, (double)0, (double)0, (double)0, (double)1, (double)0, (double)0, (double)0, (double)0, (double)1} },
			{UniformType.Double4x3, new object[] {(double)1, (double)0, (double)0, (double)0, (double)0, (double)1, (double)0, (double)0, (double)0, (double)0, (double)1, (double)0} },
			{UniformType.Double4x2, new object[] {(double)1, (double)0, (double)0, (double)0, (double)0, (double)1, (double)0, (double)0} }
		};
		public static Dictionary<UniformType, Type> UniformType_ComponentType = new Dictionary<UniformType, Type>()
		{
			{UniformType.Int, typeof(int) },
			{UniformType.Float, typeof(float) },
			{UniformType.Double, typeof(double) },
			{UniformType.Int2, typeof(int) },
			{UniformType.Float2, typeof(float) },
			{UniformType.Double2, typeof(double) },
			{UniformType.Int3, typeof(int) },
			{UniformType.Float3, typeof(float)},
			{UniformType.Double3, typeof(double) },
			{UniformType.Int4, typeof(int) },
			{UniformType.Float4, typeof(float) },
			{UniformType.Double4, typeof(double) },
			{UniformType.Float2x2, typeof(float) },
			{UniformType.Float2x3, typeof(float) },
			{UniformType.Float2x4, typeof(float) },
			{UniformType.Float3x3, typeof(float) },
			{UniformType.Float3x2, typeof(float) },
			{UniformType.Float3x4, typeof(float) },
			{UniformType.Float4x4, typeof(float) },
			{UniformType.Float4x3, typeof(float) },
			{UniformType.Float4x2, typeof(float) },
			{UniformType.Double2x2, typeof(double) },
			{UniformType.Double2x3, typeof(double) },
			{UniformType.Double2x4, typeof(double) },
			{UniformType.Double3x3, typeof(double) },
			{UniformType.Double3x2, typeof(double) },
			{UniformType.Double3x4, typeof(double) },
			{UniformType.Double4x4, typeof(double) },
			{UniformType.Double4x3, typeof(double) },
			{UniformType.Double4x2, typeof(double) }
		};
		public static List<object[]> StringToArray(string str, out int intComponentCount, out int intComponentType)
		{
			UniformType enumType = UniformType.Int;
			intComponentType = -1;
			Match matchType = Regex.Match(str, @"\<[\w|\d]+\>");
			if (matchType.Success)
			{
				str = str.Replace(matchType.Value, "").Trim();
				string strType = matchType.Value.Trim('<', '>');
				if (Enum.TryParse(strType, out enumType)) intComponentType = (int)enumType;
			}
			if (intComponentType == -1) { intComponentCount = 1; } else { intComponentCount = UniformType_ComponentCount(enumType); }
			List<object[]> aryRet = new List<object[]>();
			if (!str.Contains("(") && !str.Contains(")")) { str = "(" + str + ")"; }
			foreach (Match regMatch in Regex.Matches(str, @"\((\S+?\s{0,}\,{0,}\s{0,}){1,}\)"))
			{
				string[] aryStr = regMatch.Value.Split(',');
				List<object> elem = new List<object>();
				for (int itr = 0; itr < aryStr.Length; itr++)
				{
					string strI = aryStr[itr].Trim('(', ')').Trim();
					if (double.TryParse(strI, out double obj)) { elem.Add(obj); } else { elem.Add(0); }
				}
				if (intComponentType == -1)
				{
					intComponentCount = Math.Max(intComponentCount, elem.Count);
				}
				aryRet.Add(elem.ToArray());
			}
			for (int itr = 0; itr < aryRet.Count; itr++)
			{
				List<object> elem = new List<object>(aryRet[itr]);
				ResizeList(ref elem, intComponentCount, itmEmpty => 0);
				aryRet[itr] = elem.ToArray();
			}
			return aryRet;
		}
		public static string ArrayToString(List<object[]> ary)
		{
			string strRet = "";
			for (int itr = 0; itr < ary.Count; itr++)
			{
				if (ary.Count > 1) strRet += "(";
				for (int itrComp = 0; itrComp < ary[itr].Length; itrComp++)
				{
					strRet += ary[itr][itrComp].ToString();
					if (itrComp < ary[itr].Length - 1) strRet += ", ";
				}
				if (ary.Count > 1) { strRet += ")"; if (itr < ary.Count - 1) strRet += " "; };
			}
			return strRet;
		}
		public static string ArrayToString(List<object[]> ary, UniformType typ)
		{
			string strRet = "";
			Type typObj = UniformType_ComponentType[typ];
			for (int itr = 0; itr < ary.Count; itr++)
			{
				if (ary.Count > 1) strRet += "(";
				for (int itrComp = 0; itrComp < ary[itr].Length; itrComp++)
				{
					object objData = ary[itr][itrComp];
					if (objData == null) objData = 0;
					objData = Convert.ChangeType(objData, typObj);
					string str = objData.ToString();
					strRet += str;
					if (itrComp < ary[itr].Length - 1) strRet += ", ";
				}
				if (ary.Count > 1) { strRet += ")"; if (itr < ary.Count - 1) strRet += " "; };
			}
			return strRet;
		}
		private UniformType typType = UniformType.Int;
		public UniformType Type
		{
			get => typType;
			set
			{
				typType = value;
				ComponentType = UniformType_ComponentType[typType];
				ComponentPerElement = UniformType_ComponentCount(typType);
				ElementCount = aryDataInlined.Length / ComponentPerElement;
			}
		}
		public Type ComponentType { get; private set; } = typeof(int);
		public int ComponentPerElement { get; private set; } = 1;
		public int ElementCount { get; private set; } = 1;
		public KeyValuePair<string, int> ShaderUniformLink { get; set; } = new KeyValuePair<string, int>("", -1);
		public clsUniformSet() { }
		public clsUniformSet(UniformType type, object[] ary)
		{
			object[][] dat = new object[][] { ary };
			Type = type;
			SetData(dat);
		}
		public clsUniformSet(UniformType type, object[][] ary)
		{
			object[][] dat = ary;
			Type = type;
			SetData(dat);
		}
		public clsUniformSet(string str)
		{
			object[][] dat = StringToArray(str, out int intNewCompLen, out int intNewCompType).ToArray();
			if (intNewCompType != -1) { Type = (UniformType)intNewCompType; }
			if (intNewCompLen != -1) { ComponentPerElement = intNewCompLen; }
			SetData(dat);
		}
		public static implicit operator object[](clsUniformSet uds) => uds.aryDataInlined;
		public static implicit operator object[][](clsUniformSet uds) => uds.GetData();
		public static implicit operator string(clsUniformSet uds) => uds.ToString();
		public static implicit operator clsUniformSet(string str) => new clsUniformSet(str);
		private object[] aryDataInlined = new object[] { 0 };
		private object[] aryDataInlined_Formatted = new object[] { 0 };
		public object[] DataInlined
		{
			get => aryDataInlined;
			set
			{
				object[] ary = Array.ConvertAll(value, itm => Convert.ChangeType(itm, ComponentType));
				Array.Copy(value, 0, aryDataInlined, 0, Math.Min(value.Length, aryDataInlined.Length));
				Array.Copy(ary, 0, aryDataInlined_Formatted, 0, Math.Min(value.Length, aryDataInlined_Formatted.Length));
			}
		}
		public object[] DataInlined_Formatted { get => aryDataInlined_Formatted; set { DataInlined = value; } }
		public object[][] Data { get => GetData(); set { SetData(value); } }
		public object[] this[int Element, bool bolFormatted = false]
		{
			get => GetData(Element, bolFormatted);
			set { SetData(Element, value); }
		}
		public object this[int Element, int Component, bool bolFormatted = false]
		{
			get => GetData(Element, Component, bolFormatted);
			set { SetData(Element, Component, value); }
		}
		public object[][] GetData(bool bolFormatted = false)
		{
			object[][] lstRet = new object[ElementCount][];
			object[] arySelected = (bolFormatted) ? aryDataInlined_Formatted : aryDataInlined;
			for (int itr = 0; itr < lstRet.Length; itr++)
			{
				lstRet[itr] = new object[ComponentPerElement];
				arySelected.CopyTo(lstRet[itr], itr * ComponentPerElement);
			}
			return lstRet;
		}
		public void SetData(object[][] ary)
		{
			ElementCount = ary.Length;
			aryDataInlined = new object[ElementCount * ComponentPerElement];
			for (int itr = 0; itr < ElementCount; itr++)
			{
				ary[itr].CopyTo(aryDataInlined, itr * ComponentPerElement);
			}
			aryDataInlined_Formatted = Array.ConvertAll(aryDataInlined, itm => Convert.ChangeType(itm, ComponentType));
		}
		public object[] GetData(int Element, bool bolFormatted = false)
		{
			object[] aryRet = new object[ComponentPerElement];
			object[] arySelected = (bolFormatted) ? aryDataInlined_Formatted : aryDataInlined;
			Array.Copy(arySelected, Element * ComponentPerElement, aryRet, 0, ComponentPerElement);
			return aryRet;
		}
		public void SetData(int Element, object[] ary)
		{
			object[] aryFormatted = Array.ConvertAll(ary, itm => Convert.ChangeType(itm, ComponentType));
			ary.CopyTo(aryDataInlined, Element * ComponentPerElement);
			aryFormatted.CopyTo(aryDataInlined_Formatted, Element * ComponentPerElement);
		}
		public object GetData(int Element, int Component, bool bolFormatted = false)
		{
			object[] arySelected = (bolFormatted) ? aryDataInlined_Formatted : aryDataInlined;
			return arySelected[Component + Element * ComponentPerElement];
		}
		public void SetData(int Element, int Component, object value)
		{
			aryDataInlined[Component + Element * ComponentPerElement] = value;
			aryDataInlined_Formatted[Component + Element * ComponentPerElement] = Convert.ChangeType(value, ComponentType);
		}
		public void BindData()
		{
			if (string.IsNullOrEmpty(ShaderUniformLink.Key)) return;
			if(ShaderUniformLink.Value < 0) return;
			UniformBindDelegate[Type](ShaderUniformLink.Value, ElementCount, aryDataInlined_Formatted);
		}
		public override string ToString()
		{
			return $"<{Type}> " + ArrayToString(GetData(true).ToList());
		}
		public IEnumerator GetEnumerator()
		{
			return aryDataInlined.GetEnumerator();
		}
	}
	public class clsUniformSetCollection : IEnumerable
	{
		public List<KeyValuePair<string, clsUniformSet>> Collection;
		public clsUniformSetCollection(List<KeyValuePair<string, clsUniformSet>> collection)
		{
			Collection = collection;
		}
		public clsUniformSet this[int index]
		{
			get => Collection[index].Value;
			set { Collection[index].Value.SetData(value.GetData()); }
		}
		public clsUniformSet this[string key]
		{
			get => Collection.FirstOrDefault(itm => itm.Key.Equals(key)).Value;
			set
			{
				int index = Collection.FindIndex(itm => itm.Key.Equals(key));
				if (index >= 0) Collection[index].Value.SetData(value.GetData());
			}
		}
		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)Collection).GetEnumerator();
		}
		public override string ToString()
		{
			return generalUtils.ArrayToString(Collection);
		}
	}
	public class clsEventScript
	{
		public enum EventType
		{
			OnLoad = 0,
			OnRender = 1,
			OnResize = 2,
			OnPointerStart = 3,
			OnPointerMove = 4,
			OnPointerEnd = 5
		}
		public static ParameterInfo[] EventType_Parameters(EventType typ)
		{
			string str = $"Raise{typ.ToString().Replace("On", "")}Event";
			return typeof(clsRender).GetMethod(str)?.GetParameters();
		}
		public static clsEventScript EventScript_FromString(string str)
		{
			clsEventScript ret = new clsEventScript();
			EventScript_FromString(str, ref ret);
			return ret;
		}
		public static void EventScript_FromString(string str, ref clsEventScript obj)
		{
			EventScript_FromString(str, out EventType typ, out string src);
			obj.Type = typ; obj.Source = src;
		}
		public static void EventScript_FromString(string str, out EventType typ, out string src)
		{
			typ = EventType.OnLoad;
			Match mch;
			mch = Regex.Match(str, @"\w+\s{0,}");
			if (mch.Success)
			{
				if (Enum.TryParse(mch.Value, out EventType intTyp)) typ = intTyp;
				str = str.Replace(mch.Value, "").Trim();
			}
			mch = Regex.Match(str, @"\((\s{0,}\w+\s+\w+\,{0,}){0,}\s{0,}\)");
			if (mch.Success)
			{
				str = str.Replace(mch.Value, "").Trim();
			}
			src = str.Trim('{', '}').Trim();
		}
		public static string EventScript_ToString(clsEventScript obj)
		{
			return EventScript_ToString(obj.Type, obj.Source);
		}
		public static string EventScript_ToString(EventType typ, string src)
		{
			ParameterInfo[] parms = EventType_Parameters(typ);
			string str = "";
			for (int itr = 0; itr < parms.Length; itr++)
			{
				str += parms[itr].ParameterType.Name + " " + parms[itr].Name + ((itr < parms.Length - 1) ? ", " : "");
			}
			return $"{typ} ( {str} ) {{ {src} }}";
		}
		public override string ToString()
		{
			return EventScript_ToString(this);
		}
		public static class clsEventScriptContext_Functions
		{
			public static double pi => Math.PI;
			public static double tau => pi * 2;
			public static double sin(double x) => Math.Sin(x);
			public static double cos(double x) => Math.Cos(x);
			public static double tan(double x) => Math.Tan(x);
			public static double sinh(double x) => Math.Sinh(x);
			public static double cosh(double x) => Math.Cosh(x);
			public static double tanh(double x) => Math.Tanh(x);
			public static double arcsin(double x) => Math.Asin(x);
			public static double arccos(double x) => Math.Acos(x);
			public static double arctan(double x) => Math.Atan(x);
			public static double arctan(double y, double x) => Math.Atan2(y, x);
			public static double sqrt(double x) => Math.Sqrt(x);
			public static double pow(double b, double exp) => Math.Pow(b, exp);
			public static double e => Math.E;
			public static double exp(double x) => Math.Exp(x);
			public static double ln(double x) => Math.Log(x);
			public static double log(double x, double b) => Math.Log(x, b);
			public static double log(double x) => Math.Log10(x);
			public static double abs(double x) => Math.Abs(x);
			public static double sgn(double x) => Math.Sign(x);
			public static double[] Vals(object[] args)
			{
				return Array.ConvertAll(args, itm => 
				{ 
					double.TryParse(itm.ToString(), out double itmRet); 
					return itmRet; 
				}).ToArray();
			}
			public static object[] Vec(int numElems)
			{
				return ArrayList.Repeat(0, numElems).ToArray();
			}
			public static object[] Vec<T>(params T[] args)
			{
				return Array.ConvertAll(args, itm => (object)itm);
			}
			public static object[] Vec(int Len, object Val)
			{
				return ArrayList.Repeat(Val, Len).ToArray();
			}
			public static object Vec_Elem(object[] args, int index)
			{
				object objRet = 0;
				if (index < args.Length) objRet = args[index];
				return objRet;
			}
			public static void Vec_Elem<T>(ref object[] args, int index, T value)
			{
				if (index < args.Length)
				{
					args[index] = value;
				}
			}
			public static object[] Vec_Sum(object[] VecA, object[] VecB)
			{
				object[] VecRet = new object[Math.Max(VecA.Length, VecB.Length)];
				for (int itr = 0; itr < VecRet.Length; itr++)
				{
					double.TryParse(Vec_Elem(VecA, itr).ToString(), out double dA);
					double.TryParse(Vec_Elem(VecB, itr).ToString(), out double dB);
					VecRet[itr] = dA + dB;
				}
				return VecRet;
			}
			public static object[] Vec_Diff(object[] VecA, object[] VecB)
			{
				object[] VecRet = new object[Math.Max(VecA.Length, VecB.Length)];
				for (int itr = 0; itr < VecRet.Length; itr++)
				{
					double.TryParse(Vec_Elem(VecA, itr).ToString(), out double dA);
					double.TryParse(Vec_Elem(VecB, itr).ToString(), out double dB);
					VecRet[itr] = dA - dB;
				}
				return VecRet;
			}
			public static object[] Vec_Div(object[] VecA, object[] VecB)
			{
				object[] VecRet = new object[Math.Max(VecA.Length, VecB.Length)];
				for (int itr = 0; itr < VecRet.Length; itr++)
				{
					double.TryParse(Vec_Elem(VecA, itr).ToString(), out double dA);
					double.TryParse(Vec_Elem(VecB, itr).ToString(), out double dB);
					VecRet[itr] = dA / dB;
				}
				return VecRet;
			}
			public static object[] Vec_Recipical(object[] Vec)
			{
				object[] VecRet = new object[Vec.Length];
				for (int itr = 0; itr < VecRet.Length; itr++)
				{
					double.TryParse(Vec_Elem(Vec, itr).ToString(), out double dVec);
					VecRet[itr] = 1.0 / dVec;
				}
				return VecRet;
			}
			public static object Vec_Dot(object[] VecA, object[] VecB)
			{
				double fRet = 0;
				for (int itr = 0; itr < Math.Max(VecA.Length, VecB.Length); itr++)
				{
					double.TryParse(Vec_Elem(VecA, itr).ToString(), out double dA);
					double.TryParse(Vec_Elem(VecB, itr).ToString(), out double dB);
					fRet += dA * dB;
				}
				return fRet;
			}
			public static object[] Vec_Mul(object[] VecA, object[] VecB)
			{
				object[] VecRet = new object[Math.Max(VecA.Length, VecB.Length)];
				for (int itr = 0; itr < VecRet.Length; itr++)
				{
					double.TryParse(Vec_Elem(VecA, itr).ToString(), out double dA);
					double.TryParse(Vec_Elem(VecB, itr).ToString(), out double dB);
					VecRet[itr] = dA * dB;
				}
				return VecRet;
			}
			public static object[] Vec_Mul(object Val, object[] Vec)
			{
				object[] VecRet = new object[Vec.Length];
				double.TryParse(Val.ToString(), out double dVal);
				for (int itr = 0; itr < VecRet.Length; itr++)
				{
					double.TryParse(Vec_Elem(Vec, itr).ToString(), out double dVec);
					VecRet[itr] = dVal * dVec;
				}
				return VecRet;
			}
			public static object[] Vec_Mul(object[] Vec, int numColumns, int numRows, object[] matrix)
			{
				return Matrix_Mul(1, Vec.Length, Vec, Math.Min(Vec.Length, numColumns), numRows, matrix);
			}
			public static object[] Vec_Mul(int numColumns, int numRows, object[] matrix, object[] Vec)
			{
				return Matrix_Mul(numColumns, numRows, matrix, Math.Min(Vec.Length, numRows), 1, Vec);
			}
			public static object Vec_Len(object[] Vec)
			{
				double.TryParse(Vec_Dot(Vec, Vec).ToString(), out double dDot);
				return Math.Sqrt(dDot);
			}
			public static object[] Vec_Norm(object[] Vec)
			{
				double.TryParse(Vec_Len(Vec).ToString(), out double dLen);
				return Vec_Mul(1.0 / dLen, Vec);
			}
			public static object Vec_Angle(object[] VecA, object[] VecB)
			{
				double.TryParse(Vec_Dot(VecA, VecB).ToString(), out double dDot);
				double.TryParse(Vec_Len(VecA).ToString(), out double dLenA);
				double.TryParse(Vec_Len(VecB).ToString(), out double dLenB);
				return Math.Acos(dDot / (dLenA * dLenB));
			}
			public static object[] Vec_Cross(object[] VecA, object[] VecB)
			{
				object[] VecRet = new object[Math.Max(VecA.Length, VecB.Length)];
				for (int itr = 0; itr < VecRet.Length; itr++)
				{
					double.TryParse(Vec_Elem(VecA, (itr + 1) % (VecRet.Length)).ToString(), out double dA);
					double.TryParse(Vec_Elem(VecB, (itr + 2) % (VecRet.Length)).ToString(), out double dB);
					double.TryParse(Vec_Elem(VecB, (itr + 1) % (VecRet.Length)).ToString(), out double dnA);
					double.TryParse(Vec_Elem(VecA, (itr + 2) % (VecRet.Length)).ToString(), out double dnB);
					VecRet[itr] = (dA * dB) - (dnA * dnB);
				}
				return VecRet;
			}
			public static object[] Matrix(int numCols, int numRows)
			{
				object[] objRet = ArrayList.Repeat(0, numCols * numRows).ToArray();
				for (int itr = 0; itr < objRet.Length; itr++)
				{
					int tmpCol = itr % numCols;
					int tmpRow = (int)Math.Floor((double)itr / numCols);
					if (tmpCol == tmpRow)
					{
						objRet[itr] = 1.0;
					}
				}
				return objRet;
			}
			public static object[] Matrix<T>(int numCols, int numRows, params T[][] args)
			{
				object[] objRet = ArrayList.Repeat(0, numCols * numRows).ToArray();
				for (int idxRow = 0; idxRow < Math.Min(numRows, args.Length); idxRow++)
				{
					for (int idxCol = 0; idxCol < Math.Min(numCols, args[idxRow].Length); idxCol++)
					{
						int idx = idxCol + idxRow * numCols;
						if (idx < objRet.Length)
						{
							objRet[idx] = args[idxRow][idxCol];
						}
					}
				}
				return objRet;
			}
			public static object[] Matrix<T>(int numCols, int numRows, params T[] args)
			{
				object[] objRet = ArrayList.Repeat(0, numCols * numRows).ToArray();
				int argCols = (int)Math.Floor((double)args.Length / numRows);
				for (int itr = 0; itr < objRet.Length; itr++)
				{
					int tmpCol = itr % argCols;
					int tmpRow = (int)Math.Floor((double)itr / argCols);
					int idx = tmpCol + tmpRow * numCols;
					if (idx < objRet.Length)
					{
						objRet[idx] = args[itr];
					}
				}
				return objRet;
			}
			public static object Matrix_Elem(int numCols, int numRows, int indexCol, int indexRow, object[] args)
			{
				object objRet = 0;
				if (indexCol < numCols && indexRow < numRows && indexCol + indexRow * numCols < args.Length)
				{
					double.TryParse(args[indexCol + indexRow * numCols].ToString(), out double val);
					objRet = val;
				}
				return objRet;
			}
			public static void Matrix_Elem<T>(int numCols, int numRows, int indexCol, int indexRow, ref object[] args, T value)
			{
				if (indexCol < numCols && indexRow < numRows && indexCol + indexRow * numCols < args.Length)
				{
					args[indexCol + indexRow * numCols] = value;
				}
			}
			public static object[] Matrix_Row(int numCols, int numRows, int indexRow, object[] args)
			{
				object[] objRet = ArrayList.Repeat(0, numCols).ToArray();
				if (indexRow < numRows)
				{
					for (int idxCol = 0; idxCol < numCols; idxCol++)
					{
						if (idxCol + indexRow * numCols < args.Length)
						{
							objRet[idxCol] = args[idxCol + indexRow * numCols];
						}
					}
				}
				return objRet;
			}
			public static void Matrix_Row<T>(int numCols, int numRows, int indexRow, ref object[] args, T[] value)
			{
				if (indexRow < numRows)
				{
					for (int idxCol = 0; idxCol < Math.Min(numCols, value.Length); idxCol++)
					{
						if (idxCol + indexRow * numCols < args.Length)
						{
							args[idxCol + indexRow * numCols] = value[idxCol];
						}
					}
				}
			}
			public static object[] Matrix_Column(int numCols, int numRows, int indexColumn, object[] args)
			{
				object[] objRet = ArrayList.Repeat(0, numRows).ToArray();
				if (indexColumn < numRows)
				{
					for (int idxRow = 0; idxRow < numRows; idxRow++)
					{
						if (indexColumn + idxRow * numCols < args.Length)
						{
							objRet[idxRow] = args[indexColumn + idxRow * numCols];
						}
					}
				}
				return objRet;
			}
			public static void Matrix_Column<T>(int numCols, int numRows, int indexColumn, ref object[] args, T[] value)
			{
				if (indexColumn < numRows)
				{
					for (int idxRow = 0; idxRow < Math.Min(numRows, value.Length); idxRow++)
					{
						if (indexColumn + idxRow * numCols < args.Length)
						{
							args[indexColumn + idxRow * numCols] = value[idxRow];
						}
					}
				}
			}
			public static object[] Matrix_MinorAt(int numCols, int numRows, int AtCol, int AtRow, object[] args)
			{
				object[] aryRet = ArrayList.Repeat(0, (numCols - 1) * (numRows - 1)).ToArray();
				for (int itr = 0; itr < args.Length; itr++)
				{
					int tmpCol = itr % numCols;
					int tmpRow = (int)Math.Floor((double)itr / numCols);
					if (tmpCol == AtCol) continue;
					if (tmpRow == AtRow) continue;
					if (tmpCol >= AtCol) tmpCol--;
					if (tmpRow >= AtRow) tmpRow--;
					int idx = tmpCol + tmpRow * (numCols - 1);
					aryRet[idx] = args[itr];
				}
				return aryRet;
			}
			public static object[] Matrix_OfMinors(int numCols, int numRows, object[] args)
			{
				object[] aryRet = ArrayList.Repeat(0, numCols * numRows).ToArray();
				for (int itr = 0; itr < aryRet.Length; itr++)
				{
					int tmpCol = itr % numCols;
					int tmpRow = (int)Math.Floor((double)itr / numCols);
					int idx = tmpCol + tmpRow * numCols;
					aryRet[idx] = Matrix_Det(numCols - 1, numRows - 1, Matrix_MinorAt(numCols, numRows, tmpCol, tmpRow, args));
				}
				return aryRet;
			}
			public static object[] Matrix_OfCoFactors(int numCols, int numRows, object[] args)
			{
				object[] aryRet = ArrayList.Repeat(0, numCols * numRows).ToArray();
				for (int itr = 0; itr < aryRet.Length; itr++)
				{
					int tmpCol = itr % numCols;
					int tmpRow = (int)Math.Floor((double)itr / numCols);
					int idx = tmpCol + tmpRow * numCols;
					double.TryParse(Matrix_Det(numCols - 1, numRows - 1, Matrix_MinorAt(numCols, numRows, tmpCol, tmpRow, args)).ToString(), out double dMnVal);
					double sngCoF = Math.Pow(-1, tmpCol + tmpRow);
					aryRet[idx] = sngCoF * dMnVal;
				}
				return aryRet;
			}
			public static object[] Matrix_Transpose(int numCols, int numRows, object[] args)
			{
				object[] aryRet = ArrayList.Repeat(0, numCols * numRows).ToArray();
				int tmp = numCols;
				numCols = numRows;
				numRows = tmp;
				for (int itr = 0; itr < aryRet.Length; itr++)
				{
					int tmpCol = (int)Math.Floor((double)itr / numCols);
					int tmpRow = itr % numCols;
					int idx = tmpCol + tmpRow * numRows;
					if (idx < args.Length) aryRet[itr] = args[idx];
				}
				return aryRet;
			}
			public static object[] Matrix_Adjugate(int numCols, int numRows, object[] args)
			{
				return Matrix_Transpose(numCols, numRows, Matrix_OfCoFactors(numCols, numRows, args));
			}
			public static object Matrix_Det(int numCols, int numRows, object[] args)
			{
				double dRet = 0;
				if ((numCols == 1) && (numRows == 1)) return args[0];
				for (int itrCol = 0; itrCol < ((numCols == 2) ? 1 : numCols); itrCol++)
				{
					double dProdPos = 1;
					double dProdNeg = 1;
					for (int itrRow = 0; itrRow < numRows; itrRow++)
					{
						double.TryParse(Matrix_Elem(numCols, numRows, (itrCol + itrRow) % numCols, itrRow, args).ToString(), out double dPos);
						double.TryParse(Matrix_Elem(numCols, numRows, (itrCol + itrRow) % numCols, (numRows - 1) - itrRow, args).ToString(), out double dNeg);
						dProdPos *= dPos;
						dProdNeg *= dNeg;
					}
					dRet += (dProdPos - dProdNeg);
				}
				return dRet;
			}
			public static object[] Matrix_Inverse(int numCols, int numRows, object[] args)
			{
				double.TryParse(Matrix_Det(numCols, numRows, args).ToString(), out double dDet);
				return Matrix_Mul(1 / dDet, Matrix_Adjugate(numCols, numRows, args));
			}
			public static object[] Matrix_Mul(double scl, object[] args)
			{
				object[] aryRet = ArrayList.Repeat(0, args.Length).ToArray();
				for (int itr = 0; itr < aryRet.Length; itr++)
				{
					double.TryParse(args[itr].ToString(), out double itm);
					aryRet[itr] = scl * itm;
				}
				return aryRet;
			}
			public static object[] Matrix_Mul(int numColsA, int numRowsA, object[] argsA, int numColsB, int numRowsB, object[] argsB)
			{
				int tmpCols = numColsA;
				int tmpRows = numRowsB;
				object[] aryRet = ArrayList.Repeat(0, tmpCols * tmpRows).ToArray();
				for (int itr = 0; itr < aryRet.Length; itr++)
				{
					int tmpCol = itr % tmpCols;
					int tmpRow = (int)Math.Floor((double)itr / tmpCols);
					object[] vecCol = Matrix_Column(numColsA, numRowsA, tmpCol, argsA);
					object[] vecRow = Matrix_Row(numColsB, numRowsB, tmpRow, argsB);
					double.TryParse(Vec_Dot(vecCol, vecRow).ToString(), out double dDot);
					aryRet[itr] = dDot;
				}
				return aryRet;
			}
			public static object[] Matrix_Rot2x2(double ang)
			{
				double cos = Math.Cos(ang);
				double sin = Math.Sin(ang);
				return new object[] { cos, sin, -sin, cos };
			}
			public static void Matrix_Rot2x2(int argCols, int argRows, double ang, ref object[] args)
			{
				double cos = Math.Cos(ang);
				double sin = Math.Sin(ang);
				Matrix_Elem(argCols, argRows, 0, 0, ref args, cos);
				Matrix_Elem(argCols, argRows, 1, 0, ref args, sin);
				Matrix_Elem(argCols, argRows, 0, 1, ref args, -sin);
				Matrix_Elem(argCols, argRows, 1, 1, ref args, cos);
				Matrix_Elem(argCols, argRows, 2, 2, ref args, 1);
			}
			public static object[] Matrix_Rot3x3(double ang, object[] vecAxis)
			{
				double cos = Math.Cos(ang);
				double sin = Math.Sin(ang);
				double.TryParse(Vec_Elem(vecAxis, 0).ToString(), out double x);
				double.TryParse(Vec_Elem(vecAxis, 1).ToString(), out double y);
				double.TryParse(Vec_Elem(vecAxis, 2).ToString(), out double z);
				return new object[] { x*x*(1-cos) + cos,   x*y*(1-cos) - z*sin, x*z*(1-cos) + y*sin,
								  y*x*(1-cos) + z*sin, y*y*(1-cos) + cos,   y*z*(1-cos) - x*sin,
								  z*x*(1-cos) - y*sin, z*y*(1-cos) + x*sin, z*z*(1-cos) + cos   };
			}
			public static void Matrix_Rot3x3(int argCols, int argRows, double ang, object[] vecAxis, ref object[] args)
			{
				double cos = Math.Cos(ang);
				double sin = Math.Sin(ang);
				double.TryParse(Vec_Elem(vecAxis, 0).ToString(), out double x);
				double.TryParse(Vec_Elem(vecAxis, 1).ToString(), out double y);
				double.TryParse(Vec_Elem(vecAxis, 2).ToString(), out double z);
				Matrix_Elem(argCols, argRows, 0, 0, ref args, x * x * (1 - cos) + cos);
				Matrix_Elem(argCols, argRows, 1, 0, ref args, x * y * (1 - cos) - z * sin);
				Matrix_Elem(argCols, argRows, 2, 0, ref args, x * z * (1 - cos) + y * sin);
				Matrix_Elem(argCols, argRows, 0, 1, ref args, y * x * (1 - cos) + z * sin);
				Matrix_Elem(argCols, argRows, 1, 1, ref args, y * y * (1 - cos) + cos);
				Matrix_Elem(argCols, argRows, 2, 1, ref args, y * z * (1 - cos) - x * sin);
				Matrix_Elem(argCols, argRows, 0, 2, ref args, z * x * (1 - cos) - y * sin);
				Matrix_Elem(argCols, argRows, 1, 2, ref args, z * y * (1 - cos) + x * sin);
				Matrix_Elem(argCols, argRows, 2, 2, ref args, z * z * (1 - cos) + cos);
				Matrix_Elem(argCols, argRows, 3, 3, ref args, 1);
			}
			public static void Matrix_Reflect2x2(object[] vecCeoficients, int argCols, int argRows, ref object[] args)
			{
				double.TryParse(Vec_Elem(vecCeoficients, 0).ToString(), out double a);
				double.TryParse(Vec_Elem(vecCeoficients, 1).ToString(), out double b);
				double.TryParse(Vec_Elem(vecCeoficients, 2).ToString(), out double c);
				Matrix_Elem(argCols, argRows, 0, 0, ref args, 1 - 2 * a * a);
				Matrix_Elem(argCols, argRows, 1, 0, ref args, -2 * a * b);
				Matrix_Elem(argCols, argRows, 2, 0, ref args, -2 * a * c);
				Matrix_Elem(argCols, argRows, 0, 1, ref args, -2 * a * b);
				Matrix_Elem(argCols, argRows, 1, 1, ref args, 1 - 2 * b * b);
				Matrix_Elem(argCols, argRows, 2, 1, ref args, -2 * b * c);
				Matrix_Elem(argCols, argRows, 0, 2, ref args, -2 * a * c);
				Matrix_Elem(argCols, argRows, 1, 2, ref args, -2 * b * c);
				Matrix_Elem(argCols, argRows, 2, 2, ref args, 1 - 2 * c * c);
			}
			public static void Matrix_Reflect3x3(object[] vecCeoficients, int argCols, int argRows, ref object[] args)
			{
				double.TryParse(Vec_Elem(vecCeoficients, 0).ToString(), out double a);
				double.TryParse(Vec_Elem(vecCeoficients, 1).ToString(), out double b);
				double.TryParse(Vec_Elem(vecCeoficients, 2).ToString(), out double c);
				double.TryParse(Vec_Elem(vecCeoficients, 3).ToString(), out double d);
				Matrix_Elem(argCols, argRows, 0, 0, ref args, 1 - 2 * a * a);
				Matrix_Elem(argCols, argRows, 1, 0, ref args, -2 * a * b);
				Matrix_Elem(argCols, argRows, 2, 0, ref args, -2 * a * c);
				Matrix_Elem(argCols, argRows, 3, 0, ref args, -2 * a * d);
				Matrix_Elem(argCols, argRows, 0, 1, ref args, -2 * a * b);
				Matrix_Elem(argCols, argRows, 1, 1, ref args, 1 - 2 * b * b);
				Matrix_Elem(argCols, argRows, 2, 1, ref args, -2 * b * c);
				Matrix_Elem(argCols, argRows, 3, 1, ref args, -2 * b * d);
				Matrix_Elem(argCols, argRows, 0, 2, ref args, -2 * a * c);
				Matrix_Elem(argCols, argRows, 1, 2, ref args, -2 * b * c);
				Matrix_Elem(argCols, argRows, 2, 2, ref args, 1 - 2 * c * c);
				Matrix_Elem(argCols, argRows, 3, 2, ref args, -2 * c * d);
				Matrix_Elem(argCols, argRows, 0, 3, ref args, 0);
				Matrix_Elem(argCols, argRows, 1, 3, ref args, 0);
				Matrix_Elem(argCols, argRows, 2, 3, ref args, 0);
				Matrix_Elem(argCols, argRows, 3, 3, ref args, 1);
			}
			public static object[] Matrix_Perspective(double NearDist, double FarDist, double HorizontalAngle, double VerticalAngle)
			{
				double[] fov = new double[] { 1.0 / Math.Tan(0.5 * HorizontalAngle), 1.0 / Math.Tan(0.5 * VerticalAngle) };
				double Nf = -FarDist / (FarDist - NearDist);
				return Matrix(4, 4, fov[0], 0, 0, 0, 0, fov[1], 0, 0, 0, 0, Nf, -1, 0, 0, Nf * NearDist, 0);
			}
			public static object[] Quat(double w, double x, double y, double z)
			{
				return Vec(w, x, y, z);
			}
			public static object[] Quat_Sum(object[] q1, object[] q2)
			{
				return Vec_Sum(q1, q2);
			}
			public static object[] Quat_Mul(object val, object[] q)
			{
				return Vec_Mul(val, q);
			}
			public static object[] Quat_Mul(object[] q1, object[] q2)
			{
				double[] a = new double[2], b = new double[2], c = new double[2], d = new double[2];
				double.TryParse(Vec_Elem(q1, 0).ToString(), out a[0]);
				double.TryParse(Vec_Elem(q2, 0).ToString(), out a[1]);
				double.TryParse(Vec_Elem(q1, 1).ToString(), out b[0]);
				double.TryParse(Vec_Elem(q2, 1).ToString(), out b[1]);
				double.TryParse(Vec_Elem(q1, 2).ToString(), out c[0]);
				double.TryParse(Vec_Elem(q2, 2).ToString(), out c[1]);
				double.TryParse(Vec_Elem(q1, 3).ToString(), out d[0]);
				double.TryParse(Vec_Elem(q2, 3).ToString(), out d[1]);
				return Vec(a[0] * a[1] - b[0] * b[1] - c[0] * c[1] - d[0] * d[1],
						   a[0] * b[1] + b[0] * a[1] + c[0] * d[1] - d[0] * c[1],
						   a[0] * c[1] - b[0] * d[1] + c[0] * a[1] + d[0] * b[1],
						   a[0] * d[1] + b[0] * c[1] - c[0] * b[1] + d[0] * a[1]);
			}
			public static object Quat_Len(object[] q)
			{
				return Vec_Len(q);
			}
			public static object[] Quat_Norm(object[] q)
			{
				return Vec_Norm(q);
			}
			public static object[] Quat_Conjugate(object[] q)
			{
				double.TryParse(Vec_Elem(q, 0).ToString(), out double w);
				double.TryParse(Vec_Elem(q, 1).ToString(), out double x);
				double.TryParse(Vec_Elem(q, 2).ToString(), out double y);
				double.TryParse(Vec_Elem(q, 3).ToString(), out double z);
				return Vec(w, -x, -y, -z);
			}
			public static object[] Quat_Inverse(object[] q)
			{
				return Vec_Mul(1 / (double)Vec_Dot(q, q), Quat_Conjugate(q));
			}
			public static object Quat_ToAxisAngle(object[] q)
			{
				object[] qv = Vec(Vec_Elem(q, 1), Vec_Elem(q, 2), Vec_Elem(q, 3));
				double qd = (double)Vec_Len(qv);
				object[] axis = Vec_Mul(1 / qd, qv);
				double angle = 2 * Math.Atan2(qd, (double)Vec_Elem(q, 0));
				return new { Angle = angle, Axis = axis };
			}
		}
		public struct clsEventScriptContextConsole
		{
			private controlConsole Console;
			public clsEventScriptContextConsole(controlConsole console)
			{
				Console = console;
			}
			public void Write(object obj, string Label)
			{
				if(Console != null)
				{
					Console.Write(obj, Label);
				}
			}
			public void Write(string message, string Label)
			{
				if (Console != null)
				{
					Console.Write(message, Label);
				}
			}
			public void Write(object obj, string prompt, string scope)
			{
				if (Console != null)
				{
					Console.Write(obj, prompt, scope, false);
				}
			}
			public void Write(string message, string prompt, string scope)
			{
				if (Console != null)
				{
					Console.Write(message, prompt, scope, false);
				}
			}
		}
		public class clsEventScriptContext
		{
			[Browsable(false)]
			public delegate void delegateArgumentsUpdated(Dictionary<string, object> args);
			[Browsable(false)]
			public event delegateArgumentsUpdated ArgumentsUpdated;
			public clsRender RenderSubject { get; set; }
			public clsUniformSetCollection Uniforms => new clsUniformSetCollection(RenderSubject.Uniforms);
			public Dictionary<string, object> Arguments { get; private set; } = new Dictionary<string, object>();
			private controlConsole refConsole
			{
				get
				{
					if (RenderSubject == null) return null;
					frmRenderConfigure frm = frmRenderConfigure.FormWithSubjectObject(RenderSubject);
					if (frm == null) return null;
					return frm.Console;
				}
			}
			public clsEventScriptContextConsole Console { get { return new clsEventScriptContextConsole(refConsole); } }
			public object[][] Uniform_Get(string name)
			{
				object[][] objRet = new object[][] { new object[] { } };
				int idx = RenderSubject.Uniforms.FindIndex(itm => itm.Key == name);
				if (idx >= 0)
				{
					if (RenderSubject.Uniforms[idx].Value != null) 
					{ 
						objRet = RenderSubject.Uniforms[idx].Value.GetData();
					}
				}
				return objRet;
			}
			public object[] Uniform_Get(string name, int index)
			{
				object[] objRet = new object[] { };
				int idx = RenderSubject.Uniforms.FindIndex(itm => itm.Key == name);
				if (idx >= 0)
				{
					if (RenderSubject.Uniforms[idx].Value != null) 
					{ 
						if (index >= 0 && index < RenderSubject.Uniforms[idx].Value.ElementCount)
						{
							objRet = RenderSubject.Uniforms[idx].Value.GetData(index);
						}
					}
				}
				return objRet;
			}
			public object Uniform_Get(string name, int index, int comp)
			{
				object objRet = null;
				int idx = RenderSubject.Uniforms.FindIndex(itm => itm.Key == name);
				if (idx >= 0)
				{
					if(RenderSubject.Uniforms[idx].Value != null) 
					{ 
						if (index >= 0 && index < RenderSubject.Uniforms[idx].Value.ElementCount)
						{
							if(comp >= 0 && comp < RenderSubject.Uniforms[idx].Value.ComponentPerElement)
							{
								objRet = RenderSubject.Uniforms[idx].Value.GetData(index,comp);
							}
						}
					}
				}
				return objRet;
			}
			public void Uniform_Set(string name, params object[][] args)
			{
				int idx = RenderSubject.Uniforms.FindIndex(itm => itm.Key == name);
				if(idx >= 0)
				{
					if(RenderSubject.Uniforms[idx].Value != null) 
					{ 
						RenderSubject.Uniforms[idx].Value.SetData(args);
					}
				}
			}
			public void Uniform_Set(string name, int index, params object[] args)
			{
				int idx = RenderSubject.Uniforms.FindIndex(itm => itm.Key == name);
				if (idx >= 0)
				{
					if(RenderSubject.Uniforms[idx].Value != null)
					{
						if (index >= 0 && index < RenderSubject.Uniforms[idx].Value.ElementCount)
						{
							RenderSubject.Uniforms[idx].Value.SetData(index, args);
						}
					}
				}
			}
			public void Uniform_Set(string name, int index, int comp, object arg)
			{
				int idx = RenderSubject.Uniforms.FindIndex(itm => itm.Key == name);
				if (idx >= 0)
				{
					if (RenderSubject.Uniforms[idx].Value != null)
					{
						if (index >= 0 && index < RenderSubject.Uniforms[idx].Value.ElementCount)
						{
							if (comp >= 0 && comp < RenderSubject.Uniforms[idx].Value.ComponentPerElement)
							{
								RenderSubject.Uniforms[idx].Value.SetData(index,comp,arg);
							}
						}
					}
				}
			}
			[Browsable(false)]
			public void SetArguments(EventType typeEvent, params object[] args)
			{
				Arguments.Clear();
				ParameterInfo[] EventParams = EventType_Parameters(typeEvent);
				for (int paramI = 0; paramI < Math.Min(args.Length, EventParams.Length); paramI++)
				{
					Arguments.Add(EventParams[paramI].Name, (dynamic)args[paramI]);
				}
				ArgumentsUpdated?.Invoke(Arguments);
			}
			[Browsable(false)]
			public void InitArguments(EventType typeEvent)
			{
				if (RenderSubject == null) return;
				frmRenderConfigure RenderSubjectConfigure = frmRenderConfigure.FormWithSubjectObject(RenderSubject);
				if (RenderSubjectConfigure == null) return;
				frmRender RenderSubjectForm = RenderSubjectConfigure.RenderSubjectForm;
				if (RenderSubjectForm == null) return;
				object[] args = Array.Empty<object>();
				switch(typeEvent)
				{
					case EventType.OnLoad:
						args = Array.Empty<object>();
						break;
					case EventType.OnRender:
						args = new object[] {RenderSubjectForm.FrameCount, RenderSubjectForm.DeltaTimeStamp, RenderSubjectForm.CurrentTimeStamp};
						break;
					case EventType.OnResize:
						args = new object[] {RenderSubjectForm.glRender.Width, RenderSubjectForm.glRender.Height};
						break;
					case EventType.OnPointerStart:
						args = new object[] { new InputEventArgs() { InputInterface = RenderSubjectForm.glRender.InterfaceTouch, InputTouch = RenderSubjectForm.glRender.MouseTouch, InputPoint = RenderSubjectForm.glRender.MouseTouch.CurrentTouchPoint } };
						break;
					case EventType.OnPointerMove:
						args = new object[] { new InputEventArgs() { InputInterface = RenderSubjectForm.glRender.InterfaceTouch, InputTouch = RenderSubjectForm.glRender.MouseTouch, InputPoint = RenderSubjectForm.glRender.MouseTouch.CurrentTouchPoint } };
						break;
					case EventType.OnPointerEnd:
						args = new object[] { new InputEventArgs() { InputInterface = RenderSubjectForm.glRender.InterfaceTouch, InputTouch = RenderSubjectForm.glRender.MouseTouch, InputPoint = RenderSubjectForm.glRender.MouseTouch.CurrentTouchPoint } };
						break;
				}
				Arguments.Clear();
				ParameterInfo[] EventParams = EventType_Parameters(typeEvent);
				for (int paramI = 0; paramI < Math.Min(args.Length, EventParams.Length); paramI++)
				{
					Arguments.Add(EventParams[paramI].Name, (dynamic)args[paramI]);
				}
				ArgumentsUpdated?.Invoke(Arguments);
			}
			[Browsable(false)]
			public void ClearArguments()
			{
				Arguments.Clear();
			}
			[Browsable(false)]
			public void AttachUpdateArguments(EventType eventType)
			{
				if (RenderSubject == null) return;
				switch (eventType)
				{
					case EventType.OnLoad:
						RenderSubject.Load += SetArguments;
						break;
					case EventType.OnRender:
						RenderSubject.Render += SetArguments;
						break;
					case EventType.OnResize:
						RenderSubject.Resize += SetArguments;
						break;
					case EventType.OnPointerStart:
						RenderSubject.PointerStart += SetArguments;
						break;
					case EventType.OnPointerMove:
						RenderSubject.PointerMove += SetArguments;
						break;
					case EventType.OnPointerEnd:
						RenderSubject.PointerEnd += SetArguments;
						break;
				}
			}
			[Browsable(false)]
			public void DetachUpdateArguments(EventType eventType)
			{
				if (RenderSubject == null) return;
				switch (eventType)
				{
					case EventType.OnLoad:
						RenderSubject.Load -= SetArguments;
						break;
					case EventType.OnRender:
						RenderSubject.Render -= SetArguments;
						break;
					case EventType.OnResize:
						RenderSubject.Resize -= SetArguments;
						break;
					case EventType.OnPointerStart:
						RenderSubject.PointerStart -= SetArguments;
						break;
					case EventType.OnPointerMove:
						RenderSubject.PointerMove -= SetArguments;
						break;
					case EventType.OnPointerEnd:
						RenderSubject.PointerEnd -= SetArguments;
						break;
				}
			}
		}
		public clsEventScriptContext ScriptContext = new clsEventScriptContext();
		private EventType typEvent;
		public EventType Type 
		{ 
			get => typEvent;
			set 
			{ 
				clsRender subj = renderSubject; 
				DetachSubject(); 
				typEvent = value; 
				AttachSubject(subj); 
			} 
		}
		private clsRender renderSubject;
		public clsRender Subject 
		{ 
			get => renderSubject;
			set 
			{ 
				DetachSubject(); 
				renderSubject = value;
				ScriptContext.RenderSubject = value;
				AttachSubject(renderSubject); 
			} 
		}
		[Browsable(false)]
		public frmRenderConfigure ConfigureForm
		{
			get
			{
				if (renderSubject == null) return null;
				return frmRenderConfigure.FormWithSubjectObject(renderSubject);
			}
			set
			{
				frmRenderConfigure frm = value as frmRenderConfigure;
				if (frm == null) return;
				Subject = frm.RenderSubject;
			}
		}
		public string Source { get; set; }
		[Browsable(false)]
		public void AttachSubject(clsRender subject)
		{
			renderSubject = subject;
			if (renderSubject == null) return;
			switch(typEvent)
			{
				case EventType.OnLoad:
					renderSubject.Load += Run;
					break;
				case EventType.OnRender:
					renderSubject.Render += Run;
					break;
				case EventType.OnResize:
					renderSubject.Resize += Run;
					break;
				case EventType.OnPointerStart:
					renderSubject.PointerStart += Run;
					break;
				case EventType.OnPointerMove:
					renderSubject.PointerMove += Run;
					break;
				case EventType.OnPointerEnd:
					renderSubject.PointerEnd += Run;
					break;
			}
		}
		[Browsable(false)]
		public void DetachSubject()
		{
			if (renderSubject == null) return;
			switch (typEvent)
			{
				case EventType.OnLoad:
					renderSubject.Load -= Run;
					break;
				case EventType.OnRender:
					renderSubject.Render -= Run;
					break;
				case EventType.OnResize:
					renderSubject.Resize -= Run;
					break;
				case EventType.OnPointerStart:
					renderSubject.PointerStart -= Run;
					break;
				case EventType.OnPointerMove:
					renderSubject.PointerMove -= Run;
					break;
				case EventType.OnPointerEnd:
					renderSubject.PointerEnd -= Run;
					break;
			}
			renderSubject = null;
		}
		private ScriptRunner<object> script = null;
		public void Compile()
		{
			script = null;
			GC.Collect();
			if (string.IsNullOrEmpty(Source)) return;
			string str = PreProcess(Source, ScriptContext);
			Script<object> scriptCompile = CSharpScript.Create(str, GenericScriptOptions, typeof(clsEventScriptContext));
			scriptCompile.Compile();
			try
			{
				script = scriptCompile.CreateDelegate();
			}
			catch(CompilationErrorException err)
			{
				string strErr = err.Diagnostics.Aggregate("", (strRet, errItm) => strRet += errItm + "\r\n");
				ScriptContext.Console.Write(strErr, "Compilation Error> ", Type.ToString());
				script = null;
			}
			scriptCompile = null;
			GC.Collect();
		}
		[Browsable(false)]
		public void Run(EventType eventType, params object[] args)
		{
			if (script == null) return;
			ScriptContext.SetArguments(eventType, args);
			Task<object> scriptExe = null;
			try
			{
				scriptExe = script.Invoke(ScriptContext);
			}
			catch(Exception err)
			{
				ScriptContext.Console.Write(ExceptionFullString(err), "Error> ", Source);
			}
			if(scriptExe != null)
			{
				if(scriptExe.Result != null)
				{
					ScriptContext.Console.Write(scriptExe.Result, "Log> ", Source);
				}
				scriptExe.Dispose();
			}
			if (ConfigureForm != null)
			{
				ConfigureForm.UpdateDataGrid();
			}
			ScriptContext.ClearArguments();
		}
		public void Dispose()
		{
			DetachSubject();
			Source = null;
			script = null;
			ScriptContext = null;
			GC.Collect();
		}
	}
	
	public class clsShader : clsProjectObject
	{
		private ShaderType typeShader;
		public ShaderType Type 
		{
			get => typeShader;
			set
			{
				if (IsValid) GL.DeleteShader(glID);
				glID = GL.CreateShader(value);
				typeShader = value;
				Compile();
			} 
		}
		public string Source { set; get; }
		public string[] Lines { get { return Regex.Split(string.IsNullOrEmpty(Source)?"":Source,"\n"); } }
		public int glID { private set; get; } = -1;
		public bool IsValid { get => (glID >= 0 && GL.IsShader(glID)); }
		public clsInfoString CompileInfo { get => (IsValid) ? (new clsInfoString(GL.GetShaderInfoLog(glID))) : (new clsInfoString()); }
		public clsShader(ShaderType typ) : base(ProjectObjectTypes.Shader)
		{
			Type = typ;
			UpdateGLID();
			AddToCollection();
		}
		public void UpdateGLID(bool bolForceNewID = false)
		{
			glContext_Main.MakeCurrent(infoWindow);
			if (IsValid)
			{
				if (!bolForceNewID) return;
				GL.DeleteShader(glID);
				glID = -1;
			}
			glID = GL.CreateShader(typeShader);
		}
		public string[] Inputs { get => parseRefLinks(Source, "in|attribute"); }
		public string[] Outputs { get => parseRefLinks(Source, "out|varying"); }
		public string[] Uniforms { get => parseRefLinks(Source, "uniform"); }
		private static string[] parseRefLinks(string str, string prefix)
		{
			List<string> ret = new List<string>();
			string strTmp = Regex.Replace(str+" ", @"\{(\s|.)+\}", "{...};");
			strTmp = Regex.Replace(strTmp, @"\((\s|.)+\)", "(...)");
			foreach (Match match in (Regex.Matches(strTmp, $@"({prefix})\s+(?<typ>\w+\d{{0,}})\s+(?<name>(\w|\d|_)+)")))
			{
				ret.Add(match.Groups["name"].Value);
			}
			return ret.ToArray();
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
			UpdateGLID();
			GL.ShaderSource(glID, Source+"\n");
			GL.CompileShader(glID);
			foreach(clsProjectObject projObj in projectMain.ProjectObjects.FindAll(itm => itm.ProjectObjType == ProjectObjectTypes.Program))
			{
				clsProgram progObj = projObj as clsProgram;
				if(progObj.Shaders.Contains(this))
				{
					progObj.Link();
				}
			}
		}
		public void Delete()
		{
			if(IsValid) GL.DeleteShader(glID);
			glID = -1;
		}
		protected override void Dispose(bool bolDisposing)
		{
			Delete();
			Source = null;
			typeShader = 0;
			base.Dispose(bolDisposing);
		}
		public new void Dispose()
		{
			Dispose(true);
		}
		public override string ToFullString(string name)
		{
			return Regex.Replace(Type.ToString(), @"Arb\z", "") + "_" + name;
		}

		[Browsable(false)]
		public override Xml_ProjectObject Xml
		{
			get => new Xml_Shader(this);
		}
	}
	
	public class clsProgram : clsProjectObject
	{
		public List<clsShader> Shaders { private set; get; } = new List<clsShader>() { };
		public int glID { private set; get; } = -1;
		public bool IsValid { get => (glID >= 0 && GL.IsProgram(glID)); }
		public clsInfoString LinkInfo { get => (IsValid) ? (new clsInfoString(GL.GetProgramInfoLog(glID))) : (new clsInfoString()); }
		public clsProgram() : base(ProjectObjectTypes.Program)
		{
			UpdateGLID();
			AddToCollection();
		}
		public void UpdateGLID(bool bolForceNewID = false)
		{
			glContext_Main.MakeCurrent(infoWindow);
			if (IsValid)
			{
				if (!bolForceNewID) return;
				GL.DeleteProgram(glID);
				glID = -1;
			}
			glID = GL.CreateProgram();
		}
		public void Link()
		{
			UpdateGLID();
			foreach (clsShader objShader in Shaders)
			{
				GL.AttachShader(glID, objShader.glID);
			}
			GL.LinkProgram(glID);
			foreach (clsShader objShader in Shaders)
			{
				GL.DetachShader(glID, objShader.glID);
			}
		}
		public void Delete()
		{
			if(IsValid) GL.DeleteProgram(glID);
			glID = -1;
		}
		public string[] Inputs 
		{
			get
			{
				List<string> aryRet = new List<string>();
				foreach (clsShader shdItr in Shaders) { aryRet.AddRange(shdItr.Inputs); }
				return aryRet.Distinct().ToArray();
			}
		}
		public string[] Outputs
		{
			get
			{
				List<string> aryRet = new List<string>();
				foreach (clsShader shdItr in Shaders) { aryRet.AddRange(shdItr.Outputs); }
				return aryRet.Distinct().ToArray();
			}
		}
		public string[] Uniforms
		{
			get
			{
				List<string> aryRet = new List<string>();
				foreach (clsShader shdItr in Shaders) { aryRet.AddRange(shdItr.Uniforms); }
				return aryRet.Distinct().ToArray();
			}
		}
		protected override void Dispose(bool disposing)
		{
			Delete();
			Shaders.Clear();
			Shaders = null;
			base.Dispose(disposing);
		}
		public new void Dispose()
		{
			Dispose(true);
		}

		[Browsable(false)]
		public override Xml_ProjectObject Xml
		{
			get => new Xml_Program(this);
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
				PropertyDescriptor propNew = null;
				clsVertexCollection aryVrt = value as clsVertexCollection;
				if(aryVrt != null)
				{
					List<Attribute> attrs = new List<Attribute>(attributes);
					attrs.Add(new NotifyParentPropertyAttribute(true));
					propNew = new MemberPropertyDescriptor(typeof(int), typeof(int), "Count", "[Count]", attrs.ToArray());
					props.Add(propNew);
					string strFormat = new string('0', aryVrt.Count.ToString().Length);
					for (int vrtItr = 0; vrtItr < aryVrt.Count; vrtItr++)
					{
						props.Add(new ArrayPropertyDescriptor(typeof(clsVertex), typeof(clsVertex), vrtItr, $"v[{vrtItr.ToString(strFormat)}]", attributes));
					}
				}
				return props;
			}
			private class MemberPropertyDescriptor : SimplePropertyDescriptor
			{
				private string strName = "";
				public override bool SupportsChangeEvents => true;
				public MemberPropertyDescriptor(Type componentType, Type elementType, string Name, string strLabel, Attribute[] attr) : base(componentType, strLabel, elementType, attr)
				{
					this.strName = Name;
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
					prop.SetValue(instance, Convert.ChangeType(value, propTyp), null);
					OnValueChanged(instance, EventArgs.Empty);
				}
			}
			private class ArrayPropertyDescriptor : SimplePropertyDescriptor
			{
				private int index;
				public override bool SupportsChangeEvents => true;
				public ArrayPropertyDescriptor(Type arrayType, Type elementType, int index, string strLabel, Attribute[] attr) : base(arrayType, strLabel, elementType, attr)
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
							props.Add(new ArrayPropertyDescriptor(typeof(clsVertexProperty), typeof(clsVertexProperty), propVrt[vrtItr].ComponentName, vrtItr, attributes));
						}
					}
				}
				return props;
			}
			private class ArrayPropertyDescriptor : SimplePropertyDescriptor
			{
				private int index;
				public override bool SupportsChangeEvents => true;
				public ArrayPropertyDescriptor(Type arrayType, Type elementType, string strName, int index, Attribute[] attr) : base(arrayType, strName, elementType, attr)
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
				public override bool SupportsChangeEvents => true;
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
							object[] ary = GetList(vals, comp.ElementType);
							vrt.Components[comp.Index].Elements = ary;
							return vrt.Components[comp.Index];
						}
					}
				}
				return base.ConvertFrom(context, culture, value);
			}
			private object[] GetList(string[] vals, Type Typ)
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
		public class TriagnelCollectionConverter : ArrayConverter
		{
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection props = new PropertyDescriptorCollection(new PropertyDescriptor[] { });
				PropertyDescriptor propNew = null;
				clsTriangleCollection aryTri = value as clsTriangleCollection;
				if (aryTri != null)
				{
					List<Attribute> attrs = new List<Attribute>(attributes);
					attrs.Add(new NotifyParentPropertyAttribute(true));
					propNew = new MemberPropertyDescriptor(typeof(int), typeof(int), "Count", "[Count]", attrs.ToArray());
					props.Add(propNew);
					string strFormat = new string('0', aryTri.Count.ToString().Length);
					for (int triItr = 0; triItr < aryTri.Count; triItr++)
					{
						props.Add(new ArrayPropertyDescriptor(typeof(clsTriangle), typeof(clsTriangle), triItr, $"t[{triItr.ToString(strFormat)}]", attributes));
					}
				}
				return props;
			}
			private class MemberPropertyDescriptor : SimplePropertyDescriptor
			{
				private string strName = "";
				public override bool SupportsChangeEvents => true;
				public MemberPropertyDescriptor(Type componentType, Type elementType, string Name, string strLabel, Attribute[] attr) : base(componentType, strLabel, elementType, attr)
				{
					this.strName = Name;
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
					prop.SetValue(instance, Convert.ChangeType(value, propTyp), null);
					OnValueChanged(instance, EventArgs.Empty);
				}
			}
			private class ArrayPropertyDescriptor : SimplePropertyDescriptor
			{
				private int index;
				public override bool SupportsChangeEvents => true;
				public ArrayPropertyDescriptor(Type arrayType, Type elementType, int index, string strLabel, Attribute[] attr) : base(arrayType, strLabel, elementType, attr)
				{
					this.index = index;
				}
				public override object GetValue(object instance)
				{
					clsTriangleCollection ary = instance as clsTriangleCollection;
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
					clsTriangleCollection ary = instance as clsTriangleCollection;
					if (ary != null)
					{
						if (ary.Count > index)
						{
							ary[index] = (clsTriangle)value;
							OnValueChanged(instance, EventArgs.Empty);
						}
					}
				}
			}
		}
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class clsVertexDescriptionComponent : IDisposable
		{
			public delegate void delegateBufferData(object[] ary, int ElementSize, BufferTarget BufferType, BufferUsageHint BufferUsage);
			public delegate void delegateBufferSubData(object[] ary, int ElementSize, BufferTarget BufferType);
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
			public static Dictionary<VertexAttribPointerType, VertexPointerType> VertexPointerTypes = new Dictionary<VertexAttribPointerType, VertexPointerType>()
			{
				{VertexAttribPointerType.Byte, VertexPointerType.Short},
				{VertexAttribPointerType.Short, VertexPointerType.Short},
				{VertexAttribPointerType.UnsignedShort, VertexPointerType.Short},
				{VertexAttribPointerType.Int, VertexPointerType.Int},
				{VertexAttribPointerType.UnsignedInt, VertexPointerType.Int},
				{VertexAttribPointerType.HalfFloat, VertexPointerType.HalfFloat},
				{VertexAttribPointerType.Fixed, VertexPointerType.Float},
				{VertexAttribPointerType.Float, VertexPointerType.Float},
				{VertexAttribPointerType.Double, VertexPointerType.Double}
			};
			public static Dictionary<VertexAttribPointerType, delegateBufferData> VertexSetDataDelegate = new Dictionary<VertexAttribPointerType, delegateBufferData>()
			{
				{VertexAttribPointerType.Byte, (ary, size, buff, usage) => {GL.BufferData<sbyte>(buff, ary.Length * size, ObjectArrayAsType<sbyte>(ary), usage); } },
				{VertexAttribPointerType.UnsignedByte, (ary, size, buff, usage) => {GL.BufferData<byte>(buff, ary.Length * size, ObjectArrayAsType<byte>(ary), usage); }},
				{VertexAttribPointerType.Short, (ary, size, buff, usage) => {GL.BufferData<short>(buff, ary.Length * size, ObjectArrayAsType<short>(ary), usage); }},
				{VertexAttribPointerType.UnsignedShort, (ary, size, buff, usage) => {GL.BufferData<ushort>(buff, ary.Length * size, ObjectArrayAsType<ushort>(ary), usage); }},
				{VertexAttribPointerType.Int, (ary, size, buff, usage) => {GL.BufferData<int>(buff, ary.Length * size, ObjectArrayAsType<int>(ary), usage); }},
				{VertexAttribPointerType.UnsignedInt, (ary, size, buff, usage) => {GL.BufferData<uint>(buff, ary.Length * size, ObjectArrayAsType<uint>(ary), usage); }},
				{VertexAttribPointerType.HalfFloat, (ary, size, buff, usage) => {GL.BufferData<Half>(buff, ary.Length * size, ObjectArrayAsType<Half>(ary), usage); }},
				{VertexAttribPointerType.Fixed, (ary, size, buff, usage) => {GL.BufferData<Single>(buff, ary.Length * size, ObjectArrayAsType<Single>(ary), usage); }},
				{VertexAttribPointerType.Float, (ary, size, buff, usage) => {GL.BufferData<float>(buff, ary.Length * size, ObjectArrayAsType<float>(ary), usage); }},
				{VertexAttribPointerType.Double, (ary, size, buff, usage) => {GL.BufferData<double>(buff, ary.Length * size, ObjectArrayAsType<double>(ary), usage); }}
			};
			public static Dictionary<VertexAttribPointerType, delegateBufferSubData> VertexSetSubDataDelegate = new Dictionary<VertexAttribPointerType, delegateBufferSubData>()
			{
				{VertexAttribPointerType.Byte, (ary, size, buff) => {GL.BufferSubData<sbyte>(buff, (IntPtr)0, (IntPtr)(ary.Length * size), ObjectArrayAsType<sbyte>(ary)); } },
				{VertexAttribPointerType.UnsignedByte, (ary, size, buff) => {GL.BufferSubData<byte>(buff, (IntPtr)0, (IntPtr)(ary.Length * size), ObjectArrayAsType<byte>(ary)); }},
				{VertexAttribPointerType.Short, (ary, size, buff) => {GL.BufferSubData<short>(buff, (IntPtr)0, (IntPtr)(ary.Length * size), ObjectArrayAsType<short>(ary)); }},
				{VertexAttribPointerType.UnsignedShort, (ary, size, buff) => {GL.BufferSubData<ushort>(buff, (IntPtr)0, (IntPtr)(ary.Length * size), ObjectArrayAsType<ushort>(ary)); }},
				{VertexAttribPointerType.Int, (ary, size, buff) => {GL.BufferSubData<int>(buff, (IntPtr)0, (IntPtr)(ary.Length * size), ObjectArrayAsType<int>(ary)); }},
				{VertexAttribPointerType.UnsignedInt, (ary, size, buff) => {GL.BufferSubData<uint>(buff, (IntPtr)0, (IntPtr)(ary.Length * size), ObjectArrayAsType<uint>(ary)); }},
				{VertexAttribPointerType.HalfFloat, (ary, size, buff) => {GL.BufferSubData<Half>(buff, (IntPtr)0, (IntPtr)(ary.Length * size), ObjectArrayAsType<Half>(ary)); }},
				{VertexAttribPointerType.Fixed, (ary, size, buff) => {GL.BufferSubData<Single>(buff, (IntPtr)0, (IntPtr)(ary.Length * size), ObjectArrayAsType<Single>(ary)); }},
				{VertexAttribPointerType.Float, (ary, size, buff) => {GL.BufferSubData<float>(buff, (IntPtr)0, (IntPtr)(ary.Length * size), ObjectArrayAsType<float>(ary)); }},
				{VertexAttribPointerType.Double, (ary, size, buff) => {GL.BufferSubData<double>(buff, (IntPtr)0, (IntPtr)(ary.Length * size), ObjectArrayAsType<double>(ary)); }}
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
			public object InitialElementValue { set; get; } = 0;
			private VertexAttribPointerType glElementType;
			public VertexAttribPointerType ElementGLType { get => glElementType;
				set { glElementType = value; InitialElementValue = Convert.ChangeType(InitialElementValue, ElementType);  Desc.RaiseUpdated(); } }
			public Type ElementType { get { return VertexTypes[ElementGLType]; } }
			public int ElementSize { get { return Marshal.SizeOf(VertexTypes[ElementGLType]); } }
			private int intElementCount;
			public int ElementCount { set { intElementCount = value; Desc.RaiseUpdated(); } get => intElementCount; }
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
			public override string ToString()
			{
				return $"{Name} <{ElementGLType}{ElementCount}>";
			}
			private bool disposedValue = false;
			protected virtual void Dispose(bool disposing)
			{
				if (!disposedValue)
				{
					if (disposing)
					{
						
					}
					Desc = null;
					disposedValue = true;
				}
			}
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
		}
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class clsVertexDescription : clsProjectObject, IList<clsVertexDescriptionComponent>
		{
			internal List<clsVertexDescriptionComponent> aryComponents = new List<clsVertexDescriptionComponent>();
			public delegate void delegateUpdated();
			public event delegateUpdated Updated;
			public clsVertexDescription() : base(ProjectObjectTypes.VertexDescription)
			{
				AddToCollection();
			}
			internal void RaiseUpdated()
			{
				Updated?.Invoke();
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
			public new void Dispose()
			{
				foreach (clsVertexDescriptionComponent itm in aryComponents) itm.Dispose();
				aryComponents.Clear();
				base.Dispose();
			}

			[Browsable(false)]
			public override Xml_ProjectObject Xml
			{
				get => new Xml_VertexDescription(this);
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
			internal clsGeometry Geometry { private set; get; }
			public clsVertexCollection(clsGeometry refGeometry)
			{
				Geometry = refGeometry;
				Desc = refGeometry.VertexDescription;
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
					if(value != null)
					{
						value.Updated += desc_Updated;
					}
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
				if (refDesc == null) return;
				KeyValuePair<clsVertexDescriptionComponent, List<object>>[] aryComp = Data.ToArray();
				for(int itr = 0; itr < aryComp.Length; itr++)
				{
					if (!refDesc.Contains(aryComp[itr].Key)) Data.Remove(aryComp[itr].Key);
				}
				for(int itr = 0; itr < refDesc.Count; itr++)
				{
					clsVertexDescriptionComponent compI = refDesc[itr];
					if (!Data.ContainsKey(compI))
					{
						object[] aryElements = ArrayList.Repeat(Convert.ChangeType(compI.InitialElementValue, compI.ElementType), compI.ElementCount*intCount).ToArray();
						Data.Add(compI, new List<object>((IEnumerable<object>)aryElements));
					} else
					{
						object[] ary = Data[compI].ToArray();
						int strideOld = ary.Length/Math.Max(intCount, 1);
						int strideNew = compI.ElementCount;
						Data[compI].Clear();
						Type typNew = compI.ElementType;
						for(int iItm = 0; iItm < intCount; iItm++) {
							for(int iElem = 0; iElem < strideNew; iElem++) {
								Data[compI].Add( ((iElem < strideOld)?(ConvertToType(ary[iItm * strideOld + iElem], typNew)) : (compI.InitialElementValue)) );
							}
						}
					}
				}
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
			
			private bool disposedValue = false;
			protected virtual void Dispose(bool disposing)
			{
				if (!disposedValue)
				{
					if (disposing)
					{
						refDesc.Updated -= desc_Updated;
						Count = 0;
						Data.Clear();
						Data = null;
						refDesc = null;
					}
					disposedValue = true;
				}
			}
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
		}
		[TypeConverter(typeof(TriagnelCollectionConverter))]
		public class clsTriangleCollection : IDisposable, IList<clsTriangle>
		{
			internal List<uint> aryIndices = new List<uint>();
			internal List<clsTriangle> aryTriangles = new List<clsTriangle>();
			internal clsGeometry Geometry { private set; get; }
			public uint[] Indices { get => aryIndices.ToArray(); }
			public clsTriangleCollection(clsGeometry refGeometry)
			{
				Geometry = refGeometry;
			}
			private bool disposedValue = false;
			protected virtual void Dispose(bool disposing)
			{
				if (!disposedValue)
				{
					if (disposing)
					{
						aryIndices.Clear();
						aryIndices = null;
						aryTriangles.Clear();
						aryTriangles = null;
					}
					disposedValue = true;
				}
			}
			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}
			public int Count
			{
				set
				{
					ResizeList(ref aryTriangles, value, idx => new clsTriangle(this));
					ResizeList<uint>(ref aryIndices, value * 3, idx => 0);
				}
				get => (int)(aryIndices.Count/3);
			}

			[Browsable(false)]
			public bool IsReadOnly => false;

			public clsTriangle this[int index]
			{
				get
				{
					return aryTriangles[index];
				}
				set
				{
					aryIndices[index * 3 + 0] = value.v_Id0;
					aryIndices[index * 3 + 1] = value.v_Id1;
					aryIndices[index * 3 + 2] = value.v_Id2;
				}
			}
			[Browsable(false)]
			public clsTriangle[] Items
			{
				get
				{
					return aryTriangles.ToArray();
				}
			}

			public int IndexOf(clsTriangle item)
			{
				return aryTriangles.IndexOf(item);
			}

			public void Insert(int index, clsTriangle item)
			{
				aryIndices.Insert(index * 3, item.v_Id2);
				aryIndices.Insert(index * 3, item.v_Id1);
				aryIndices.Insert(index * 3, item.v_Id0);
				ResizeList(ref aryTriangles, aryIndices.Count/3, idx => new clsTriangle(this));
			}

			public void RemoveAt(int index)
			{
				for (int itr = 0; itr < 3; itr++) aryIndices.RemoveAt(index * 3);
				ResizeList(ref aryTriangles, aryIndices.Count / 3, idx => new clsTriangle(this));
			}

			public void Add(clsTriangle item)
			{
				aryIndices.AddRange(item.Items);
				ResizeList(ref aryTriangles, aryIndices.Count / 3, idx => new clsTriangle(this));
			}

			public void Clear()
			{
				aryIndices.Clear();
				aryTriangles.Clear();
			}

			public bool Contains(clsTriangle item)
			{
				return IndexOf(item) >= 0;
			}

			public void CopyTo(clsTriangle[] array, int arrayIndex)
			{
				for(int itr = 0; itr < array.Length; itr++)
				{
					uint[] ary = array[itr].Items;
					for (int itrN = 0; itrN < 3; itrN++)  aryIndices[arrayIndex * 3 + itr] = ary[itrN];
				}
			}

			public bool Remove(clsTriangle item)
			{
				int idx = IndexOf(item);
				if (idx < 0) return false;
				RemoveAt(idx);
				return true;
			}

			public IEnumerator<clsTriangle> GetEnumerator()
			{
				return (IEnumerator<clsTriangle>)Items.GetEnumerator();
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
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public class clsTriangle
		{
			public uint v_Id0 { get => (Data != null && Index >= 0) ? (Data[Index * 3 + 0]) : (0);
								   set { if (Data != null && Index >= 0) Data[Index * 3 + 0] = value; } }
			public uint v_Id1 { get => (Data != null && Index >= 0) ? (Data[Index * 3 + 1]) : (0);
								   set { if (Data != null && Index >= 0) Data[Index * 3 + 1] = value; } }
			public uint v_Id2 { get => (Data != null && Index >= 0) ? (Data[Index * 3 + 2]) : (0);
								   set { if (Data != null && Index >= 0) Data[Index * 3 + 2] = value; } }
			[Browsable(false)]
			public List<uint> Data { get => (Collection != null)?(Collection.aryIndices):(null); }
			[Browsable(false)]
			public int Index { get => (Collection != null) ? (Collection.IndexOf(this)) : (-1); }
			[Browsable(false)]
			public clsTriangleCollection Collection { private set; get; }
			public clsTriangle(clsTriangleCollection refCollection)
			{
				Collection = refCollection;
			}
			public uint this[int index]
			{
				get
				{
					if (index == 0) return v_Id0;
					if (index == 1) return v_Id1;
					if (index == 2) return v_Id2;
					return 0;
				}
				set
				{
					if (index == 0) v_Id0 = value;
					if (index == 1) v_Id1 = value;
					if (index == 2) v_Id2 = value;
				}
			}
			[Browsable(false)]
			public uint[] Items
			{
				get
				{
					return new uint[] { v_Id0, v_Id1, v_Id2 };
				}
				set
				{
					if (value.Length >= 1) v_Id0 = value[0];
					if (value.Length >= 2) v_Id1 = value[1];
					if (value.Length >= 3) v_Id2 = value[2];
				}
			}
			public override string ToString()
			{
				string strFormat = new string('0', Collection.Geometry.Vertices.Count.ToString().Length);
				return $"v[{v_Id0.ToString(strFormat)}], v[{v_Id1.ToString(strFormat)}], v[{v_Id2.ToString(strFormat)}]";
			}
		}
		public clsVertexDescription VertexDescription { set; get; } = null;
		public clsVertexCollection Vertices { set; get; } = null;
		public clsTriangleCollection Triangles { set; get; } = null;
		public int glIndexBuffer = -1;
		public List<int> glBuffers = new List<int>();
		public bool IsIndexBufferValid { get => glIndexBuffer >= 0 && GL.IsBuffer(glIndexBuffer); }
		public bool IsAllVertexBuffersValid 
		{
			get
			{
				foreach (int glID in glBuffers)
				{
					if (glID < 0) return false;
					if (!GL.IsBuffer(glID)) return false;
				}
				return true;
			}
		}
		public bool IsValid { get => IsIndexBufferValid && IsAllVertexBuffersValid; }
		public int InvalidVertexBufferFlags
		{
			get
			{
				int intRet = 0;
				for (int idx = 0, rnkFlag = 1; idx < glBuffers.Count; idx++, rnkFlag *= 2) {
					int glID = glBuffers[idx];
					if (glID < 0 || !GL.IsBuffer(glID)) intRet += rnkFlag;
				}
				return intRet;
			}
		}
		public int ValidVertexBufferFlags
		{
			get
			{
				int intRet = 0;
				for (int idx = 0, rnkFlag = 1; idx < glBuffers.Count; idx++, rnkFlag *= 2)
				{
					int glID = glBuffers[idx];
					if (glID >= 0 && GL.IsBuffer(glID)) intRet += rnkFlag;
				}
				return intRet;
			}
		}
		public int VertexBuffersCountMask { get => (int)Math.Pow(2, glBuffers.Count); }
		public clsGeometry() : base(ProjectObjectTypes.Geometry)
		{
			glUpdateBuffers();
			AddToCollection();
		}
		public void glUpdateBuffers()
		{
			if (VertexDescription == null) return;
			if (Triangles == null) return;
			if (Vertices == null) return;
			glContext_Main.MakeCurrent(infoWindow);
			if (!IsIndexBufferValid) glIndexBuffer = GL.GenBuffer();
			if (VertexDescription.Count > glBuffers.Count)
			{
				int intDiff = VertexDescription.Count - glBuffers.Count;
				int[] intBuffersNew = new int[intDiff];
				GL.GenBuffers(intDiff, intBuffersNew);
				glBuffers.AddRange(intBuffersNew);
			}
			if(VertexDescription.Count < glBuffers.Count)
			{
				int[] ary = glBuffers.ToArray();
				int intDiff = glBuffers.Count - VertexDescription.Count;
				for(int itr = VertexDescription.Count; itr < glBuffers.Count; itr++) GL.DeleteBuffer(ary[itr]);
				glBuffers.RemoveRange(VertexDescription.Count, intDiff);
			}
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, glIndexBuffer);
			GL.BufferData<uint>(BufferTarget.ElementArrayBuffer, sizeof(uint) * Triangles.Indices.Length, Triangles.Indices.ToArray(), BufferUsageHint.DynamicDraw);
			//GL.BufferSubData<uint>(BufferTarget.ElementArrayBuffer, (IntPtr)0, (IntPtr)(sizeof(uint) * Triangles.Indices.Length), Triangles.Indices.ToArray());
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
			for(int itrBuff = 0; itrBuff < glBuffers.Count; itrBuff++)
			{
				int intBuff = glBuffers[itrBuff];
				clsVertexDescriptionComponent desc = VertexDescription[itrBuff];
				object[] ary = Vertices.Data[desc].ToArray();
				GL.BindBuffer(BufferTarget.ArrayBuffer, intBuff);
				clsVertexDescriptionComponent.VertexSetDataDelegate[desc.ElementGLType](ary, desc.ComponentSize, BufferTarget.ArrayBuffer, BufferUsageHint.DynamicDraw);
				//clsVertexDescriptionComponent.VertexSetSubDataDelegate[desc.ElementGLType](ary, desc.ComponentSize, BufferTarget.ArrayBuffer);
				GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (Triangles != null)
			{
				Triangles.Dispose();
				Triangles = null;
			}
			if (Vertices != null)
			{
				Vertices.Dispose();
				Vertices = null;
			}
			if (VertexDescription != null)
			{
				VertexDescription.Dispose();
				VertexDescription = null;
			}
			GL.DeleteBuffer(glIndexBuffer);
			glIndexBuffer = -1;
			GL.DeleteBuffers(glBuffers.Count, glBuffers.ToArray());
			glBuffers.Clear();
			base.Dispose(disposing);
		}
		public new void Dispose()
		{
			Dispose(true);
		}

		[Browsable(false)]
		public override Xml_ProjectObject Xml
		{
			get => new Xml_Geometry(this);
		}
	}
	public class clsRender : clsProjectObject
	{
		public double RenderInterval { get; set; } = 1000.0 / 60.0;
		public clsGeometry Geometry { get; set; } = null;
		public clsProgram Program { get; set; } = null;
		public List<KeyValuePair<string, clsVertexDescriptionComponent>> GeometryShaderLinks { private set; get; } = new List<KeyValuePair<string, clsVertexDescriptionComponent>>();
		public List<KeyValuePair<string, clsUniformSet>> Uniforms { private set; get; } = new List<KeyValuePair<string, clsUniformSet>>();
		public List<KeyValuePair<string, string>> UniformShaderLinks { private set; get; } = new List<KeyValuePair<string, string>>();
		public List<clsEventScript> EventScripts { private set; get; } = new List<clsEventScript>();
		public delegate void ScriptHandler(EventType eventType, params object[] args);
		public event ScriptHandler Load;
		public event ScriptHandler Render;
		public event ScriptHandler Resize;
		public event ScriptHandler PointerStart;
		public event ScriptHandler PointerMove;
		public event ScriptHandler PointerEnd;
		public void RaiseLoadEvent() { Load?.Invoke(EventType.OnLoad); }
		public void RaiseRenderEvent(double Frame, double DeltaTime, double ElapsedTime) { Render?.Invoke(EventType.OnRender, Frame, DeltaTime, ElapsedTime); }
		public void RaiseResizeEvent(int Width, int Height) { Resize?.Invoke(EventType.OnResize, Width, Height); }
		public void RaisePointerStartEvent(InputEventArgs InputInfo) { PointerStart?.Invoke(EventType.OnPointerStart, InputInfo); }
		public void RaisePointerMoveEvent(InputEventArgs InputInfo) { PointerMove?.Invoke(EventType.OnPointerMove, InputInfo); }
		public void RaisePointerEndEvent(InputEventArgs InputInfo) { PointerEnd?.Invoke(EventType.OnPointerEnd, InputInfo); }
		public clsRender() : base(ProjectObjectTypes.Render)
		{
			AddToCollection();
		}
		public void LinkShaderUniforms()
		{
			int intProgramID = (Program != null && Program.IsValid) ? Program.glID : -1;
			if (intProgramID < 0) return;
			GL.UseProgram(intProgramID);
			foreach (KeyValuePair<string, clsUniformSet> itm in Uniforms)
			{
				if (itm.Key == null) continue;
				foreach (KeyValuePair<string, string> uni in UniformShaderLinks.FindAll(x => x.Value == itm.Key))
				{
					string uniName = "";
					int uniLoc = -1;
					uniName = (uni.Key != null) ? uni.Key : "";
					uniLoc = (!string.IsNullOrEmpty(uniName) && intProgramID >= 0) ? GL.GetUniformLocation(intProgramID, uniName) : -1;
					itm.Value.ShaderUniformLink = new KeyValuePair<string, int>(uniName, uniLoc);
				}
			}
		}
		protected override void Dispose(bool disposing)
		{
			Geometry = null;
			Program = null;
			foreach(var script in EventScripts)
			{
				script.Dispose();
			}
			EventScripts.Clear();
			Uniforms.Clear();
			GeometryShaderLinks.Clear();
			UniformShaderLinks.Clear();
			RenderInterval = 0;
			base.Dispose(disposing);
		}
		public new void Dispose()
		{
			Dispose(true);
		}

		[Browsable(false)]
		public override Xml_ProjectObject Xml
		{
			get => new Xml_Render(this);
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
				MatchCollection matches = Regex.Matches(FullString, @"(?<column>[\w\d]{0,})\((?<line>\d+)\)");
				if(matches.Count > 0)
				{
					string strLine = matches[0].Groups["line"].Value;
					if(!string.IsNullOrEmpty(strLine)) int.TryParse(strLine, out retLine);
					string strColumn = matches[0].Groups["column"].Value;
					if (!string.IsNullOrEmpty(strColumn)) int.TryParse(strColumn, out retColumn);
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
				return $"{((Line >= 0)?($"Line: {Line};"):("//"))} {((Column > 0) ? ($"Column: {Column};") : (""))} ";
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
					MatchCollection matches = Regex.Matches(FullString, @"\:\s+(?<msglevel>\w+)\s(?<msgnum>\w\d+)\:");
					return (matches.Count > 0) ? (matches[0].Groups["msglevel"].Value).ToUpper() : ("");
				}
			}
			public InfoLocation Location
			{
				get
				{
					MatchCollection matches = Regex.Matches(FullString, @"\A(?<location>[\w\d]{0,}\(\d+\))\s+\:");
					return (matches.Count > 0) ? (new InfoLocation(matches[0].Groups["location"].Value)) : (new InfoLocation());
				}
			}
			public string Message
			{
				get
				{
					MatchCollection matches = Regex.Matches(FullString, @"\w\d+\:(\w|\W)+");
					return (matches.Count > 0) ? (matches[0].Value) : FullString;
				}
			}
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
				return Array.ConvertAll(Array.FindAll(Info.Split('\n'), str => str != ""), str => new InfoMessage(str));
			}
		}
		public InfoMessage[] WarningMessages
		{
			get
			{
				return Array.FindAll(AllMessages, str => str.Level == "WARNING");
			}
		}
		public InfoMessage[] ErrorMessages
		{
			get
			{
				return Array.FindAll(AllMessages, str => str.Level == "ERROR");
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
}
