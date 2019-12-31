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
using static modProject.clsEventScript;

namespace WinOpenGL_ShaderToy
{
	public partial class controlEventScript : UserControl
	{
		public controlEventScript()
		{
			InitializeComponent();
		}
		private void controlEventScript_Load(object sender, EventArgs e)
		{
			lstEventType.Items.AddRange(Enum.GetNames(typeof(EventType)));
		}
		public override string Text => txtSource.Text;
		public EventType Type
		{
			get => (EventType)lstEventType.SelectedIndex;
			set
			{
				lstEventType.SelectedIndex = (int)value;
			}
		}
	}
}
