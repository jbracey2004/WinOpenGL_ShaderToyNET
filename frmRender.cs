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
		private infoFramePerformance tsRender;
		private infoFramePerformance tsRenderTimer;
		private Stopwatch timeRun;
		public double CurrentTimeStamp { get; private set; }
		public double PreviousTimeStamp { get; private set; }
		public double DeltaTimeStamp { get => CurrentTimeStamp - PreviousTimeStamp; }
		private void FrmGLMain_Load(object sender, EventArgs e)
		{
			InitDialog();
			InitConfig();
			timeRun.Start();
			timerRender = new clsHPTimer(this);
			timerRender.IntervalEnd += new HPIntervalEventHandler(TimerRender_Tick);
			txtInterval.Text = ((Render!=null)?(Render.RenderInterval):(1000.0/60.0)).ToString("#.########");
			timerRender.Start();
			if(Render != null) { Render.RaiseLoadEvent(); }
		}
		private void FrmGLMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			DockPanel = null;
			timerRender?.Stop();
			timerRender.Dispose();
			timerRender = null;
			panelMain.ProjectObject = null;
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
			timeRun = new Stopwatch();
			tsRender = new infoFramePerformance();
			tsRenderTimer = new infoFramePerformance();
			tsRenderTimer.HistoryDuration = 10.0;
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
			PreviousTimeStamp = CurrentTimeStamp;
			CurrentTimeStamp = timeRun.Elapsed.TotalSeconds;
			double tsDelta = CurrentTimeStamp - PreviousTimeStamp;
			Render?.RaiseRenderEvent(tsDelta, CurrentTimeStamp);
			glMain_Render();
			tsRenderTimer.StopInterval();
			tsRenderTimer.SampleInterval();
			btnFPS.Text = string.Format("{0,15:#,##0.00000 FPS}", tsRenderTimer.Median_Rate);
			btnInterval.Text = string.Format("{0,15:#,##0.00000 ms}", tsRenderTimer.Median * 1000.0);
			tsRenderTimer.ResetInterval();
			tsRenderTimer.StartInterval();
			lblRenderDuration.Text = string.Format("{0,15:##,##0.00000 ms}", tsRender.Median * 1000.0);
			lblRenderFreq.Text = string.Format("{0,15:##,##0.00000 Hz}", tsRender.Median_Rate);
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
			glRender.MakeCurrent();
			tsRender.ResetInterval();
			tsRender.StartInterval();
			GL.Viewport(glRender.ClientRectangle);
			GL.ClearColor(glRender.BackColor);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			UpdateUniformRouting();
			if (Render.Program != null)
			{
				GL.UseProgram(Render.Program.glID);
				if (Render.Geometry != null)
				{
					GL.EnableClientState(ArrayCap.IndexArray);
					GL.BindBuffer(BufferTarget.ElementArrayBuffer, Render.Geometry.glIndexBuffer);
					GL.DrawElements(PrimitiveType.Triangles, Render.Geometry.Triangles.Indices.Length, DrawElementsType.UnsignedInt, 0);
				}
			}
			glRender.Context.SwapBuffers();
			tsRender.StopInterval();
			tsRender.SampleInterval(timeRun.Elapsed.TotalSeconds);
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
				//timerRender.SleepInterval = (sleepTmp > 1) ? (sleepTmp) : (0);
			}
		}
		private void BtnFPS_DropDownClosed(object sender, EventArgs e)
		{
			txtFPS.Text = (1000.0 / Render.RenderInterval).ToString("#.########");
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
				//timerRender.SleepInterval = (sleepTmp > 1) ? (sleepTmp) : (0);
			}
		}
		private void BtnInterval_DropDownClosed(object sender, EventArgs e)
		{
			txtInterval.Text = (Render.RenderInterval).ToString("#.########");
		}
		private void btnConfigure_Click(object sender, EventArgs e)
		{
			ConfigureDialog.Show(dockMainPanel, this.DockState);
		}
	}
}
