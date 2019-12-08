using System;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Collections.Generic;
using modProject;
using Gecko;
using Gecko.DOM;
using Gecko.Net;
using Gecko.Utils;
using Gecko.Interop;
using Gecko.Events;
using Gecko.IO;
using Gecko.WebIDL;
using Gecko.CustomMarshalers;
using Gecko.Images;
using Gecko.JQuery;
using Gecko.Collections;
using Gecko.Windows;
using Microsoft.JScript;

namespace WinOpenGL_ShaderToy
{
	public partial class frmShaderIOLinks : DockContent
	{
		private GeckoWebBrowser browserCode = null;
		private AutoJSContext JSContext = null;
		public frmShaderIOLinks(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public frmShaderIOLinks()
		{
			InitializeComponent();
		}
		private void frmShaderIOLinks_Load(object sender, EventArgs e)
		{
			if (!Xpcom.IsInitialized) Xpcom.Initialize("Firefox64");
			browserCode = new GeckoWebBrowser();
			browserCode.DocumentCompleted += browserCode_Ready;
			browserCode.Navigate("about:blank");
		}
		private void frmShaderIOLinks_FormClosing(object sender, FormClosingEventArgs e)
		{
			browserCode.Dispose();
		}
		private void browserCode_Ready(object sender, EventArgs e)
		{
			browserCode.DocumentCompleted -= browserCode_Ready;
			GeckoHeadElement elemHead = browserCode.Document.Head;
			GeckoElement elemScript = browserCode.Document.CreateElement("script");
			GeckoScriptElement domScript = (GeckoScriptElement)elemScript;
			elemHead.AppendChild(elemScript);
			domScript.Id = "scriptCommon";
			domScript.InnerHtml = "\n" +
				"	var Uniforms = {};\n"
			;
			JSContext = new AutoJSContext(browserCode.Window);
		}
		public bool IsUniformDefined(string name)
		{
			return JSContext.EvaluateScript($"Object.keys(Uniforms).includes(\"{name}\")").ToBoolean();
		}
		public void Uniform_Add(string name)
		{
			if (!IsUniformDefined(name)) {
				JSContext.EvaluateScript($"Uniforms.{name} = [];");
			}
		}
		public void Uniform_Remove(string name)
		{
			if(IsUniformDefined(name))
			{
				JSContext.EvaluateScript($"Uniforms.{name};");
			}
		}
		public int Uniform_GetComponentSize(string name)
		{
			if (IsUniformDefined(name)) {
				JsVal jsCount = JSContext.EvaluateScript($"Uniforms.{name}.length");
				if(jsCount.IsNull || jsCount.IsUndefined)
				{
					return 0;
				} else
				{
					jsCount = JSContext.EvaluateScript($"Uniforms.{name}[0].length");
					if (jsCount.IsNull || jsCount.IsUndefined)
					{
						return 0;
					}
					else
					{
						return jsCount.ToInteger();
					}
				}
			} else {
				return -1;
			}
		}
		public int Uniform_GetComponentCount(string name)
		{
			if (IsUniformDefined(name)) {
				JsVal jsCount = JSContext.EvaluateScript($"Uniforms.{name}.length");
				if(jsCount.IsNull || jsCount.IsUndefined)
				{
					return 0;
				} else
				{
					return jsCount.ToInteger();
				}
			} else {
				return -1;
			}
		}
		public void Uniform_SetComponentCount(string name, int count)
		{
			if (IsUniformDefined(name)) {
				JSContext.EvaluateScript($"Uniforms.{name}.length = {count};");
			}
		}
		public void SetUniform(string name, double[] value)
		{
			if (IsUniformDefined(name))
			{
				if(value != null)
				{
					string str = "";
					for (int itr = 0; itr < value.Length; itr++)
					{
						str += value[itr].ToString();
						if (itr < value.Length - 1) str += ", ";
					}
					JSContext.EvaluateScript($"Uniforms.{name} = [{str}];");
				} else
				{
					JSContext.EvaluateScript($"delete Uniforms.{name};");
				}
			}
		}
		public void SetUniform(string name, double[][] value)
		{
			if (IsUniformDefined(name))
			{
				if (value != null)
				{
					for(int itmitr = 0; itmitr < value.Length; itmitr++)
					{
						string str = "";
						for (int itr = 0; itr < value[itmitr].Length; itr++)
						{
							str += value[itmitr][itr].ToString();
							if (itr < value[itmitr].Length - 1) str += ", ";
						}
						JSContext.EvaluateScript($"Uniforms.{name}[{itmitr}] = [{str}];");
					}
				}
				else
				{
					JSContext.EvaluateScript($"delete Uniforms.{name};");
				}
			}
		}
		public double[][] GetUniform(string name)
		{
			double[][] aryRet = null;
			if (IsUniformDefined(name))
			{
				int compCount = Uniform_GetComponentSize(name);
				int compLen = Uniform_GetComponentCount(name);
				if (compCount <= 0)
				{
					aryRet = new double[1][];
					if (compLen > 0)
					{
						aryRet[0] = new double[compLen];
						for (int itr = 0; itr < aryRet[0].Length; itr++)
						{
							JsVal res = JSContext.EvaluateScript($"Uniforms.{name}[{itr}]");
							if (res.IsNull || res.IsUndefined || !res.IsNumber) { aryRet[0][itr] = 0; } else {
								if (res.IsDouble) { aryRet[0][itr] = res.ToDouble(); } else aryRet[0][itr] = res.ToInteger(); }
						}
					} else
					{
						JsVal res = JSContext.EvaluateScript($"Uniforms.{name}");
						if (res.IsNull || res.IsUndefined || !res.IsNumber) { aryRet[0] = new double[] { 0 }; } else {
							if (res.IsDouble) { aryRet[0] = new double[] { res.ToDouble() }; } else aryRet[0] = new double[] { res.ToInteger() }; }
					}
				} else
				{
					aryRet = new double[compCount][];
					for(int itmitr = 0; itmitr < aryRet.Length; itmitr++)
					{
						aryRet[itmitr] = new double[compLen];
						for (int itr = 0; itr < aryRet[itmitr].Length; itr++)
						{
							JsVal res = JSContext.EvaluateScript($"Uniforms.{name}[{itmitr}][{itr}]");
							if (res.IsNull || res.IsUndefined || !res.IsNumber) { aryRet[itmitr][itr] = 0; } else {
								if (res.IsDouble) { aryRet[itmitr][itr] = res.ToDouble(); } else { aryRet[itmitr][itr] = res.ToInteger(); } }
						}
					}
				}
				return aryRet;
			} else
			{
				return aryRet;
			}
		}
		public Dictionary<string, double[][]> GetUniforms()
		{
			Dictionary<string, double[][]> ret = new Dictionary<string, double[][]>();
			int intCount = JSContext.EvaluateScript($"Object.keys(Uniforms).length").ToInteger();
			for(int itr = 0; itr < intCount; itr++)
			{
				string strName = JSContext.EvaluateScript($"Object.keys(Uniforms)[{itr}]").ToString();
				ret.Add(strName, GetUniform(strName));
			}
			return ret;
		}
	}
}
