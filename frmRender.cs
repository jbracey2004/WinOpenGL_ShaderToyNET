using modProject;
using OpenTK;
using OpenTK.Graphics;
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
using System.Drawing;
using System.Drawing.Imaging;

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
		public double FrameCount { get; private set; }
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
			Render?.LinkShaderUniforms();
			Render?.RaiseLoadEvent();
			ProjectDef.AllForms.Add(this);
		}
		private void FrmGLMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			DockPanel = null;
			tsTimeElapsed.Stop();
			timerRender.Stop();
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
			tsRender.HistoryDuration = 5.0;
			tsRenderTimer = new infoFramePerformance();
			tsRenderTimer.HistoryDuration = 1.0;
			glRender = new controlRender();
			glRender.Name = Render.ToString();
			glRender.Parent = panelMain.Content;
			glRender.Dock = DockStyle.Fill;
			glRender.Cursor = Cursors.Cross;
			glRender.SizeChanged += glMain_Resized;
			glRender.PointerStart += glMain_PointerStart;
			glRender.PointerMove += glMain_PointerMove;
			glRender.PointerEnd += glMain_PointerEnd;
			UpdateGeometryRouting();
			UpdateUniformRouting();
			tsTimeElapsed = new Stopwatch();
			tsTimeElapsed.Start();
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
			glMain_Render3D();
			double tsDelta = e.TimeDelta * 0.001;
			if (bolIsRenderFrameValid)
			{
				PreviousTimeStamp = CurrentTimeStamp;
				CurrentTimeStamp = tsTimeElapsed.Elapsed.TotalSeconds;
				tsRenderTimer.SampleFrame(tsDelta, CurrentTimeStamp);
			}
			Render?.RaiseRenderEvent(FrameCount, tsDelta, CurrentTimeStamp);
			if(bolIsRenderFrameValid) FrameCount += tsDelta * tsRenderTimer.Median_Rate;
		}
		private void TimerUpdateStats_Tick(object sender, HPIntervalEventArgs e)
		{
			btnFPS.Text = $"{tsRenderTimer.Median_Rate:0.000000} FPS";
			btnInterval.Text = $"{floatToSIUnits(tsRenderTimer.Median, SIUnits)}s";
			lblRenderDuration.Text = $"{floatToSIUnits(tsRender.Median, SIUnits)}s";
			lblRenderFreq.Text = $"{floatToSIUnits(tsRender.Median_Rate, SIUnits)}Hz";
			lblFrameNumber.Text = $"# {FrameCount:0.000000}";
			lblFrameTimeStamp.Text = $"{CurrentTimeStamp:0.000000} s";
		}
		private void LogFrameStatus()
		{
			if (Render == null) return;
			if (ConfigureDialog != null)
			{
				if (ConfigureDialog.Console != null)
				{
					double tsElapsed = glRender.RenderResult.RenderTime;
					string str = $"Rendered {floatToSIUnits(tsElapsed, SIUnits)}s {floatToSIUnits(1.0/tsElapsed, SIUnits)}Hz; " +
									$"Progrm:{((Render.Program != null) ? Render.Program.IsValid.ToString() : "Ø")}; " +
									$"Geometry:{{Index:{((Render.Geometry != null) ? Render.Geometry.IsIndexBufferValid.ToString() : "Ø")}; " +
									$"Buffers:[{((Render.Geometry != null) ? NumberAsBase(Render.Geometry.ValidVertexBufferFlags, 2) : "Ø")}]}};";
					ConfigureDialog.Console.Write(str, "Frame Render Status");
					ErrorCode glErr = glRender.RenderResult.GLResult;
					if (glErr != 0)
					{
						ConfigureDialog.Console.Write($"{glErr}", $"glError Code {(int)glErr}");
					}
				}
			}
		}
		private void glMain_Resized(object sender, EventArgs e)
		{
			Render?.RaiseResizeEvent(glRender.Width, glRender.Height);
		}
		private bool bolIsRenderFrameValid = false;
		private void glMain_Render3D()
		{
			bolIsRenderFrameValid = true;
			if (Render.Geometry == null || !Render.Geometry.IsValid) bolIsRenderFrameValid = false;
			if (Render.Program != null)
			{
				if (!Render.Program.IsValid) bolIsRenderFrameValid = false;
				if (Render.Program.LinkInfo.ErrorMessages.Length > 0) bolIsRenderFrameValid = false;
			}
			else
			{
				bolIsRenderFrameValid = false;
			}
			if (bolIsRenderFrameValid) tsRender.StartInterval();
			bool bolRendered = glRender.GLRender(new Action(() => 
			{
				if (bolIsRenderFrameValid)
				{
					UpdateUniformRouting();
					GL.EnableClientState(ArrayCap.IndexArray);
					GL.BindBuffer(BufferTarget.ElementArrayBuffer, Render.Geometry.glIndexBuffer);
					GL.DrawElements(PrimitiveType.Triangles, Render.Geometry.Triangles.Indices.Length, DrawElementsType.UnsignedInt, 0);
				}
			}));
			if (bolIsRenderFrameValid)
			{
				tsRender.StopInterval();
				if(bolRendered) tsRender.SampleInterval(CurrentTimeStamp);
				tsRender.ResetInterval();
			}
			if(bolRendered)
			{
				glRender.GDIDraw(glMain_Render2D);
				LogFrameStatus();
			}
		}
		private void glMain_Render2D(Graphics gdi)
		{
			infoRenderQueueResult infoRenderResult = glRender.RenderResult;
			if (infoRenderResult == null) return;
			double tsElapsed = infoRenderResult.RenderTime;
			string str = $"ProjectObject: {Render};\n" +
						 $"Renderer: {GL.GetString(StringName.Renderer)};\n" +
						 $"OpenGL: {GL.GetString(StringName.Version)};\n" +
						 $"GLSL: {GL.GetString(StringName.ShadingLanguageVersion)};\n" +
						 $"TimeRender: {tsElapsed*1000.0} ms, {1.0/tsElapsed} Hz;\n" +
						 $"Progrm:{((Render.Program != null) ? Render.Program.IsValid.ToString() : "Ø")}; " +
						 $"Geometry:{{Index:{((Render.Geometry != null) ? Render.Geometry.IsIndexBufferValid.ToString() : "Ø")}; " +
						 $"Buffers:[{((Render.Geometry != null) ? NumberAsBase(Render.Geometry.ValidVertexBufferFlags, 2) : "Ø")}]}}; \n" +
						 $"GLResult {(int)infoRenderResult.GLResult}: {infoRenderResult.GLResult};";
			gdi.DrawString(str, Font, new SolidBrush(glRender.ForeColor), new Point(8, 8));
		}
		public void UpdateGeometryRouting()
		{
			if (Render == null) return;
			if (Render.Program == null) return;
			if (Render.Geometry == null) return;
			GL.UseProgram(Render.Program.glID);
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
		private void UpdateUniformRouting()
		{
			if (Render == null) return;
			if (Render.Program == null) return;
			if (!Render.Program.IsValid) return;
			GL.UseProgram(Render.Program.glID);
			foreach (KeyValuePair<string, clsUniformSet> itm in Render.Uniforms)
			{
				itm.Value.BindData();
			}
		}
		private void txtFPS_Change(object sender, EventArgs e)
		{
			if(double.TryParse(txtFPS.Text, out double val))
			{
				if (Render == null) return;
				Render.RenderInterval = 1000.0 / val;
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
			if (double.TryParse(txtInterval.Text, out double val))
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
		private void FrameCount_MouseEnter(object sender, EventArgs e)
		{
			Control control = sender as Control;
			if(control != null) control.BackColor = SystemColors.ControlDarkDark;
		}
		private void FrameCount_MouseLeave(object sender, EventArgs e)
		{
			Control control = sender as Control;
			if (control != null) control.BackColor = Color.Transparent;
		}
	}
}
