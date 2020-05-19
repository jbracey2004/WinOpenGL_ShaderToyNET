using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using static OpenTK.Platform.Utilities;
using ShaderToy_Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static modCommon.modWndProcInterop;
using static modCommon.modWndProcInterop.InputInterface;
using static generalUtils;
using modProject;
using System.Web.ModelBinding;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;

namespace WinOpenGL_ShaderToy
{
	public partial class controlRender : UserControl
	{
		public EventHandler<InputEventArgs> PointerStart;
		public EventHandler<InputEventArgs> PointerMove;
		public EventHandler<InputEventArgs> PointerEnd;
		public InputInterface InterfaceTouch { get => interfaceTouch; }
		private InputInterface interfaceTouch;
		private clsRenderQueueItem queueRenderInfo;
		private GraphicsContext glContext;
		private Graphics gdiContext;
		private void InitializeComponent()
		{
			this.SuspendLayout();
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
			queueRenderInfo = new clsRenderQueueItem();
			queueRenderInfo.Widget = this;
			queueRenderInfo.RenderComplete = true;
			queueRenderInfo.Result = null;
			InitializeComponent();
		}
		protected override void OnHandleCreated(EventArgs e)
		{
			InitContext();
			InitTouchInterface();
			base.OnHandleCreated(e);
		}
		protected override void OnHandleDestroyed(EventArgs e)
		{
			UnloadContext();
			UnloadTouchInterface();
			base.OnHandleDestroyed(e);
		}
		protected override void OnResize(EventArgs e)
		{
			if (glContext != null && queueRenderInfo.Window != null)
			{
				glContext.Update(queueRenderInfo.Window);
			}
			base.OnResize(e);
		}
		private void InitContext()
		{
			UnloadContext();
			queueRenderInfo.Window = CreateWindowsWindowInfo(Handle);
			glContext = new GraphicsContext(ProjectDef.glContext_Main.GraphicsMode, queueRenderInfo.Window, ProjectDef.glContext_Main, 5, 0, GraphicsContextFlags.ForwardCompatible);
			glContext.MakeCurrent(queueRenderInfo.Window);
			GL.DrawBuffer(DrawBufferMode.Front);
			GL.ClearColor(BackColor);
			gdiContext = CreateGraphics();
		}
		private void UnloadContext()
		{
			if (gdiContext != null)
			{
				gdiContext.Dispose();
				gdiContext = null;
			}
			if (glContext != null)
			{
				glContext.Dispose();
				glContext = null;
			}
			if (queueRenderInfo.Window != null)
			{
				queueRenderInfo.Window.Dispose();
				queueRenderInfo.Window = null;
			}
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
			PointerStart?.Invoke(this, new InputEventArgs() { InputTouch = MouseTouch, InputPoint = Pt });
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
			PointerEnd?.Invoke(this, new InputEventArgs() { InputTouch = MouseTouch, InputPoint = Pt });
			MouseTouch.TouchPoints.Clear();
			MouseTouch.Distance = 0;
			MouseTouch.DistanceUV = 0;
			MouseTouch.ID = 0;
			GC.Collect();
			base.OnMouseUp(e);
		}
		protected override void OnBackColorChanged(EventArgs e)
		{
			if (glContext != null)
			{
				if (!glContext.IsCurrent)
				{
					glContext.MakeCurrent(queueRenderInfo.Window);
					GL.DrawBuffer(DrawBufferMode.Front);
				}
				GL.ClearColor(BackColor);
			}
			base.OnBackColorChanged(e);
		}
		protected override void WndProc(ref Message m)
		{
			bool bolHandled = false;
			if (interfaceTouch != null)
			{
				bolHandled = WinProc_HandleTouch(this, ref m, ref interfaceTouch);
				if (bolHandled) interfaceTouch.RaiseEvents();
			}
			if (!bolHandled) base.WndProc(ref m);
		}
		private Stopwatch ts = new Stopwatch();
		private ErrorCode glErr;
		public bool GLRender(Action RenderFunction)
		{
			if (queueRenderInfo == null) return false;
			if (glContext == null) return false;
			if (!queueRenderInfo.RenderComplete) return false;
			queueRenderInfo.RenderComplete = false;
			queueRenderInfo.Result = null;
			ts.Start();
			if (!glContext.IsCurrent) glContext.MakeCurrent(queueRenderInfo.Window);
			GL.Viewport(ClientRectangle);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			queueRenderInfo.Widget.Invoke(RenderFunction);
			GL.Finish();
			glErr = GL.GetError();
			ts.Stop();
			queueRenderInfo.Result = new infoRenderQueueResult()
			{
				RenderTime = ts.Elapsed.TotalSeconds,
				GLResult = glErr
			};
			queueRenderInfo.RenderComplete = true;
			ts.Reset();
			return true;
		}
		public void GDIDraw(Action<Graphics> DrawFunction)
		{
			Invoke(new Action(() =>
			{
				DrawFunction.Invoke(gdiContext);
			}));
		}
		public void MakeContextCurrent() 
		{
			if (glContext == null) return;
			if (queueRenderInfo == null) return;
			if (queueRenderInfo.Window == null) return;
			if (glContext.IsCurrent) return;
			glContext.MakeCurrent(queueRenderInfo.Window);
		}
		public float AspectRatio { get { Size sz = ClientSize; return Math.Max(1.0f, sz.Width) / Math.Max(1.0f, sz.Height); } }
		public bool QueueItemComplete { get => queueRenderInfo.RenderComplete; }
		public infoRenderQueueResult RenderResult { get => queueRenderInfo.Result; }
	}
}
