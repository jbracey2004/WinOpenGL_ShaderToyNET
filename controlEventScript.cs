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

namespace WinOpenGL_ShaderToy
{
	public partial class controlEventScript : UserControl
	{
		public controlEventScript()
		{
			InitializeComponent();
			lstEventType.Items.AddRange(Enum.GetNames(typeof(EventType)));
			lstEventType.SelectedIndex = 0;
		}
		private void controlEventScript_Load(object sender, EventArgs e)
		{
			
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
