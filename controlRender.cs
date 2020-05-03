using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES10;
using ShaderToy_Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
			// 
			// controlRender
			// 
			this.BackColor = System.Drawing.Color.Black;
			this.ForeColor = System.Drawing.Color.White;
			this.Name = "controlRender";
			this.Size = new System.Drawing.Size(400, 400);
			this.ResumeLayout(false);

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
		private void TouchInterface_TouchStart(object sender, InputEventArgs e)
		{
			PointerStart?.Invoke(sender, e);
		}
		private void TouchInterface_TouchMove(object sender, InputEventArgs e)
		{
			PointerMove?.Invoke(sender, e);
		}
		private void TouchInterface_TouchEnd(object sender, InputEventArgs e)
		{
			PointerEnd?.Invoke(sender, e);
		}
		public TouchInput MouseTouch { get; private set; } = new TouchInput();
		protected override void OnMouseDown(MouseEventArgs e)
		{
			MouseTouch.ID = -1;
			MouseTouch.InputFlags = (int)InputFlags_MouseButtons(e.Button);
			MouseTouch.TouchPoints.Clear();
			GC.Collect();
			TouchPoint Pt = new TouchPoint() { ControlSize = Size, Location = e.Location };
			MouseTouch.TouchPoints.Add(Pt);
			PointerStart?.Invoke(this, new InputEventArgs() { InputTouch = MouseTouch, InputPoint = Pt } );
			base.OnMouseDown(e);
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			Point ptDiff = Point.Subtract(MouseTouch.CurrentTouchPoint.Location, new Size(e.Location));
			if (!ptDiff.IsEmpty)
			{
				MouseTouch.InputFlags = (int)InputFlags_MouseButtons(e.Button);
				TouchPoint Pt = new TouchPoint() { ControlSize = Size, Location = e.Location };
				MouseTouch.TouchPoints.Add(Pt);
				Point mov = MouseTouch.MoveDelta;
				PointF movUV = MouseTouch.MoveDeltaUV;
				double dist = Math.Sqrt(mov.X * mov.X + mov.Y * mov.Y);
				double distUV = Math.Sqrt(movUV.X * movUV.X + movUV.Y * movUV.Y);
				MouseTouch.Distance += dist;
				MouseTouch.DistanceUV += distUV;
				PointerMove?.Invoke(this, new InputEventArgs() { InputTouch = MouseTouch, InputPoint = Pt });
				if (MouseTouch.ID != -1) { MouseTouch.TouchPoints.Clear(); MouseTouch.Distance = 0; MouseTouch.DistanceUV = 0; MouseTouch.TouchPoints.Add(Pt); }
				base.OnMouseMove(e);
			}
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
			MouseTouch.DistanceUV = 0;
			MouseTouch.ID = 0;
			GC.Collect();
			base.OnMouseUp(e);
		}
		protected override void WndProc(ref Message m)
		{
			bool bolHandled = false;
			if (interfaceTouch != null)
			{
				bolHandled = WinProc_HandleTouch(this, ref m, ref interfaceTouch);
				if (bolHandled) interfaceTouch.RaiseEvents();
			}
			if(!bolHandled) base.WndProc(ref m);
		}
	}
}
