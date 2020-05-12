using FastColoredTextBoxNS;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static generalUtils;
using static modProject.clsEventScript;

namespace WinOpenGL_ShaderToy
{
	public class clsAutoCompleteItem : AutocompleteItem
	{
		public clsAutoCompleteItem() : base() { }
		public clsAutoCompleteItem(string text) : base(text) { }
		public clsAutoCompleteItem(string text, string textMenu) : base(text, 0, textMenu) { }
		public override CompareResult Compare(string fragmentText)
		{
			//return base.Compare(fragmentText);
			return CompareResult.Visible;
		}
		public override string GetTextForReplace()
		{
			return base.GetTextForReplace();
		}
		public override void OnSelected(AutocompleteMenu popupMenu, SelectedEventArgs e)
		{
			base.OnSelected(popupMenu, e);
		}
	}
	public class clsAutoCompleteCollection : IEnumerable<clsAutoCompleteItem>
	{
		public clsAutoComplete menu;
		public clsEventScriptContext context;
		public clsAutoCompleteCollection(clsEventScriptContext _context, clsAutoComplete _menu)
		{
			context = _context;
			menu = _menu;
		}
		public class clsFragmentPart
		{
			public string Name { get; set; }
			public string Parameters { get; set; }
			public string Indexer { get; set; }
			public string[] IndexList
			{
				get
				{
					if (Indexer == null) return new string[] { };
					List<string> aryRet = new List<string>();
					foreach (Match mch in Regex.Matches(Indexer, @"(?<indexer>((?<indexarg>(\d+|\"".*?\"")((\s+)?\,(\s+)?)?)+))"))
					{
						Group groupParse = mch.Groups["indexarg"];
						if (groupParse.Success)
						{
							foreach (Capture groupCapture in groupParse.Captures)
							{
								aryRet.Add(groupCapture.Value);
							}
						}
					}
					return aryRet.ToArray();
				}
				set
				{
					Indexer = string.Join(", ", value);
				}
			}
			public string[] ParamList
			{
				get
				{
					if (Parameters == null) return new string[] { };
					return Array.ConvertAll(Parameters.Split(','), itm => itm.Trim());
				}
				set
				{
					Parameters = string.Join(", ", value);
				}
			}
			public override string ToString()
			{
				string strRet = Name;
				if (!string.IsNullOrEmpty(Parameters)) strRet += "(" + Parameters + ")";
				if (!string.IsNullOrEmpty(Indexer)) strRet += "[" + Indexer + "]";
				return strRet;
			}
			public static clsFragmentPart[] ArrayFromString(string strFragment)
			{
				List<clsFragmentPart> aryRet = new List<clsFragmentPart>();
				foreach (Match mch in Regex.Matches(strFragment, @"((?<part>([\w\d_]+))(\((?<params>.*?)\))?(\[(?<indexer>.*?)\])?\.?)+"))
				{
					clsFragmentPart objPart = new clsFragmentPart();
					Group groupParse = mch.Groups["part"];
					if (groupParse.Success)
					{
						objPart.Name = groupParse.Value;
					}
					groupParse = mch.Groups["params"];
					if (groupParse.Success)
					{
						objPart.Parameters = groupParse.Value;
					}
					groupParse = mch.Groups["indexer"];
					if (groupParse.Success)
					{
						objPart.Indexer = groupParse.Value;
					}
					aryRet.Add(objPart);
				}
				return aryRet.ToArray();
			}
			public static object[] GetParamObjectArray(object context, string[] aryStr)
			{
				object[] aryRet = new object[aryStr.Length];
				for (int itr = 0; itr < aryStr.Length; itr++)
				{
					object objArg;
					try
					{
						objArg = CSharpScript.EvaluateAsync(aryStr[itr], ProjectDef.GenericScriptOptions, context).Result;
					}
					catch (Exception err)
					{
						Console.WriteLine(ProjectDef.ExceptionFullString(err));
						objArg = null;
					}
					aryRet[itr] = objArg;
				}
				return aryRet;
			}
			public static KeyValuePair<string, object>[] ObjectsFromArray(object context, clsFragmentPart[] aryParts)
			{
				List<KeyValuePair<string, object>> aryRet = new List<KeyValuePair<string, object>>();
				aryRet.Add(new KeyValuePair<string, object>("", context));
				object objPart = context;
				string strPart = "";
				for (int itr = 0; itr < aryParts.Length; itr++)
				{
					Type typeItm = objPart.GetType();
					GetMethod(aryParts[itr], context, typeItm, out MethodInfo methodItm, out object methodObj);
					GetProperty(aryParts[itr], context, typeItm, out PropertyInfo propItm, out object propObj);
					object obj = null;
					if (propItm != null) obj = propObj;
					if (methodItm != null) obj = methodObj;
					if (obj != null)
					{
						aryRet.Add(new KeyValuePair<string, object>(aryParts[itr].Name, obj));
						strPart += aryParts[itr].Name + ".";
						objPart = obj;
						continue;
					}
					break;
				}
				return aryRet.ToArray();
			}
		}
		public static clsAutoCompleteItem[] GetAutoCompleteItems(Type obj)
		{
			List<clsAutoCompleteItem> ret = new List<clsAutoCompleteItem>();
			foreach (MethodInfo t in obj.GetMethods())
			{
				if (!t.IsSpecialName && IsMemberBrowsable(t))
				{
					string strItem = t.ReturnType.Name + " \t " + t.Name;
					strItem += "(";
					ParameterInfo[] aryParams = t.GetParameters();
					for (int tpI = 0; tpI < aryParams.Length; tpI++)
					{
						strItem += aryParams[tpI].ParameterType.Name + " " + aryParams[tpI].Name;
						if (tpI < aryParams.Length - 1) strItem += ", ";
					}
					strItem += ")";
					ret.Add(new clsAutoCompleteItem(t.Name, strItem));
				}
			}
			foreach (var t in obj.GetProperties())
			{
				if (!t.IsSpecialName && IsPropertyBrowsable(t))
				{
					string strItem = t.PropertyType.Name + " \t " + t.Name;
					ParameterInfo[] aryParams = t.GetIndexParameters();
					if (aryParams.Length > 0)
					{
						strItem += "[";
						for (int tpI = 0; tpI < aryParams.Length; tpI++)
						{
							strItem += aryParams[tpI].ParameterType.Name + " " + aryParams[tpI].Name;
							if (tpI < aryParams.Length - 1) strItem += ", ";
						}
						strItem += "]";
					}
					ret.Add(new clsAutoCompleteItem(t.Name, strItem));
				}
			}
			return ret.ToArray();
		}
		public static void GetMethod(clsFragmentPart part, object context, Type obj, out MethodInfo methodRet, out object objRet)
		{
			methodRet = null;
			objRet = null;
			object[] aryParams = clsFragmentPart.GetParamObjectArray(context, part.ParamList);
			foreach (MethodInfo t in obj.GetMethods())
			{
				if (!IsMemberBrowsable(t)) continue;
				if (t.IsSpecialName) continue;
				if (t.Name != part.Name) continue;
				ParameterInfo[] paramsItm = t.GetParameters();
				if (paramsItm.Length != aryParams.Length) continue;
				bool bolSignatureValid = true;
				for (int itr = 0; itr < paramsItm.Length; itr++)
				{
					if (aryParams[itr] == null) { bolSignatureValid = false; break; }
					if (aryParams[itr].GetType() != paramsItm[itr].ParameterType) { bolSignatureValid = false; break; }
				}
				if (!bolSignatureValid) continue;
				methodRet = t;
				objRet = t.Invoke(context, aryParams);
				break;
			}
		}
		public static void GetProperty(clsFragmentPart part, object context, Type obj, out PropertyInfo propRet, out object objRet)
		{
			propRet = null;
			objRet = null;
			object[] aryParams = clsFragmentPart.GetParamObjectArray(context, part.IndexList);
			foreach (PropertyInfo t in obj.GetProperties())
			{
				if (!IsPropertyBrowsable(t)) continue;
				if (t.IsSpecialName) continue;
				if (t.Name != part.Name) continue;
				ParameterInfo[] paramsItm = t.GetIndexParameters();
				if (paramsItm.Length != aryParams.Length) continue;
				bool bolSignatureValid = true;
				for (int itr = 0; itr < paramsItm.Length; itr++)
				{
					if (aryParams[itr] == null) { bolSignatureValid = false; break; }
					if (aryParams[itr].GetType() != paramsItm[itr].ParameterType) { bolSignatureValid = false; break; }
				}
				if (!bolSignatureValid) continue;
				propRet = t;
				objRet = t.GetValue(context, aryParams);
				break;
			}
		}
		public IEnumerator<clsAutoCompleteItem> GetEnumerator()
		{
			string strFragment = menu.Fragment.Text;
			clsFragmentPart[] aryParts = clsFragmentPart.ArrayFromString(strFragment);
			List<KeyValuePair<string, object>> aryPartObjects = new List<KeyValuePair<string, object>>();
			aryPartObjects.AddRange(clsFragmentPart.ObjectsFromArray(context, aryParts));
			aryPartObjects.AddRange(clsFragmentPart.ObjectsFromArray(ProjectDef.ScriptContextFunctions, aryParts));
			int idxEnd = (aryParts.Length > 0)?aryPartObjects.FindIndex(itm => aryParts[aryParts.Length - 1].Name == itm.Key):-1;
			if (strFragment.LastIndexOf("[") > strFragment.LastIndexOf("."))
			{
				IEnumerable ary = aryPartObjects[idxEnd].Value as IEnumerable;
				if (ary != null)
				{
					foreach (var itm in ary)
					{
						if (TryKeyPairParse(itm, out KeyValuePair<dynamic, dynamic> kvp))
						{
							yield return new clsAutoCompleteItem($"{aryPartObjects[idxEnd].Key}[\"{kvp.Key}\"]", $"\"{kvp.Key}\"");
						}
					}
				}
				yield break;
			}
			if (idxEnd >= 0)
			{
				KeyValuePair<string, object> part = aryPartObjects[idxEnd];
				foreach (var itm in GetAutoCompleteItems(part.Value.GetType()))
				{
					itm.Text = $"{part.Key}{((part.Key != "") ? (".") : (""))}{itm.Text}";
					yield return itm;
				}
			}
			yield break;
		}
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	public class clsAutoComplete : AutocompleteMenu
	{
		private Graphics gfx;
		private Size InitialSize = new Size();
		public clsAutoComplete(FastColoredTextBox textbox) : base(textbox) { InitialSize = Size; }
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}
		public override ToolStripItem GetNextItem(ToolStripItem start, ArrowDirection direction)
		{
			return base.GetNextItem(start, direction);
		}
		protected override void OnItemAdded(ToolStripItemEventArgs e)
		{
			base.OnItemAdded(e);
		}
		protected override void OnItemRemoved(ToolStripItemEventArgs e)
		{
			base.OnItemRemoved(e);
		}
		protected override void SetDisplayedItems()
		{
			base.SetDisplayedItems();
		}
		protected override void OnOpening(CancelEventArgs e)
		{
			UpdateWidth();
			base.OnOpening(e);
		}
		protected override void OnClosing(ToolStripDropDownClosingEventArgs e)
		{
			base.OnClosing(e);
		}
		protected override void OnHandleCreated(EventArgs e)
		{
			gfx = CreateGraphics();
			base.OnHandleCreated(e);
		}
		protected override void OnHandleDestroyed(EventArgs e)
		{
			gfx.Dispose();
			gfx = null;
			base.OnHandleDestroyed(e);
		}
		private void UpdateWidth()
		{
			if (Items == null) return;
			if (gfx == null) return;
			Size sizeNew = new Size();
			for (int itr = 0; itr < Items.VisibleItems.Length; itr++)
			{
				SizeF szItm = gfx.MeasureString(Items.VisibleItems[itr].MenuText, Font);
				sizeNew.Width = Math.Max(sizeNew.Width, (int)szItm.Width + 64);
			}
			Items.MinimumSize = new Size();
			Items.Width = InitialSize.Width;
			Items.MinimumSize = sizeNew;
		}
	}
}
