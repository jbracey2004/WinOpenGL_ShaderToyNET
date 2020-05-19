using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static modCommon.modWndProcInterop;
using static modProject.clsEventScript;

namespace WinOpenGL_ShaderToy
{
	public partial class controlEventScript : UserControl
	{
		private clsAutoComplete menuAutoComplete;
		private clsAutoCompleteCollection menuItemsCollection;
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
			menuAutoComplete.AutoSize = true;
			menuAutoComplete.MinFragmentLength = 1;
			menuItemsCollection = new clsAutoCompleteCollection(ScriptContext, menuAutoComplete);
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
