using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using modProject;
using static modCommon.modWndProcInterop;
using static modProject.clsEventScript;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Reflection;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;
using System.Collections;
using static generalUtils;

namespace WinOpenGL_ShaderToy
{
	public partial class controlEventScript : UserControl
	{
		public class AutoCompleteCollection : IEnumerable<AutocompleteItem>
		{
			public clsAutoComplete menu;
			public FastColoredTextBox sourceControl;
			public clsEventScriptContext context;
			public AutoCompleteCollection(clsEventScriptContext _context, FastColoredTextBox _control, clsAutoComplete _menu)
			{
				context = _context;
				sourceControl = _control;
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
									aryRet.Add(groupCapture.Value.Trim('\"'));
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
					if (Parameters != "") strRet += "(" + Parameters + ")";
					return base.ToString();
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
							objArg = CSharpScript.EvaluateAsync(aryStr[itr], globals: context).Result;
						}
						catch
						{
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
			public static AutocompleteItem[] GetAutoCompleteItems(Type obj)
			{
				List<AutocompleteItem> ret = new List<AutocompleteItem>();
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
						ret.Add(new AutocompleteItem(t.Name, 0, strItem));
					}
				}
				foreach (var t in obj.GetProperties())
				{
					if(!t.IsSpecialName && IsPropertyBrowsable(t))
					{
						string strItem = t.PropertyType.Name + " \t " + t.Name;
						ParameterInfo[] aryParams = t.GetIndexParameters();
						if(aryParams.Length > 0)
						{
							strItem += "[";
							for (int tpI = 0; tpI < aryParams.Length; tpI++)
							{
								strItem += aryParams[tpI].ParameterType.Name + " " + aryParams[tpI].Name;
								if (tpI < aryParams.Length - 1) strItem += ", ";
							}
							strItem += "]";
						}
						ret.Add(new AutocompleteItem(t.Name, 0, strItem));
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
					for(int itr = 0; itr < paramsItm.Length; itr++)
					{
						if(aryParams[itr] == null) { bolSignatureValid = false; break; }
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
			public static bool IsMemberBrowsable(MemberInfo member)
			{
				foreach(CustomAttributeData attr in member.CustomAttributes)
				{
					if (attr.AttributeType != typeof(BrowsableAttribute)) continue;
					if (attr.ConstructorArguments.Count != 1) continue;
					if (attr.ConstructorArguments[0].ArgumentType != typeof(bool)) continue;
					return (bool)attr.ConstructorArguments[0].Value;
				}
				return true;
			}
			public static bool IsPropertyBrowsable(PropertyInfo prop)
			{
				foreach (CustomAttributeData attr in prop.CustomAttributes)
				{
					if (attr.AttributeType != typeof(BrowsableAttribute)) continue;
					if (attr.ConstructorArguments.Count != 1) continue;
					if (attr.ConstructorArguments[0].ArgumentType != typeof(bool)) continue;
					return (bool)attr.ConstructorArguments[0].Value;
				}
				return true;
			}
			public IEnumerator<AutocompleteItem> GetEnumerator()
			{
				string strFragment = menu.Fragment.Text;
				clsFragmentPart[] aryParts = clsFragmentPart.ArrayFromString(strFragment);
				KeyValuePair<string, object>[] aryPartObjects = clsFragmentPart.ObjectsFromArray(context, aryParts);
				if (strFragment.EndsWith("["))
				{
					yield break;
				}
				int idxEnd = aryPartObjects.Length - 1;
				if (idxEnd >= 0)
				{
					KeyValuePair<string, object> part = aryPartObjects[idxEnd];
					foreach (var itm in GetAutoCompleteItems(part.Value.GetType()))
					{
						itm.Text = $"{part.Key}{((part.Key!="")?("."):(""))}{itm.Text}";
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
			protected override void OnOpening(CancelEventArgs e)
			{
				UpdateWidth();
				base.OnOpening(e);
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
				for(int itr = 0; itr < Items.VisibleItems.Length; itr++)
				{
					SizeF szItm = gfx.MeasureString(Items.VisibleItems[itr].MenuText, Font);
					sizeNew.Width = Math.Max(sizeNew.Width, (int)szItm.Width + 64);
				}
				Items.MinimumSize = new Size();
				Items.Width = InitialSize.Width;
				Items.MinimumSize = sizeNew;
			}
		}
		private clsAutoComplete menuAutoComplete;
		private AutoCompleteCollection menuItemsCollection;
		private clsEventScriptContext _context;
		public clsEventScriptContext ScriptContext
		{
			get => _context;
			set
			{
				if(_context != null)
				{
					_context.ArgumentsUpdated -= ScriptContext_ArgumentsUpdated;
					_context.DetachUpdateArguments(eventType);
				}
				_context = value;
				if(_context != null)
				{
					_context.ArgumentsUpdated += ScriptContext_ArgumentsUpdated;
					_context.InitArguments(Type);
					_context.AttachUpdateArguments(eventType);
					menuItemsCollection.context = _context;
					menuAutoComplete.Items.SetAutocompleteItems(menuItemsCollection);
				}
			}
		}
		public controlEventScript()
		{
			InitializeComponent();
			lstEventType.Items.AddRange(Enum.GetNames(typeof(EventType)));
			lstEventType.SelectedIndex = 0;
			menuAutoComplete = new clsAutoComplete(txtSource);
			menuAutoComplete.SearchPattern = @"[\w\d\.\[\(\]\)\""]";
			menuAutoComplete.AutoSize = true;
			menuAutoComplete.MinFragmentLength = 1;
			menuItemsCollection = new AutoCompleteCollection(ScriptContext, txtSource, menuAutoComplete);
			menuAutoComplete.Items.SetAutocompleteItems(menuItemsCollection);
			panelEventArguments.SizeChanged += EventArgumentsContainer_Adjust;
			panelEventArguments_Container.SizeChanged += EventArgumentsContainer_Adjust;
			scrollerEventArguments.ValueChanged += ScrollerEventArguments_ValueChanged;
		}
		public void Open()
		{
			if (_context == null) return;
			_context.ArgumentsUpdated += ScriptContext_ArgumentsUpdated;
			_context.InitArguments(eventType);
			_context.AttachUpdateArguments(eventType);
		}
		public void Close()
		{
			if (_context == null) return;
			_context.ArgumentsUpdated -= ScriptContext_ArgumentsUpdated;
			_context.ClearArguments();
			_context.DetachUpdateArguments(eventType);
		}
		private void EventArgumentsContainer_Adjust(object sender, EventArgs e)
		{
			scrollerEventArguments.Maximum = Math.Max(panelEventArguments.Width - panelEventArguments_Container.Width, 0);
			//scrollerEventArguments.LargeChange = panelEventArguments_Container.Width / Math.Max(aryLabelArguments.Count, 1);
		}
		private void ScrollerEventArguments_ValueChanged(object sender, EventArgs e)
		{
			panelEventArguments.Left = -scrollerEventArguments.Value;
		}
		public string Source
		{
			get => txtSource.Text.Trim();
			set { txtSource.Text = value; }
		}
		private List<Label> aryLabelArguments = new List<Label>();
		private void ScriptContext_ArgumentsUpdated(Dictionary<string, object> args)
		{
			int intOldCount = aryLabelArguments.Count;
			string[] aryKeys = args.Keys.ToArray();
			if(args.Count < intOldCount)
			{
				for (int itr = args.Count; itr < intOldCount; itr++)
				{
					aryLabelArguments[itr]?.Dispose();
					aryLabelArguments[itr] = null;
				}
			}
			generalUtils.ResizeList(ref aryLabelArguments, args.Count, (idx) =>
			{
				Label itm = new Label();
				itm.Parent = panelEventArguments;
				itm.BorderStyle = BorderStyle.Fixed3D;
				itm.TextAlign = ContentAlignment.MiddleLeft;
				itm.AutoEllipsis = false;
				itm.Dock = DockStyle.Left;
				itm.BringToFront();
				itm.Font = new Font(FontFamily.GenericMonospace, 8f);
				itm.Margin = new Padding(3);
				itm.AutoSize = true;
				return itm;
			});
			for (int itr = 0; itr < args.Count; itr++)
			{
				aryLabelArguments[itr].Text = $"{aryKeys[itr]}: {args[aryKeys[itr]].ToString()}";
			}
		}
		private EventType eventType = EventType.OnLoad;
		private void lstEventType_SelectedIndexChanged(object sender, EventArgs e)
		{
			Type = (EventType)lstEventType.SelectedIndex;
		}
		public EventType Type
		{
			get => eventType;
			set
			{
				lstEventType.SelectedIndex = (int)value;
				_context?.DetachUpdateArguments(eventType);
				_context?.AttachUpdateArguments(value);
				_context?.InitArguments(value);
				eventType = value;
			}
		}
		public override string Text
		{
			get => EventScript_ToString(eventType, txtSource.Text);
			set
			{
				EventScript_FromString(value, out EventType typ, out string src);
				Type = typ; txtSource.Text = src;
			}
		}
		protected Dictionary<WinHitTest, Rectangle> ResizeGrips 
		{ 
			get => new Dictionary<WinHitTest, Rectangle>() {
				{WinHitTest.Right, Rectangle.FromLTRB(Width - Padding.Right, Padding.Top, Width, Height - Padding.Bottom) },
				{WinHitTest.Bottom, Rectangle.FromLTRB(Padding.Left, Height - Padding.Bottom, Width - Padding.Right, Height) },
				{WinHitTest.BottomRight, Rectangle.FromLTRB(Width - Padding.Right, Height - Padding.Bottom, Width, Height) }
			};
		}
		protected override void WndProc(ref Message m)
		{
			if (WinProc_HandleHitRegions(this, ref m, ResizeGrips)) return;
			base.WndProc(ref m);
		}
	}
}
