using modProject;
using OpenTK.Graphics.OpenGL;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using static WinOpenGL_ShaderToy.ProjectDef;
using static clsHPTimer;
using static generalUtils;
using System.Collections.Generic;
using static modProject.clsGeometry;
using static modCommon.modWndProcInterop.InputInterface;

namespace WinOpenGL_ShaderToy
{
	public partial class frmRender : DockContent
	{
		public frmRender(clsProjectObject refObj) { InitializeComponent(); panelMain.ProjectObject = refObj; }
		public clsRender Render { set { panelMain.ProjectObject = value; } get { return panelMain.ProjectObject as clsRender; } }
		public controlRender glRender;
		private frmRenderConfigure ConfigureDialog;
		private clsHPTimer timerRender;
		private clsHPTimer timerUpdateStats;
		private infoFramePerformance tsRender;
		private infoFramePerformance tsRenderTimer;
		private Stopwatch tsTimeElapsed;
		public int FrameCount { get; private set; }
		public double CurrentTimeStamp { get; private set; }
		public double PreviousTimeStamp { get; private set; }
		public double DeltaTimeStamp { get => CurrentTimeStamp - PreviousTimeStamp; }
		private void FrmGLMain_Load(object sender, EventArgs e)
		{
			InitDialog();
			InitConfig();
			btnFPS.MouseLeave += BtnFPS_DropDownClosed;
			btnFPS.VisibleChanged += BtnFPS_DropDownClosed;
			btnInterval.MouseLeave += BtnInterval_DropDownClosed;
			btnInterval.VisibleChanged += BtnInterval_DropDownClosed;
			btnFPS.DropDownOpening += BtnFPS_DropDownClosed;
			btnInterval.DropDownOpening += BtnInterval_DropDownClosed;
			timerRender = new clsHPTimer(this);
			timerRender.IntervalEnd += TimerRender_Tick;
			txtInterval.Text = ((Render!=null)?(Render.RenderInterval):(1000.0/60.0)).ToString("0.########");
			timerRender.Start();
			timerUpdateStats = new clsHPTimer(this);
			timerUpdateStats.Interval = 50;
			timerUpdateStats.SleepInterval = 50;
			timerUpdateStats.IntervalEnd += TimerUpdateStats_Tick;
			timerUpdateStats.Start();
			Render?.RaiseLoadEvent();
			ProjectDef.AllForms.Add(this);
		}
		private void FrmGLMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			tsTimeElapsed?.Stop();
			tsTimeElapsed = null;
			DockPanel = null;
			timerRender?.Stop();
			timerRender.Dispose();
			timerRender = null;
			panelMain.ProjectObject = null;
			ProjectDef.AllForms.Remove(this);
		}
		private void InitDialog()
		{
			ConfigureDialog = frmRenderConfigure.FormWithSubjectObject(Render);
			if(ConfigureDialog == null)
			{
				ConfigureDialog = new frmRenderConfigure();
				ConfigureDialog.RenderSubject = Render;
				ConfigureDialog.HideOnClose = true;
			}
		}
		private void InitConfig()
		{
			tsRender = new infoFramePerformance();
			tsRenderTimer = new infoFramePerformance();
			tsRenderTimer.HistoryDuration = 5.0;
			glRender = new controlRender();
			glRender.Name = Render.ToString();
			glRender.Parent = panelMain.Content;
			glRender.Dock = DockStyle.Fill;
			glRender.VSync = false;
			glRender.HandleCreated += glMain_HandleCreated;
			glRender.SizeChanged += glMain_Resized;
			glRender.PointerStart += glMain_PointerStart;
			glRender.PointerMove += glMain_PointerMove;
			glRender.PointerEnd += glMain_PointerEnd;
			glRender.MakeCurrent();
			tsTimeElapsed = new Stopwatch();
			tsTimeElapsed.Start();
			UpdateGeometryRouting();
		}
		private void glMain_PointerStart(object sender, InputEventArgs e)
		{
			Render?.RaisePointerStartEvent(e);
		}
		private void glMain_PointerMove(object sender, InputEventArgs e)
		{
			Render?.RaisePointerMoveEvent(e);
		}
		private void glMain_PointerEnd(object sender, InputEventArgs e)
		{
			Render?.RaisePointerEndEvent(e);
		}
		private void TimerRender_Tick(object sender, HPIntervalEventArgs e)
		{
			double tsDelta = e.TimeDelta * 0.001;
			PreviousTimeStamp = CurrentTimeStamp;
			CurrentTimeStamp = tsTimeElapsed.Elapsed.TotalSeconds;
			tsRenderTimer.SampleFrame(tsDelta, CurrentTimeStamp);
			Render?.RaiseRenderEvent(FrameCount, DeltaTimeStamp, CurrentTimeStamp);
			glMain_Render();
			FrameCount++;
		}
		private void TimerUpdateStats_Tick(object sender, HPIntervalEventArgs e)
		{
			btnFPS.Text = string.Format("{0,15:#,##0.00000 FPS}", tsRenderTimer.Median_Rate);
			btnInterval.Text = string.Format("{0,15:#,##0.00000 ms}", tsRenderTimer.Median * 1000.0);
			lblRenderDuration.Text = string.Format("{0,15:##,##0.00000 ms}", tsRender.Median * 1000.0);
			lblRenderFreq.Text = string.Format("{0,15:##,##0.00000 Hz}", tsRender.Median_Rate);
			lblFrameNumber.Text = FrameCount.ToString();
			lblFrameTimeStamp.Text = CurrentTimeStamp.ToString("0.00000");
		}
		private void glMain_Resized(object sender, EventArgs e)
		{
			glRender.Context.Update(glRender.WindowInfo);
			glRender.Context.MakeCurrent(glRender.WindowInfo);
			GL.Viewport(glRender.ClientRectangle);
			Render?.RaiseResizeEvent(glRender.Width, glRender.Height);
		}
		private void glMain_HandleCreated(object sender, EventArgs e)
		{
			glRender.Context.Update(glRender.WindowInfo);
			glRender.Context.MakeCurrent(glRender.WindowInfo);
			GL.Viewport(glRender.ClientRectangle);
			UpdateGeometryRouting();
		}
		private void glMain_Render()
		{
			tsRender.ResetInterval();
			tsRender.StartInterval();
			glRender.MakeCurrent();
			GL.Viewport(glRender.ClientRectangle);
			GL.ClearColor(glRender.BackColor);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			UpdateUniformRouting();
			/*Console.WriteLine($"Progrm:{Render.Program.IsValid}; Geometry:{{Index:{Render.Geometry.IsIndexBufferValid}; 
								  Buffers:[{NumberAsBase(Render.Geometry.ValidVertexBufferFlags, 2)}]}};");*/
			if (Render.Program != null /*&& Render.Program.IsValid*/)
			{
				if(Render.Program.LinkInfo.ErrorMessages.Length == 0)
				{
					GL.UseProgram(Render.Program.glID);
					if (Render.Geometry != null /*&& Render.Geometry.IsValid*/)
					{
						GL.EnableClientState(ArrayCap.IndexArray);
						GL.BindBuffer(BufferTarget.ElementArrayBuffer, Render.Geometry.glIndexBuffer);
						GL.DrawElements(PrimitiveType.Triangles, Render.Geometry.Triangles.Indices.Length, DrawElementsType.UnsignedInt, 0);
					}
				}
			}
			glRender.Context.SwapBuffers();
			tsRender.StopInterval();
			tsRender.SampleInterval(CurrentTimeStamp);
		}
		public void UpdateGeometryRouting()
		{
			if (Render == null) return;
			if (Render.Program == null) return;
			if (Render.Geometry == null) return;
			GL.UseProgram(Render.Program.glID);
			GL.Viewport(glRender.ClientRectangle);
			foreach (KeyValuePair<string, clsVertexDescriptionComponent> itrItm in Render.GeometryShaderLinks)
			{
				string strAttr = itrItm.Key;
				clsVertexDescriptionComponent comp = itrItm.Value as clsVertexDescriptionComponent;
				if (strAttr == null) continue;
				if (comp == null) continue;
				int intUniformLocation = GL.GetAttribLocation(Render.Program.glID, strAttr);
				GL.EnableVertexAttribArray(intUniformLocation);
				GL.BindBuffer(BufferTarget.ArrayBuffer, Render.Geometry.glBuffers[comp.Index]);
				GL.VertexAttribPointer(intUniformLocation, comp.ElementCount, comp.ElementGLType, false, 0, 0);
			}
		}
		public void UpdateUniformRouting()
		{
			if (Render == null) return;
			if (Render.Program == null) return;
			GL.UseProgram(Render.Program.glID);
			foreach (KeyValuePair<string, clsUniformSet> itm in Render.Uniforms)
			{
				if (itm.Key != null)
				{
					foreach (KeyValuePair<string, string> uni in Render.UniformShaderLinks.FindAll(x => x.Value == itm.Key))
					{
						if (uni.Key == null) continue;
						int glLoc = GL.GetUniformLocation(Render.Program.glID, uni.Key);
						if(glLoc >= 0)
						{
							clsUniformSet.UniformBindDelegate[itm.Value.Type](glLoc, itm.Value.Data.Count, itm.Value.DataInlined);
						}
					}
				}
			}
		}
		private void txtFPS_Change(object sender, EventArgs e)
		{
			double val = 0.0;
			if(double.TryParse(txtFPS.Text, out val))
			{
				if (Render == null) return;
				Render.RenderInterval = (1000.0 / val);
				timerRender.Interval = Render.RenderInterval;
				int sleepTmp = (int)Math.Floor(timerRender.Interval * 0.125);
				timerRender.SleepInterval = (sleepTmp > 1) ? (sleepTmp) : (0);
			}
		}
		private void BtnFPS_DropDownClosed(object sender, EventArgs e)
		{
			txtFPS.Text = (1000.0 / Render.RenderInterval).ToString("0.########");
		}
		private void txtInterval_Change(object sender, EventArgs e)
		{
			double val = 0.0;
			if (double.TryParse(txtInterval.Text, out val))
			{
				if (Render == null) return;
				Render.RenderInterval = val;
				timerRender.Interval = Render.RenderInterval;
				int sleepTmp = (int)Math.Floor(timerRender.Interval * 0.125);
				timerRender.SleepInterval = (sleepTmp > 1) ? (sleepTmp) : (0);
			}
		}
		private void BtnInterval_DropDownClosed(object sender, EventArgs e)
		{
			txtInterval.Text = (Render.RenderInterval).ToString("0.########");
		}
		private void btnConfigure_Click(object sender, EventArgs e)
		{
			ConfigureDialog.Show(dockMainPanel, this.DockState);
		}
		private void lblFrameCount_DoubleClick(object sender, EventArgs e)
		{
			FrameCount = 0;
			tsTimeElapsed.Restart();
		}
		private void lblFrameTimeStamp_DoubleClick(object sender, EventArgs e)
		{
			tsTimeElapsed.Restart();
		}
		private void lblFrameNumber_DoubleClick(object sender, EventArgs e)
		{
			FrameCount = 0;
		}
	}
}
