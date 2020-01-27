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
using System.Reflection;
using System.Text.RegularExpressions;
using FastColoredTextBoxNS;
using System.Collections;

namespace WinOpenGL_ShaderToy
{
	public partial class controlEventScript : UserControl
	{
		private class AutoCompleteCollection : IEnumerable<AutocompleteItem>
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
			public IEnumerator<AutocompleteItem> GetEnumerator()
			{
				string strFragment = menu.Fragment.Text;
				List<string> aryParts = new List<string>();
				foreach(Match mch in Regex.Matches(strFragment, @"((?<part>([\w\d_\-]+))\.)+"))
				{
					Group groupPart = mch.Groups["part"];
					if(groupPart != null)
					{
						aryParts.Add(groupPart.Value);
					}
				}
				Type typePart = context.GetType();
				string strPart = "";
				for (int itr = 0; itr < aryParts.Count; itr++)
				{
					Type typeItm = FindTypeByName(aryParts[itr], typePart);
					if (typeItm != null) 
					{ 
						typePart = typeItm; 
						strPart += aryParts[itr] + ".";
					} else 
					{ 
						break; 
					}
				}
				foreach (var itm in GetMethodsAndProperties(typePart))
				{
					itm.Text = strPart + itm.Text;
					yield return itm;
				}
			}
			public static AutocompleteItem[] GetMethodsAndProperties(Type obj)
			{
				List<AutocompleteItem> ret = new List<AutocompleteItem>();
				foreach (var t in obj.GetMethods())
				{
					if ((int)(t.Attributes | ~MethodAttributes.SpecialName) != -1)
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
					string strItem = t.PropertyType.Name + " \t " + t.Name;
					ret.Add(new AutocompleteItem(t.Name, 0, strItem));
				}
				return ret.ToArray();
			}
			private static Type FindTypeByName(string name, Type RootType)
			{
				foreach (var t in RootType.GetProperties())
				{
					if (t.Name == name)
					{
						return t.PropertyType;
					}
				}
				return null;
			}
			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}
		}
		private class clsAutoComplete : AutocompleteMenu
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
					sizeNew.Width = Math.Max(sizeNew.Width, (int)szItm.Width + 16);
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
				_context = value;
				menuItemsCollection.context = _context;
				menuAutoComplete.Items.SetAutocompleteItems(menuItemsCollection);
			}
		}
		public controlEventScript()
		{
			InitializeComponent();
			lstEventType.Items.AddRange(Enum.GetNames(typeof(EventType)));
			lstEventType.SelectedIndex = 0;
			menuAutoComplete = new clsAutoComplete(txtSource);
			menuAutoComplete.SearchPattern = @"[\w\.]";
			menuAutoComplete.AutoSize = true;
			menuAutoComplete.MinFragmentLength = 1;
			menuItemsCollection = new AutoCompleteCollection(ScriptContext, txtSource, menuAutoComplete);
			menuAutoComplete.Items.SetAutocompleteItems(menuItemsCollection);
		}
		public string Source
		{
			get => txtSource.Text;
			set { txtSource.Text = value; }
		}
		public EventType Type
		{
			get => (EventType)lstEventType.SelectedIndex;
			set
			{
				lstEventType.SelectedIndex = (int)value;
			}
		}
		public override string Text
		{
			get => EventScript_ToString(Type, txtSource.Text);
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
