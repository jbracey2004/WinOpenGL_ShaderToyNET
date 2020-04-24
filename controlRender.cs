using OpenTK;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static modCommon.modWndProcInterop;
using static modCommon.modWndProcInterop.InputInterface;

namespace WinOpenGL_ShaderToy
{
	public partial class controlRender : GLControl
	{
		public EventHandler<InputEventArgs> PointerStart;
		public EventHandler<InputEventArgs> PointerMove;
		public EventHandler<InputEventArgs> PointerEnd;
		public InputInterface InterfaceTouch { get => interfaceTouch; }
		private InputInterface interfaceTouch;
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// controlRender
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.BackColor = System.Drawing.Color.Black;
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "controlRender";
			this.Size = new System.Drawing.Size(260, 260);
			this.ResumeLayout(false);

		}
		public controlRender() : base()
		{
			InitializeComponent();
			base.DoubleBuffered = true;
		}
		protected override void Dispose(bool disposing)
		{
			Context.Dispose();
			base.Dispose(disposing);
		}
		protected override void OnHandleCreated(EventArgs e)
		{
			InitTouchInterface();
			base.OnHandleCreated(e);
		}
		protected override void OnHandleDestroyed(EventArgs e)
		{
			UnloadTouchInterface();
			base.OnHandleDestroyed(e);
		}
		private void InitTouchInterface()
		{
			UnloadTouchInterface();
			interfaceTouch = new InputInterface(this);
			interfaceTouch.TouchStart += TouchInterface_TouchStart;
			interfaceTouch.TouchMove += TouchInterface_TouchMove;
			interfaceTouch.TouchEnd += TouchInterface_TouchEnd;
		}
		private void UnloadTouchInterface()
		{
			if (interfaceTouch == null) return;
			interfaceTouch.TouchStart -= TouchInterface_TouchStart;
			interfaceTouch.TouchMove -= TouchInterface_TouchMove;
			interfaceTouch.TouchEnd -= TouchInterface_TouchEnd;
			interfaceTouch.Dispose();
			interfaceTouch = null;
		}
		public new bool DoubleBuffered { get => base.DoubleBuffered; set { base.DoubleBuffered = value; } } 
		private void TouchInterface_TouchStart(object sender, InputEventArgs e)
		{
			PointerStart?.Invoke(this, e);
		}
		private void TouchInterface_TouchMove(object sender, InputEventArgs e)
		{
			PointerMove?.Invoke(this, e);
		}
		private void TouchInterface_TouchEnd(object sender, InputEventArgs e)
		{
			PointerEnd?.Invoke(this, e);
		}
		public TouchInput MouseTouch { get; private set; } = new TouchInput();
		protected override void OnMouseDown(MouseEventArgs e)
		{
			MouseTouch.ID = -1;
			MouseTouch.InputFlags = (int)InputFlags_MouseButtons(e.Button);
			MouseTouch.TouchPoints.Clear();
			TouchPoint Pt = new TouchPoint() { ControlSize = Size, Location = e.Location };
			MouseTouch.TouchPoints.Add(Pt);
			PointerStart?.Invoke(this, new InputEventArgs() { InputTouch = MouseTouch, InputPoint = Pt } );
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			MouseTouch.InputFlags = (int)InputFlags_MouseButtons(e.Button);
			TouchPoint Pt = new TouchPoint() { ControlSize = Size, Location = e.Location };
			MouseTouch.TouchPoints.Add(Pt);
			Point mov = MouseTouch.MoveDelta;
			double dist = Math.Sqrt(mov.X * mov.X + mov.Y * mov.Y);
			MouseTouch.Distance += dist;
			PointerMove?.Invoke(this, new InputEventArgs() { InputTouch = MouseTouch, InputPoint = Pt } );
			if (MouseTouch.ID != -1) { MouseTouch.TouchPoints.Clear(); MouseTouch.Distance = 0; MouseTouch.TouchPoints.Add(Pt); }
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (MouseTouch.ID != -1) MouseTouch.TouchPoints.Clear();
			MouseTouch.InputFlags = 0;
			TouchPoint Pt = new TouchPoint() { ControlSize = Size, Location = e.Location };
			MouseTouch.TouchPoints.Add(Pt);
			PointerEnd?.Invoke(this, new InputEventArgs() { InputTouch = MouseTouch, InputPoint = Pt } );
			MouseTouch.TouchPoints.Clear();
			MouseTouch.Distance = 0;
			MouseTouch.ID = 0;
			base.OnMouseUp(e);
		}
		protected override void WndProc(ref Message m)
		{
			bool bolHandled = WinProc_HandleTouch(this, ref m, ref interfaceTouch);
			if (m.Result.ToInt64() != 0) return;
			base.WndProc(ref m);
		}
	}
}
