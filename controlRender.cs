using OpenTK;
using System;
using System.Windows.Forms;
using static modCommon.modWndProcInterop;
using static modCommon.modWndProcInterop.clsTouchInterface;

namespace WinOpenGL_ShaderToy
{
	public partial class controlRender : GLControl
	{
		public EventHandler<TouchEventArgs> PointerStart;
		public EventHandler<TouchEventArgs> PointerMove;
		public EventHandler<TouchEventArgs> PointerEnd;
		private clsTouchInterface TouchInterface;
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// controlRender
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "controlRender";
			this.Size = new System.Drawing.Size(256, 256);
			this.ResumeLayout(true);

		}
		public controlRender() : base()
		{
			InitializeComponent();
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
			TouchInterface = new clsTouchInterface(this);
			TouchInterface.TouchStart += TouchInterface_TouchStart;
			TouchInterface.TouchMove += TouchInterface_TouchMove;
			TouchInterface.TouchEnd += TouchInterface_TouchEnd;
		}
		private void UnloadTouchInterface()
		{
			if (TouchInterface == null) return;
			TouchInterface.TouchStart -= TouchInterface_TouchStart;
			TouchInterface.TouchMove -= TouchInterface_TouchMove;
			TouchInterface.TouchEnd -= TouchInterface_TouchEnd;
			TouchInterface.Dispose();
			TouchInterface = null;
		}
		public bool DoubleBuffering { get => base.DoubleBuffered; set { base.DoubleBuffered = value; } } 
		private void TouchInterface_TouchStart(object sender, TouchEventArgs e)
		{
			PointerStart?.Invoke(this, e);
		}
		private void TouchInterface_TouchMove(object sender, TouchEventArgs e)
		{
			PointerMove?.Invoke(this, e);
		}
		private void TouchInterface_TouchEnd(object sender, TouchEventArgs e)
		{
			PointerEnd?.Invoke(this, e);
		}
		private clsTouchInput MouseTouch = new clsTouchInput();
		protected override void OnMouseDown(MouseEventArgs e)
		{
			MouseTouch.ID = -1;
			MouseTouch.InputFlags |= (int)e.Button;
			MouseTouch.TouchPoints.Clear();
			MouseTouch.TouchPoints.Add(new TouchPoint() { ControlSize = Size, Location = e.Location });
			PointerStart?.Invoke(this, new TouchEventArgs(MouseTouch));
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (MouseTouch.ID != -1) MouseTouch.TouchPoints.Clear();
			MouseTouch.InputFlags |= (int)e.Button;
			MouseTouch.TouchPoints.Add(new TouchPoint() { ControlSize = Size, Location = e.Location });
			PointerMove?.Invoke(this, new TouchEventArgs(MouseTouch));
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (MouseTouch.ID != -1) MouseTouch.TouchPoints.Clear();
			MouseTouch.InputFlags |= (int)e.Button;
			MouseTouch.TouchPoints.Add(new TouchPoint() { ControlSize = Size, Location = e.Location });
			PointerEnd?.Invoke(this, new TouchEventArgs(MouseTouch));
			MouseTouch.TouchPoints.Clear();
			MouseTouch.ID = 0;
			base.OnMouseUp(e);
		}
		protected override void WndProc(ref Message m)
		{
			bool bolHandled = WinProc_HandleTouch(this, ref m, ref TouchInterface);
			base.WndProc(ref m);
			if (bolHandled) m.Result = new IntPtr(1);
		}
	}
}
